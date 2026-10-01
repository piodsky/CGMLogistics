<?php
// Purchase Request API — Phase 1 of PR -> RFQ / Canvass -> Abstract -> PO.
//
// A thin router: the rules (status ladder, validation, numbering, the item save
// that keeps PR_Item_ID stable) are in lib/pr_workflow.php. Moving a PR through
// its statuses is api/purchase_request_status.php.
//
// Keyed on PR_ID internally; the PR number is what people see and what deep
// links use (?pr_number=). Never touches the legacy pr / items_pr tables.
require_once '../config.php';
require_once '../lib/pr_workflow.php';
requireAuth();
// The sidebar link hides the page; this is what actually blocks a direct hit on
// the API. Both are required — see the checklist in CLAUDE.md.
requireModule('purchase_requests');
header('Content-Type: application/json');
$db = getPoDB();
$method = $_SERVER['REQUEST_METHOD'];
$me = getCurrentUser();
$actor = $me['username'] ?? 'unknown';
$roleId = hrmisRoleId($me['id'] ?? 0);
$isApprover = prActionPermission($roleId, 'can_approve');
$canDelete  = prActionPermission($roleId, 'can_delete');

// A database failure as a JSON answer, not an uncaught 500 with an HTML body the
// page then fails to parse. Worth it for one case above all: until
// database/pr_phase1.sql has been run, EVERY query here raises 42S02.
function prDbFailure(Throwable $e, string $fallback): void {
    error_log('purchase_requests: ' . $e->getMessage());
    $code = ($e instanceof PDOException) ? $e->getCode() : '';
    if ($code === '42S02') {
        jsonResponse(false, 'The Purchase Request tables have not been created yet. '
            . 'Run database/pr_phase1.sql against the HRMIS server, then reload this page.');
    }
    jsonResponse(false, $fallback);
}

// DB column names -> what the page reads. Explicit, like the PO API: the page
// works in snake_case and the table in PascalCase.
function prPublicHeader(array $r): array {
    return [
        'id' => (int)$r['PR_ID'], 'pr_number' => $r['PR_Number'], 'pr_date' => $r['PR_Date'],
        'fund_cluster' => $r['Fund_Cluster'], 'category' => $r['Category'],
        'department' => $r['Department'], 'division' => $r['Division'], 'section' => $r['Section'],
        'responsibility_center_code' => $r['Responsibility_Center_Code'],
        'purpose' => $r['Purpose'], 'mode_of_procurement' => $r['Mode_of_Procurement'],
        'requested_by' => $r['Requested_By'], 'requested_by_position' => $r['Requested_By_Position'],
        'funds_certified_by' => $r['Funds_Certified_By'], 'funds_certified_position' => $r['Funds_Certified_Position'],
        'approved_by' => $r['Approved_By'], 'approved_by_position' => $r['Approved_By_Position'],
        'total_estimated_cost' => $r['Total_Estimated_Cost'], 'status' => $r['Status'], 'remarks' => $r['Remarks'],
        'row_version' => (int)$r['Row_Version'], 'created_by' => $r['Created_By'], 'date_created' => $r['Date_Created'],
    ];
}

function prPublicItem(array $r): array {
    return [
        'pr_item_id' => (int)$r['PR_Item_ID'], 'line_no' => (int)$r['Line_No'], 'stock_no' => $r['Stock_No'],
        'is_title' => (bool)$r['Is_Title'], 'unit' => $r['Unit'], 'items_description' => $r['Items_Description'],
        'quantity' => $r['Quantity'], 'unit_cost' => $r['Estimated_Unit_Cost'], 'total_cost' => $r['Estimated_Total_Cost'],
        'remarks' => $r['Remarks'], 'item_status' => $r['Item_Status'],
    ];
}

// The department / division / section the person typed, snapped to the master
// list's own spelling when it matches — the same canonicalising the PO does, so
// "City Engineers Office" and "CITY ENGINEER OFFICE" do not become two offices.
function prCanonicalHeader(PDO $db, array $data): array {
    foreach (['department' => 'canonicalDepartment', 'division' => 'canonicalDivision', 'section' => 'canonicalSection'] as $key => $fn) {
        $data[$key] = $fn($db, $data[$key] ?? '');
    }
    return $data;
}

function prNotifyLink(string $number): string { return 'purchase_requests.php?pr_number=' . urlencode($number); }

