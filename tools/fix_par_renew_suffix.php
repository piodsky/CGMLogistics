<?php
/* Collapses a PAR / ICS number whose renewal suffix was stacked by the old
 * behaviour — PAR2026900516-R2026-R2026-R2026 — down to a single suffix,
 * PAR2026900516-R2026.
 *
 *     php tools/fix_par_renew_suffix.php              (dry run — lists what it would change)
 *     php tools/fix_par_renew_suffix.php --commit     (does it)
 *
 * NEW renewals no longer stack (api/inventory_issuance.php, lib/par_numbers.php),
 * and a stacked number is cleaned the next time it is renewed anyway. This is only
 * for tidying the ones already on file, and it is optional: a number that has
 * already been PRINTED on a form reads with its stacked suffix on that paper, so
 * changing it in the database makes the record differ from the copy in someone's
 * hand. That is why it does nothing without --commit.
 *
 * What it touches: PAR_ICS_Number on CURRENT item rows (isDeleted = '0') of
 * tbl_inventory_items_details, and nothing else. Older revisions and the issuance
 * log (auto_parnew / auto_icsnew — the record of what was actually printed) keep
 * the number as it was. The year kept is the LAST suffix's. The reverse UPDATEs are
 * written to a file beside this one before anything is changed.
 */
require_once __DIR__ . '/../system/config.php';
require_once __DIR__ . '/../system/lib/par_numbers.php';

$db = getPoDB();
$commit = in_array('--commit', $argv, true);
echo ($commit ? 'COMMITTING' : 'DRY RUN — nothing will be changed') . "\n\n";

$rows = $db->query(
    "SELECT Inv_ID, PO_Number, PAR_ICS_Number FROM tbl_inventory_items_details
     WHERE isDeleted = '0' AND PAR_ICS_Number REGEXP '-R[0-9]{4}-R[0-9]{4}' ORDER BY Inv_ID"
)->fetchAll(PDO::FETCH_ASSOC);
if (!$rows) { echo "Nothing to fix — no number has a repeated renewal suffix.\n"; exit(0); }

$changes = [];
foreach ($rows as $r) {
    $old = $r['PAR_ICS_Number'];
    $year = parIcsIssuedYear($old);                       // the last suffix's year
    $new = $year === null ? null : parIcsRenewedNumber($old, $year);
    printf("  Inv_ID %-6s PO %-22s %s\n%s-> %s\n", $r['Inv_ID'], $r['PO_Number'], $old, str_repeat(' ', 41), $new ?? '(left alone — year not readable)');
    if ($new !== null && $new !== $old) $changes[] = ['id' => (int)$r['Inv_ID'], 'old' => $old, 'new' => $new];
}
if (!$commit) { echo "\n" . count($changes) . " row(s) would change. Re-run with --commit to change them.\n"; exit(0); }

$undoFile = __DIR__ . '/fix_par_renew_suffix_undo_' . date('Ymd_His') . '.sql';
$undo = "-- Reverses tools/fix_par_renew_suffix.php run " . date('Y-m-d H:i:s') . "\n";
foreach ($changes as $c) $undo .= 'UPDATE tbl_inventory_items_details SET PAR_ICS_Number = ' . $db->quote($c['old']) . ' WHERE Inv_ID = ' . $c['id'] . ' AND PAR_ICS_Number = ' . $db->quote($c['new']) . ";\n";
if (file_put_contents($undoFile, $undo) === false) { echo "\nCould not write the undo file, so nothing was changed.\n"; exit(1); }

try {
    $db->beginTransaction();
    $upd = $db->prepare("UPDATE tbl_inventory_items_details SET PAR_ICS_Number = ? WHERE Inv_ID = ? AND PAR_ICS_Number = ? AND isDeleted = '0'");
    $done = 0;
    foreach ($changes as $c) { $upd->execute([$c['new'], $c['id'], $c['old']]); $done += $upd->rowCount(); }
    $db->commit();
    echo "\nChanged $done row(s). Undo with: $undoFile\n";
} catch (Throwable $e) {
    if ($db->inTransaction()) $db->rollBack();
    echo "\nNothing was changed: " . $e->getMessage() . "\n";
    exit(1);
}
