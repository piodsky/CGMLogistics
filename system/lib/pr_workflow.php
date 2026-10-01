<?php
// Purchase Request — the rules, kept in one place.
//
// Everything that decides what a PR may do lives here and nowhere else: the
// status ladder, who may move it, what has to be filled in first, how the
// number is built, how item rows are validated and saved. The API files only
// route requests to it and the page only reflects the answers, so the badge, the
// buttons and the server can never disagree about what state a PR is in.
//
// Functions THROW InvalidArgumentException for a request that cannot be carried
// out (the message is written to be shown to the person) and let anything else
// bubble up; the API turns the first into a plain message and the second into a
// generic failure. That also makes every function testable without a web request.
//
// Table names are a parameter (PR_TABLES by default) so the write logic can be
// exercised against session-local TEMPORARY tables — the live database is real
// procurement data and never a place to rehearse.
require_once __DIR__ . '/procurement_items.php';

// 'catalog' => true feeds new item descriptions into the shared item catalogue
// (tbl_ppmp_bac_items) exactly as a Purchase Order does. Tests pass false, so a
// rehearsal never writes to that live table.
const PR_TABLES = ['pr' => 'tbl_pr', 'items' => 'tbl_pr_items', 'log' => 'tbl_pr_status_log', 'catalog' => true];

// The approval lifecycle. NOT the procurement pipeline: where a PR is in RFQ /
// Abstract / PO is derived later from those phases' own tables, so there is one
// source of truth for it and it cannot disagree with them.
const PR_STATUSES = ['Draft', 'Submitted', 'Returned', 'Approved', 'Cancelled'];

// Only these can have their header or items changed. Approved and Submitted are
// closed: changing a request after it has been approved would leave the paper in
// someone's hand disagreeing with the record.
const PR_EDITABLE_STATUSES = ['Draft', 'Returned'];

// action => allowed source statuses, target, whether a reason is mandatory, and
// who may do it. 'user' = anyone with the Purchase Request flag; 'approver' =
// a role with can_approve. 'cancel' is split by source: a requester can withdraw
// their own draft, but once it has been submitted or approved only an approver
// can kill it.
const PR_ACTIONS = [
    'submit'  => ['from' => ['Draft', 'Returned'],                       'to' => 'Submitted', 'reason' => false, 'who' => ['Draft' => 'user',     'Returned' => 'user']],
    'approve' => ['from' => ['Submitted'],                               'to' => 'Approved',  'reason' => false, 'who' => ['Submitted' => 'approver']],
    'return'  => ['from' => ['Submitted'],                               'to' => 'Returned',  'reason' => true,  'who' => ['Submitted' => 'approver']],
    'reopen'  => ['from' => ['Approved'],                                'to' => 'Draft',     'reason' => true,  'who' => ['Approved' => 'approver']],
    'cancel'  => ['from' => ['Draft', 'Returned', 'Submitted', 'Approved'], 'to' => 'Cancelled', 'reason' => true,
                  'who' => ['Draft' => 'user', 'Returned' => 'user', 'Submitted' => 'approver', 'Approved' => 'approver']],
];

const PR_MAX_ITEMS = 1000;

// request key => [column, max length | 'date']. The one list the validator, the
// INSERT and the UPDATE all read, so adding a field to the form is a line here
// and a column in database/pr_phase1.sql, nothing else.
const PR_HEADER_FIELDS = [
    'pr_date'                    => ['PR_Date',                   'date'],
    'fund_cluster'               => ['Fund_Cluster',              100],
    'category'                   => ['Category',                  255],
    'department'                 => ['Department',                255],
    'division'                   => ['Division',                  255],
    'section'                    => ['Section',                   255],
    'responsibility_center_code' => ['Responsibility_Center_Code', 50],
    'purpose'                    => ['Purpose',                   20000],
    'mode_of_procurement'        => ['Mode_of_Procurement',       100],
    'requested_by'               => ['Requested_By',              255],
    'requested_by_position'      => ['Requested_By_Position',     255],
    'funds_certified_by'         => ['Funds_Certified_By',        255],
    'funds_certified_position'   => ['Funds_Certified_Position',  255],
    'approved_by'                => ['Approved_By',               255],
    'approved_by_position'       => ['Approved_By_Position',      255],
    'remarks'                    => ['Remarks',                   20000],
];

