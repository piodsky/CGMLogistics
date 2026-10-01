<?php
// Inventory & Property API.
//
// Mirrors vb/Inventory/CGSO_Inventory.vb and CGSO_PO_Select.vb: an inventory
// record is a snapshot of one purchase order's items, brought in for property
// accountability (PAR/ICS custody) once they have arrived — a step that only
// applies to durable/capitalized goods, not consumables, which is why only
// ~12% of purchase orders ever go through this module (confirmed against live
// data before writing a line of this). It runs ALONGSIDE Inspection &
// Acceptance, not instead of it: a purchase order is commonly in both.
//
// Same versioning convention as tbl_inspection / tbl_purchase_order elsewhere
// in this app: a revision does not UPDATE the header or item rows, it soft-
// deletes the current ones and inserts new ones, so every earlier version
// stays queryable. tbl_inventory and tbl_inventory_items_details are linked
// by PO_Number only — each keeps its own Inv_ID sequence, exactly as the VB
// source does (the header's Inv_ID is never referenced anywhere outside its
// own table).
//
// What this file does NOT do: generate or reprint a PAR/ICS/ITR/PTR number,
// or record a transfer. That is api/inventory_issuance.php — kept separate
// because numbering has to be transactional in a way plain item edits do not.
require_once '../config.php';
require_once '../lib/inventory_officers.php';
require_once '../lib/par_numbers.php';
require_once '../lib/po_amendment_items.php';
require_once '../lib/cb_processing_items.php';
require_once '../lib/audit_history.php';
requireAuth();
requireModule('inventory');
header('Content-Type: application/json');
$db = getPoDB();
$method = $_SERVER['REQUEST_METHOD'];

const INV_AMEND_FIELD_MAP = [
    'Items_Description' => 'Amended_Items_Description',
    'Unit'               => 'Amended_Unit',
    'Quantity'           => 'Amended_Quantity',
    'Unit_Cost'          => 'Amended_Unit_Cost',
    'Total_Cost'         => 'Amended_Total_Cost',
];

function invParseDate($value) {
    if (!$value) return null;
    $ts = strtotime($value);
    return $ts ? date('Y-m-d H:i:s', $ts) : null;
}

// ── PAR / ICS classification ─────────────────────────────────────────────
//
// Same three cutoffs the VB source uses (₱5,000 and ₱49,001) — confirmed
// against live data to be this office's real, deliberate cutoffs (PAR items
// on file start at ₱49,450, not the textbook COA ₱50,000). What changes is
// that the legacy's penny-wide gap — a unit cost of exactly ₱49,000.00
// satisfied neither "< 49000.00" nor ">= 49001.01" and fell through to a
// literal "ERROR" category, which really happened to 3 live items — is
// closed. Every value now lands in exactly one bucket.
function invClassify($unitCost): string {
    $c = (float)$unitCost;
    if ($c < 5000)  return 'ICS LOW VALUE';
    if ($c < 49001) return 'ICS HIGH VALUE';
    return 'PAR';
}

// A heading row carried over from the purchase order — "SET 1", "LOT 1" — has
// no unit, quantity or cost against it there either, so it is not a stocked
// item here: it cannot be classified, can never be issued a PAR/ICS number,
// and should not count toward "how many lines is this PO waiting to issue."
// Same "blank" test purchase_orders.php's item grid already uses to tell a
// title row from a real one (see invalidItemRowNumbers() in
// api/purchase_orders.php) — kept identical on purpose so the two modules
// never disagree about which rows are labels.
function invIsTitleRow($unit, $qty, $unitCost, $totalCost): bool {
    return trim((string)$unit) === ''
        && (float)$qty == 0 && (float)$unitCost == 0 && (float)$totalCost == 0;
}

// SQL form of invIsTitleRow(), negated — for queries that count/sum
// tbl_inventory_items_details rows aliased `d` and must not count a title
// row as a stocked item.
const INV_NOT_TITLE_ROW_SQL =
    "NOT (TRIM(COALESCE(d.Unit,'')) = '' AND COALESCE(d.Quantity,0) = 0
          AND COALESCE(d.Unit_Cost,0) = 0 AND COALESCE(d.Total_Cost,0) = 0)";

// ── Pull a purchase order's items in, ready to stock in ─────────────────
// Same SELECT CGSO_PO_Select.vb runs on double-clicking a PO. Each item is
// pre-classified so the operator sees where it will land before anything is
// saved — the VB left this to a CellEndEdit handler that only ever looked at
// the LAST selected row, so opening a multi-item PO left every row but one
// unclassified until clicked individually. All rows are classified here.
// A search over purchase orders, scoped to this file rather than reused from
// api/purchase_orders.php on purpose: that endpoint requires
// requireModule('purchase_orders'), and an Inventory-only account (the real,
// distinct HRMIS flag confirmed earlier) must be able to find a PO to receive
// without also holding procurement access. Read-only, five columns, capped —
// nothing that endpoint's own broader access rule needs to guard.
function invSearchPO(PDO $db, string $term): array {
    $stmt = $db->prepare(
        "SELECT PO_Number AS po_number, PR_Number AS pr_number, Supplier_Name AS supplier_name,
                Department AS department, Purpose AS purpose
         FROM tbl_purchase_order
         WHERE isDeleted = 0 AND (PO_Number LIKE ? OR PR_Number LIKE ? OR Supplier_Name LIKE ? OR Department LIKE ?)
         ORDER BY Date_Created DESC LIMIT 20"
    );
    $like = '%' . $term . '%';
    $stmt->execute([$like, $like, $like, $like]);
    $orders = $stmt->fetchAll(PDO::FETCH_ASSOC);
    foreach ($orders as &$o) $o['source'] = 'po';
    unset($o);
    return fixLegacyEncodingDeep(array_merge($orders, invSearchBids($db, $term)));
}

// ── Competitive Bidding contracts as receivable "purchase orders" ─────────
// A bid contract is itself the purchase document — none of them is linked to
// a PO in tbl_purchase_order — so Inventory can receive one directly. It is
// filed under the key CB-<Bid No.>-<Contract No.> (CB-<Bid No.> when there is
// no contract number): the user's choice, 2026-09-29. It cannot clash with a
// real PO number, reads as a bid at a glance, and stays unique because Bid
// No. + Contract No. is unique (api/competitive_bidding.php cbExists()).
//
// From there the key is used exactly like a PO Number — the inventory record,
// PAR/ICS issuance, transfers and History all work on it unchanged. It is a
// snapshot like every intake: renaming the bid later does not follow it here.
function invBidKey(string $bidNumber, string $contractNumber): string {
    $bidNumber = trim($bidNumber);
    $contractNumber = trim($contractNumber);
    return 'CB-' . $bidNumber . ($contractNumber !== '' ? '-' . $contractNumber : '');
}

