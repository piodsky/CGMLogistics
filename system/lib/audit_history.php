<?php
// Per-record History for Inventory, Inspection & Acceptance and Competitive
// Bidding — the same idea as the Purchase Order's History button: a read of
// tbl_po_audit_logs (what logAudit() writes and the Activity Log screen
// shows), filtered down to one record. No new table: the log is already there.
//
// The PO module keeps its own getPoHistory() in api/purchase_orders.php; that
// file is live and is deliberately not refactored onto this one.

// Every log line in $module that names this PO. Both modules' logAudit()
// calls put "PO <number>" in Details, so the match is that phrase as a whole
// token — a bare LIKE would let PO 1017 pull in PO 10170's rows. The LIKE
// narrows the rows first; the regex makes the final call. The row limit is
// applied after the filter, and the LIKE pass is capped generously so a PO
// whose number is a prefix of many others still fills its own history.
function auditHistoryForPo(PDO $db, string $module, string $poNumber): array {
    $stmt = $db->prepare(
        "SELECT Username, Action, Details, Date_Created
           FROM tbl_po_audit_logs
          WHERE Module = ? AND Details LIKE ?
          ORDER BY Date_Created DESC, ID DESC
          LIMIT 2000"
    );
    $stmt->execute([$module, '%PO ' . addcslashes($poNumber, '%_\\') . '%']);
    // Ends where the number ends: these modules write "on PO X: 1 item…" and
    // "(PO X)", so punctuation may follow. A letter, digit or dash may not —
    // that is a longer number (TF-sys2026726016 vs TF-sys2026726016-2).
    $pattern = '/\bPO\s+' . preg_quote($poNumber, '/') . '(?![\w-])/u';
    $rows = array_values(array_filter($stmt->fetchAll(PDO::FETCH_ASSOC),
        fn($r) => preg_match($pattern, (string)($r['Details'] ?? '')) === 1));
    return fixLegacyEncodingDeep(array_slice($rows, 0, 200));
}

// Every Competitive Bidding log line about ONE bid record.
//
// A Bid Number alone does not name a record any more — several contracts can
// share one — so since 2026-09-28 the log names a bid as
// "2026-130 (Contract 2026-167)" whenever it has a Contract Number. A line is
// this record's when:
//   * it names the Bid Number as a whole token (2026-130, not 2026-130-2);
//   * if it names a contract right after it, that is this record's contract;
//     a line with no contract is this record's only if the record has no
//     Contract Number, or the line predates contract labels (back then a Bid
//     Number was unique, so the line can only be about the one record);
//   * it was written after this record was created — which keeps an earlier,
//     deleted record under the same number (2026-091 has one) out of it.
function auditHistoryForBid(PDO $db, string $bidNumber, string $contractNumber, ?string $createdAt): array {
    if ($bidNumber === '') return [];
    $stmt = $db->prepare(
        "SELECT Username, Action, Details, Date_Created
           FROM tbl_po_audit_logs
          WHERE Module = 'competitive_bidding' AND Details LIKE ?
          ORDER BY Date_Created DESC, ID DESC
          LIMIT 1000"
    );
    $stmt->execute(['%' . addcslashes($bidNumber, '%_\\') . '%']);
    $rows = $stmt->fetchAll(PDO::FETCH_ASSOC);

    // When contract labels began: the first line carrying one. Before it,
    // every line is unlabelled by construction.
    $labelsSince = $db->query(
        "SELECT MIN(Date_Created) FROM tbl_po_audit_logs
          WHERE Module = 'competitive_bidding' AND Details LIKE '%(Contract %'"
    )->fetchColumn() ?: null;

    // A few seconds' slack: the record's own CREATE line is written just
    // after its row, but both come from the same clock.
    $since = $createdAt ? date('Y-m-d H:i:s', strtotime($createdAt) - 5) : null;
    $number = '(?<![\w-])' . preg_quote($bidNumber, '/') . '(?![\w-])';
    $out = [];
    foreach ($rows as $r) {
        $details = (string)($r['Details'] ?? '');
        if ($since && $r['Date_Created'] < $since) continue;
        if (!preg_match('/' . $number . '(?:\s*\(Contract ([^)]*)\))?/u', $details, $m)) continue;
        if (isset($m[1])) {
            if (strcasecmp(trim($m[1]), $contractNumber) !== 0) continue;
        } elseif ($contractNumber !== '' && $labelsSince && $r['Date_Created'] >= $labelsSince) {
            continue;   // an unlabelled line from after labels began belongs to a no-contract sibling
        }
        $out[] = $r;
        if (count($out) >= 200) break;
    }
    return fixLegacyEncodingDeep($out);
}