// Words for the messages, so a failed field is named the way the form names it.
const PR_FIELD_LABELS = [
    'pr_date' => 'PR Date', 'fund_cluster' => 'Fund Cluster', 'category' => 'Category', 'department' => 'Department',
    'division' => 'Division', 'section' => 'Section', 'responsibility_center_code' => 'Responsibility Center Code',
    'purpose' => 'Purpose', 'mode_of_procurement' => 'Mode of Procurement', 'requested_by' => 'Requested by',
    'requested_by_position' => 'Requested by (designation)', 'funds_certified_by' => 'Funds certified by',
    'funds_certified_position' => 'Funds certified by (designation)', 'approved_by' => 'Approved by',
    'approved_by_position' => 'Approved by (designation)', 'remarks' => 'Remarks',
];

// ── Numbers ─────────────────────────────────────────────────────────────

// PR-sys<Y><n><id>: the same shape the PO uses (PO-sys202699999999), with the
// row's own AUTO_INCREMENT id, so it is unique by construction and two people
// saving in the same second cannot be handed the same one. Month is NOT
// zero-padded, matching every PO number already in the system.
function prFormatNumber(int $id, ?int $ts = null): string {
    $ts = $ts ?? time();
    return sprintf('PR-sys%s%s%05d', date('Y', $ts), date('n', $ts), $id);
}

// The scannable Code 128 value printed on the form. Same construction as the
// PO's poNumberBarcode() — a numeric string built from the row id and creation
// date rather than parsed out of the printed number's text — with its own
// two-digit prefix (98, where the PO's is 99) so a scan says which kind of
// document it came from and can never collide with a PO barcode.
function prBarcodeValue($id, $dateCreated): string {
    $id = (int)$id;
    $ts = $dateCreated ? strtotime((string)$dateCreated) : false;
    if ($id <= 0 || $ts === false) return '';
    return '98' . date('y', $ts) . date('m', $ts) . str_pad((string)$id, 5, '0', STR_PAD_LEFT);
}

// ── Permissions ─────────────────────────────────────────────────────────

const PR_PERMISSION_COLUMNS = ['can_approve', 'can_delete'];

// Same rule as poActionPermission(): a Super Administrator (role_id 1) always
// passes, so a bad row can never lock the system's own administrators out; a
// role with NO row in tbl_pr_permissions falls back to Super Administrator /
// Administrator / Procurement Administrator (1/2/3); a role with a row gets
// exactly what the row says. Fails closed to the default if the table is not
// there yet.
function prActionPermission(int $roleId, string $action, ?PDO $db = null): bool {
    static $cache = [];
    if ($roleId === 1) return true;
    if (!in_array($action, PR_PERMISSION_COLUMNS, true)) return false;
    $key = $roleId . '|' . $action;
    if ($db === null && array_key_exists($key, $cache)) return $cache[$key];
    $default = in_array($roleId, [1, 2, 3], true);
    try {
        $stmt = ($db ?? getPoDB())->prepare("SELECT `{$action}` FROM tbl_pr_permissions WHERE role_id = ?");
        $stmt->execute([$roleId]);
        $val = $stmt->fetchColumn();
        $answer = ($val === false) ? $default : ((int)$val === 1);
    } catch (Throwable $e) {
        $answer = $default;
    }
    if ($db === null) $cache[$key] = $answer;
    return $answer;
}

// ── The status ladder ───────────────────────────────────────────────────

function prIsEditable(string $status): bool { return in_array($status, PR_EDITABLE_STATUSES, true); }

// The actions this person could take right now, for the page to draw buttons
// from. The server re-checks every one of them on the way in.
function prAvailableActions(string $status, bool $isApprover): array {
    $out = [];
    foreach (PR_ACTIONS as $action => $meta) {
        if (!in_array($status, $meta['from'], true)) continue;
        $who = $meta['who'][$status] ?? 'approver';
        if ($who === 'approver' && !$isApprover) continue;
        $out[] = $action;
    }
    return $out;
}