function invSearchBids(PDO $db, string $term): array {
    try {
        $stmt = $db->prepare(
            "SELECT Bid_Number, Contract_Number, Contract_Name, Contract_Location, Supplier_Name
               FROM tbl_competitive_bidding
              WHERE isDeleted = 0 AND (Bid_Number LIKE ? OR Contract_Number LIKE ? OR Contract_Name LIKE ?
                                       OR Supplier_Name LIKE ? OR CONCAT('CB-', Bid_Number) LIKE ?)
              ORDER BY Date_Created DESC LIMIT 10"
        );
        $like = '%' . $term . '%';
        $stmt->execute([$like, $like, $like, $like, $like]);
    } catch (Throwable $e) {
        return [];   // Competitive Bidding tables absent on this server — POs still search fine
    }
    return array_map(fn($b) => [
        'po_number'     => invBidKey((string)$b['Bid_Number'], (string)$b['Contract_Number']),
        'pr_number'     => '',
        'supplier_name' => $b['Supplier_Name'],
        'department'    => $b['Contract_Location'],
        'purpose'       => $b['Contract_Name'],
        'source'        => 'bid',
        'bid_number'    => $b['Bid_Number'],
        'contract_number' => $b['Contract_Number'],
    ], $stmt->fetchAll(PDO::FETCH_ASSOC));
}

// The intake preview for a CB- key: the contract's header and its own item
// grid, shaped exactly like invPreviewPO()'s answer so the page needs no
// second path. Found by rebuilding each live bid's key (a handful of rows)
// rather than parsing the key, since a Bid or Contract Number can itself
// contain dashes.
function invPreviewBid(PDO $db, string $key): ?array {
    $bid = null;
    foreach ($db->query("SELECT * FROM tbl_competitive_bidding WHERE isDeleted = 0")->fetchAll(PDO::FETCH_ASSOC) as $b) {
        if (invBidKey((string)$b['Bid_Number'], (string)$b['Contract_Number']) === $key) { $bid = $b; break; }
    }
    if (!$bid) return null;

    $stmt = $db->prepare(
        "SELECT Items_Description, Unit, Quantity, Unit_Cost, Total_Cost
           FROM tbl_competitive_bidding_items_details
          WHERE CB_ID = ? AND isDeleted = 0 ORDER BY Sort_Order, ID"
    );
    $stmt->execute([(int)$bid['ID']]);
    $items = [];
    foreach ($stmt->fetchAll(PDO::FETCH_ASSOC) as $it) {
        $isTitle = invIsTitleRow($it['Unit'], $it['Quantity'], $it['Unit_Cost'], $it['Total_Cost']);
        $items[] = $it + [
            'PO_Item_ID' => null, 'Item_ID' => null, 'NumberSeries' => null,
            'is_title' => $isTitle,
            'inventory_category' => $isTitle ? '' : invClassify($it['Unit_Cost']),
        ];
    }
    $address = trim(implode(', ', array_filter([trim((string)$bid['Supplier_Address']), trim((string)$bid['Supplier_Address2'])])));
    return fixLegacyEncodingDeep([
        'po_number'           => $key,
        'pr_number'           => '',
        'department'          => $bid['Contract_Location'],   // the office the contract is for
        'division'            => '',
        'section'             => '',
        'supplier_name'       => $bid['Supplier_Name'],
        'supplier_address'    => $address,
        'category'            => '',
        'mode_of_procurement' => 'Public Bidding',
        'po_date'             => $bid['Contract_Date'] ?: $bid['NOA_Date'],
        'purpose'             => $bid['Contract_Name'],
        'source_of_fund'      => '',
        'source'              => 'bid',
        'bid_number'          => $bid['Bid_Number'],
        'contract_number'     => $bid['Contract_Number'],
        'items'               => $items,
    ]);
}

function invPreviewPO(PDO $db, string $poNumber): ?array {
    if (strncmp($poNumber, 'CB-', 3) === 0) return invPreviewBid($db, $poNumber);
    // No Source_of_Fund here on purpose: the purchase order carries no such
    // column, and CGSO_PO_Select.vb never populated it either — it is filled
    // in by hand at Inventory intake, same as the legacy form's txtSource.
    $stmt = $db->prepare(
        "SELECT PO_Number, PR_Number, Department, Division, Section, Supplier_Name,
                Supplier_Address, Category, Mode_of_Procurement, Date_PO, Purpose
         FROM tbl_purchase_order WHERE PO_Number = ? AND isDeleted = 0"
    );
    $stmt->execute([$poNumber]);
    $po = $stmt->fetch(PDO::FETCH_ASSOC);
    if (!$po) return null;

    $stmt = $db->prepare(
        "SELECT ID AS PO_Item_ID, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost, NumberSeries
         FROM tbl_purchase_order_items_details WHERE PO_Number = ? AND isDeleted = 0 ORDER BY ID"
    );
    $stmt->execute([$poNumber]);
    $items = $stmt->fetchAll(PDO::FETCH_ASSOC);
    // Overlays the latest amendment (if any) so a new intake previews — and
    // then snapshots at save time, same as it already snapshots everything
    // else — the amended description/spec, never the stale original. Read-
    // only; never touches tbl_purchase_order_items_details. The PO Number
    // stays exactly as-is: no duplicate PO is created by this. See
    // lib/po_amendment_items.php.
    $items = applyPoItemAmendments($db, $poNumber, $items, 'PO_Item_ID', INV_AMEND_FIELD_MAP);
    // Read-only Competitive Bidding overlay — attaches cb_bid_number/
    // cb_processed_quantity/cb_remaining_quantity when this PO is linked to
    // a bid with processing steps recorded. Informational only; intake is
    // never blocked on it. See lib/cb_processing_items.php.
    $items = applyCbProcessingOverlay($db, $poNumber, $items, 'Items_Description');
    foreach ($items as &$it) {
        $isTitle = invIsTitleRow($it['Unit'], $it['Quantity'], $it['Unit_Cost'], $it['Total_Cost']);
        $it['is_title']          = $isTitle;
        $it['inventory_category'] = $isTitle ? '' : invClassify($it['Unit_Cost']);
    }
    unset($it);

    return fixLegacyEncodingDeep([
        'po_number'           => $po['PO_Number'],
        'pr_number'           => $po['PR_Number'],
        'department'          => $po['Department'],
        'division'            => $po['Division'],
        'section'             => $po['Section'],
        'supplier_name'       => $po['Supplier_Name'],
        'supplier_address'    => $po['Supplier_Address'],
        'category'            => $po['Category'],
        'mode_of_procurement' => $po['Mode_of_Procurement'],
        'po_date'             => $po['Date_PO'],
        'purpose'             => $po['Purpose'],
        'source_of_fund'      => $po['Source_of_Fund'],
        'items'               => $items,
    ]);
}

