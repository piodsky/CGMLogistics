<?php
// Competitive Bidding — Progressive Item Processing API.
//
// Adapted from api/inspection_progress.php (Progress Billing) — same
// sibling-table shape (see database/cb_processing.sql), but keyed on the
// bid's own record ID (CB_ID, database/cb_parent_id.sql) rather than its Bid
// Number, which several contracts under one bid can share,
// same append-only save/undo versioning. Two deliberate departures from
// that precedent, both explained at the point they happen below:
//   1. over-processing is HARD-BLOCKED here, where Progress Billing only
//      flags it — an explicit requirement for this feature, unlike PB's.
//   2. status ("idle"/"partial"/"complete"/"over") is computed via
//      cbProcessingStatus() in config.php, mirroring but distinct from
//      progressBillingStatus() (different vocabulary, no PO in the loop).
//
// This file NEVER writes to tbl_competitive_bidding or
// tbl_competitive_bidding_items_details — only to the two new sibling
// tables. The original bid document, and the three documents printed from
// it, are read here but never altered.
require_once '../config.php';
require_once '../lib/cb_processing_print.php';
requireAuth();
requireModule('competitive_bidding');
header('Content-Type: application/json');
$db = getPoDB();
$method = $_SERVER['REQUEST_METHOD'];

function cbpDate($value) {
    $value = trim((string)$value);
    if ($value === '') return null;
    $ts = strtotime($value);
    return $ts ? date('Y-m-d H:i:s', $ts) : null;
}

// Same three-way split Progress Billing's pbLineKind() uses, and for the
// same reason: Total Cost is the test for whether a bid line is priced at
// all, not the presence of Unit + Quantity + Unit Cost. Matches
// cbInvalidItemRows()'s own test in api/competitive_bidding.php, so the two
// screens never disagree about which bid lines are description-only.
function cbpLineKind($quantity, $unitCost, $totalCost): string {
    $q = (float)$quantity; $u = (float)$unitCost; $t = (float)$totalCost;
    if ($t <= 0 && $u <= 0) return 'caption';
    return $q > 0 ? 'item' : 'lump';
}

// What a processing step multiplies its entered quantity by. A lump sum has
// no per-unit cost, so it is treated as one indivisible unit worth its
// whole amount — same convention as pbBillUnitCost().
function cbpProcessUnitCost(array $item): float {
    $kind = cbpLineKind($item['quantity'] ?? 0, $item['unit_cost'] ?? 0, $item['total_cost'] ?? 0);
    if ($kind === 'lump') {
        $t = (float)($item['total_cost'] ?? 0);
        return $t > 0 ? $t : (float)($item['unit_cost'] ?? 0);
    }
    return (float)($item['unit_cost'] ?? 0);
}

// The bid's own item rows, each annotated with how much of it CAN be
// processed in total (orderable_quantity — 1.0 for a lump sum, the raw
// quantity otherwise; a caption/description-only row is 0 and never
// processable).
function cbpGetOrderedItems(PDO $db, int $cbId): array {
    $stmt = $db->prepare(
        'SELECT ID AS cb_item_id, Items_Description AS items_description, Unit AS unit,
                Quantity AS quantity, Unit_Cost AS unit_cost, Total_Cost AS total_cost
         FROM tbl_competitive_bidding_items_details
         WHERE CB_ID = ? AND isDeleted = 0 ORDER BY Sort_Order, ID'
    );
    $stmt->execute([$cbId]);
    $items = $stmt->fetchAll(PDO::FETCH_ASSOC);
    foreach ($items as &$it) {
        $kind = cbpLineKind($it['quantity'], $it['unit_cost'], $it['total_cost']);
        $it['line_kind']          = $kind;
        $it['process_unit_cost']  = cbpProcessUnitCost($it);
        $it['orderable_quantity'] = $kind === 'caption' ? 0.0 : ($kind === 'lump' ? 1.0 : (float)$it['quantity']);
    }
    unset($it);
    return $items;
}

