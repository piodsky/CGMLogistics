<?php
/* Creates five SAMPLE Purchase Requests dated 2026, one in each status, so the
 * Purchase Requests screen can be tried against something safe to open, edit,
 * print, approve and remove.
 *
 *     php tools/create_sample_pr2026.php              (dry run — shows what it would do)
 *     php tools/create_sample_pr2026.php --commit     (does it)
 *
 * Remove them again with:  php tools/remove_sample_pr2026.php --commit
 *
 * They go into the LIVE tbl_pr / tbl_pr_items / tbl_pr_status_log, alongside
 * whatever real requests are raised, so they are built to be unmistakable and to
 * be found again:
 *   * every Purpose begins "SAMPLE RECORD -", and Created_By is 'sample2026'
 *     (the removal tool matches on that, and on nothing else);
 *   * the people named are invented.
 *
 * Made with the module's own functions (lib/pr_workflow.php) — the same code the
 * screen uses — so the numbers, totals, item ids and status history are exactly
 * what a real request gets. Two deliberate differences: no notification is sent
 * and no audit line is written (nobody needs a bell for a sample), and the items
 * are NOT added to the shared item catalogue (tbl_ppmp_bac_items), which real
 * requests and purchase orders feed and a sample should not pollute.
 *
 * Re-running is safe: it stops if the samples are already there.
 */
require_once __DIR__ . '/../system/config.php';
require_once __DIR__ . '/../system/lib/pr_workflow.php';

const SAMPLE_TAG = 'sample2026';
const SAMPLE_ACTOR = 'SAMPLE DATA';
const SAMPLE_TABLES = ['pr' => 'tbl_pr', 'items' => 'tbl_pr_items', 'log' => 'tbl_pr_status_log', 'catalog' => false];

$db = getPoDB();
$commit = in_array('--commit', $argv, true);
echo ($commit ? 'COMMITTING' : 'DRY RUN — nothing will be changed') . "\n\n";

$already = $db->prepare('SELECT COUNT(*) FROM tbl_pr WHERE Created_By = ? AND isDeleted = 0');
$already->execute([SAMPLE_TAG]);
if ((int)$already->fetchColumn() > 0) {
    echo "The sample requests are already there. Remove them first with tools/remove_sample_pr2026.php --commit.\n";
    exit(0);
}

// [title-row?, description, unit, qty, unit cost, total (lump sum only), remarks]
$T = fn($d) => ['items_description' => $d];
$I = fn($d, $u, $q, $c, $r = '') => ['items_description' => $d, 'unit' => $u, 'quantity' => $q, 'unit_cost' => $c, 'remarks' => $r];
$L = fn($d, $u, $total, $r = '') => ['items_description' => $d, 'unit' => $u, 'total_cost' => $total, 'remarks' => $r];