// ── Worklist ──────────────────────────────────────────────────────────────
function invGetList(PDO $db): array {
    $search = trim($_GET['search'] ?? '');
    $from   = $_GET['from'] ?? '';
    $to     = $_GET['to']   ?? '';
    $status = strtolower(trim($_GET['status'] ?? ''));
    $params = [];

    // One row per live inventory header, with the item count / total value /
    // "has this been issued a PAR or ICS yet" folded in as correlated
    // subqueries — the same shape api/inspection.php uses for its billing
    // count, for the same reason: a LEFT JOIN onto the item-detail table
    // would multiply the header rows.
    //
    // item_count/issued_count both exclude title rows (INV_NOT_TITLE_ROW_SQL)
    // — a row like 'SET 1' can never be issued a PAR/ICS number, so it must
    // not inflate the "how many lines are left to issue" denominator.
    $sql = "SELECT ti.Inv_ID AS header_id, ti.PO_Number AS record_key,
                   ti.PO_Number AS po_number, ti.PR_Number AS pr_number,
                   ti.Department AS department, ti.Supplier_Name AS supplier_name,
                   ti.Category AS category, ti.Purpose AS purpose, ti.PO_Date AS po_date,
                   ti.Date_Entry AS date_entry, ti.Date_Created AS date_created,
                   ti.Is_Manual AS is_manual,
                   (SELECT COUNT(*) FROM tbl_inventory_items_details d
                     WHERE d.PO_Number = ti.PO_Number AND d.isDeleted = '0'
                       AND " . INV_NOT_TITLE_ROW_SQL . ") AS item_count,
                   (SELECT COALESCE(SUM(d.Total_Cost), 0) FROM tbl_inventory_items_details d
                     WHERE d.PO_Number = ti.PO_Number AND d.isDeleted = '0') AS total_value,
                   (SELECT COUNT(*) FROM tbl_inventory_items_details d
                     WHERE d.PO_Number = ti.PO_Number AND d.isDeleted = '0'
                       AND d.PAR_ICS_Number IS NOT NULL AND d.PAR_ICS_Number <> ''
                       AND " . INV_NOT_TITLE_ROW_SQL . ") AS issued_count
            FROM tbl_inventory ti
            WHERE ti.isDeleted = '0'";
    $baseSql = $sql;      // before any filter, for the "due to renew" list below

    if ($from) { $sql .= ' AND ti.Date_Created >= ?'; $params[] = date('Y-m-d 00:00:00', strtotime($from)); }
    if ($to)   { $sql .= ' AND ti.Date_Created <= ?'; $params[] = date('Y-m-d 23:59:59', strtotime($to)); }
    if ($search !== '') {
        $sql .= ' AND (ti.PO_Number LIKE ? OR ti.PR_Number LIKE ? OR ti.Department LIKE ?
                       OR ti.Supplier_Name LIKE ? OR ti.Purpose LIKE ?)';
        $term = '%' . $search . '%';
        $params = array_merge($params, array_fill(0, 5, $term));
    }
    $sql .= ' ORDER BY ti.Date_Created DESC LIMIT 500';

    $stmt = $db->prepare($sql);
    $stmt->execute($params);
    $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);

    // Which records have lines due for renewal (every three years — see
    // lib/par_numbers.php), and how many. Over EVERY live record, not just the 500
    // this list shows: a number that is three years old belongs to an old record,
    // which is exactly the kind the newest-500 window leaves out. The age of a
    // number is only in the number, so they are read and judged here by the same
    // function the detail view uses, rather than by a second copy of the rule in SQL.
    $dueByPo = [];
    // EXISTS, not a JOIN: a purchase order can have more than one live header
    // row, and a join would count each of its lines once per header.
    $numStmt = $db->query(
        "SELECT d.PO_Number, d.PAR_ICS_Number FROM tbl_inventory_items_details d
         WHERE d.isDeleted = '0' AND d.PAR_ICS_Number IS NOT NULL AND d.PAR_ICS_Number <> ''
           AND EXISTS (SELECT 1 FROM tbl_inventory ti WHERE ti.PO_Number = d.PO_Number AND ti.isDeleted = '0')"
    );
    foreach ($numStmt->fetchAll(PDO::FETCH_NUM) as [$po, $number]) {
        if (parIcsRenewalDue((string)$number)) $dueByPo[$po] = ($dueByPo[$po] ?? 0) + 1;
    }

    $shape = function (array $rows) use ($dueByPo): array {
        foreach ($rows as &$r) {
            $r['due_count']     = $dueByPo[$r['record_key']] ?? 0;
            $r['item_count']    = (int)$r['item_count'];
            $r['issued_count']  = (int)$r['issued_count'];
            $r['total_value']   = (float)$r['total_value'];
            $r['is_manual']     = (bool)$r['is_manual'];
            // "Awaiting issuance" = at least one line still has no PAR/ICS number.
            // Confirmed against live data this is common (up to 84% of ICS LOW
            // VALUE lines) — a real backlog, not an edge case, so it is a first-
            // class, filterable state rather than something the operator has to
            // notice on their own.
            $r['awaiting_issuance'] = $r['issued_count'] < $r['item_count'];
        }
        unset($r);
        return $rows;
    };
    $rows = $shape($rows);

    $counts = ['total' => count($rows), 'awaiting' => 0, 'issued' => 0, 'manual' => 0, 'due' => count($dueByPo)];
    foreach ($rows as $r) {
        $r['awaiting_issuance'] ? $counts['awaiting']++ : $counts['issued']++;
        if ($r['is_manual']) $counts['manual']++;
    }

    if ($status === 'awaiting') {
        $rows = array_values(array_filter($rows, fn($r) => $r['awaiting_issuance']));
    } elseif ($status === 'issued') {
        $rows = array_values(array_filter($rows, fn($r) => !$r['awaiting_issuance']));
    } elseif ($status === 'manual') {
        $rows = array_values(array_filter($rows, fn($r) => $r['is_manual']));
    } elseif ($status === 'due') {
        // Its own query, not a filter over the rows above: those are the newest 500
        // in the date range, and a record due for renewal is by definition an old
        // one. So this ignores the date range and the cap; the search box still
        // narrows it. (The other chips' counts stay as they were.)
        $rows = [];
        if ($dueByPo) {
            // One row per purchase order — its latest live header, the one the detail
            // view opens — even where an order has been given two.
            $dueSql = $baseSql . ' AND ti.PO_Number IN (' . implode(',', array_fill(0, count($dueByPo), '?')) . ')
                       AND ti.Inv_ID = (SELECT MAX(t2.Inv_ID) FROM tbl_inventory t2 WHERE t2.PO_Number = ti.PO_Number AND t2.isDeleted = \'0\')';
            $dueParams = array_map('strval', array_keys($dueByPo));
            if ($search !== '') {
                $dueSql .= ' AND (ti.PO_Number LIKE ? OR ti.PR_Number LIKE ? OR ti.Department LIKE ?
                               OR ti.Supplier_Name LIKE ? OR ti.Purpose LIKE ?)';
                $dueParams = array_merge($dueParams, array_fill(0, 5, '%' . $search . '%'));
            }
            $dueStmt = $db->prepare($dueSql . ' ORDER BY ti.Date_Created ASC');   // oldest first: most overdue on top
            $dueStmt->execute($dueParams);
            $rows = $shape($dueStmt->fetchAll(PDO::FETCH_ASSOC));
        }
    }

    return ['records' => fixLegacyEncodingDeep($rows), 'counts' => $counts];
}