// Returns the status the PR moves to, or throws. $reason is the person's own
// words; a mandatory one must have some.
function prCheckTransition(string $action, string $status, bool $isApprover, string $reason): string {
    if (!isset(PR_ACTIONS[$action])) throw new InvalidArgumentException('Unknown action.');
    $meta = PR_ACTIONS[$action];
    if (!in_array($status, $meta['from'], true)) {
        throw new InvalidArgumentException("A {$status} purchase request cannot be " . prActionWord($action) . '.');
    }
    if (($meta['who'][$status] ?? 'approver') === 'approver' && !$isApprover) {
        throw new InvalidArgumentException('Only an approver can ' . $action . ' a ' . strtolower($status) . ' purchase request.');
    }
    if ($meta['reason'] && mb_strlen(trim($reason)) < 5) {
        throw new InvalidArgumentException('Please give a reason (at least a few words).');
    }
    return $meta['to'];
}

function prActionWord(string $action): string {
    return ['submit' => 'submitted', 'approve' => 'approved', 'return' => 'returned', 'reopen' => 'reopened', 'cancel' => 'cancelled'][$action] ?? $action;
}

// What must be true before a PR leaves the requester. Returned as a list so the
// person sees everything that is missing at once rather than one error per try.
function prSubmitProblems(array $pr, array $items): array {
    $bad = [];
    if (trim((string)($pr['PR_Number'] ?? '')) === '')          $bad[] = 'it has no PR number';
    if (mb_strlen(trim((string)($pr['Purpose'] ?? ''))) < 5)    $bad[] = 'the Purpose is missing';
    if (trim((string)($pr['Department'] ?? '')) === '')         $bad[] = 'the Requesting Office is missing';
    if (trim((string)($pr['Requested_By'] ?? '')) === '')       $bad[] = 'Requested by is missing';
    $real = array_filter($items, fn($i) => !(int)$i['Is_Title']);
    if (!$real)                                                 $bad[] = 'it has no items';
    $total = 0.0;
    foreach ($real as $i) $total += (float)$i['Estimated_Total_Cost'];
    if ($real && $total <= 0)                                   $bad[] = 'the estimated total is zero';
    return $bad;
}

// ── Validation ──────────────────────────────────────────────────────────

// Header values, trimmed and length-checked, as column => value. Required at
// every save, Draft included, because the columns are NOT NULL: a PR with no
// office or purpose cannot exist even as a draft.
function prCleanHeader(array $data): array {
    $out = [];
    foreach (PR_HEADER_FIELDS as $key => [$col, $limit]) {
        $raw = trim((string)($data[$key] ?? ''));
        if ($limit === 'date') {
            $ts = $raw === '' ? false : strtotime($raw);
            if ($ts === false) throw new InvalidArgumentException('PR Date is required.');
            if ($ts > strtotime('tomorrow 23:59:59')) throw new InvalidArgumentException('PR Date cannot be in the future.');
            $out[$col] = date('Y-m-d', $ts);
            continue;
        }
        if (mb_strlen($raw) > $limit) {
            throw new InvalidArgumentException(PR_FIELD_LABELS[$key] . " is too long (at most {$limit} characters).");
        }
        $out[$col] = $raw === '' ? null : $raw;
    }
    if ($out['Department'] === null) throw new InvalidArgumentException('Requesting Office (Department) is required.');
    if ($out['Purpose'] === null)    throw new InvalidArgumentException('Purpose is required.');
    return $out;
}

// A number as a person types or pastes it. null = blank, false = not a number.
// "10,650,000.00" and "₱1,200" both work, so a pasted figure does not quietly
// become 10.
function prNum($v) {
    if ($v === null) return null;
    $s = trim((string)$v);
    if ($s === '') return null;
    $clean = preg_replace('/[^0-9.\-]/', '', $s);
    if ($clean === '' || !is_numeric($clean)) return false;
    return (float)$clean;
}

function prRowList(array $rows): string {
    $shown = array_slice($rows, 0, 6);
    return (count($rows) === 1 ? 'Row ' : 'Rows ') . '#' . implode(', #', $shown)
         . (count($rows) > 6 ? ' and ' . (count($rows) - 6) . ' more' : '');
}

