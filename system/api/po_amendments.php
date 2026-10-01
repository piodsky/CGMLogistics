<?php
// Purchase Order AMENDMENT API.
//
// A formal, tracked correction to an existing PO's item specs (description/
// unit/quantity/unit cost) — NOT a replacement for the ordinary Edit flow in
// api/purchase_orders.php, which is for fixing mistakes on the working
// record. An amendment captures WHY a change was made and preserves exactly
// what the line said before, numbered AM-001/AM-002/... per PO.
//
// This file never writes to tbl_purchase_order or
// tbl_purchase_order_items_details — not on save, not anywhere below. It
// only reads them (to know what the PO/items currently say) and writes the
// two new sibling tables from database/po_amendments.sql
// (tbl_po_amendments / tbl_po_amendment_items). The PO Number is never
// reassigned or duplicated; "the PO has an amendment" is entirely a fact
// about these two new tables, joined in at read time by
// lib/po_amendment_items.php — the same overlay api/inspection.php,
// api/inspection_progress.php and api/inventory.php use so every screen
// agrees on "the latest valid amendment."
require_once '../config.php';
require_once '../lib/po_amendment_items.php';
requireAuth();
requireModule('purchase_orders');
header('Content-Type: application/json');
$db = getPoDB();
$method = $_SERVER['REQUEST_METHOD'];

// Own, independent read of the PO header — same reasoning invPreviewPO() /
// pbGetDetail() already use for reading tbl_purchase_order from outside
// api/purchase_orders.php: this file must not require_once that API (it has
// its own requireModule() top-of-file gate and its own $method dispatch),
// so it keeps a small copy of just the fields the Amendment modal needs to
// display, not the full getOrderDetail() column list.
function poaGetHeader(PDO $db, string $poNumber): ?array {
    $stmt = $db->prepare(
        'SELECT PO_Number AS po_number, PR_Number AS pr_number, Department AS department,
                Division AS division, Section AS section, Supplier_Name AS supplier_name,
                Category AS category, Purpose AS purpose, Date_PO AS date_po,
                Print_PO AS print_po, Status AS status
         FROM tbl_purchase_order WHERE PO_Number = ? AND isDeleted = 0'
    );
    $stmt->execute([$poNumber]);
    $po = $stmt->fetch(PDO::FETCH_ASSOC);
    return $po ?: null;
}

// Item rows WITH their own tbl_purchase_order_items_details.ID exposed as
// po_item_id — that column is the table's own primary key (already used for
// ORDER BY everywhere it's read) but no existing query in this app selects
// it out. Amendments have to reference a specific line, so this file is the
// first to need it.
function poaGetItems(PDO $db, string $poNumber): array {
    $stmt = $db->prepare(
        'SELECT ID AS po_item_id, Item_ID AS item_id, Unit AS unit,
                Items_Description AS items_description, Quantity AS quantity,
                Unit_Cost AS unit_cost, Total_Cost AS total_cost, NumberSeries AS number_series
         FROM tbl_purchase_order_items_details
         WHERE PO_Number = ? AND isDeleted = 0 ORDER BY ID'
    );
    $stmt->execute([$poNumber]);
    return $stmt->fetchAll(PDO::FETCH_ASSOC);
}

// Same blank test purchase_orders.php's item grid and api/inventory.php's
// invIsTitleRow() both already use — kept identical on purpose. A title/
// LOT-divider row ("LOT 1", "SCOPE OF WORK:") has no unit/qty/cost and is
// not an amendable line item.
function poaIsTitleRow($unit, $qty, $unitCost, $totalCost): bool {
    return trim((string)$unit) === ''
        && (float)$qty == 0 && (float)$unitCost == 0 && (float)$totalCost == 0;
}