$samples = [
    [
        'to' => 'Draft', 'date' => '2026-09-14',
        'h' => ['department' => 'CITY HEALTH OFFICE', 'category' => 'Other Supplies', 'fund_cluster' => 'GENERAL FUND', 'mode_of_procurement' => 'SVP',
            'purpose' => 'SAMPLE RECORD - safe to delete. Medical and office supplies for the use of the City Health Office, 4th quarter 2026.',
            'requested_by' => 'MARIA L. SANTOS', 'requested_by_position' => 'NURSE IV'],
        'items' => [
            $I('Alcohol, ethyl, 70%, 500ml', 'bottle', 120, 95.00),
            $I('Surgical mask, 3-ply, box of 50', 'box', 40, 165.00),
            $I("Gloves, nitrile, examination, medium\nPowder-free, box of 100", 'box', 30, 320.00, 'Size M'),
            $I('Thermometer, digital, infrared', 'pc', 6, 1450.00),
            $I('Bond paper, A4, 70gsm', 'ream', 25, 235.00),
        ],
    ],
    [
        'to' => 'Submitted', 'date' => '2026-09-10',
        'h' => ['department' => 'CITY ENGINEER OFFICE', 'division' => 'ADMINISTRATIVE DIVISION', 'section' => 'MOTOR POOL SECTION', 'category' => 'Repairs and Maintenance',
            'fund_cluster' => 'GENERAL FUND', 'responsibility_center_code' => '1-01-0010', 'mode_of_procurement' => 'SVP',
            'purpose' => 'SAMPLE RECORD - safe to delete. Repair and maintenance of motor pool vehicles for the 4th quarter of 2026.',
            'requested_by' => 'JUAN A. DELA CRUZ', 'requested_by_position' => 'CITY ENGINEER',
            'funds_certified_by' => 'ANA B. REYES', 'funds_certified_position' => 'CITY BUDGET OFFICER'],
        'items' => [
            $T('LOT 1 - VEHICLE PARTS'),
            $I("Brake pad set, front\nFor Isuzu Elf NKR\nOEM or equivalent", 'set', 4, 1850.00, 'Urgent'),
            $I('Engine oil, 15W-40, diesel', 'liters', 60, 385.50),
            $I('Oil filter, for Isuzu Elf NKR', 'pc', 8, 420.00),
            $T('LOT 2 - LABOR'),
            $L('Labor for engine overhaul, dump truck', 'lot', 45000.00, 'Lump sum'),
        ],
    ],
    [
        'to' => 'Approved', 'date' => '2026-08-25',
        'h' => ['department' => 'CITY GENERAL SERVICES OFFICE', 'category' => 'Office Supplies', 'fund_cluster' => 'GENERAL FUND', 'mode_of_procurement' => 'SVP',
            'responsibility_center_code' => '1-05-0002',
            'purpose' => 'SAMPLE RECORD - safe to delete. Common-use office supplies for the City General Services Office, 3rd quarter 2026.',
            'requested_by' => 'ROBERTO C. MENDOZA', 'requested_by_position' => 'SUPPLY OFFICER III',
            'funds_certified_by' => 'ANA B. REYES', 'funds_certified_position' => 'CITY BUDGET OFFICER',
            'approved_by' => 'LUIS P. GARCIA', 'approved_by_position' => 'CITY ADMINISTRATOR'],
        'items' => [
            $I('Ballpen, black, 0.5mm', 'box', 30, 120.00),
            $I('Bond paper, legal, 70gsm', 'ream', 60, 265.00),
            $I('Folder, long, with fastener', 'box', 20, 480.00),
            $I('Stapler, heavy duty', 'pc', 6, 750.00),
            $I('Staple wire, no. 35', 'box', 40, 38.00),
            $I('Masking tape, 1 inch', 'roll', 50, 42.00),
            $I('Correction tape', 'pc', 48, 35.00),
            $I('Marker, permanent, black', 'pc', 60, 28.00),
        ],
    ],
    [
        'to' => 'Returned', 'date' => '2026-09-03', 'reason' => 'Please attach the technical specifications of the knapsack sprayer.',
        'h' => ['department' => 'CITY AGRICULTURE OFFICE', 'category' => 'Agricultural Supplies', 'fund_cluster' => 'GENERAL FUND', 'mode_of_procurement' => 'SVP',
            'purpose' => 'SAMPLE RECORD - safe to delete. Farm inputs and tools for the vegetable demonstration farm.',
            'requested_by' => 'ELENA M. TORRES', 'requested_by_position' => 'CITY AGRICULTURIST'],
        'items' => [
            $I('Fertilizer, complete, 14-14-14, 50kg', 'bag', 40, 1850.00),
            $I('Vegetable seeds, assorted, 100g', 'pack', 120, 95.00),
            $I('Sprayer, knapsack, 16 liters', 'pc', 5, 2400.00),
        ],
    ],
    [
        'to' => 'Cancelled', 'date' => '2026-08-12', 'reason' => 'Sample: requirement no longer needed.',
        'h' => ['department' => 'CITY BUDGET OFFICE', 'category' => 'Office Supplies', 'fund_cluster' => 'GENERAL FUND',
            'purpose' => 'SAMPLE RECORD - safe to delete. Printer ink for the City Budget Office (cancelled example).',
            'requested_by' => 'ANA B. REYES', 'requested_by_position' => 'CITY BUDGET OFFICER'],
        'items' => [
            $I('Ink, black, refill, 70ml', 'bottle', 24, 260.00),
            $I('Ink, colour set, refill, 70ml', 'set', 8, 780.00),
        ],
    ],
];

// The path each sample takes to reach its status, using the module's own transitions.
$path = [
    'Draft'     => [],
    'Submitted' => ['submit'],
    'Approved'  => ['submit', 'approve'],
    'Returned'  => ['submit', 'return'],
    'Cancelled' => ['cancel'],
];

foreach ($samples as $s) {
    $rows = prNormalizeItems($s['items']);
    printf("  %-10s %-32s %2d rows  %12s\n", $s['to'], $s['h']['department'], count($rows), number_format(prTotal($rows), 2));
    if (!$commit) continue;

    $h = $s['h'] + ['pr_date' => $s['date']];
    foreach (['department' => 'canonicalDepartment', 'division' => 'canonicalDivision', 'section' => 'canonicalSection'] as $k => $fn) {
        $h[$k] = $fn($db, $h[$k] ?? '');
    }
    $h['remarks'] = 'Sample data made by tools/create_sample_pr2026.php. Remove with tools/remove_sample_pr2026.php.';
    $r = prCreate($db, prCleanHeader($h), $rows, SAMPLE_TAG, null, SAMPLE_TABLES);
    foreach ($path[$s['to']] as $action) {
        $reason = in_array($action, ['return', 'cancel'], true) ? $s['reason'] : '';
        // Approver = true: this is the module's own rule set applied by the person creating the sample.
        prTransition($db, $r['id'], $action, $reason, true, null, SAMPLE_ACTOR, SAMPLE_TABLES);
    }
    echo "             -> {$r['number']}\n";
}
echo $commit ? "\nDone. Remove them any time with: php tools/remove_sample_pr2026.php --commit\n" : "\nRe-run with --commit to create them.\n";
