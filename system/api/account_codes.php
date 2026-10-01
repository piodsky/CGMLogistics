<?php
// Account Codes API — the chart of accounts in three levels:
//
//     ref_accountcode_major          MID      "5"            Expenses
//     ref_accountcode_subcategories  SUB_ID   "5-02"         Maintenance and Other Operating Expenses
//     ref_accountcode_details        DID      "5-02-03"      Supplies and Materials Expenses   (heading)
//                                             "5-02-03-010"  Office Supplies Expenses          (account)
//
// The details table mixes two levels: a heading (three segments) and the
// accounts under it (four). Nothing but the code prefix ties an account to its
// heading — there is no column for it — so the heading is derived, never stored.
//
// The tables already existed (created 2026-09-07) and nothing else in this app
// reads them yet; this screen is the first. Purchase Request will pick an
// account from ref_accountcode_details.DID later, which is why the rules below
// care about clean codes now rather than after PRs point at them.
//
// Same audience as Master Data: the System tools flag. Every request, read or
// write, goes through requireSystemToolsAdmin(); when Purchase Request needs to
// read the list it gets its own read-only endpoint.
require_once '../config.php';
requireSystemToolsAdmin();
header('Content-Type: application/json');
$db = getPoDB();
$method = $_SERVER['REQUEST_METHOD'];

// One entry per level. 'pattern' is the shape a code must have on save;
// existing rows that break it are still listed (and flagged), never hidden.
const AC_LEVELS = [
    'major'  => ['table' => 'ref_accountcode_major',         'id' => 'MID',    'parent' => null,     'parentLevel' => null,
                 'pattern' => '/^\d+$/',                      'example' => '5',                      'label' => 'major group'],
    'sub'    => ['table' => 'ref_accountcode_subcategories', 'id' => 'SUB_ID', 'parent' => 'MID',    'parentLevel' => 'major',
                 'pattern' => '/^\d+-\d{2}$/',                'example' => '5-02',                   'label' => 'subcategory'],
    'detail' => ['table' => 'ref_accountcode_details',       'id' => 'DID',    'parent' => 'SUB_ID', 'parentLevel' => 'sub',
                 'pattern' => '/^\d+-\d{2}-\d{2}(-\d{3})?$/', 'example' => '5-02-03 or 5-02-03-010', 'label' => 'account code'],
];

function acLevelOrFail($level): array {
    if (!isset(AC_LEVELS[$level])) jsonResponse(false, 'Unknown level.');
    return AC_LEVELS[$level];
}

// Titles on file carry stray and doubled spaces ("Cash ", " Machinery  and
// Equipment "). Shown cleaned, and saved cleaned, so the same title can never
// look like two different ones.
function acCleanTitle($value): string {
    return trim(preg_replace('/\s+/', ' ', (string)$value));
}