// ── Detail ────────────────────────────────────────────────────────────────
function invGetDetail(PDO $db, string $poNumber): ?array {
    $stmt = $db->prepare(
        "SELECT Inv_ID AS header_id, PO_Number AS record_key,
                PO_Number AS po_number, PR_Number AS pr_number,
                Department AS department, Category AS category, Mode_of_Procurement AS mode_of_procurement,
                Supplier_Name AS supplier_name, Supplier_Address AS supplier_address,
                Purpose AS purpose, Source_of_Fund AS source_of_fund,
                PO_Date AS po_date, Date_Entry AS date_entry, Date_Created AS date_created,
                Initial AS initial, Is_Manual AS is_manual
         FROM tbl_inventory WHERE PO_Number = ? AND isDeleted = '0' ORDER BY Inv_ID DESC LIMIT 1"
    );
    $stmt->execute([$poNumber]);
    $header = $stmt->fetch(PDO::FETCH_ASSOC);
    if (!$header) return null;
    $header['is_manual'] = (bool)$header['is_manual'];

    $stmt = $db->prepare(
        "SELECT Inv_ID AS inv_id, Item_ID AS item_id, Items_Description AS items_description,
                Unit AS unit, Quantity AS quantity, Unit_Cost AS unit_cost, Total_Cost AS total_cost,
                Tag_Number AS tag_number, Serial_Number AS serial_number,
                Inventory_Category AS inventory_category, PAR_ICS_Number AS par_ics_number,
                Accountable_Officer AS accountable_officer, Position AS position, Location AS location,
                Classification AS classification, Remarks AS remarks, `Condition` AS `condition`,
                NumberSeries AS number_series, Fund_Cluster AS fund_cluster,
                Date_Transfered AS date_transferred, PTR_ITR_Number AS ptr_itr_number,
                Estimated_Life AS estimated_life
         FROM tbl_inventory_items_details WHERE PO_Number = ? AND isDeleted = '0' ORDER BY Inv_ID"
    );
    $stmt->execute([$poNumber]);
    $items = $stmt->fetchAll(PDO::FETCH_ASSOC);

    // Sub-details (serialized breakdown) for every item line in one query,
    // grouped in PHP — same "one round trip regardless of how many lines"
    // shape api/inspection_progress.php uses for its billing items.
    $subByInv = [];
    if ($items) {
        $ids = array_column($items, 'inv_id');
        $in  = implode(',', array_fill(0, count($ids), '?'));
        $stmt = $db->prepare(
            "SELECT sub_ID AS sub_id, Inv_ID AS inv_id, Items_Description AS items_description,
                    Unit_Cost AS unit_cost, Serial_Number AS serial_number, Tag_Number AS tag_number,
                    Accountable_Officer AS accountable_officer, Location AS location,
                    Remarks AS remarks, `Condition` AS `condition`, Date_Entry AS date_entry
             FROM tbl_inventory_sub_details WHERE Inv_ID IN ($in) AND isDeleted = 0 ORDER BY sub_ID"
        );
        $stmt->execute($ids);
        foreach ($stmt->fetchAll(PDO::FETCH_ASSOC) as $s) $subByInv[(int)$s['inv_id']][] = $s;
    }
    foreach ($items as &$it) {
        $it['sub_details'] = $subByInv[(int)$it['inv_id']] ?? [];
        // When this number was last issued or renewed, and whether it is now due
        // to be renewed (every three years). Decided here, once, so the grid and
        // the Issue dialog only ever display the answer.
        $number = (string)($it['par_ics_number'] ?? '');
        $it['issued_year'] = $number === '' ? null : parIcsIssuedYear($number);
        $it['renew_due']   = $number !== '' && parIcsRenewalDue($number);
    }
    unset($it);

    // What an undo would restore, same lookup shape as inspection's — named so
    // the confirmation dialog can say exactly what pressing it will do.
    $prev = $db->prepare(
        "SELECT Date_Created AS date_created FROM tbl_inventory
         WHERE PO_Number = ? AND isDeleted = '1' AND Inv_ID < ? ORDER BY Inv_ID DESC LIMIT 1"
    );
    $prev->execute([$poNumber, $header['header_id']]);
    $previous = $prev->fetch(PDO::FETCH_ASSOC) ?: null;

    return fixLegacyEncodingDeep([
        'header'   => $header,
        'items'    => $items,
        'exists'   => true,
        'previous' => $previous,
    ]);
}

// Everything currently in ONE person's custody, across every PO — the
// Personnel Memorandum Receipt. Every other read in this file scopes by
// PO_Number/Inv_ID; this is the first one scoped by Accountable_Officer
// instead (see database/inventory_officer_index.sql for the index this
// needs). Combines both tables that carry that column: line-item rows on
// tbl_inventory_items_details, and — separately — individually-tracked
// UNITS on tbl_inventory_sub_details, because a person can be custodian of
// just one unit out of a multi-unit line rather than the whole line. A
// sub-detail row has no Quantity/Total_Cost/Unit of its own (it IS one
// physical unit; qty is implicitly 1), so those are synthesised here rather
// than left null, and Unit is inherited from the parent line.
function invMemorandumReceipt(PDO $db, string $officer): array {
    $officer = trim($officer);
    if ($officer === '') return ['rows' => [], 'total_value' => 0];

    // Everything saved under any spelling of this person, not just the one
    // picked. A name matching no group (typed straight into the box) is looked
    // up as it stands.
    $spellings = [$officer];
    $key = mb_strtoupper($officer);
    foreach (invOfficerGroups($db) as $g) {
        if (in_array($key, array_map('mb_strtoupper', $g['members']), true)) { $spellings = $g['members']; break; }
    }
    $in = implode(',', array_fill(0, count($spellings), '?'));

    $stmt = $db->prepare(
        "SELECT d.Inv_ID AS row_id, h.PO_Number AS po_number,
                d.Items_Description AS items_description,
                d.Unit AS unit, d.Quantity AS quantity, d.Unit_Cost AS unit_cost, d.Total_Cost AS total_cost,
                d.Tag_Number AS tag_number, d.Serial_Number AS serial_number, d.Position AS position,
                d.Location AS location, d.`Condition` AS `condition`, d.Date_Entry AS date_entry,
                h.Department AS department, 'line' AS source
         FROM tbl_inventory_items_details d
         INNER JOIN tbl_inventory h ON h.PO_Number = d.PO_Number AND h.isDeleted = '0'
         WHERE d.Accountable_Officer IN ({$in}) AND d.isDeleted = '0'"
    );
    $stmt->execute($spellings);
    $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);

    $stmt = $db->prepare(
        "SELECT s.sub_ID AS row_id, h.PO_Number AS po_number,
                s.Items_Description AS items_description,
                p.Unit AS unit, 1 AS quantity, s.Unit_Cost AS unit_cost, s.Unit_Cost AS total_cost,
                s.Tag_Number AS tag_number, s.Serial_Number AS serial_number, NULL AS position,
                s.Location AS location, s.`Condition` AS `condition`, s.Date_Entry AS date_entry,
                h.Department AS department, 'unit' AS source
         FROM tbl_inventory_sub_details s
         INNER JOIN tbl_inventory h ON h.PO_Number = s.PO_Number AND h.isDeleted = '0'
         LEFT JOIN tbl_inventory_items_details p ON p.Inv_ID = s.Inv_ID
         WHERE s.Accountable_Officer IN ({$in}) AND s.isDeleted = 0"
    );
    $stmt->execute($spellings);
    $rows = array_merge($rows, $stmt->fetchAll(PDO::FETCH_ASSOC));

    usort($rows, fn($a, $b) => strcmp((string)($b['date_entry'] ?? ''), (string)($a['date_entry'] ?? '')));
    $total = array_sum(array_map(fn($r) => (float)$r['total_cost'], $rows));
    // Position/Department aren't asked for separately — they ride along on
    // the item rows (Position only exists on tbl_inventory_items_details, not
    // the sub-detail table), so the most recent row's is offered as a
    // reasonable default for the printed header rather than left for the
    // caller to dig out itself.
    $first = $rows[0] ?? [];

    return fixLegacyEncodingDeep([
        'rows' => $rows,
        'total_value' => $total,
        'position' => $first['position'] ?? '',
        'department' => $first['department'] ?? '',
        // The other spellings folded in, so the screen can say so rather than
        // quietly showing more than the name that was picked.
        'also_saved_as' => array_values(array_filter($spellings, fn($s) => mb_strtoupper($s) !== $key)),
    ]);
}

