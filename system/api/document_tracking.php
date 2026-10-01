<?php
// Document Tracking — a read-only window onto the DTS (TAS) database.
//
// DTS belongs to another team and is the authority on where a physical
// document currently sits. Nothing here writes to it; every statement is a
// SELECT, and a DTS outage must never take a purchase order screen down.
//
// The bridge between the two systems is the barcode: a purchase order's
// Barcode_Number IS the DTS BarCodeNo — verified, every sampled value resolved
// to exactly one DTS document. Where a PO has no barcode on file, it can still
// be derived from its PR number (see dtsBarcodeForPrNumber() in config.php).
require_once '../config.php';
requireAuth();
requireModule('document_tracking');
header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] !== 'GET') jsonResponse(false, 'Unsupported request.');

// Two different failures, two different fixes: nobody has set DTS up, versus
// DTS is down right now. Telling them apart saves an admin from hunting a
// network problem that is really a missing line in .env.
function dtsUnavailableMessage(): string {
    return dtsIsConfigured()
        ? 'The Document Tracking system could not be reached. It may be offline.'
        : 'Document Tracking is not set up on this server — the TASDB_DB_* settings are missing from .env.';
}

// A document's header plus its full office-to-office trail.
function dtsDocument(string $barcode): ?array {
    $dts = getDtsDB();

    $stmt = $dts->prepare(
        'SELECT BarCodeNo, OfficeOrig, Head, Designation, TPM, UserID,
                DateCreated, Payee, Purpose, Amount, Status, Location
         FROM tbl_transaction WHERE BarCodeNo = ? LIMIT 1'
    );
    $stmt->execute([$barcode]);
    $header = $stmt->fetch(PDO::FETCH_ASSOC) ?: null;

    $stmt = $dts->prepare(
        'SELECT OfficeFrom, OfficeTo, DateIn, DateOut, UserID, UserIDOut, Remarks
         FROM tas_inout WHERE BarCodeNo = ? ORDER BY DateIn, DateOut'
    );
    $stmt->execute([$barcode]);
    $trail = $stmt->fetchAll(PDO::FETCH_ASSOC);

    if (!$header && !$trail) return null;

    // Where it is now: the last hop received but not yet released. A row whose
    // DateOut is still empty means the document is sitting in that office.
    $current = null;
    foreach ($trail as $r) {
        if (dtsIsBlankDate($r['DateOut'])) {
            $current = trim((string)$r['OfficeTo']) !== '' ? $r['OfficeTo'] : $r['OfficeFrom'];
        }
    }
    if ($current === null && $trail) {
        $last = end($trail);
        $current = trim((string)$last['OfficeTo']) !== '' ? $last['OfficeTo'] : $last['OfficeFrom'];
    }

    // Days since it last moved — the number that says whether it is stuck,
    // which is the reason anyone opens this screen.
    $lastMoved = null;
    $idleDays  = null;
    $times = [];
    foreach ($trail as $r) {
        foreach (['DateIn', 'DateOut'] as $k) {
            if (!dtsIsBlankDate($r[$k] ?? '')) $times[] = strtotime((string)$r[$k]);
        }
    }
    if ($times) {
        $lastMoved = date('Y-m-d H:i:s', max($times));
        $idleDays  = (int)floor((time() - max($times)) / 86400);
    }

    return fixLegacyEncodingDeep([
        'barcode'    => $barcode,
        'header'     => $header,
        'trail'      => $trail,
        'hops'       => count($trail),
        'current'    => $current,
        'last_moved' => $lastMoved,
        'idle_days'  => $idleDays,
    ]);
}

// Works out what to look up, and reports which route was used so the screen can
// be honest about whether the barcode was on file or inferred.
function dtsResolve($barcode, $poNumber): array {
    $barcode = trim((string)$barcode);
    if ($barcode !== '') return ['barcode' => $barcode, 'source' => 'typed', 'po' => null];

    $poNumber = trim((string)$poNumber);
    if ($poNumber === '') return ['barcode' => null, 'source' => null, 'po' => null];

    $stmt = getPoDB()->prepare(
        'SELECT PO_Number, PR_Number, Barcode_Number, Supplier_Name, Department, Purpose, Date_Created
         FROM tbl_purchase_order WHERE PO_Number = ? AND isDeleted = 0 LIMIT 1'
    );
    $stmt->execute([$poNumber]);
    $po = $stmt->fetch(PDO::FETCH_ASSOC);
    if (!$po) return ['barcode' => null, 'source' => null, 'po' => null];

    $stored = trim((string)$po['Barcode_Number']);
    if ($stored !== '') return ['barcode' => $stored, 'source' => 'on_file', 'po' => $po];

    $derived = dtsBarcodeForPrNumber($po['PR_Number']);
    return ['barcode' => $derived, 'source' => $derived ? 'derived_from_pr' : null, 'po' => $po];
}