// Every amendment event for this PO, newest first, each with its line-level
// before/after diffs — powers the Amendment History view.
//
// Wrapped like getLatestPoItemAmendments() in lib/po_amendment_items.php:
// on a deploy where database/po_amendments.sql hasn't been run yet, this
// must degrade to "no amendments" rather than a fatal error, so the rest of
// the View modal / Amendment modal still loads.
function poaGetHistory(PDO $db, string $poNumber): array {
    try {
        $stmt = $db->prepare(
            'SELECT Amendment_ID AS amendment_id, Amendment_No AS amendment_no, Reason AS reason,
                    Created_By AS created_by, Created_By_ID AS created_by_id, Date_Created AS date_created
             FROM tbl_po_amendments
             WHERE PO_Number = ? AND isDeleted = 0
             ORDER BY Date_Created DESC, Amendment_ID DESC'
        );
        $stmt->execute([$poNumber]);
        $amendments = $stmt->fetchAll(PDO::FETCH_ASSOC);
    } catch (Throwable $e) {
        error_log('poaGetHistory failed: ' . $e->getMessage());
        return [];
    }
    if (!$amendments) return [];

    $ids = array_column($amendments, 'amendment_id');
    $placeholders = implode(',', array_fill(0, count($ids), '?'));
    $itemStmt = $db->prepare(
        "SELECT Amendment_ID AS amendment_id, PO_Item_ID AS po_item_id,
                Original_Items_Description AS original_items_description,
                Amended_Items_Description AS amended_items_description,
                Original_Unit AS original_unit, Amended_Unit AS amended_unit,
                Original_Quantity AS original_quantity, Amended_Quantity AS amended_quantity,
                Original_Unit_Cost AS original_unit_cost, Amended_Unit_Cost AS amended_unit_cost,
                Original_Total_Cost AS original_total_cost, Amended_Total_Cost AS amended_total_cost,
                Changed_Fields AS changed_fields
         FROM tbl_po_amendment_items
         WHERE Amendment_ID IN ({$placeholders})
         ORDER BY Amendment_Item_ID"
    );
    $itemStmt->execute($ids);
    $byAmendment = [];
    foreach ($itemStmt->fetchAll(PDO::FETCH_ASSOC) as $row) {
        $byAmendment[(int)$row['amendment_id']][] = $row;
    }
    foreach ($amendments as &$a) {
        $a['items'] = $byAmendment[(int)$a['amendment_id']] ?? [];
    }
    unset($a);
    return $amendments;
}

const POA_ITEM_FIELD_MAP = [
    'items_description' => 'Amended_Items_Description',
    'unit'               => 'Amended_Unit',
    'quantity'           => 'Amended_Quantity',
    'unit_cost'          => 'Amended_Unit_Cost',
    'total_cost'         => 'Amended_Total_Cost',
];

if ($method === 'GET') {
    $poNumber = trim($_GET['po_number'] ?? '');
    if (!$poNumber) jsonResponse(false, 'PO number is required.');
    $po = poaGetHeader($db, $poNumber);
    if (!$po) jsonResponse(false, 'Purchase order not found.');

    $items = poaGetItems($db, $poNumber);
    $items = applyPoItemAmendments($db, $poNumber, $items, 'po_item_id', POA_ITEM_FIELD_MAP);

    jsonResponse(true, '', fixLegacyEncodingDeep([
        'po'              => $po,
        'effective_items' => $items,
        'amendments'      => poaGetHistory($db, $poNumber),
    ]));
}