// ── Master lists this screen needs ───────────────────────────────────────
function invSignatories(PDO $db): array {
    // How a person's name is written on an inventory record: FIRST M. SURNAME —
    // the same shape sourceload() in the VB produced, so a name suggested today
    // matches the Accountable_Officer already saved against their items (the
    // Personnel MR lookup matches on that exact text). One difference: a person
    // with no middle name used to drop out of the list entirely, because
    // CONCAT() with a NULL is NULL; they now read FIRST SURNAME.
    $nameSql = "CONCAT(TRIM(p.FirstName), ' ',
                       IF(TRIM(COALESCE(p.MiddleName,'')) = '', '', CONCAT(LEFT(TRIM(p.MiddleName),1), '. ')),
                       TRIM(p.SurName))";

    // The Personnel MR search suggests the names actually SAVED as Accountable
    // Officer on inventory, not HRMIS personnel — one per person, however many
    // spellings of them are saved (see invOfficerGroups()).
    $mrOfficers = array_column(invOfficerGroups($db), 'rep');
    sort($mrOfficers, SORT_STRING);

    // The people who should be suggested as an Accountable Officer or Transfer
    // recipient: those on a payroll right now, with the position from their
    // most recent payroll line. tblpersonalinformation cannot say who is active
    // (EmploymentStatus reads "Regular" on all 9,000+ rows), the payroll can.
    //   - Regular, Casual and Job Order payrolls are each a table of their own;
    //     a person can move between them, so the newest line across all three wins.
    //   - "Now" = a non-deleted line dated within the last 90 days and not in the
    //     future (the regular payroll carries periods dated months ahead).
    //   - Name comes from personnel, not payroll: the payrolls each write names
    //     differently ("CEBALLOS FELBERT P." vs "BAUTISTA, RONDE TAPO").
    // Read-only; ~50ms for ~3,700 people. Falls back to everyone if it fails, so
    // a payroll table hiccup cannot take the picklist away.
    try {
        $window = "Period_Date BETWEEN DATE_SUB(CURDATE(), INTERVAL 90 DAY) AND CURDATE()
                   AND isDeleted = 0 AND TRIM(COALESCE(Position,'')) <> ''";
        $activeRows = $db->query(
            "WITH pay AS (
                 SELECT CAST(PIID AS UNSIGNED) AS piid, Position, Period_Date FROM hrmis.tbl_syl_payroll_for_print        WHERE {$window}
                 UNION ALL
                 SELECT CAST(PIID AS UNSIGNED),          Position, Period_Date FROM hrmis.tbl_syl_payroll_for_print_casual WHERE {$window}
                 UNION ALL
                 SELECT PIID,                            Position, Period_Date FROM hrmis.tbl_syl_payroll_for_print_jo     WHERE {$window}
             ), newest AS (
                 SELECT piid, Position, ROW_NUMBER() OVER (PARTITION BY piid ORDER BY Period_Date DESC) AS rn FROM pay
             )
             SELECT {$nameSql} AS name, TRIM(n.Position) AS position
             FROM newest n JOIN hrmis.tblpersonalinformation p ON p.PIID = n.piid
             WHERE n.rn = 1 AND TRIM(COALESCE(p.FirstName,'')) <> '' AND TRIM(COALESCE(p.SurName,'')) <> ''
             ORDER BY name"
        )->fetchAll(PDO::FETCH_ASSOC);
    } catch (Throwable $e) {
        error_log('invSignatories: active payroll lookup failed, using all personnel: ' . $e->getMessage());
        $activeRows = null;
    }

    if ($activeRows) {
        $officers = array_values(array_unique(array_column($activeRows, 'name')));
        $officerPositions = [];
        foreach ($activeRows as $r) $officerPositions[$r['name']] = $r['position'];
        $positions = array_values(array_unique(array_column($activeRows, 'position')));
        sort($positions, SORT_STRING);
    } else {
        // Fallback: the old sources, so the screen behaves exactly as before.
        $officers = $db->query(
            "SELECT DISTINCT {$nameSql} AS name FROM hrmis.tblpersonalinformation p
             WHERE TRIM(COALESCE(p.FirstName,'')) <> '' AND TRIM(COALESCE(p.SurName,'')) <> ''
             ORDER BY name"
        )->fetchAll(PDO::FETCH_COLUMN);
        $officerPositions = [];
        $positions = $db->query(
            "SELECT DISTINCT Position FROM hrmis.tbl_syl_payroll_parent
             WHERE YEAR = " . (int)date('Y') . " AND Position IS NOT NULL AND Position <> ''
             ORDER BY Position"
        )->fetchAll(PDO::FETCH_COLUMN);
    }

    // Location suggestions: ref_department already carries every city office
    // AND every barangay (47 of them) — confirmed live, so nothing new is
    // needed here; Location on an inventory item is "which office or
    // barangay currently holds this", and that is exactly what this list is.
    $locations = $db->query(
        "SELECT Department FROM ref_department WHERE isDeleted = 0 ORDER BY Department"
    )->fetchAll(PDO::FETCH_COLUMN);

    // The CGSO officer who signs every PAR/ICS/Property Card. The legacy VB
    // hardcodes ref_signaturies.id = 6; here the same row is found by its
    // title instead, so replacing whoever holds that post is a Master Data
    // edit, not a code change — the same rule the Disbursement Voucher's
    // three officers already use. Returned as a suggestion, not locked: the
    // field stays editable, exactly like every other signatory field in this
    // app.
    $cgso = $db->query(
        "SELECT Signaturies AS name, Position AS position FROM ref_signaturies
         WHERE isDeleted = 0 AND Position LIKE '%CGSO%DESIGNATE%' LIMIT 1"
    )->fetch(PDO::FETCH_ASSOC);

    // Everyone who might sign as CGSO Officer / Approved-by / Issued-by on a
    // transfer — the same CGSO office roster used elsewhere in this app.
    $cgsoStaff = $db->query(
        "SELECT Signaturies AS name, Position AS position FROM ref_signaturies
         WHERE isDeleted = 0 AND Department = 'CITY GENERAL SERVICES OFFICE'
         ORDER BY Signaturies"
    )->fetchAll(PDO::FETCH_ASSOC);

    // Suggestions for Manual Entry's Supplier Name and Category — the same
    // ref_supplier / ref_category master data the Purchase Order screen
    // suggests from (Department reuses $locations above, which IS
    // ref_department). Read here rather than through api/master_data.php,
    // which is gated on the Purchase Orders module and would turn the
    // suggestions off for an Inventory-only account. Still free text on save:
    // a legacy record may name a supplier that was never on the list.
    $suppliers = $db->query(
        "SELECT DISTINCT TRIM(Supplier_Name) FROM ref_supplier
         WHERE TRIM(COALESCE(Supplier_Name,'')) <> '' ORDER BY 1"
    )->fetchAll(PDO::FETCH_COLUMN);

    $categories = $db->query(
        "SELECT Category FROM ref_category
         WHERE isDeleted = 0 AND TRIM(COALESCE(Category,'')) <> '' ORDER BY Category"
    )->fetchAll(PDO::FETCH_COLUMN);

    // The Warehouse Officer who signs "Released/Issued by" on every ITR and PTR.
    // Found by TITLE in ref_signatory, not by name, for the same reason as the
    // CGSO officer above: when the post changes hands that is a Master Data
    // edit, not a code change. Prefilled on the Transfer dialog, still editable.
    $warehouse = $db->query(
        "SELECT Name AS name, Title AS position FROM ref_signatory
         WHERE isDeleted = 0 AND Title LIKE '%WAREHOUSE%' ORDER BY ID LIMIT 1"
    )->fetch(PDO::FETCH_ASSOC);

    $classifications = $db->query(
        "SELECT Classification FROM ref_classification
         WHERE isDeleted = 0 AND TRIM(COALESCE(Classification,'')) <> '' ORDER BY Classification"
    )->fetchAll(PDO::FETCH_COLUMN);

    return fixLegacyEncodingDeep([
        'officers'        => $officers,            // active on payroll
        'officer_positions' => $officerPositions,  // name => their latest payroll position
        'mr_officers'     => array_values(array_unique($mrOfficers)),  // saved Accountable Officers — Personnel MR search only
        'positions'       => $positions,
        'locations'       => $locations,
        'suppliers'       => $suppliers,
        'categories'      => $categories,
        'cgso_officer'    => $cgso ?: null,
        'warehouse_officer' => $warehouse ?: null,
        'cgso_staff'      => $cgsoStaff,
        'classifications' => $classifications,
        // Free text on the live column, but stable in practice — 7 distinct
        // values across the whole table — offered as a picklist that stays
        // editable, the same treatment Condition gets nowhere near a rework
        // of the column itself.
        'conditions'      => ['IN GOOD CONDITION', 'NEEDING REPAIR', 'DAMAGE', 'UNSERVICEABLE',
                               'OBSOLETE', 'NO LONGER NEEDED', 'STOLEN'],
    ]);
}