// The "Track a Document" box's own resolver — takes whatever was typed and
// tries it, in order, as: a PO Number, a PR Number, a Barcode already on
// file, then (2026-09-22, "search not barcode only, all available columns")
// a broader LIKE search across those same columns plus Supplier/Department —
// the same five columns dtsList()'s own filter already searches, so typing
// a supplier name here finds the same purchase orders the list below would.
//
// A single match resolves straight to its document, same as typing an exact
// barcode always has. Several matches come back as 'multiple' with the row
// list, so the caller can show a picker rather than guessing which one was
// meant — silently tracking the wrong PO's paperwork is worse than one extra
// click. Falls through to treating the input as a literal DTS barcode only
// if nothing on file matches it at all, preserving the original "scan a
// barcode DTS knows about that isn't on any PO here" behavior.
function dtsResolveQuery(PDO $db, string $query): array {
    $query = trim($query);
    if ($query === '') return ['barcode' => null, 'source' => null, 'po' => null, 'matches' => []];

    $cols = 'PO_Number, PR_Number, Barcode_Number, Supplier_Name, Department, Purpose, Date_Created';
    foreach (['PO_Number', 'PR_Number', 'Barcode_Number'] as $col) {
        $stmt = $db->prepare(
            "SELECT {$cols} FROM tbl_purchase_order
             WHERE isDeleted = 0 AND TRIM({$col}) = ? ORDER BY Date_Created DESC LIMIT 1"
        );
        $stmt->execute([$query]);
        if ($po = $stmt->fetch(PDO::FETCH_ASSOC)) {
            $stored = trim((string)$po['Barcode_Number']);
            if ($stored !== '') return ['barcode' => $stored, 'source' => 'on_file', 'po' => $po, 'matches' => []];
            $derived = dtsBarcodeForPrNumber($po['PR_Number']);
            return ['barcode' => $derived, 'source' => $derived ? 'derived_from_pr' : null, 'po' => $po, 'matches' => []];
        }
    }

    $stmt = $db->prepare(
        "SELECT {$cols} FROM tbl_purchase_order
         WHERE isDeleted = 0 AND (PO_Number LIKE ? OR PR_Number LIKE ? OR Supplier_Name LIKE ?
               OR Department LIKE ? OR Barcode_Number LIKE ?)
         ORDER BY Date_Created DESC LIMIT 21"
    );
    $term = '%' . $query . '%';
    $stmt->execute([$term, $term, $term, $term, $term]);
    $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);

    if (count($rows) === 1) {
        $po = $rows[0];
        $stored = trim((string)$po['Barcode_Number']);
        if ($stored !== '') return ['barcode' => $stored, 'source' => 'on_file', 'po' => $po, 'matches' => []];
        $derived = dtsBarcodeForPrNumber($po['PR_Number']);
        return ['barcode' => $derived, 'source' => $derived ? 'derived_from_pr' : null, 'po' => $po, 'matches' => []];
    }
    if (count($rows) > 1) {
        // Capped at 20 for the picker — a search this broad matching more
        // than that needs a narrower term anyway, not a longer dropdown.
        return ['barcode' => null, 'source' => 'multiple', 'po' => null,
                'matches' => fixLegacyEncodingDeep(array_slice($rows, 0, 20))];
    }

    // Nothing on file at all — last resort, treat it as a literal DTS
    // barcode (same as the original barcode-only box always did).
    return ['barcode' => $query, 'source' => 'typed', 'po' => null, 'matches' => []];
}

// Every live purchase order that carries this barcode.
//
// DTS tracks one physical document, but procurement routinely files several
// purchase orders under the same one — up to 5 in current data — so this is
// deliberately a list. $origin is the PO the popup was opened from; it is
// folded in even when its barcode was only derived from the PR number, so the
// screen always names the purchase order that was actually asked about.
function dtsPosForBarcode(?string $barcode, ?array $origin): array {
    $rows = [];
    if ($barcode !== null && $barcode !== '') {
        $stmt = getPoDB()->prepare(
            'SELECT PO_Number, PR_Number, Supplier_Name, Department, Date_Created
             FROM tbl_purchase_order
             WHERE isDeleted = 0 AND TRIM(Barcode_Number) = ?
             ORDER BY Date_Created DESC LIMIT 20'
        );
        $stmt->execute([$barcode]);
        $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);
    }
    if ($origin) {
        $seen = false;
        foreach ($rows as $r) {
            if ($r['PO_Number'] === $origin['PO_Number']) { $seen = true; break; }
        }
        if (!$seen) array_unshift($rows, $origin);
    }
    return fixLegacyEncodingDeep($rows);
}