// Total quantity already processed per CB_Item_ID, across every LIVE step
// (isDeleted = 0 on both the step header and the line). $excludeStepNo
// leaves one step's own lines out of the sum — used when revising that
// exact step, so its own prior quantity doesn't count against itself.
//
// Lines reach their bid through Step_ID -> tbl_cb_processing.CB_ID. A live
// line always sits under the live header of its own Step_ID (save and undo
// move the two together), so this is the same set the old Bid_Number +
// Step_No join found, without the ambiguity of a shared Bid Number.
function cbpProcessedByItem(PDO $db, int $cbId, ?int $excludeStepNo = null): array {
    $sql = 'SELECT pi.CB_Item_ID, SUM(pi.Quantity) AS processed
            FROM tbl_cb_processing_items pi
            INNER JOIN tbl_cb_processing p ON p.Step_ID = pi.Step_ID AND p.isDeleted = 0
            WHERE p.CB_ID = ? AND pi.isDeleted = 0';
    $params = [$cbId];
    if ($excludeStepNo !== null) { $sql .= ' AND p.Step_No <> ?'; $params[] = $excludeStepNo; }
    $sql .= ' GROUP BY pi.CB_Item_ID';
    $stmt = $db->prepare($sql);
    $stmt->execute($params);
    $out = [];
    foreach ($stmt->fetchAll(PDO::FETCH_ASSOC) as $r) { $out[(int)$r['CB_Item_ID']] = (float)$r['processed']; }
    return $out;
}

// Per-item progress (ordered / processed / remaining), matched by
// CB_Item_ID — the read-side twin of the hard-block check the save handler
// runs, so the editor's own "Remaining" column can never show a number the
// save handler would then refuse.
function cbpProgress(array $orderedItems, array $processedByItem): array {
    $rows = [];
    foreach ($orderedItems as $oi) {
        if ($oi['line_kind'] === 'caption') continue;
        $id = $oi['cb_item_id'];
        $processed = $processedByItem[$id] ?? 0.0;
        $rows[] = [
            'cb_item_id'         => $id,
            'items_description'  => $oi['items_description'],
            'unit'               => $oi['unit'],
            'line_kind'          => $oi['line_kind'],
            'ordered_quantity'   => $oi['orderable_quantity'],
            'processed_quantity' => $processed,
            'remaining_quantity' => $oi['orderable_quantity'] - $processed,
            'unit_cost'          => $oi['process_unit_cost'],
        ];
    }
    return $rows;
}

// "2026-130 (Contract 2026-167)" — the Bid Number alone can name several
// contracts, so messages and notifications carry both.
function cbpLabel(array $row): string {
    $number   = (string)($row['Bid_Number'] ?? '');
    $contract = trim((string)($row['Contract_Number'] ?? ''));
    return $contract !== '' ? "{$number} (Contract {$contract})" : $number;
}

function cbpNextStepNumber(array $steps): int {
    $used = array_map(fn($s) => (int)$s['step_no'], $steps);
    for ($n = 1; $n <= 200; $n++) { if (!in_array($n, $used, true)) return $n; }
    return count($used) + 1;
}