function invStats(PDO $db): array {
    $s = [];
    $s['received_today'] = (int)$db->query(
        "SELECT COUNT(*) FROM tbl_inventory WHERE isDeleted = '0' AND DATE(Date_Created) = CURDATE()"
    )->fetchColumn();
    $s['received_month'] = (int)$db->query(
        "SELECT COUNT(*) FROM tbl_inventory WHERE isDeleted = '0'
          AND Date_Created >= DATE_FORMAT(NOW(), '%Y-%m-01')"
    )->fetchColumn();
    $s['value_month'] = (float)$db->query(
        "SELECT COALESCE(SUM(d.Total_Cost), 0) FROM tbl_inventory_items_details d
         INNER JOIN tbl_inventory h ON h.PO_Number = d.PO_Number AND h.isDeleted = '0'
         WHERE d.isDeleted = '0' AND h.Date_Created >= DATE_FORMAT(NOW(), '%Y-%m-01')"
    )->fetchColumn();
    // Count each issued document once, even when it covers several item lines.
    $s['par_ics_issued'] = (int)$db->query(
        "SELECT COUNT(DISTINCT TRIM(d.PAR_ICS_Number)) FROM tbl_inventory_items_details d
         INNER JOIN tbl_inventory h ON h.PO_Number = d.PO_Number AND h.isDeleted = '0'
         WHERE d.isDeleted = '0' AND TRIM(COALESCE(d.PAR_ICS_Number, '')) <> ''
           AND " . INV_NOT_TITLE_ROW_SQL
    )->fetchColumn();
    return $s;
}

if ($method === 'GET') {
    if (isset($_GET['signatories'])) jsonResponse(true, '', invSignatories($db));
    if (isset($_GET['stats']))       jsonResponse(true, '', ['stats' => invStats($db)]);
    // A Code128 PNG for the Property Tag printout — same encoder
    // renderPoBarcodePng() already uses for the Purchase Order barcode
    // (lib/barcode128.php, loaded once in config.php). Barcodes the PAR/ICS
    // number, matching what Cells(11).Value fed fontencoder.Code128() in the
    // legacy tag button — the tag identifies which custody document covers
    // this property, not a separate property-number scheme.
    if (isset($_GET['barcode'])) {
        $value = poBarcodeValue($_GET['barcode']);
        jsonResponse(true, '', [
            'png'   => $value === '' ? '' : renderPoBarcodePng($_GET['barcode']),
            'width' => $value === '' ? 0 : poBarcodeDisplayWidth($_GET['barcode']),
        ]);
    }
    if (isset($_GET['search_po'])) {
        $term = trim($_GET['search_po']);
        jsonResponse(true, '', ['orders' => $term === '' ? [] : invSearchPO($db, $term)]);
    }
    if (isset($_GET['preview_po'])) {
        $preview = invPreviewPO($db, trim($_GET['preview_po']));
        if (!$preview) jsonResponse(false, 'Purchase order not found.');
        jsonResponse(true, '', ['preview' => $preview]);
    }
    if (isset($_GET['mr'])) {
        $officer = trim($_GET['officer'] ?? '');
        if ($officer === '') jsonResponse(false, 'Select a person first.');
        jsonResponse(true, '', invMemorandumReceipt($db, $officer));
    }

    $recordKey = trim($_GET['record_key'] ?? ($_GET['po_number'] ?? ''));
    // The record's History (the modal's History button): every Inventory log
    // line for this PO — received, revised, undone, PAR/ICS issued or renewed,
    // transfers, returns, unit breakdown. See lib/audit_history.php.
    if ($recordKey !== '' && isset($_GET['history'])) {
        $displayStmt = $db->prepare(
            "SELECT PO_Number FROM tbl_inventory WHERE PO_Number = ? ORDER BY Inv_ID DESC LIMIT 1"
        );
        $displayStmt->execute([$recordKey]);
        $displayPoNumber = $displayStmt->fetchColumn() ?: $recordKey;
        jsonResponse(true, '', ['history' => auditHistoryForPo($db, 'inventory', (string)$displayPoNumber)]);
    }
    if ($recordKey !== '') {
        $detail = invGetDetail($db, $recordKey);
        if (!$detail) jsonResponse(false, 'This purchase order has not been received into Inventory.');
        jsonResponse(true, '', $detail);
    }
    $list = invGetList($db);
    jsonResponse(true, '', $list);
}

$data = getInput();