// Purchase orders with their tracking status.
//
// The barcode is deliberately NOT derived per row: that lookup is a 73,000-row
// scan inside DTS (~24 ms each), which would add seconds to a 500-row list.
// The list shows what is already on file; deriving happens when one document
// is actually opened.
function dtsList(PDO $db): array {
    $search = trim($_GET['search'] ?? '');
    $from   = $_GET['from'] ?? '';
    $to     = $_GET['to'] ?? '';
    $params = [];

    $sql = "SELECT PO_Number AS po_number, PR_Number AS pr_number, Supplier_Name AS supplier_name,
                   Department AS department, Date_Created AS date_created,
                   Barcode_Number AS barcode_number
            FROM tbl_purchase_order
            WHERE isDeleted = 0 AND TRIM(PO_Number) <> ''";
    if ($from) { $sql .= ' AND Date_Created >= ?'; $params[] = date('Y-m-d 00:00:00', strtotime($from)); }
    if ($to)   { $sql .= ' AND Date_Created <= ?'; $params[] = date('Y-m-d 23:59:59', strtotime($to)); }
    if ($search !== '') {
        $sql .= ' AND (PO_Number LIKE ? OR PR_Number LIKE ? OR Supplier_Name LIKE ?
                       OR Department LIKE ? OR Barcode_Number LIKE ?)';
        $term = '%' . $search . '%';
        $params = array_merge($params, array_fill(0, 5, $term));
    }
    $sql .= ' ORDER BY Date_Created DESC LIMIT 500';

    $stmt = $db->prepare($sql);
    $stmt->execute($params);
    $rows = fixLegacyEncodingDeep($stmt->fetchAll(PDO::FETCH_ASSOC));
    foreach ($rows as &$r) { $r['tracked'] = trim((string)$r['barcode_number']) !== ''; }
    unset($r);

    $counts = ['total' => count($rows), 'tracked' => 0, 'untracked' => 0];
    foreach ($rows as $r) { $r['tracked'] ? $counts['tracked']++ : $counts['untracked']++; }

    $status = strtolower(trim($_GET['status'] ?? ''));
    if ($status === 'tracked')   $rows = array_values(array_filter($rows, fn($r) => $r['tracked']));
    if ($status === 'untracked') $rows = array_values(array_filter($rows, fn($r) => !$r['tracked']));

    return ['orders' => $rows, 'counts' => $counts];
}

if (isset($_GET['document'])) {
    $query = trim($_GET['q'] ?? '');
    // 'q' is the new all-columns box; 'barcode'/'po_number' stay as they
    // were for existing callers (dtsTrackBarcode()/dtsTrackPo(), and any
    // ?po_number=/?barcode= deep link into this page from elsewhere).
    $r = $query !== ''
        ? dtsResolveQuery(getPoDB(), $query)
        : dtsResolve($_GET['barcode'] ?? null, $_GET['po_number'] ?? null);

    if ($r['source'] === 'multiple') {
        jsonResponse(false, count($r['matches']) . ' purchase orders match "' . $query
            . '" — pick one below.', ['matches' => $r['matches']]);
    }
    if (!$r['barcode']) {
        // With DTS unavailable a PR number can never be resolved, so blaming
        // the purchase order for having no barcode would be a lie.
        if (!dtsIsConfigured()) jsonResponse(false, dtsUnavailableMessage());
        jsonResponse(false, $r['po']
            ? 'No DTS barcode on this purchase order, and none could be found from its PR number.'
            : ($query !== '' ? 'No purchase order or barcode matches "' . $query . '".'
                              : 'Enter a barcode, or open a purchase order that has one.'));
    }
    try {
        $doc = dtsDocument($r['barcode']);
    } catch (Throwable $e) {
        error_log('DTS lookup failed for ' . $r['barcode'] . ': ' . $e->getMessage());
        jsonResponse(false, dtsUnavailableMessage());
    }
    if (!$doc) jsonResponse(false, 'Barcode ' . $r['barcode'] . ' is not in the Document Tracking system.');
    $doc['source']    = $r['source'];
    $doc['po']        = $r['po'] ? fixLegacyEncodingDeep($r['po']) : null;
    $doc['pos']       = dtsPosForBarcode($r['barcode'], $r['po']);
    $doc['origin_po'] = $r['po']['PO_Number'] ?? null;
    jsonResponse(true, '', $doc);
}

if (isset($_GET['stats'])) {
    $db = getPoDB();
    $total   = (int)$db->query("SELECT COUNT(*) FROM tbl_purchase_order WHERE isDeleted = 0 AND TRIM(PO_Number) <> ''")->fetchColumn();
    $tracked = (int)$db->query("SELECT COUNT(*) FROM tbl_purchase_order WHERE isDeleted = 0 AND TRIM(COALESCE(Barcode_Number,'')) <> ''")->fetchColumn();
    $hops = null;
    try { $hops = (int)getDtsDB()->query('SELECT COUNT(*) FROM tas_inout')->fetchColumn(); }
    catch (Throwable $e) { error_log('DTS stats unavailable: ' . $e->getMessage()); }
    jsonResponse(true, '', ['stats' => [
        'total_pos' => $total,
        'tracked'   => $tracked,
        'untracked' => $total - $tracked,
        'dts_hops'  => $hops,
    ]]);
}

$list = dtsList(getPoDB());
jsonResponse(true, '', ['orders' => $list['orders'], 'counts' => $list['counts']]);