if ($method === 'GET') {
    try {
        $idParam = isset($_GET['id']) ? (int)$_GET['id'] : 0;
        $numberParam = trim((string)($_GET['pr_number'] ?? ''));
        if ($idParam || $numberParam !== '') {
            $pr = prLoad($db, $idParam ?: $numberParam);
            if (!$pr) jsonResponse(false, 'Purchase request not found.');
            $out = prPublicHeader($pr);
            $out['items']   = array_map('prPublicItem', $pr['items']);
            $out['history'] = $pr['history'];
            $out['actions'] = prAvailableActions($pr['Status'], $isApprover);
            $out['can_edit']   = prIsEditable($pr['Status']);
            $out['can_delete'] = $canDelete && $pr['Status'] === 'Draft' && count($pr['history']) <= 1;
            // Code 128 for the printed form, built the way the PO's is (see
            // prBarcodeValue()); the caption is exactly what a scanner returns.
            $barcode = prBarcodeValue($pr['PR_ID'], $pr['Date_Created']);
            $out['barcode_png']   = $barcode !== '' ? renderPoBarcodePng($barcode) : '';
            $out['barcode_text']  = $barcode;
            $out['barcode_width'] = $barcode !== '' ? poBarcodeDisplayWidth($barcode) : 0;
            jsonResponse(true, '', ['pr' => fixLegacyEncodingDeep($out)]);
        }

        // List
        $search = trim((string)($_GET['search'] ?? ''));
        $from = $_GET['from'] ?? '';
        $to   = $_GET['to'] ?? '';
        $sql = "SELECT p.PR_ID, p.PR_Number, p.PR_Date, p.Department, p.Division, p.Section, p.Purpose, p.Requested_By,
                       p.Total_Estimated_Cost, p.Status, p.Date_Created,
                       (SELECT COUNT(*) FROM tbl_pr_items i WHERE i.PR_ID = p.PR_ID AND i.isDeleted = 0 AND i.Is_Title = 0) AS item_count
                FROM tbl_pr p WHERE p.isDeleted = 0";
        $params = [];
        if ($from) { $sql .= ' AND p.PR_Date >= ?'; $params[] = date('Y-m-d', strtotime($from)); }
        if ($to)   { $sql .= ' AND p.PR_Date <= ?'; $params[] = date('Y-m-d', strtotime($to)); }
        if ($search !== '') {
            $sql .= ' AND (p.PR_Number LIKE ? OR p.Department LIKE ? OR p.Purpose LIKE ? OR p.Requested_By LIKE ?)';
            $params = array_merge($params, array_fill(0, 4, '%' . $search . '%'));
        }
        $sql .= ' ORDER BY p.PR_ID DESC';
        $stmt = $db->prepare($sql);
        $stmt->execute($params);
        $rows = fixLegacyEncodingDeep($stmt->fetchAll(PDO::FETCH_ASSOC));

        // Counted in PHP off the rows already fetched, so the chips and the
        // table cannot disagree.
        $counts = ['total' => count($rows)];
        foreach (PR_STATUSES as $s) $counts[$s] = 0;
        foreach ($rows as $r) $counts[$r['Status']] = ($counts[$r['Status']] ?? 0) + 1;

        $status = trim((string)($_GET['status'] ?? ''));
        if (in_array($status, PR_STATUSES, true)) $rows = array_values(array_filter($rows, fn($r) => $r['Status'] === $status));

        $list = array_map(fn($r) => [
            'id' => (int)$r['PR_ID'], 'pr_number' => $r['PR_Number'], 'pr_date' => $r['PR_Date'],
            'department' => $r['Department'], 'division' => $r['Division'], 'section' => $r['Section'],
            'purpose' => $r['Purpose'], 'requested_by' => $r['Requested_By'],
            'total' => (float)$r['Total_Estimated_Cost'], 'status' => $r['Status'], 'item_count' => (int)$r['item_count'],
        ], $rows);
        jsonResponse(true, '', ['prs' => $list, 'counts' => $counts, 'is_approver' => $isApprover, 'can_delete' => $canDelete]);
    } catch (Throwable $e) {
        prDbFailure($e, 'Unable to load purchase requests.');
    }
}

$data = getInput();

if ($method === 'POST') {
    try {
        $header = prCleanHeader(prCanonicalHeader($db, $data));
        $rows = prNormalizeItems(is_array($data['items'] ?? null) ? $data['items'] : []);
        $r = prCreate($db, $header, $rows, $actor, (int)($me['id'] ?? 0) ?: null);
    } catch (InvalidArgumentException $e) {
        jsonResponse(false, $e->getMessage());
    } catch (Throwable $e) {
        prDbFailure($e, 'Unable to save the purchase request.');
    }
    logAudit('CREATE_PR', 'purchase_requests', "Created {$r['number']} ({$header['Department']}, " . count($rows) . ' rows, ' . number_format(prTotal($rows), 2) . ')');
    notifyPoUsers('Purchase Request Created',
        ($me['full_name'] ?? 'Someone') . " created {$r['number']} — " . mb_substr($header['Purpose'], 0, 80),
        prNotifyLink($r['number']), null, 'all');
    jsonResponse(true, "Saved as a draft: {$r['number']}", ['id' => $r['id'], 'pr_number' => $r['number']]);
}

if ($method === 'PUT') {
    $id = (int)($data['id'] ?? 0);
    if (!$id) jsonResponse(false, 'Purchase request id is required.');
    try {
        $header = prCleanHeader(prCanonicalHeader($db, $data));
        $rows = prNormalizeItems(is_array($data['items'] ?? null) ? $data['items'] : []);
        $summary = prUpdate($db, $id, (int)($data['row_version'] ?? 0), $header, $rows, $actor);
        $pr = prLoad($db, $id);
    } catch (InvalidArgumentException $e) {
        jsonResponse(false, $e->getMessage());
    } catch (Throwable $e) {
        prDbFailure($e, 'Unable to update the purchase request.');
    }
    logAudit('UPDATE_PR', 'purchase_requests', "Updated {$pr['PR_Number']}: items {$summary}; total " . number_format(prTotal($rows), 2));
    jsonResponse(true, "{$pr['PR_Number']} updated.", ['id' => $id, 'pr_number' => $pr['PR_Number'], 'row_version' => (int)$pr['Row_Version']]);
}

if ($method === 'DELETE') {
    if (!$canDelete) jsonResponse(false, 'You do not have permission to delete a purchase request.');
    $id = (int)($_GET['id'] ?? 0);
    if (!$id) jsonResponse(false, 'Purchase request id is required.');
    try {
        $number = prSoftDelete($db, $id, $actor);
    } catch (InvalidArgumentException $e) {
        jsonResponse(false, $e->getMessage());
    } catch (Throwable $e) {
        prDbFailure($e, 'Unable to delete the purchase request.');
    }
    logAudit('DELETE_PR', 'purchase_requests', "Deleted draft {$number}");
    jsonResponse(true, "{$number} deleted.");
}

jsonResponse(false, 'Unsupported request.');
