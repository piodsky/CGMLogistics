<?php
/* Gives every ITR / PTR that was recorded with a SHARED number a number of its own.
 *
 *     php tools/renumber_duplicate_transfers.php              (dry run — shows what it would do)
 *     php tools/renumber_duplicate_transfers.php --commit     (does it)
 *
 * Before the transfer numbering was fixed (lib/inventory_numbers.php), every transfer
 * was given the same number — 17 ITRs, all ITR202691204. In each group sharing a
 * number, the EARLIEST transfer (lowest Transfer_ID, i.e. the order they were
 * recorded) keeps it, and every later one gets the next new number from the
 * numbering log, in that order. What changes:
 *   * tbl_inventory_transfers.Transfer_Number;
 *   * the numbering log (auto_itr / auto_ptr): a proper row for each number;
 *   * PTR_ITR_Number on each affected item's CURRENT row — the number of the LATEST
 *     transfer that item was in (an item moved six times shows its sixth).
 * Superseded (soft-deleted) item rows and the audit log are left as they were.
 *
 * The file that undoes it is written BEFORE anything is changed, and puts back exactly
 * the numbers that are there now. (The numbering-log rows it added are left in place:
 * they are just numbers that were used, and removing them would let a number be
 * issued twice.) If a transfer form has already been PRINTED with the shared number,
 * the paper still says so — that is the price of changing the record.
 */
require_once __DIR__ . '/../system/config.php';
require_once __DIR__ . '/../system/lib/inventory_numbers.php';

$db = getPoDB();
$commit = in_array('--commit', $argv, true);
echo ($commit ? 'COMMITTING' : 'DRY RUN — nothing will be changed') . "\n\n";

// The two numbering logs, exactly as api/inventory_issuance.php's ISSUANCE_TABLES has them.
$logs = [
    'ITR' => ['seq' => 'auto_itr', 'idCol' => 'ITR_ID', 'numCol' => 'ITR_Number', 'prefix' => 'ITR', 'pad' => 4],
    'PTR' => ['seq' => 'auto_ptr', 'idCol' => 'PTR_ID', 'numCol' => 'PTR_Number', 'prefix' => 'PTR', 'pad' => 4],
];

$plans = [];
foreach ($logs as $type => $t) {
    $plans[$type] = invRenumberDuplicateTransfers($db, $t, $type, false);
    if (!$plans[$type]['groups']) { echo "$type: no transfers share a number — nothing to do.\n"; continue; }
    foreach ($plans[$type]['groups'] as $g) echo "$type {$g['number']}: shared by {$g['transfers']} transfers — Transfer_ID {$g['keeps']} keeps it.\n";
    // What the new numbers WILL be: the log's next ids, in order. (Reading the counter
    // fresh; MySQL otherwise serves a cached figure.)
    $db->exec('SET SESSION information_schema_stats_expiry = 0');
    $next = (int)$db->query("SHOW TABLE STATUS LIKE '{$t['seq']}'")->fetch(PDO::FETCH_ASSOC)['Auto_increment'];
    $keptNeedsRow = false;
    foreach ($plans[$type]['groups'] as $g) {
        $has = $db->prepare("SELECT COUNT(*) FROM {$t['seq']} WHERE {$t['numCol']} = ?"); $has->execute([$g['number']]);
        if ((int)$has->fetchColumn() === 0) $keptNeedsRow = true;
    }
    if ($keptNeedsRow) $next++;                     // the kept number gets its own log row first
    foreach ($plans[$type]['changes'] as $c) {
        $expected = $t['prefix'] . date('Y') . (int)date('n') . str_pad((string)$next++, $t['pad'], '0', STR_PAD_LEFT);
        printf("   Transfer_ID %-4s PO %-20s by %-14s %s -> %s   (%d item%s)\n", $c['id'], $c['po'], $c['printed_by'], $c['old'], $expected, count($c['items']), count($c['items']) === 1 ? '' : 's');
    }
}
$total = array_sum(array_map(fn($p) => count($p['changes']), $plans));
if (!$total) { echo "\nNothing to change.\n"; exit(0); }
if (!$commit) { echo "\n$total transfer(s) would be renumbered. Re-run with --commit to do it.\n"; exit(0); }

// The undo file first, from what is on file right now: the old number for every
// transfer and every current item row that is about to change.
$undoFile = __DIR__ . '/renumber_duplicate_transfers_undo_' . date('Ymd_His') . '.sql';
$undo = "-- Puts back the transfer numbers as they were before tools/renumber_duplicate_transfers.php ran " . date('Y-m-d H:i:s') . "\n"
      . "-- (The numbering-log rows it added are deliberately left.)\n";
foreach ($plans as $type => $p) foreach ($p['changes'] as $c) {
    $undo .= "UPDATE tbl_inventory_transfers SET Transfer_Number = " . $db->quote($c['old']) . " WHERE Transfer_ID = {$c['id']};\n";
    foreach ($c['items'] as $invId) {
        $cur = $db->prepare("SELECT COUNT(*) FROM tbl_inventory_items_details WHERE Inv_ID = ? AND isDeleted = '0' AND PTR_ITR_Number = ?");
        $cur->execute([$invId, $c['old']]);
        if ((int)$cur->fetchColumn()) $undo .= "UPDATE tbl_inventory_items_details SET PTR_ITR_Number = " . $db->quote($c['old']) . " WHERE Inv_ID = {$invId} AND isDeleted = '0';\n";
    }
}
$undo = implode("\n", array_unique(explode("\n", $undo)));      // an item moved six times is listed once
if (file_put_contents($undoFile, $undo . "\n") === false) { echo "\nCould not write the undo file, so nothing was changed.\n"; exit(1); }

try {
    foreach ($logs as $type => $t) {
        if (!$plans[$type]['changes']) continue;
        $r = invRenumberDuplicateTransfers($db, $t, $type, true);
        echo "\n$type renumbered:\n";
        foreach ($r['changes'] as $c) echo "   Transfer_ID {$c['id']}: {$c['old']} -> {$c['new']}\n";
        echo "   " . count($r['items']) . " item row(s) now show their latest transfer's number.\n";
    }
    echo "\nDone. Undo file: $undoFile\n";
} catch (Throwable $e) {
    echo "\nNothing was changed: " . $e->getMessage() . "\n";
    exit(1);
}