// Live rows of one level as [id => row].
function acRows(PDO $db, array $meta): array {
    $parent = $meta['parent'] ? ", {$meta['parent']} AS parent_id" : ', NULL AS parent_id';
    $rows = $db->query("SELECT {$meta['id']} AS id, Account_Code AS code, Account_Title AS title{$parent}
                        FROM {$meta['table']} WHERE isDeleted = 0 ORDER BY Account_Code, {$meta['id']}")
               ->fetchAll(PDO::FETCH_ASSOC);
    $out = [];
    foreach (fixLegacyEncodingDeep($rows) as $r) {
        $r['id'] = (int)$r['id'];
        $r['parent_id'] = $r['parent_id'] === null ? null : (int)$r['parent_id'];
        $r['code'] = trim((string)$r['code']);
        $r['title'] = acCleanTitle($r['title']);
        $out[$r['id']] = $r;
    }
    return $out;
}

// What is wrong with each row, worked out here so the list, the counts and the
// "Needs attention" filter all read one answer. Informational only — nothing
// is corrected automatically, because which of two duplicate codes is the
// right one is a judgment for a person, not for this file.
function acFlag(array $rows, array $meta, array $parents): array {
    $codeCount = [];
    foreach ($rows as $r) $codeCount[$r['code']] = ($codeCount[$r['code']] ?? 0) + 1;
    foreach ($rows as &$r) {
        $issues = [];
        if (!preg_match($meta['pattern'], $r['code'])) {
            $issues[] = 'Code is not in the ' . $meta['example'] . ' format';
        }
        if ($codeCount[$r['code']] > 1) {
            $issues[] = 'Same code as another ' . $meta['label'];
        }
        if ($meta['parent']) {
            $parent = $parents[$r['parent_id']] ?? null;
            if (!$parent) {
                $issues[] = 'Its ' . AC_LEVELS[$meta['parentLevel']]['label'] . ' is missing';
            } elseif (strpos($r['code'], $parent['code'] . '-') !== 0) {
                $issues[] = 'Code does not start with ' . $parent['code'] . ', the ' . AC_LEVELS[$meta['parentLevel']]['label'] . ' it is filed under';
            }
        }
        $r['issues'] = $issues;
    }
    unset($r);
    return $rows;
}

if ($method === 'GET') {
    try {
        $majors  = acRows($db, AC_LEVELS['major']);
        $subs    = acRows($db, AC_LEVELS['sub']);
        $details = acRows($db, AC_LEVELS['detail']);
    } catch (Throwable $e) {
        error_log('account_codes GET: ' . $e->getMessage());
        jsonResponse(false, 'Unable to load the account codes.');
    }
    $majors  = acFlag($majors, AC_LEVELS['major'], []);
    $subs    = acFlag($subs, AC_LEVELS['sub'], $majors);
    $details = acFlag($details, AC_LEVELS['detail'], $subs);
    // Heading (5-02-03) vs account (5-02-03-010), by segment count.
    foreach ($details as &$d) $d['kind'] = substr_count($d['code'], '-') >= 3 ? 'account' : 'heading';
    unset($d);
    jsonResponse(true, '', [
        'majors'  => array_values($majors),
        'subs'    => array_values($subs),
        'details' => array_values($details),
    ]);
}

// ── Writes ──────────────────────────────────────────────────────────────
$data = getInput();

// The one validator for add and edit. $existing is the row being edited, or
// null for a new one.
function acValidate(PDO $db, array $meta, array $data, ?array $existing): array {
    $code  = trim((string)($data['code'] ?? ''));
    $title = acCleanTitle($data['title'] ?? '');
    if ($code === '')  jsonResponse(false, 'Account Code is required.');
    if ($title === '') jsonResponse(false, 'Account Title is required.');
    if (mb_strlen($code) > 50)   jsonResponse(false, 'Account Code is too long — 50 characters at most.');
    if (mb_strlen($title) > 255) jsonResponse(false, 'Account Title is too long — 255 characters at most.');
    if (!preg_match($meta['pattern'], $code)) {
        jsonResponse(false, "\"{$code}\" is not a valid code here. Use the {$meta['example']} format.");
    }

    // Duplicates are refused outright: once Purchase Request picks accounts by
    // code, two rows answering to one code would make that choice ambiguous.
    $stmt = $db->prepare("SELECT Account_Title FROM {$meta['table']} WHERE Account_Code = ? AND isDeleted = 0"
                       . ($existing ? " AND {$meta['id']} <> ?" : '') . ' LIMIT 1');
    $stmt->execute($existing ? [$code, $existing['id']] : [$code]);
    if (($other = $stmt->fetchColumn()) !== false) {
        jsonResponse(false, "Code {$code} is already used by \"" . acCleanTitle($other) . '".');
    }

    $parentId = null;
    if ($meta['parent']) {
        $parentMeta = AC_LEVELS[$meta['parentLevel']];
        $parentId = (int)($data['parent_id'] ?? 0);
        $stmt = $db->prepare("SELECT Account_Code FROM {$parentMeta['table']} WHERE {$parentMeta['id']} = ? AND isDeleted = 0");
        $stmt->execute([$parentId]);
        $parentCode = $stmt->fetchColumn();
        if ($parentCode === false) jsonResponse(false, 'Choose the ' . $parentMeta['label'] . ' this belongs to.');
        $parentCode = trim((string)$parentCode);

        // A code must sit under the group it is filed in (5-02-03 under 5-02).
        // Checked whenever the code or the group is being set — which is every
        // new row — but not when only the title of an existing row changes, so
        // the handful of rows already filed wrongly can still have a typo fixed
        // before someone decides where they really belong.
        $moving = !$existing || $existing['code'] !== $code || (int)$existing['parent_id'] !== $parentId;
        if ($moving && strpos($code, $parentCode . '-') !== 0) {
            jsonResponse(false, "Code {$code} must start with {$parentCode}-, the {$parentMeta['label']} it is filed under.");
        }
    }
    return [$code, $title, $parentId];
}

// The row being edited or removed, fresh from the table.
function acExisting(PDO $db, array $meta, int $id): array {
    $parent = $meta['parent'] ? ", {$meta['parent']} AS parent_id" : ', NULL AS parent_id';
    $stmt = $db->prepare("SELECT {$meta['id']} AS id, Account_Code AS code, Account_Title AS title{$parent}
                          FROM {$meta['table']} WHERE {$meta['id']} = ? AND isDeleted = 0");
    $stmt->execute([$id]);
    $row = $stmt->fetch(PDO::FETCH_ASSOC);
    if (!$row) jsonResponse(false, 'That entry no longer exists. Reload the page.');
    $row['code'] = trim((string)$row['code']);
    return $row;
}

// How many live entries sit directly under this one. A major group holds
// subcategories, a subcategory holds account codes, and a heading holds the
// accounts whose code starts with it.
function acChildCount(PDO $db, string $level, array $row): int {
    if ($level === 'major') {
        $stmt = $db->prepare('SELECT COUNT(*) FROM ref_accountcode_subcategories WHERE MID = ? AND isDeleted = 0');
        $stmt->execute([$row['id']]);
    } elseif ($level === 'sub') {
        $stmt = $db->prepare('SELECT COUNT(*) FROM ref_accountcode_details WHERE SUB_ID = ? AND isDeleted = 0');
        $stmt->execute([$row['id']]);
    } else {
        if (substr_count($row['code'], '-') >= 3) return 0;   // an account has nothing under it
        $stmt = $db->prepare('SELECT COUNT(*) FROM ref_accountcode_details WHERE Account_Code LIKE ? AND DID <> ? AND isDeleted = 0');
        $stmt->execute([addcslashes($row['code'], '%_\\') . '-%', $row['id']]);
    }
    return (int)$stmt->fetchColumn();
}

// latin1 tables: a character outside it (an emoji, some dashes) is refused by
// MySQL 8's strict mode. Turned into a sentence instead of a generic failure.
function acDbFailure(Throwable $e, string $fallback): void {
    error_log('account_codes: ' . $e->getMessage());
    if (stripos($e->getMessage(), 'Incorrect string value') !== false) {
        jsonResponse(false, 'The title contains a character this table cannot store. Retype it with plain letters and punctuation.');
    }
    jsonResponse(false, $fallback);
}

$level = $data['level'] ?? ($_GET['level'] ?? '');
$meta  = acLevelOrFail($level);

if ($method === 'POST') {
    [$code, $title, $parentId] = acValidate($db, $meta, $data, null);
    try {
        if ($meta['parent']) {
            $db->prepare("INSERT INTO {$meta['table']} (Account_Code, Account_Title, {$meta['parent']}, isDeleted) VALUES (?, ?, ?, 0)")
               ->execute([$code, $title, $parentId]);
        } else {
            $db->prepare("INSERT INTO {$meta['table']} (Account_Code, Account_Title, isDeleted) VALUES (?, ?, 0)")
               ->execute([$code, $title]);
        }
    } catch (Throwable $e) {
        acDbFailure($e, 'Unable to add that entry.');
    }
    logAudit('CREATE_ACCOUNT_CODE', 'account_codes', "Added {$meta['label']} {$code} \"{$title}\"");
    jsonResponse(true, 'Added.');
}

if ($method === 'PUT') {
    $existing = acExisting($db, $meta, (int)($data['id'] ?? 0));
    [$code, $title, $parentId] = acValidate($db, $meta, $data, $existing);
    // Renumbering a group that has entries under it would leave every one of
    // them with a code that no longer starts with it. Move or renumber those
    // first; the title can still be changed freely.
    if ($code !== $existing['code'] && ($n = acChildCount($db, $level, $existing)) > 0) {
        jsonResponse(false, "Code {$existing['code']} has {$n} " . ($n === 1 ? 'entry' : 'entries')
            . ' under it, so it cannot be renumbered. Only its title can be changed.');
    }
    try {
        if ($meta['parent']) {
            $db->prepare("UPDATE {$meta['table']} SET Account_Code = ?, Account_Title = ?, {$meta['parent']} = ? WHERE {$meta['id']} = ?")
               ->execute([$code, $title, $parentId, $existing['id']]);
        } else {
            $db->prepare("UPDATE {$meta['table']} SET Account_Code = ?, Account_Title = ? WHERE {$meta['id']} = ?")
               ->execute([$code, $title, $existing['id']]);
        }
    } catch (Throwable $e) {
        acDbFailure($e, 'Unable to save that entry.');
    }
    logAudit('UPDATE_ACCOUNT_CODE', 'account_codes',
        "Edited {$meta['label']} {$existing['code']} \"" . acCleanTitle($existing['title']) . "\" -> {$code} \"{$title}\"");
    jsonResponse(true, 'Saved.');
}

if ($method === 'DELETE') {
    $existing = acExisting($db, $meta, (int)($_GET['id'] ?? 0));
    if (($n = acChildCount($db, $level, $existing)) > 0) {
        jsonResponse(false, "{$existing['code']} still has {$n} " . ($n === 1 ? 'entry' : 'entries')
            . ' under it. Remove or move those first.');
    }
    // Soft delete, the convention all three tables were built with: the row
    // stays on file, so anything that later points at its ID never dangles.
    try {
        $db->prepare("UPDATE {$meta['table']} SET isDeleted = 1 WHERE {$meta['id']} = ?")->execute([$existing['id']]);
    } catch (Throwable $e) {
        acDbFailure($e, 'Unable to remove that entry.');
    }
    logAudit('DELETE_ACCOUNT_CODE', 'account_codes',
        "Removed {$meta['label']} {$existing['code']} \"" . acCleanTitle($existing['title']) . '"');
    jsonResponse(true, 'Removed.');
}

jsonResponse(false, 'Unsupported request.');