// One bid: header, ordered items, every live step + its lines (grouped),
// per-item progress, and order-wide totals/status.
function cbpGetDetail(PDO $db, int $cbId): ?array {
    $bidStmt = $db->prepare(
        'SELECT ID AS id, Bid_Number AS bid_number, Contract_Number AS contract_number,
                Contract_Name AS contract_name, Supplier_Name AS supplier_name,
                Contract_Amount AS contract_amount, PO_Number AS po_number
         FROM tbl_competitive_bidding WHERE ID = ? AND isDeleted = 0'
    );
    $bidStmt->execute([$cbId]);
    $bid = $bidStmt->fetch(PDO::FETCH_ASSOC);
    if (!$bid) return null;

    $orderedItems = cbpGetOrderedItems($db, $cbId);
    $processedByItem = cbpProcessedByItem($db, $cbId);
    $progress = cbpProgress($orderedItems, $processedByItem);

    $stepStmt = $db->prepare(
        'SELECT Step_ID AS step_id, Step_No AS step_no, Processing_Date AS processing_date,
                Remarks AS remarks, Created_By AS created_by, Date_Created AS date_created
         FROM tbl_cb_processing WHERE CB_ID = ? AND isDeleted = 0 ORDER BY Step_No'
    );
    $stepStmt->execute([$cbId]);
    $steps = $stepStmt->fetchAll(PDO::FETCH_ASSOC);

    $lineStmt = $db->prepare(
        'SELECT p.Step_No AS step_no, pi.CB_Item_ID AS cb_item_id, pi.Items_Description AS items_description,
                pi.Unit AS unit, pi.Quantity AS quantity, pi.Unit_Cost AS unit_cost, pi.Total_Cost AS total_cost
         FROM tbl_cb_processing_items pi
         INNER JOIN tbl_cb_processing p ON p.Step_ID = pi.Step_ID AND p.isDeleted = 0
         WHERE p.CB_ID = ? AND pi.isDeleted = 0 ORDER BY p.Step_No, pi.ID'
    );
    $lineStmt->execute([$cbId]);
    $byStep = [];
    foreach ($lineStmt->fetchAll(PDO::FETCH_ASSOC) as $l) { $byStep[(int)$l['step_no']][] = $l; }

    // What an undo would restore, per step — same reasoning as Progress
    // Billing's per-batch 'previous' lookup: the confirmation has to name
    // the real consequence, not a vague "undo".
    $prevStmt = $db->prepare(
        'SELECT Processing_Date AS processing_date, Remarks AS remarks
         FROM tbl_cb_processing
         WHERE CB_ID = ? AND Step_No = ? AND isDeleted = 1
         ORDER BY Step_ID DESC LIMIT 1'
    );
    $processedAmount = 0.0;
    foreach ($steps as &$s) {
        $n = (int)$s['step_no'];
        $s['items'] = $byStep[$n] ?? [];
        $s['total'] = 0.0;
        foreach ($s['items'] as $it) { $s['total'] += (float)$it['total_cost']; $processedAmount += (float)$it['total_cost']; }
        $prevStmt->execute([$cbId, $n]);
        $s['previous'] = $prevStmt->fetch(PDO::FETCH_ASSOC) ?: null;
    }
    unset($s);

    $orderedAmount = 0.0;
    foreach ($orderedItems as $oi) {
        if ($oi['line_kind'] === 'caption') continue;
        $orderedAmount += $oi['orderable_quantity'] * $oi['process_unit_cost'];
    }
    $totals = cbProcessingStatus($orderedAmount, $processedAmount, count($steps));

    return fixLegacyEncodingDeep([
        'bid'             => $bid,
        'ordered_items'   => $orderedItems,
        'progress'        => $progress,
        'steps'           => $steps,
        'totals'          => $totals,
        'next_step_no'    => cbpNextStepNumber($steps),
    ]);
}

if ($method === 'GET') {
    $cbId = (int)($_GET['id'] ?? 0);
    if (!$cbId) jsonResponse(false, 'Bid document id is required.');
    try {
        $detail = cbpGetDetail($db, $cbId);
    } catch (Throwable $e) {
        error_log('cb_processing GET failed: ' . $e->getMessage());
        jsonResponse(false, 'The Competitive Bidding processing tables have not been created yet. '
            . 'Run database/cb_processing.sql against the HRMIS server, then reload this page.');
    }
    if (!$detail) jsonResponse(false, 'Bid document not found.');
    if (isset($_GET['print_step'])) {
        $stepNo = filter_var($_GET['print_step'], FILTER_VALIDATE_INT, ['options' => ['min_range' => 1]]);
        $step = null;
        foreach ($detail['steps'] as $candidate) {
            if ($stepNo !== false && (int)$candidate['step_no'] === $stepNo) { $step = $candidate; break; }
        }
        if (!$step) jsonResponse(false, 'This processing step no longer exists. Reload the processing history.');
        try {
            $print = cbpGetPoPrintData($db, $cbId, $step);
        } catch (Throwable $e) {
            error_log('cb_processing PO print failed: ' . $e->getMessage());
            jsonResponse(false, 'Unable to load this processing step for PO printing.');
        }
        jsonResponse(true, '', $print);
    }
    jsonResponse(true, '', $detail);
}

