<?php
/* Removes the sample Purchase Requests made by tools/create_sample_pr2026.php.
 *
 *     php tools/remove_sample_pr2026.php              (dry run — shows what it would do)
 *     php tools/remove_sample_pr2026.php --commit     (does it)
 *
 * Matches on Created_By = 'sample2026' and nothing else, so a real request can
 * never be caught by it. SOFT delete — isDeleted = 1, never DELETE FROM — like
 * every other retire path in this app; the numbers are freed (the unique index
 * ignores deleted rows) and the append-only status history is left in place.
 */
require_once __DIR__ . '/../system/config.php';

const SAMPLE_TAG = 'sample2026';

$db = getPoDB();
$commit = in_array('--commit', $argv, true);
echo ($commit ? 'COMMITTING' : 'DRY RUN — nothing will be changed') . "\n\n";

$q = $db->prepare('SELECT PR_ID, PR_Number, Status FROM tbl_pr WHERE Created_By = ? AND isDeleted = 0 ORDER BY PR_ID');
$q->execute([SAMPLE_TAG]);
$prs = $q->fetchAll(PDO::FETCH_ASSOC);
if (!$prs) { echo "Nothing to remove — no sample requests are there.\n"; exit(0); }
foreach ($prs as $p) echo "  {$p['PR_Number']}  {$p['Status']}\n";
if (!$commit) { echo "\nRe-run with --commit to remove them.\n"; exit(0); }

try {
    $db->beginTransaction();
    $ids = array_column($prs, 'PR_ID');
    $in = implode(',', array_fill(0, count($ids), '?'));
    $db->prepare("UPDATE tbl_pr_items SET isDeleted = 1, Date_Updated = NOW() WHERE PR_ID IN ($in) AND isDeleted = 0")->execute($ids);
    $db->prepare("UPDATE tbl_pr SET isDeleted = 1, Deleted_By = 'remove_sample_pr2026', Date_Deleted = NOW() WHERE PR_ID IN ($in) AND Created_By = '" . SAMPLE_TAG . "' AND isDeleted = 0")->execute($ids);
    $db->commit();
    echo "\nRemoved " . count($ids) . " sample request(s) and their items (soft-deleted).\n";
} catch (Throwable $e) {
    if ($db->inTransaction()) $db->rollBack();
    echo "\nNothing was changed: " . $e->getMessage() . "\n";
    exit(1);
}
