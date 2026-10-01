<?php
// Fix Names — merging spellings of one custodian into the one that is kept.
//
// Only ever a decision somebody makes on screen: nothing here runs on its own,
// and nothing here decides two names are the same person. invOfficerGroups()
// SUGGESTS; the person at the screen ticks the spellings and picks the one to
// keep. Functions only — including this file has no side effects.
//
// What is rewritten: Accountable_Officer on the CURRENT rows of the item lines
// and the individually-tracked units. What is deliberately left alone:
//   - superseded revisions (isDeleted) — the history of what was saved;
//   - tbl_inventory_transfers — an immutable log of who handed what to whom,
//     spelled as it was typed that day.
// Every merge writes down each row it touches BEFORE touching it, so it can be
// undone; if that record cannot be written, nothing is changed.

// The two tables that carry the name: primary key and the "current row" test.
// A parameter everywhere below (defaulting to this) so the logic can be tested
// against temporary copies without going near live rows.
const OFC_TABLES = [
    'items' => ['table' => 'tbl_inventory_items_details', 'id' => 'Inv_ID', 'live' => "isDeleted = '0'"],
    'units' => ['table' => 'tbl_inventory_sub_details',   'id' => 'sub_ID', 'live' => 'isDeleted = 0'],
];
const OFC_MAX_SPELLINGS = 25;
const OFC_UNDO_DIR = __DIR__ . '/../uploads/officer_cleanup';

function ofcClean(string $s): string { return trim($s, " \t\n\r\0\x0B\xC2\xA0"); }

// Throws InvalidArgumentException, whose message is safe to show, when the
// request is not a merge that can be carried out. Returns [names, target].
//
// The target has to be one of the spellings being merged — the same rule the
// supplier cleanup uses — so this can never be turned into a way of writing an
// arbitrary string into Accountable_Officer. Every spelling also has to still
// exist on a current row: the screen may be minutes old.
function ofcValidate(PDO $db, array $names, string $target, array $tables = OFC_TABLES): array {
    $seen = [];
    foreach ($names as $n) {
        $n = ofcClean((string)$n);
        if ($n !== '') $seen[$n] = true;
    }
    $names  = array_map('strval', array_keys($seen));
    $target = ofcClean($target);

    if (count($names) < 2)                 throw new InvalidArgumentException('Pick at least two spellings to merge.');
    if (count($names) > OFC_MAX_SPELLINGS) throw new InvalidArgumentException('That is too many spellings at once — ' . OFC_MAX_SPELLINGS . ' at most.');
    if (!in_array($target, $names, true))  throw new InvalidArgumentException('The name to keep must be one of the spellings being merged.');

    foreach ($names as $n) {
        $found = false;
        foreach ($tables as $t) {
            $st = $db->prepare("SELECT 1 FROM {$t['table']} WHERE {$t['live']} AND Accountable_Officer = ? LIMIT 1");
            $st->execute([$n]);
            if ($st->fetchColumn()) { $found = true; break; }
        }
        if (!$found) throw new InvalidArgumentException('"' . $n . '" is no longer saved on any item — reload the list.');
    }
    return [$names, $target];
}

// The rows a merge would rewrite: current rows carrying any of the names, other
// than those already spelled exactly as the target. BINARY, because the column
// is case-insensitive and a row that differs from the target only in capitals
// still needs rewriting to it. $lock takes row locks (inside a transaction), so
// what is read is what gets written.
function ofcRowsToChange(PDO $db, array $names, string $target, array $tables = OFC_TABLES, bool $lock = false): array {
    $in = implode(',', array_fill(0, count($names), '?'));
    $rows = [];
    foreach ($tables as $key => $t) {
        $st = $db->prepare(
            "SELECT {$t['id']} AS id, Accountable_Officer AS name FROM {$t['table']}
              WHERE {$t['live']} AND Accountable_Officer IN ({$in})
                AND BINARY Accountable_Officer <> BINARY ?" . ($lock ? ' FOR UPDATE' : '')
        );
        $st->execute(array_merge($names, [$target]));
        foreach ($st->fetchAll(PDO::FETCH_ASSOC) as $r) {
            $rows[] = ['t' => $key, 'id' => (int)$r['id'], 'from' => $r['name']];
        }
    }
    return $rows;
}