if ($method === 'POST') {
    // Restricted to Procurement Administrator / Administrator / Super
    // Administrator (role_id 1/2/3) — deliberately narrower than the
    // ordinary Edit gate. See canAmendPurchaseOrder() in config.php.
    if (!canAmendPurchaseOrder(hrmisRoleId(getCurrentUser()['id'] ?? 0))) {
        jsonResponse(false, 'You do not have permission to amend purchase orders.');
    }
    $data = getInput();
    $poNumber = trim($data['po_number'] ?? '');
    $reason   = trim($data['reason'] ?? '');
    $submitted = is_array($data['items'] ?? null) ? $data['items'] : [];
    if (!$poNumber) jsonResponse(false, 'PO number is required.');
    if ($reason === '') jsonResponse(false, 'A reason/remarks is required for the amendment.');
    $po = poaGetHeader($db, $poNumber);
    if (!$po) jsonResponse(false, 'Purchase order not found.');
    // Locking (Print_PO) is deliberately NOT checked here — a formal
    // amendment is exactly the tool used to correct something on a PO that
    // has already been printed/finalized, unlike the ordinary Edit flow
    // which the lock is meant to freeze.

    // Current effective values (original overlaid with whatever the latest
    // prior amendment already changed) — the diff baseline. Never trust an
    // "original" value the client might submit; only what this PO's own
    // rows currently say counts.
    $currentItems = poaGetItems($db, $poNumber);
    $currentItems = applyPoItemAmendments($db, $poNumber, $currentItems, 'po_item_id', POA_ITEM_FIELD_MAP);
    $currentById = [];
    foreach ($currentItems as $ci) { $currentById[(int)$ci['po_item_id']] = $ci; }

    $toInsert = [];
    foreach ($submitted as $row) {
        $poItemId = (int)($row['po_item_id'] ?? 0);
        if (!$poItemId || !isset($currentById[$poItemId])) continue; // stray/unknown id, ignore
        $current = $currentById[$poItemId];
        if (poaIsTitleRow($current['unit'], $current['quantity'], $current['unit_cost'], $current['total_cost'])) {
            continue; // title/LOT-divider rows are not amendable line items
        }

        $newDescription = trim((string)($row['items_description'] ?? $current['items_description']));
        $newUnit        = trim((string)($row['unit'] ?? $current['unit']));
        $newQuantity    = is_numeric($row['quantity'] ?? null) ? (float)$row['quantity'] : (float)$current['quantity'];
        $newUnitCost    = is_numeric($row['unit_cost'] ?? null) ? (float)$row['unit_cost'] : (float)$current['unit_cost'];
        $newTotalCost   = $newQuantity > 0 ? round($newQuantity * $newUnitCost, 2) : round($newUnitCost, 2);

        $changed = [];
        if ($newDescription !== trim((string)$current['items_description'])) $changed[] = 'items_description';
        if ($newUnit !== trim((string)$current['unit'])) $changed[] = 'unit';
        if ((float)$newQuantity !== (float)$current['quantity']) $changed[] = 'quantity';
        if ((float)$newUnitCost !== (float)$current['unit_cost']) $changed[] = 'unit_cost';
        if ((float)$newTotalCost !== (float)$current['total_cost']) $changed[] = 'total_cost';
        if (!$changed) continue; // nothing actually different on this line

        $toInsert[] = [
            'po_item_id'      => $poItemId,
            'orig_desc'       => $current['items_description'],
            'new_desc'        => $newDescription,
            'orig_unit'       => $current['unit'],
            'new_unit'        => $newUnit,
            'orig_qty'        => $current['quantity'],
            'new_qty'         => $newQuantity,
            'orig_unit_cost'  => $current['unit_cost'],
            'new_unit_cost'   => $newUnitCost,
            'orig_total_cost' => $current['total_cost'],
            'new_total_cost'  => $newTotalCost,
            'changed_fields'  => implode(',', $changed),
        ];
    }

    if (!$toInsert) jsonResponse(false, 'No changes were made to any item.');

    try {
        $db->beginTransaction();
        // Scoped to this PO_Number, inside the same transaction as the
        // insert below, so two amendments saved back-to-back can never land
        // on the same AM-00N.
        $countStmt = $db->prepare('SELECT COUNT(*) FROM tbl_po_amendments WHERE PO_Number = ?');
        $countStmt->execute([$poNumber]);
        $amendmentNo = sprintf('AM-%03d', (int)$countStmt->fetchColumn() + 1);

        $user = getCurrentUser();
        $headerStmt = $db->prepare(
            'INSERT INTO tbl_po_amendments (PO_Number, Amendment_No, Reason, Created_By, Created_By_ID, Date_Created, isDeleted)
             VALUES (?, ?, ?, ?, ?, NOW(), 0)'
        );
        $headerStmt->execute([$poNumber, $amendmentNo, $reason, $user['username'] ?? 'system', $user['id'] ?? null]);
        $amendmentId = (int)$db->lastInsertId();

        $itemStmt = $db->prepare(
            'INSERT INTO tbl_po_amendment_items
                (Amendment_ID, PO_Item_ID, Original_Items_Description, Amended_Items_Description,
                 Original_Unit, Amended_Unit, Original_Quantity, Amended_Quantity,
                 Original_Unit_Cost, Amended_Unit_Cost, Original_Total_Cost, Amended_Total_Cost,
                 Changed_Fields, Date_Created)
             VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, NOW())'
        );
        foreach ($toInsert as $it) {
            $itemStmt->execute([
                $amendmentId, $it['po_item_id'], $it['orig_desc'], $it['new_desc'],
                $it['orig_unit'], $it['new_unit'], $it['orig_qty'], $it['new_qty'],
                $it['orig_unit_cost'], $it['new_unit_cost'], $it['orig_total_cost'], $it['new_total_cost'],
                $it['changed_fields'],
            ]);
        }
        $db->commit();

        logAudit('AMEND_PO', 'purchase_orders', "Amendment {$amendmentNo} on PO {$poNumber}: {$reason}");
        $actorName = $user['full_name'] ?? 'Someone';
        notifyPoUsers(
            'Purchase Order Amended',
            "{$actorName} amended PO {$poNumber} ({$amendmentNo}): {$reason}",
            'purchase_orders.php?po_number=' . urlencode($poNumber) . '&view=1',
            $poNumber,
            'all'
        );

        $items = poaGetItems($db, $poNumber);
        $items = applyPoItemAmendments($db, $poNumber, $items, 'po_item_id', POA_ITEM_FIELD_MAP);
        jsonResponse(true, "Amendment {$amendmentNo} saved.", fixLegacyEncodingDeep([
            'amendment_no'    => $amendmentNo,
            'effective_items' => $items,
        ]));
    } catch (Exception $e) {
        if ($db->inTransaction()) $db->rollBack();
        error_log('po_amendments POST failed: ' . $e->getMessage());
        jsonResponse(false, 'Unable to save amendment.');
    }
}