// Item rows in, clean rows out — or a thrown message naming every bad row.
//
// The two shapes the whole system agrees on: a TITLE row (a LOT or heading — a
// description and nothing else) and an ITEM row (priced). Total Cost is the test
// for an item, not Unit + Qty + Unit Cost, because a lump-sum line honestly has
// a total with no quantity behind it.
//
// Two departures from the PO on purpose. The total is COMPUTED here whenever
// there is both a quantity and a unit cost — the browser's figure is not
// trusted, so a stale or hand-edited total cannot put a wrong number on an
// approved request. And an item row carries the PR_Item_ID it had when loaded,
// so an edit updates it in place (see prSaveItems).
function prNormalizeItems(array $items): array {
    if (count($items) > PR_MAX_ITEMS) {
        throw new InvalidArgumentException('A purchase request can have at most ' . PR_MAX_ITEMS . ' item rows.');
    }
    $rows = [];
    $bad = ['number' => [], 'noTotal' => [], 'title' => [], 'negative' => [], 'big' => []];
    foreach ($items as $i => $item) {
        $n = $i + 1;
        $desc = trim((string)($item['items_description'] ?? ''));
        if ($desc === '') continue;                                   // a blank row is not an error, it is nothing

        $unit = trim((string)($item['unit'] ?? ''));
        $qty  = prNum($item['quantity'] ?? null);
        $cost = prNum($item['unit_cost'] ?? null);
        $tot  = prNum($item['total_cost'] ?? null);
        if ($qty === false || $cost === false || $tot === false) { $bad['number'][] = $n; continue; }
        if (($qty !== null && $qty < 0) || ($cost !== null && $cost < 0) || ($tot !== null && $tot < 0)) { $bad['negative'][] = $n; continue; }

        $blank = $unit === '' && !$qty && !$cost && !$tot;
        $isTitle = array_key_exists('is_title', $item) ? (bool)$item['is_title'] : $blank;
        if ($isTitle) {
            if (!$blank) { $bad['title'][] = $n; continue; }
            $qty = $cost = $tot = null;
            $unit = '';
        } else {
            if ($qty && $cost) $tot = round($qty * $cost, 2);           // computed, never trusted
            if (!$tot)         { $bad['noTotal'][] = $n; continue; }
        }
        if (($tot !== null && $tot >= 1e13) || ($qty !== null && $qty >= 1e9) || ($cost !== null && $cost >= 1e13)) { $bad['big'][] = $n; continue; }
        if (mb_strlen($desc) > 60000 || mb_strlen($unit) > 50 || mb_strlen(trim((string)($item['stock_no'] ?? ''))) > 50
            || mb_strlen(trim((string)($item['remarks'] ?? ''))) > 255) { $bad['big'][] = $n; continue; }

        $rows[] = [
            'pr_item_id'        => isset($item['pr_item_id']) && (int)$item['pr_item_id'] > 0 ? (int)$item['pr_item_id'] : null,
            'is_title'          => $isTitle,
            'stock_no'          => trim((string)($item['stock_no'] ?? '')),
            'unit'              => $unit,
            'items_description' => $desc,
            'quantity'          => $qty,
            'unit_cost'         => $cost,
            'total_cost'        => $tot === null ? null : round($tot, 2),
            'remarks'           => trim((string)($item['remarks'] ?? '')),
        ];
    }
    $say = [];
    if ($bad['number'])   $say[] = prRowList($bad['number'])   . ' has a Qty, Unit Cost or Total that is not a number.';
    if ($bad['negative']) $say[] = prRowList($bad['negative']) . ' has a negative number.';
    if ($bad['noTotal'])  $say[] = prRowList($bad['noTotal'])  . ' has no Total Cost. Fill in Qty and Unit Cost, enter a Total Cost directly, or make it a LOT / title row.';
    if ($bad['title'])    $say[] = prRowList($bad['title'])    . ' is a LOT / title row but still carries a unit or a cost.';
    if ($bad['big'])      $say[] = prRowList($bad['big'])      . ' has a value that is too large or too long to store.';
    if ($say) throw new InvalidArgumentException(implode(' ', $say));
    return $rows;
}

