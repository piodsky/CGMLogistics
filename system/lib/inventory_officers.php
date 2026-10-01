<?php
// Inventory custodians: turning the hand-typed Accountable_Officer text into people.
// Shared by api/inventory.php (Personnel MR) and api/officer_cleanup.php (Fix Names).
// Functions only — including this file has no side effects.

// ── One person, however many ways their name was typed ───────────────────
// Accountable_Officer is free text, typed by hand, so the same custodian is
// saved as "MERLE C. VERGARA" on 74 items and "MERLE C.VERGARA" on 26 more —
// and a lookup on either spelling alone silently misses the other's items.
// Measured live: 39 people were split like this.
//
// Two saved names are treated as one person when, ignoring case, dots, commas
// and word order, they are the same words — or the same words plus a middle
// INITIAL on one side ("PEARL ANGELI P. LAYAGAN" / "PEARL ANGELI LAYAGAN").
// Deliberately NOT merged: a different initial ("RAUL S. LACHICA" vs
// "RAUL P. LACHICA"), because that could be two people and an MR that claims
// property for the wrong one is worse than one that shows a name twice.
function invNameTokens(string $name): array {
    $tokens = preg_split('/\s+/u', trim(str_replace(['.', ','], ' ', mb_strtoupper($name))), -1, PREG_SPLIT_NO_EMPTY);
    sort($tokens, SORT_STRING);
    return $tokens;
}

function invSameOfficer(array $a, array $b): bool {
    if ($a === $b) return true;
    [$short, $long] = count($a) <= count($b) ? [$a, $b] : [$b, $a];
    $extra = $long;
    foreach ($short as $token) {                 // every word of the shorter must be in the longer
        $at = array_search($token, $extra, true);
        if ($at === false) return false;
        unset($extra[$at]);
    }
    foreach ($extra as $token) {                 // ...and what is left over must only be initials
        if (mb_strlen($token) > 1) return false;
    }
    return true;
}

// Every distinct saved Accountable Officer with how many items sit under it,
// grouped into people. Each group: 'rep' (the most-used spelling, what the
// picklist shows), 'members' (every saved spelling), 'counts' (spelling => items
// under it), 'n' (items in total).
// Both tables that carry the column are read — a person can be custodian of a
// single unit — and only rows whose PO is still on file, like the lookup.
function invOfficerGroups(PDO $db): array {
    $rows = $db->query(
        "SELECT name, SUM(n) AS n FROM (
             SELECT TRIM(d.Accountable_Officer) AS name, COUNT(*) AS n
               FROM tbl_inventory_items_details d
               JOIN tbl_inventory h ON h.PO_Number = d.PO_Number AND h.isDeleted = '0'
              WHERE d.isDeleted = '0' AND TRIM(COALESCE(d.Accountable_Officer,'')) <> ''
              GROUP BY TRIM(d.Accountable_Officer)
             UNION ALL
             SELECT TRIM(s.Accountable_Officer), COUNT(*)
               FROM tbl_inventory_sub_details s
               JOIN tbl_inventory h ON h.PO_Number = s.PO_Number AND h.isDeleted = '0'
              WHERE s.isDeleted = 0 AND TRIM(COALESCE(s.Accountable_Officer,'')) <> ''
              GROUP BY TRIM(s.Accountable_Officer)
         ) x GROUP BY name ORDER BY n DESC, name"
    )->fetchAll(PDO::FETCH_ASSOC);

    $groups = [];
    foreach ($rows as $r) {
        $name = $r['name'];
        // MySQL's TRIM() strips spaces only, so a value made of tabs (one live
        // row is exactly that — items with no real officer behind them) gets
        // through the query. The MR lookup trims tabs too, so such a name
        // could be suggested but never opened; skip anything with no letters.
        if (trim($name, " \t\n\r\0\x0B\xC2\xA0") === '') continue;
        $tokens = invNameTokens($name);
        foreach ($groups as $i => $g) {
            if (invSameOfficer($g['tokens'], $tokens)) {
                $groups[$i]['members'][] = $name;
                $groups[$i]['counts'][$name] = (int)$r['n'];
                $groups[$i]['n'] += (int)$r['n'];
                continue 2;
            }
        }
        // Rows arrive most-used first, so the first spelling met is the rep.
        $groups[] = ['rep' => $name, 'tokens' => $tokens, 'members' => [$name],
                     'counts' => [$name => (int)$r['n']], 'n' => (int)$r['n']];
    }
    return $groups;
}
