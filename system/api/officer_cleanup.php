<?php
// Fix Names — the Inventory screen's tool for merging the several spellings a
// custodian's name is saved under.
//
// Accountable_Officer is typed by hand on every item, so one person ends up
// as "MERLE C. VERGARA" on 74 items and "MERLE C.VERGARA" on 26 more, and
// anything counted or looked up per person (the Personnel MR) is split. This
// lets someone tick the spellings that are the same person and choose the one
// to keep. It never merges on its own: the suggestions are only suggestions.
//
// The logic — validation, the rewrite, the undo record — is in
// lib/officer_cleanup.php; this file only routes requests to it. Same gate as
// the supplier cleanup (system tools), plus the Inventory module.
require_once '../config.php';
require_once '../lib/inventory_officers.php';
require_once '../lib/officer_cleanup.php';
requireModule('inventory');
requireSystemToolsAdmin();
header('Content-Type: application/json');
$db = getPoDB();
$method = $_SERVER['REQUEST_METHOD'];

if ($method === 'GET') {
    if (isset($_GET['undo_list'])) {
        jsonResponse(true, '', ['merges' => fixLegacyEncodingDeep(ofcUndoList())]);
    }

    // Finding a name by hand, for the people the suggestions did not group.
    if (isset($_GET['search'])) {
        $q = ofcClean((string)$_GET['search']);
        if (mb_strlen($q) < 2) jsonResponse(false, 'Type at least two characters.');
        jsonResponse(true, '', ['names' => fixLegacyEncodingDeep(ofcSearchNames($db, $q))]);
    }

    // Suggested groups: every person saved under more than one spelling. The
    // pre-selected spelling to keep is the most-used one.
    $groups = [];
    foreach (invOfficerGroups($db) as $g) {
        if (count($g['members']) < 2) continue;
        $variants = [];
        foreach ($g['members'] as $name) $variants[] = ['name' => $name, 'n' => (int)($g['counts'][$name] ?? 0)];
        usort($variants, fn($a, $b) => $b['n'] <=> $a['n'] ?: strcmp($a['name'], $b['name']));
        $groups[] = ['target' => $g['rep'], 'variants' => $variants, 'total' => $g['n']];
    }
    usort($groups, fn($a, $b) => $b['total'] <=> $a['total'] ?: strcmp($a['target'], $b['target']));
    jsonResponse(true, '', ['groups' => fixLegacyEncodingDeep($groups)]);
}

if ($method === 'POST') {
    $data   = getInput();
    $action = (string)($data['action'] ?? '');
    $names  = is_array($data['names'] ?? null) ? $data['names'] : [];
    $target = (string)($data['target'] ?? '');

    try {
        if ($action === 'preview') {
            jsonResponse(true, '', ofcPreview($db, $names, $target));
        }

        if ($action === 'merge') {
            $actor = getCurrentUser()['username'] ?? 'unknown';
            $r = ofcApplyMerge($db, $names, $target, $actor);

            // After the commit, and not allowed to matter: the rows are already
            // rewritten, so a failed audit line must not read as a failed merge.
            // The undo file is what makes it reversible, not this.
            try {
                logAudit('MERGE_OFFICER_NAMES', 'inventory',
                    $r['changed'] . ' row(s) set to "' . ofcClean($target) . '"; undo file ' . ($r['undo'] ?? 'none'));
            } catch (Throwable $e) {
                error_log('officer_cleanup: merge committed but audit log failed: ' . $e->getMessage());
            }
            jsonResponse(true, $r['changed']
                ? $r['changed'] . ' item row' . ($r['changed'] === 1 ? '' : 's') . ' updated.'
                : 'Nothing needed changing.', $r);
        }

        if ($action === 'undo') {
            $r = ofcUndoMerge($db, (string)($data['file'] ?? ''));
            try {
                logAudit('UNDO_OFFICER_MERGE', 'inventory',
                    $r['restored'] . ' row(s) restored, ' . $r['skipped'] . ' skipped; ' . (string)($data['file'] ?? ''));
            } catch (Throwable $e) {
                error_log('officer_cleanup: undo committed but audit log failed: ' . $e->getMessage());
            }
            jsonResponse(true, $r['restored'] . ' row' . ($r['restored'] === 1 ? '' : 's') . ' restored'
                . ($r['skipped'] ? ' (' . $r['skipped'] . ' left as they are — changed since the merge)' : '') . '.', $r);
        }

        jsonResponse(false, 'Unknown action.');
    } catch (InvalidArgumentException $e) {
        // A request that cannot be carried out; the message is written to be shown.
        jsonResponse(false, $e->getMessage());
    } catch (Throwable $e) {
        jsonResponse(false, 'Nothing was changed: ' . $e->getMessage());
    }
}

jsonResponse(false, 'Method not allowed.');