// ── Undo the most recent amendment ──────────────────────────────────────
// Same shape as api/inspection.php's "Undo the last save": always undoes
// whichever amendment is CURRENTLY latest for this PO, never a client-
// supplied amendment id — that keeps undo from ever landing on the wrong
// entry if the history changed between page load and the click. Nothing is
// hard-deleted: the amendment row is soft-deleted (isDeleted = 1), which is
// all applyPoItemAmendments()/getLatestPoItemAmendments() need to stop
// counting it — the line then falls straight back to whatever the next
// most recent amendment says, or to the PO's own original value if there
// isn't one. tbl_purchase_order / tbl_purchase_order_items_details are
// untouched, exactly as every other write in this file.
//
// Gated the same as creating an amendment (canAmendPurchaseOrder) — undoing
// one is strictly less destructive than saving a new one, which the same
// person can already do, so there is no reason for a narrower gate here.
if ($method === 'PATCH' && ($data = getInput()) && ($data['action'] ?? '') === 'undo') {
    if (!canAmendPurchaseOrder(hrmisRoleId(getCurrentUser()['id'] ?? 0))) {
        jsonResponse(false, 'You do not have permission to undo purchase order amendments.');
    }
    $poNumber = trim($data['po_number'] ?? '');
    if (!$poNumber) jsonResponse(false, 'PO number is required.');
    if (!poaGetHeader($db, $poNumber)) jsonResponse(false, 'Purchase order not found.');

    $latestStmt = $db->prepare(
        'SELECT Amendment_ID, Amendment_No FROM tbl_po_amendments
         WHERE PO_Number = ? AND isDeleted = 0
         ORDER BY Date_Created DESC, Amendment_ID DESC LIMIT 1'
    );
    $latestStmt->execute([$poNumber]);
    $latest = $latestStmt->fetch(PDO::FETCH_ASSOC);
    if (!$latest) jsonResponse(false, 'This purchase order has no amendment to undo.');

    try {
        $db->prepare('UPDATE tbl_po_amendments SET isDeleted = 1 WHERE Amendment_ID = ?')
           ->execute([$latest['Amendment_ID']]);
    } catch (Exception $e) {
        error_log('po_amendments undo failed: ' . $e->getMessage());
        jsonResponse(false, 'Could not undo the amendment. Nothing was changed.');
    }

    $amendmentNo = $latest['Amendment_No'];
    logAudit('UNDO_AMEND_PO', 'purchase_orders', "Undid amendment {$amendmentNo} on PO {$poNumber}");
    $actorName = getCurrentUser()['full_name'] ?? 'Someone';
    notifyPoUsers(
        'Purchase Order Amendment Undone',
        "{$actorName} undid amendment {$amendmentNo} on PO {$poNumber}",
        'purchase_orders.php?po_number=' . urlencode($poNumber) . '&view=1',
        $poNumber,
        'all'
    );

    $items = poaGetItems($db, $poNumber);
    $items = applyPoItemAmendments($db, $poNumber, $items, 'po_item_id', POA_ITEM_FIELD_MAP);
    jsonResponse(true, "Amendment {$amendmentNo} undone.", fixLegacyEncodingDeep([
        'undone_amendment_no' => $amendmentNo,
        'effective_items'     => $items,
        'amendments'          => poaGetHistory($db, $poNumber),
    ]));
}

jsonResponse(false, 'Unsupported request method.');