// ── Save (create or revise) ─────────────────────────────────────────────
// POST brings a purchase order into Inventory for the first time; PUT revises
// an existing intake. Both retire the current header + item rows and insert
// new ones — the identical soft-delete-then-reinsert sequence
// save_inventory_update() runs, kept so every earlier version stays on file.
//
// Issuance fields are carried forward from the database for retained rows only.
if ($method === 'POST' || $method === 'PUT') {
    $requestedManual = (bool)($data['manual'] ?? false);
    $poNumber = trim($data['po_number'] ?? '');
    $recordKey = $method === 'PUT'
        ? trim($data['record_key'] ?? $poNumber)
        : $poNumber;
    $prNumber = trim($data['pr_number'] ?? '');
    if ($poNumber === '') jsonResponse(false, 'Purchase order number is required.');

    $items = $data['items'] ?? [];
    if (!is_array($items) || !count($items)) jsonResponse(false, 'Add at least one item before saving.');

    foreach ($items as $i => $item) {
        if (trim((string)($item['items_description'] ?? '')) === '') {
            jsonResponse(false, 'Item ' . ($i + 1) . ' has no description.');
        }
        if (isset($item['unit_cost']) && $item['unit_cost'] !== '' && !is_numeric($item['unit_cost'])) {
            jsonResponse(false, 'Item ' . ($i + 1) . ' has an invalid unit cost.');
        }
    }

    $existingManual = false;
    $hasExisting = false;
    if ($method === 'PUT') {
        if ($recordKey === '') jsonResponse(false, 'Inventory record key is required.');
        $cur = $db->prepare("SELECT Is_Manual FROM tbl_inventory WHERE PO_Number = ? AND isDeleted = '0' ORDER BY Inv_ID DESC LIMIT 1");
        $cur->execute([$recordKey]);
        $existingManualRaw = $cur->fetchColumn();
        $hasExisting = $existingManualRaw !== false;
        $existingManual = $hasExisting && (bool)$existingManualRaw;
    } else {
        $cur = $db->prepare("SELECT Is_Manual FROM tbl_inventory WHERE PO_Number = ? AND isDeleted = '0' ORDER BY Inv_ID DESC LIMIT 1");
        $cur->execute([$recordKey]);
        $existingManualRaw = $cur->fetchColumn();
        $hasExisting = $existingManualRaw !== false;
        $existingManual = $hasExisting && (bool)$existingManualRaw;
    }
    if ($method === 'POST' && $hasExisting) {
        jsonResponse(false, 'This purchase order is already in Inventory. Use Revise instead.');
    }
    if ($method === 'PUT' && !$hasExisting) {
        jsonResponse(false, 'No inventory record exists yet for this purchase order.');
    }
    $isManual = $method === 'POST' ? $requestedManual : ($requestedManual || $existingManual);
    if ($isManual && $prNumber === '') jsonResponse(false, 'PR Number is required for manual entry.');
    $displayPoNumber = $poNumber;

    $actorName = getCurrentUser()['full_name'] ?? 'Someone';
    $now       = date('Y-m-d H:i:s');
    $dateEntry = invParseDate($data['date_entry'] ?? null) ?: $now;
    $poDate    = invParseDate($data['po_date'] ?? null);

    try {
        $db->beginTransaction();

        // Keep issuance data on the same saved item row during a revision.
        // Values come from the database, scoped to this PO, never from client input.
        $preservedIssuanceByInvId = [];
        if ($hasExisting) {
            $issuedRows = $db->prepare(
                "SELECT Inv_ID, PAR_ICS_Number, PTR_ITR_Number, Date_Transfered, Fund_Cluster
                 FROM tbl_inventory_items_details
                 WHERE PO_Number = ? AND isDeleted = '0' FOR UPDATE"
            );
            $issuedRows->execute([$recordKey]);
            foreach ($issuedRows->fetchAll(PDO::FETCH_ASSOC) as $issuedRow) {
                $preservedIssuanceByInvId[(int)$issuedRow['Inv_ID']] = $issuedRow;
            }
        }

        if ($hasExisting) {
            $db->prepare("UPDATE tbl_inventory SET isDeleted = '1', Deleted_by = ? WHERE PO_Number = ? AND isDeleted = '0'")
               ->execute([$actorName, $recordKey]);
            $db->prepare("UPDATE tbl_inventory_items_details SET isDeleted = '1', Deleted_by = ? WHERE PO_Number = ? AND isDeleted = '0'")
               ->execute([$actorName, $recordKey]);
        }

        $db->prepare(
            "INSERT INTO tbl_inventory
                (PO_Number, PR_Number, Department, Category, Mode_of_Procurement, Supplier_Name,
                 Supplier_Address, Purpose, Source_of_Fund, PO_Date, Date_Entry, Date_Created,
                 End_User, Initial, isDeleted, Is_Manual)
             VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '0', ?)"
        )->execute([
            $recordKey, $prNumber, trim($data['department'] ?? ''),
            trim($data['category'] ?? ''), trim($data['mode_of_procurement'] ?? ''),
            trim($data['supplier_name'] ?? ''), trim($data['supplier_address'] ?? ''),
            trim($data['purpose'] ?? ''), trim($data['source_of_fund'] ?? ''),
            $poDate, $dateEntry, $now, $actorName, trim($data['initial'] ?? ''),
            $isManual ? 1 : 0,
        ]);

        $itemStmt = $db->prepare(
            "INSERT INTO tbl_inventory_items_details
                (PO_Number, Item_ID, Items_Description, Unit, Quantity, Unit_Cost, Total_Cost,
                 Tag_Number, Serial_Number, Inventory_Category, Accountable_Officer, Position,
                 Location, Classification, Remarks, `Condition`, NumberSeries, Estimated_Life,
                 Date_Entry, Date_Created, End_User, PAR_ICS_Number, PTR_ITR_Number,
                 Date_Transfered, Fund_Cluster, isDeleted)
             VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, '0')"
        );
        foreach ($items as $item) {
            $qty  = ($item['quantity'] ?? '') === '' ? null : (float)$item['quantity'];
            $cost = ($item['unit_cost'] ?? '') === '' ? null : (float)$item['unit_cost'];
            // Recomputed server-side, never trusted from the client, same rule
            // every other item grid in this app enforces.
            $total = ($qty !== null && $cost !== null) ? round($qty * $cost, 2) : $cost;
            // Classified server-side too — every row, not just the one the
            // operator last clicked (the VB's CellEndEdit limitation) — except
            // a title row ('SET 1', a LOT divider), which stays unclassified:
            // it was never a priced item on the purchase order either, and
            // stamping it "ICS LOW VALUE" for having a $0 cost is exactly the
            // kind of item it isn't.
            $isTitle  = invIsTitleRow($item['unit'] ?? '', $qty ?? 0, $cost ?? 0, $total ?? 0);
            $category = $isTitle ? '' : invClassify($cost ?? 0);
            $oldInvId = (int)($item['inv_id'] ?? 0);
            $savedIssuance = $preservedIssuanceByInvId[$oldInvId] ?? null;
            $itemStmt->execute([
                $recordKey,
                ($item['item_id'] ?? '') !== '' ? $item['item_id'] : null,
                trim($item['items_description'] ?? ''),
                trim($item['unit'] ?? ''),
                $qty, $cost, $total,
                trim($item['tag_number'] ?? ''), trim($item['serial_number'] ?? ''),
                $category,
                trim($item['accountable_officer'] ?? ''), trim($item['position'] ?? ''),
                trim($item['location'] ?? ''), trim($item['classification'] ?? ''),
                trim($item['remarks'] ?? ''), trim($item['condition'] ?? ''),
                ($item['number_series'] ?? '') !== '' ? $item['number_series'] : null,
                trim($item['estimated_life'] ?? ''),
                $dateEntry, $now, $actorName,
                $savedIssuance['PAR_ICS_Number'] ?? null,
                $savedIssuance['PTR_ITR_Number'] ?? null,
                $savedIssuance['Date_Transfered'] ?? null,
                $savedIssuance['Fund_Cluster'] ?? null,
            ]);
        }

        $db->commit();
    } catch (Throwable $e) {
        $db->rollBack();
        jsonResponse(false, 'Save failed: ' . $e->getMessage());
    }

    logAudit($hasExisting ? 'REVISE_INVENTORY' : 'CREATE_INVENTORY', 'inventory',
        ($hasExisting ? 'Revised' : 'Received') . " PO {$displayPoNumber} into Inventory (" . count($items) . ' items)');
    notifyPoUsers(
        $hasExisting ? 'Inventory Record Revised' : 'Received into Inventory',
        "{$actorName} " . ($hasExisting ? 're-saved' : 'recorded') . " PO {$displayPoNumber} in Inventory & Property",
        'inventory.php?record_key=' . urlencode($recordKey) . '&view=1',
        $displayPoNumber, 'all'
    );
    jsonResponse(true, $hasExisting ? 'Inventory record updated.' : 'Received into Inventory.',
        ['po_number' => $displayPoNumber, 'record_key' => $recordKey]);
}