// Read-only: what a merge would do, for the confirmation dialog.
function ofcPreview(PDO $db, array $names, string $target, array $tables = OFC_TABLES): array {
    [$names, $target] = ofcValidate($db, $names, $target, $tables);
    $rows = ofcRowsToChange($db, $names, $target, $tables);
    $by = [];
    foreach ($rows as $r) $by[$r['t']] = ($by[$r['t']] ?? 0) + 1;
    return ['rows' => count($rows), 'by_table' => $by, 'target' => $target];
}

function ofcEnsureUndoDir(string $dir): bool {
    if (!is_dir($dir) && !@mkdir($dir, 0775, true)) return false;
    $guard = $dir . '/.htaccess';
    if (!is_file($guard)) {
        @file_put_contents($guard,
            "# Merge undo records. These name real people and inventory rows —\n"
          . "# nothing here is meant to be reachable over HTTP.\n"
          . "Require all denied\nOptions -Indexes\nphp_flag engine off\n");
    }
    return true;
}

function ofcWriteJson(string $path, array $data): bool {
    return @file_put_contents($path, json_encode($data,
        JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE | JSON_INVALID_UTF8_SUBSTITUTE)) !== false;
}

// Carries out a merge. Returns ['changed' => rows rewritten, 'undo' => file].
// The undo record is written INSIDE the transaction: if it cannot be saved, the
// rewrite does not happen either.
function ofcApplyMerge(PDO $db, array $names, string $target, string $actor,
                       array $tables = OFC_TABLES, string $undoDir = OFC_UNDO_DIR): array {
    [$names, $target] = ofcValidate($db, $names, $target, $tables);
    $file = null;
    $db->beginTransaction();
    try {
        $rows = ofcRowsToChange($db, $names, $target, $tables, true);
        if ($rows) {
            if (!ofcEnsureUndoDir($undoDir)) throw new RuntimeException('Could not create the undo folder.');
            $file = 'officer-merge-' . date('Ymd-His') . '-' . substr(bin2hex(random_bytes(3)), 0, 6) . '.json';
            $ok = ofcWriteJson($undoDir . '/' . $file, [
                'at' => date('c'), 'by' => $actor, 'target' => $target, 'names' => $names, 'changes' => $rows,
            ]);
            if (!$ok) throw new RuntimeException('Could not write the undo file.');

            $byTable = [];
            foreach ($rows as $r) $byTable[$r['t']][] = $r['id'];
            foreach ($byTable as $key => $ids) {
                $t = $tables[$key];
                foreach (array_chunk($ids, 500) as $chunk) {
                    $marks = implode(',', array_fill(0, count($chunk), '?'));
                    $db->prepare("UPDATE {$t['table']} SET Accountable_Officer = ?
                                   WHERE {$t['live']} AND {$t['id']} IN ({$marks})")
                       ->execute(array_merge([$target], $chunk));
                }
            }
        }
        $db->commit();
    } catch (Throwable $e) {
        if ($db->inTransaction()) $db->rollBack();
        if ($file !== null && is_file($undoDir . '/' . $file)) @unlink($undoDir . '/' . $file);
        throw $e;
    }
    return ['changed' => count($rows), 'undo' => $file];
}

// A file name that is one of ours and nothing else — it arrives from the
// request, so it must never be able to point outside the undo folder.
function ofcUndoFileOk(string $file): bool {
    return (bool)preg_match('/^officer-merge-\d{8}-\d{6}-[0-9a-f]{6}\.json$/', $file);
}

function ofcUndoList(string $undoDir = OFC_UNDO_DIR, int $limit = 15): array {
    $out = [];
    foreach (glob($undoDir . '/officer-merge-*.json') ?: [] as $path) {
        $file = basename($path);
        if (!ofcUndoFileOk($file)) continue;
        $j = json_decode((string)@file_get_contents($path), true);
        if (!is_array($j)) continue;
        $out[] = [
            'file' => $file, 'at' => $j['at'] ?? '', 'by' => $j['by'] ?? '', 'target' => $j['target'] ?? '',
            'names' => $j['names'] ?? [], 'rows' => count($j['changes'] ?? []), 'undone_at' => $j['undone_at'] ?? null,
        ];
    }
    usort($out, fn($a, $b) => strcmp($b['at'], $a['at']));
    return array_slice($out, 0, $limit);
}

// Puts each row back to the spelling it had. A row is only restored if it still
// carries the merged name (exactly): one that has been changed again since — an
// edit, a transfer — is left as it is rather than overwritten with old data.
function ofcUndoMerge(PDO $db, string $file, array $tables = OFC_TABLES, string $undoDir = OFC_UNDO_DIR): array {
    if (!ofcUndoFileOk($file)) throw new InvalidArgumentException('That is not an undo file.');
    $path = $undoDir . '/' . $file;
    $j = is_file($path) ? json_decode((string)file_get_contents($path), true) : null;
    if (!is_array($j) || empty($j['changes'])) throw new InvalidArgumentException('That undo file could not be read.');
    if (!empty($j['undone_at'])) throw new InvalidArgumentException('That merge was already undone.');

    $restored = 0;
    $skipped = 0;
    $db->beginTransaction();
    try {
        foreach ($j['changes'] as $c) {
            $t = $tables[$c['t']] ?? null;
            if (!$t) { $skipped++; continue; }
            $st = $db->prepare("UPDATE {$t['table']} SET Accountable_Officer = ?
                                 WHERE {$t['id']} = ? AND {$t['live']} AND BINARY Accountable_Officer = BINARY ?");
            $st->execute([$c['from'], (int)$c['id'], $j['target']]);
            if ($st->rowCount()) $restored++; else $skipped++;
        }
        $j['undone_at'] = date('c');
        $j['restored'] = $restored;
        $j['skipped'] = $skipped;
        if (!ofcWriteJson($path, $j)) throw new RuntimeException('Could not record the undo.');
        $db->commit();
    } catch (Throwable $e) {
        if ($db->inTransaction()) $db->rollBack();
        throw $e;
    }
    return ['restored' => $restored, 'skipped' => $skipped];
}

// Saved spellings containing $text, with how many current rows each carries —
// for finding a name by hand when it is not one of the suggested groups.
function ofcSearchNames(PDO $db, string $text, int $limit = 40): array {
    $like = '%' . str_replace(['\\', '%', '_'], ['\\\\', '\\%', '\\_'], ofcClean($text)) . '%';
    $st = $db->prepare(
        "SELECT name, SUM(n) AS n FROM (
             SELECT TRIM(Accountable_Officer) AS name, COUNT(*) AS n FROM tbl_inventory_items_details
              WHERE isDeleted = '0' AND Accountable_Officer LIKE ? GROUP BY TRIM(Accountable_Officer)
             UNION ALL
             SELECT TRIM(Accountable_Officer), COUNT(*) FROM tbl_inventory_sub_details
              WHERE isDeleted = 0 AND Accountable_Officer LIKE ? GROUP BY TRIM(Accountable_Officer)
         ) x GROUP BY name ORDER BY name LIMIT " . (int)$limit
    );
    $st->execute([$like, $like]);
    $rows = array_map(fn($r) => ['name' => $r['name'], 'n' => (int)$r['n']], $st->fetchAll(PDO::FETCH_ASSOC));
    return array_values(array_filter($rows, fn($r) => ofcClean($r['name']) !== ''));
}