// Sum of the priced rows. Titles carry nothing.
function prTotal(array $rows): float {
    $t = 0.0;
    foreach ($rows as $r) if (!$r['is_title'] && $r['total_cost'] !== null) $t += $r['total_cost'];
    return round($t, 2);
}

// ── Reading ─────────────────────────────────────────────────────────────

// One PR with its items and history, or null. By id, or by PR number.
function prLoad(PDO $db, $idOrNumber, array $T = PR_TABLES, bool $forUpdate = false): ?array {
    $col = is_int($idOrNumber) ? 'PR_ID' : 'PR_Number';
    $stmt = $db->prepare("SELECT * FROM {$T['pr']} WHERE {$col} = ? AND isDeleted = 0" . ($forUpdate ? ' FOR UPDATE' : ''));
    $stmt->execute([$idOrNumber]);
    $pr = $stmt->fetch(PDO::FETCH_ASSOC);
    if (!$pr) return null;
    $stmt = $db->prepare("SELECT * FROM {$T['items']} WHERE PR_ID = ? AND isDeleted = 0 ORDER BY Line_No, PR_Item_ID");
    $stmt->execute([$pr['PR_ID']]);
    $pr['items'] = $stmt->fetchAll(PDO::FETCH_ASSOC);
    $stmt = $db->prepare("SELECT Action, From_Status, To_Status, Reason, Actor_Name, Date_Created FROM {$T['log']} WHERE PR_ID = ? ORDER BY Log_ID");
    $stmt->execute([$pr['PR_ID']]);
    $pr['history'] = $stmt->fetchAll(PDO::FETCH_ASSOC);
    return $pr;
}

// ── Writing ─────────────────────────────────────────────────────────────