$data = getInput();

// ── Save a step (POST = new, PUT = revise an existing one) ──────────────
// Open to anyone with module access, same as api/inspection_progress.php's
// save handler — no extra role gate. Reasoning carried over unchanged: the
// same person can already edit the bid's own item grid, which is strictly
// more consequential than recording a processing step against it.
if ($method === 'POST' || $method === 'PUT') {
    $cbId   = (int)($data['id'] ?? 0);
    $stepNo = (int)($data['step_no'] ?? 0);
    $items  = is_array($data['items'] ?? null) ? $data['items'] : [];
    if (!$cbId) jsonResponse(false, 'Bid document id is required.');
    if ($stepNo <= 0) jsonResponse(false, 'A valid step number is required.');

    // Bid_Number is still written onto the step rows (for readability), so
    // it is read from the record here rather than trusted from the request.
    $bidCheck = $db->prepare('SELECT Bid_Number, Contract_Number FROM tbl_competitive_bidding WHERE ID = ? AND isDeleted = 0');
    $bidCheck->execute([$cbId]);
    $bidRow = $bidCheck->fetch(PDO::FETCH_ASSOC);
    if (!$bidRow) jsonResponse(false, 'Bid document not found.');
    $bidNumber = (string)$bidRow['Bid_Number'];
    $bidLabel  = cbpLabel($bidRow);

    $existingCheck = $db->prepare('SELECT Step_ID FROM tbl_cb_processing WHERE CB_ID = ? AND Step_No = ? AND isDeleted = 0');
    $existingCheck->execute([$cbId, $stepNo]);
    $hasExisting = (bool)$existingCheck->fetch();
    if ($method === 'POST' && $hasExisting) jsonResponse(false, "Step {$stepNo} already exists on this bid. Revise it instead of creating it again.");
    if ($method === 'PUT' && !$hasExisting) jsonResponse(false, "Step {$stepNo} does not exist yet on this bid.");

    $orderedItems = cbpGetOrderedItems($db, $cbId);
    $orderedById = [];
    foreach ($orderedItems as $oi) { $orderedById[$oi['cb_item_id']] = $oi; }
    // Excludes this exact step's own current lines when revising, so a
    // revision is compared against every OTHER step, not against itself.
    $processedElsewhere = cbpProcessedByItem($db, $cbId, $method === 'PUT' ? $stepNo : null);

    // Validated fully BEFORE any write — a step half-written because line 4
    // of 6 failed validation would leave the bid in a worse state than
    // rejecting the whole save outright.
    $toInsert = [];
    $errors = [];
    foreach ($items as $row) {
        $cbItemId = (int)($row['cb_item_id'] ?? 0);
        if (!$cbItemId || !isset($orderedById[$cbItemId])) continue; // stray/unknown id, ignore
        $oi = $orderedById[$cbItemId];
        if ($oi['line_kind'] === 'caption') continue; // a LOT/description row is never processable

        $qty = is_numeric($row['quantity'] ?? null) ? (float)$row['quantity'] : 0.0;
        if ($qty <= 0) continue; // not processed in this step — simply omitted, nothing to record

        $already = $processedElsewhere[$cbItemId] ?? 0.0;
        $remaining = $oi['orderable_quantity'] - $already;
        // THE hard block — the explicit requirement this feature diverges
        // from Progress Billing for. Recomputed here from the database, not
        // trusted from whatever the client's "Remaining" column showed.
        if ($qty > $remaining + 0.0001) {
            $errors[] = sprintf('"%s" — trying to process %s but only %s remains.',
                $oi['items_description'], rtrim(rtrim(number_format($qty, 3), '0'), '.'),
                rtrim(rtrim(number_format(max($remaining, 0), 3), '0'), '.'));
            continue;
        }

        $unitCost = $oi['process_unit_cost'];
        $toInsert[] = [
            'cb_item_id'  => $cbItemId,
            'description' => $oi['items_description'],
            'unit'        => $oi['unit'],
            'quantity'    => $qty,
            'unit_cost'   => $unitCost,
            'total_cost'  => round($qty * $unitCost, 2),
        ];
    }
    if ($errors) jsonResponse(false, implode(' ', $errors)); // nothing written
    if (!$toInsert) jsonResponse(false, 'No items were processed in this step.');

    $user = getCurrentUser();
    try {
        $db->beginTransaction();
        if ($hasExisting) {
            // Same append-only convention as Progress Billing's save: the
            // current live rows for this step are superseded (isDeleted=1),
            // never overwritten in place, so every earlier version of a
            // step stays queryable.
            // Lines first, found through their live header's Step_ID —
            // once the header is superseded it no longer marks them.
            $db->prepare('UPDATE tbl_cb_processing_items pi
                            JOIN tbl_cb_processing p ON p.Step_ID = pi.Step_ID
                             SET pi.isDeleted = 1
                           WHERE p.CB_ID = ? AND p.Step_No = ? AND p.isDeleted = 0 AND pi.isDeleted = 0')
               ->execute([$cbId, $stepNo]);
            $db->prepare('UPDATE tbl_cb_processing SET isDeleted = 1 WHERE CB_ID = ? AND Step_No = ? AND isDeleted = 0')
               ->execute([$cbId, $stepNo]);
        }
        $headerStmt = $db->prepare(
            'INSERT INTO tbl_cb_processing (CB_ID, Bid_Number, Step_No, Processing_Date, Remarks, Created_By, Created_By_ID, Date_Created, isDeleted)
             VALUES (?, ?, ?, ?, ?, ?, ?, NOW(), 0)'
        );
        $headerStmt->execute([
            $cbId, $bidNumber, $stepNo, cbpDate($data['processing_date'] ?? '') ?: date('Y-m-d H:i:s'),
            trim((string)($data['remarks'] ?? '')) ?: null,
            $user['username'] ?? 'system', $user['id'] ?? null,
        ]);
        // The exact header row (version) these lines belong to — see the
        // Step_ID column note in database/cb_processing.sql for why this
        // matters: Step_No alone is reused across every revision.
        $newStepId = (int)$db->lastInsertId();

        $itemStmt = $db->prepare(
            'INSERT INTO tbl_cb_processing_items (Step_ID, Bid_Number, Step_No, CB_Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, Date_Created, isDeleted)
             VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, NOW(), 0)'
        );
        foreach ($toInsert as $it) {
            $itemStmt->execute([
                $newStepId, $bidNumber, $stepNo, $it['cb_item_id'], $it['description'], $it['unit'],
                $it['quantity'], $it['unit_cost'], $it['total_cost'],
            ]);
        }
        $db->commit();
    } catch (Exception $e) {
        if ($db->inTransaction()) $db->rollBack();
        error_log('cb_processing save failed: ' . $e->getMessage());
        jsonResponse(false, 'Unable to save this processing step.');
    }

    $verb = $hasExisting ? 'Revised' : 'Recorded';
    logAudit(($hasExisting ? 'REVISE' : 'CREATE') . '_CB_STEP', 'competitive_bidding',
        "{$verb} processing step {$stepNo} on bid {$bidLabel}");
    notifyPoUsers(
        'Competitive Bidding Processing Step ' . ($hasExisting ? 'Revised' : 'Recorded'),
        ($user['full_name'] ?? 'Someone') . " {$verb} step {$stepNo} on bid {$bidLabel}",
        'competitive_bidding.php?id=' . $cbId,
        null,
        'all'
    );

    jsonResponse(true, "Step {$stepNo} saved.", cbpGetDetail($db, $cbId) ?? []);
}