// ── Undo ──────────────────────────────────────────────────────────────────
// Same restore-or-remove behaviour as Inspection's undo: if an earlier
// version exists it comes back; otherwise the record is taken off Inventory
// entirely and the purchase order returns to "not yet received".
if ($method === 'PATCH' && ($data['action'] ?? '') === 'undo') {
    $recordKey = trim($data['record_key'] ?? ($data['po_number'] ?? ''));
    if ($recordKey === '') jsonResponse(false, 'Purchase order number is required.');

    // Re-read fresh, in the same request that acts on it — never trust a copy
    // read earlier in the session, since someone else may be using this
    // record right now.
    $cur = $db->prepare(
        "SELECT Inv_ID, PO_Number AS display_po
         FROM tbl_inventory WHERE PO_Number = ? AND isDeleted = '0' ORDER BY Inv_ID DESC LIMIT 1"
    );
    $cur->execute([$recordKey]);
    $current = $cur->fetch(PDO::FETCH_ASSOC);
    $currentId = $current['Inv_ID'] ?? null;
    $displayPoNumber = $current['display_po'] ?? $recordKey;
    if (!$currentId) jsonResponse(false, 'No live inventory record exists for this purchase order.');

    $prev = $db->prepare(
        "SELECT Inv_ID, Date_Created FROM tbl_inventory
         WHERE PO_Number = ? AND isDeleted = '1' AND Inv_ID < ? ORDER BY Inv_ID DESC LIMIT 1"
    );
    $prev->execute([$recordKey, $currentId]);
    $previous = $prev->fetch(PDO::FETCH_ASSOC);

    try {
        $db->beginTransaction();
        $db->prepare("UPDATE tbl_inventory SET isDeleted = '1' WHERE Inv_ID = ?")->execute([$currentId]);
        $db->prepare("UPDATE tbl_inventory_items_details SET isDeleted = '1' WHERE PO_Number = ? AND isDeleted = '0'")
           ->execute([$recordKey]);

        if ($previous) {
            $db->prepare("UPDATE tbl_inventory SET isDeleted = '0' WHERE Inv_ID = ?")->execute([$previous['Inv_ID']]);
            // Items were retired by Date_Created + PO_Number, matching the
            // header version being restored — the same scoping rule
            // inspection's undo uses, and for the same reason: an item row
            // has no back-reference to which header version it belongs to
            // other than sharing that timestamp.
            $db->prepare(
                "UPDATE tbl_inventory_items_details SET isDeleted = '0'
                 WHERE PO_Number = ? AND isDeleted = '1' AND Date_Created = ?"
            )->execute([$recordKey, $previous['Date_Created']]);
        }
        $db->commit();
    } catch (Throwable $e) {
        $db->rollBack();
        jsonResponse(false, 'Undo failed: ' . $e->getMessage());
    }

    $restored = (bool)$previous;
    $actorName = getCurrentUser()['full_name'] ?? 'Someone';
    logAudit('UNDO_INVENTORY', 'inventory', ($restored
        ? "Undid the last revision on PO {$displayPoNumber}, restoring the version of {$previous['Date_Created']}"
        : "Removed PO {$displayPoNumber} from Inventory"));
    notifyPoUsers(
        $restored ? 'Inventory Revision Undone' : 'Removed from Inventory',
        $restored
            ? "{$actorName} undid the last revision on PO {$displayPoNumber} in Inventory"
            : "{$actorName} removed PO {$displayPoNumber} from Inventory — it is awaiting receipt again",
        'inventory.php?record_key=' . urlencode($recordKey) . ($restored ? '&view=1' : ''),
        $displayPoNumber, 'all'
    );
    jsonResponse(true, $restored ? 'Undone. The previous version has been restored.'
                                  : 'Undone. This purchase order is off Inventory.',
        ['restored' => $restored]);
}

// ── Sub-details (serialized breakdown) ───────────────────────────────────
// One inventory line (one Inv_ID) can cover several physically distinct
// units — five identical desktop computers under one PO line, say — each
// with its own serial number, tag and, in principle, its own custodian. This
// replaces CGSO_Inventory_Sub_Details.vb's save_subinventory() /
// update_subinventory(), same soft-delete-then-reinsert versioning.
if ($method === 'PATCH' && ($data['action'] ?? '') === 'sub_details') {
    $invId = (int)($data['inv_id'] ?? 0);
    $poNumber = trim($data['po_number'] ?? '');
    if (!$invId || $poNumber === '') jsonResponse(false, 'Item is required.');

    $rows = $data['rows'] ?? [];
    $actorName = getCurrentUser()['full_name'] ?? 'Someone';
    $now = date('Y-m-d H:i:s');

    try {
        $db->beginTransaction();
        $db->prepare("UPDATE tbl_inventory_sub_details SET isDeleted = 1 WHERE Inv_ID = ? AND isDeleted = 0")
           ->execute([$invId]);

        if (is_array($rows) && count($rows)) {
            $stmt = $db->prepare(
                "INSERT INTO tbl_inventory_sub_details
                    (Inv_ID, PO_Number, Items_Description, Unit_Cost, Serial_Number, Tag_Number,
                     Accountable_Officer, Location, Remarks, `Condition`, Date_Entry, Date_Created,
                     End_User, isDeleted)
                 VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 0)"
            );
            foreach ($rows as $r) {
                $stmt->execute([
                    $invId, $poNumber,
                    trim($r['items_description'] ?? ''),
                    ($r['unit_cost'] ?? '') !== '' ? (float)$r['unit_cost'] : null,
                    trim($r['serial_number'] ?? ''), trim($r['tag_number'] ?? ''),
                    trim($r['accountable_officer'] ?? ''), trim($r['location'] ?? ''),
                    trim($r['remarks'] ?? ''), trim($r['condition'] ?? ''),
                    $now, $now, $actorName,
                ]);
            }
        }
        $db->commit();
    } catch (Throwable $e) {
        $db->rollBack();
        jsonResponse(false, 'Save failed: ' . $e->getMessage());
    }

    $displayStmt = $db->prepare(
        "SELECT PO_Number
         FROM tbl_inventory WHERE PO_Number = ? AND isDeleted = '0'
         ORDER BY Inv_ID DESC LIMIT 1"
    );
    $displayStmt->execute([$poNumber]);
    $displayPoNumber = $displayStmt->fetchColumn() ?: $poNumber;
    logAudit('UPDATE_INVENTORY_SUB_DETAILS', 'inventory', "Updated the unit breakdown for Inv_ID {$invId} (PO {$displayPoNumber})");
    jsonResponse(true, 'Unit breakdown saved.', ['inv_id' => $invId]);
}

jsonResponse(false, 'Unsupported request.');