function prLog(PDO $db, int $prId, string $action, ?string $from, string $to, ?string $reason, ?int $actorId, string $actor, array $T): void {
    $db->prepare("INSERT INTO {$T['log']} (PR_ID, Action, From_Status, To_Status, Reason, Actor_ID, Actor_Name, Date_Created)
                  VALUES (?, ?, ?, ?, ?, ?, ?, NOW())")
       ->execute([$prId, $action, $from, $to, $reason === null || $reason === '' ? null : mb_substr($reason, 0, 500), $actorId, mb_substr($actor, 0, 150)]);
}

// Reconciles the posted rows against the stored ones BY ID.
//
//   posted row with a PR_Item_ID that is live on this PR   -> updated in place
//   posted row without one (or with a stranger's / stale)  -> inserted
//   stored live row that was not posted                     -> soft-deleted
//
// This — and not the PO's "soft-delete everything and re-insert" — is what keeps
// PR_Item_ID stable across edits, which the RFQ, quotation, abstract and PO
// phases depend on when they point at an item. A row is refused deletion once
// anything downstream references it; nothing does yet, so prItemIsReferenced()
// is the one place Phase 2+ adds that check.
//
// Returns a one-line summary for the audit log.
function prSaveItems(PDO $db, int $prId, array $rows, array $T = PR_TABLES): string {
    $stmt = $db->prepare("SELECT PR_Item_ID FROM {$T['items']} WHERE PR_ID = ? AND isDeleted = 0");
    $stmt->execute([$prId]);
    $live = array_flip(array_map('intval', $stmt->fetchAll(PDO::FETCH_COLUMN)));

    $upd = $db->prepare("UPDATE {$T['items']} SET Line_No = ?, Item_ID = ?, Stock_No = ?, Is_Title = ?, Unit = ?, Items_Description = ?,
                         Quantity = ?, Estimated_Unit_Cost = ?, Estimated_Total_Cost = ?, Remarks = ?, Date_Updated = NOW()
                         WHERE PR_Item_ID = ? AND PR_ID = ?");
    $ins = $db->prepare("INSERT INTO {$T['items']} (PR_ID, Line_No, Item_ID, Stock_No, Is_Title, Unit, Items_Description,
                         Quantity, Estimated_Unit_Cost, Estimated_Total_Cost, Remarks, Item_Status, isDeleted, Date_Created)
                         VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, 'Open', 0, NOW())");
    $kept = [];
    $added = $changed = 0;
    foreach ($rows as $i => $r) {
        $line = $i + 1;
        $catalog = ($r['is_title'] || empty($T['catalog'])) ? null : ensureCatalogItem($db, [
            'items_description' => $r['items_description'], 'unit' => $r['unit'], 'unit_cost' => $r['unit_cost'] ?? 0,
        ]);
        $vals = [$catalog, $r['stock_no'] === '' ? null : $r['stock_no'], $r['is_title'] ? 1 : 0,
                 $r['unit'] === '' ? null : $r['unit'], $r['items_description'],
                 $r['quantity'], $r['unit_cost'], $r['total_cost'], $r['remarks'] === '' ? null : $r['remarks']];
        if ($r['pr_item_id'] !== null && isset($live[$r['pr_item_id']]) && !isset($kept[$r['pr_item_id']])) {
            $upd->execute(array_merge([$line], $vals, [$r['pr_item_id'], $prId]));
            $kept[$r['pr_item_id']] = true;
            $changed++;
        } else {
            $ins->execute(array_merge([$prId, $line], $vals));
            $kept[(int)$db->lastInsertId()] = true;
            $added++;
        }
    }
    $removed = 0;
    $del = $db->prepare("UPDATE {$T['items']} SET isDeleted = 1, Date_Updated = NOW() WHERE PR_Item_ID = ? AND PR_ID = ?");
    foreach (array_keys($live) as $id) {
        if (isset($kept[$id])) continue;
        if (prItemIsReferenced($db, $id)) {
            throw new InvalidArgumentException('An item on this request is already used by a later step and cannot be removed.');
        }
        $del->execute([$id, $prId]);
        $removed++;
    }
    return "{$added} added, {$changed} kept, {$removed} removed";
}

// Phase 2+ answers this: true once an RFQ line, quotation line, abstract line or
// PO line points at the item. Nothing does yet.
function prItemIsReferenced(PDO $db, int $prItemId): bool { return false; }

// Creates a Draft and its items in one transaction, and issues the number.
// Returns ['id' => .., 'number' => ..].
function prCreate(PDO $db, array $header, array $rows, string $actor, ?int $actorId, array $T = PR_TABLES): array {
    if (!$rows) throw new InvalidArgumentException('At least one item is required.');
    $cols = array_keys($header);
    try {
        $db->beginTransaction();
        // The row must exist before the database will say what its id is, and
        // the number is built from that id. The placeholder is replaced a few
        // lines down inside this same transaction, so nobody ever sees it, and
        // a rollback removes the whole row rather than leaving a burnt number.
        $sql = "INSERT INTO {$T['pr']} (PR_Number, " . implode(', ', $cols) . ", Total_Estimated_Cost, Status, Row_Version, Created_By, Date_Created, isDeleted)
                VALUES (?, " . rtrim(str_repeat('?, ', count($cols)), ', ') . ", ?, 'Draft', 1, ?, NOW(), 0)";
        $db->prepare($sql)->execute(array_merge(['~NEW-' . bin2hex(random_bytes(6))], array_values($header), [prTotal($rows), $actor]));
        $id = (int)$db->lastInsertId();
        $number = prFormatNumber($id);
        $db->prepare("UPDATE {$T['pr']} SET PR_Number = ? WHERE PR_ID = ?")->execute([$number, $id]);
        prSaveItems($db, $id, $rows, $T);
        prLog($db, $id, 'create', null, 'Draft', null, $actorId, $actor, $T);
        $db->commit();
    } catch (Throwable $e) {
        if ($db->inTransaction()) $db->rollBack();
        throw $e;
    }
    return ['id' => $id, 'number' => $number];
}

// Saves an edit. Refused unless the PR is Draft or Returned, and unless the
// version the person opened is still the current one. Returns the audit summary.
function prUpdate(PDO $db, int $id, int $version, array $header, array $rows, string $actor, array $T = PR_TABLES): string {
    if (!$rows) throw new InvalidArgumentException('At least one item is required.');
    try {
        $db->beginTransaction();
        $cur = prLoad($db, $id, $T, true);
        if (!$cur) throw new InvalidArgumentException('Purchase request not found.');
        if (!prIsEditable($cur['Status'])) {
            throw new InvalidArgumentException("A {$cur['Status']} purchase request cannot be edited.");
        }
        if ((int)$cur['Row_Version'] !== $version) {
            throw new InvalidArgumentException('Someone else saved this purchase request after you opened it. Close it and open it again to see their changes.');
        }
        $set = implode(', ', array_map(fn($c) => "{$c} = ?", array_keys($header)));
        $db->prepare("UPDATE {$T['pr']} SET {$set}, Total_Estimated_Cost = ?, Row_Version = Row_Version + 1, Updated_By = ?, Date_Updated = NOW() WHERE PR_ID = ?")
           ->execute(array_merge(array_values($header), [prTotal($rows), $actor, $id]));
        $summary = prSaveItems($db, $id, $rows, $T);
        $db->commit();
    } catch (Throwable $e) {
        if ($db->inTransaction()) $db->rollBack();
        throw $e;
    }
    return $summary;
}

// Moves a PR along the ladder. The row is read FOR UPDATE inside the
// transaction, so two people pressing Approve at once cannot both succeed.
// Returns ['from' => .., 'to' => ..].
function prTransition(PDO $db, int $id, string $action, string $reason, bool $isApprover, ?int $actorId, string $actor, array $T = PR_TABLES): array {
    try {
        $db->beginTransaction();
        $pr = prLoad($db, $id, $T, true);
        if (!$pr) throw new InvalidArgumentException('Purchase request not found.');
        $from = $pr['Status'];
        $to = prCheckTransition($action, $from, $isApprover, $reason);
        if ($action === 'submit') {
            $problems = prSubmitProblems($pr, $pr['items']);
            if ($problems) throw new InvalidArgumentException('It cannot be submitted yet: ' . implode('; ', $problems) . '.');
        }
        // A cancel or reopen must not strand anything that later steps already
        // built on it. Nothing exists yet; the hook is here so it cannot be forgotten.
        if (in_array($action, ['cancel', 'reopen'], true)) {
            foreach ($pr['items'] as $item) {
                if (prItemIsReferenced($db, (int)$item['PR_Item_ID'])) {
                    throw new InvalidArgumentException('This purchase request is already in use by a later step and cannot be ' . prActionWord($action) . '.');
                }
            }
        }
        $db->prepare("UPDATE {$T['pr']} SET Status = ?, Row_Version = Row_Version + 1, Updated_By = ?, Date_Updated = NOW() WHERE PR_ID = ?")
           ->execute([$to, $actor, $id]);
        prLog($db, $id, $action, $from, $to, trim($reason), $actorId, $actor, $T);
        $db->commit();
    } catch (Throwable $e) {
        if ($db->inTransaction()) $db->rollBack();
        throw $e;
    }
    return ['from' => $from, 'to' => $to];
}

// Deleting is only for a PR that never left its author: still a Draft and never
// submitted. Anything that has been through the process is cancelled instead, so
// its number and its trail stay. Soft — the row stays, and its number is freed.
function prSoftDelete(PDO $db, int $id, string $actor, array $T = PR_TABLES): string {
    try {
        $db->beginTransaction();
        $pr = prLoad($db, $id, $T, true);
        if (!$pr) throw new InvalidArgumentException('Purchase request not found.');
        if ($pr['Status'] !== 'Draft') throw new InvalidArgumentException('Only a draft can be deleted. Cancel this one instead so its number and history are kept.');
        foreach ($pr['history'] as $h) {
            if ($h['Action'] !== 'create') throw new InvalidArgumentException('This request has already been through the approval process. Cancel it instead so its history is kept.');
        }
        $db->prepare("UPDATE {$T['pr']} SET isDeleted = 1, Deleted_By = ?, Date_Deleted = NOW() WHERE PR_ID = ?")->execute([$actor, $id]);
        $db->prepare("UPDATE {$T['items']} SET isDeleted = 1, Date_Updated = NOW() WHERE PR_ID = ?")->execute([$id]);
        $db->commit();
    } catch (Throwable $e) {
        if ($db->inTransaction()) $db->rollBack();
        throw $e;
    }
    return $pr['PR_Number'];
}