// ── Undo the most recent step ────────────────────────────────────────────
// Same shape as api/inspection_progress.php's undo: restores the version a
// revision replaced, or — if this was the step's only version — removes it
// entirely and frees its number for reuse. isDeleted is 3-state (0 live /
// 1 superseded / 2 retracted) so a retracted version can never be handed
// back by a later undo.
if ($method === 'DELETE') {
    $cbId   = (int)($_GET['id'] ?? 0);
    $stepNo = (int)($_GET['step_no'] ?? 0);
    if (!$cbId || $stepNo <= 0) jsonResponse(false, 'Bid document id and step number are required.');

    $bidStmt = $db->prepare('SELECT Bid_Number, Contract_Number FROM tbl_competitive_bidding WHERE ID = ? AND isDeleted = 0');
    $bidStmt->execute([$cbId]);
    $bidRow = $bidStmt->fetch(PDO::FETCH_ASSOC);
    if (!$bidRow) jsonResponse(false, 'Bid document not found.');
    $bidLabel = cbpLabel($bidRow);

    $curStmt = $db->prepare('SELECT Step_ID FROM tbl_cb_processing WHERE CB_ID = ? AND Step_No = ? AND isDeleted = 0');
    $curStmt->execute([$cbId, $stepNo]);
    $current = $curStmt->fetch(PDO::FETCH_ASSOC);
    if (!$current) jsonResponse(false, 'This step is not currently on file.');

    $prevStmt = $db->prepare(
        'SELECT Step_ID FROM tbl_cb_processing
         WHERE CB_ID = ? AND Step_No = ? AND isDeleted = 1 AND Step_ID < ?
         ORDER BY Step_ID DESC LIMIT 1'
    );
    $prevStmt->execute([$cbId, $stepNo, $current['Step_ID']]);
    $previous = $prevStmt->fetch(PDO::FETCH_ASSOC) ?: null;

    try {
        $db->beginTransaction();
        // Retract the CURRENT version precisely by Step_ID — not by
        // (CB_ID, Step_No, isDeleted), which is exactly the ambiguity
        // Step_ID exists to avoid once a step has more than one historical
        // version on file.
        $db->prepare('UPDATE tbl_cb_processing SET isDeleted = 2 WHERE Step_ID = ?')
           ->execute([$current['Step_ID']]);
        $db->prepare('UPDATE tbl_cb_processing_items SET isDeleted = 2 WHERE Step_ID = ?')
           ->execute([$current['Step_ID']]);
        if ($previous) {
            // Restores the SPECIFIC prior version this revision replaced —
            // its items are still isDeleted = 1 (superseded), untouched by
            // the two statements above, which only ever touched the current
            // version's own Step_ID.
            $db->prepare('UPDATE tbl_cb_processing SET isDeleted = 0 WHERE Step_ID = ?')->execute([$previous['Step_ID']]);
            $db->prepare('UPDATE tbl_cb_processing_items SET isDeleted = 0 WHERE Step_ID = ?')->execute([$previous['Step_ID']]);
        }
        $db->commit();
    } catch (Throwable $e) {
        if ($db->inTransaction()) $db->rollBack();
        error_log('cb_processing undo failed: ' . $e->getMessage());
        jsonResponse(false, 'Could not undo this step. Nothing was changed.');
    }

    $restored = (bool)$previous;
    logAudit('UNDO_CB_STEP', 'competitive_bidding',
        $restored ? "Undid the last revision of step {$stepNo} on bid {$bidLabel}" : "Removed processing step {$stepNo} on bid {$bidLabel}");
    $actorName = getCurrentUser()['full_name'] ?? 'Someone';
    notifyPoUsers(
        $restored ? 'CB Processing Step Undone' : 'CB Processing Step Removed',
        "{$actorName} " . ($restored ? "undid the last revision of step {$stepNo}" : "removed step {$stepNo}") . " on bid {$bidLabel}",
        'competitive_bidding.php?id=' . $cbId,
        null,
        'all'
    );
    jsonResponse(true, $restored ? 'Undone. The previous version has been restored.' : 'Undone. That step has been removed.',
        cbpGetDetail($db, $cbId) ?? []);
}

jsonResponse(false, 'Unsupported request method.');
