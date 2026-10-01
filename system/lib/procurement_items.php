<?php
// Item helpers shared by every module that keeps a PO-shaped item grid.
//
// MOVED, not rewritten: these four functions were defined inside
// api/purchase_orders.php, which cannot be included from anywhere else without
// also running that API. The bodies below are byte-for-byte what they were
// there, so the Purchase Order screen behaves exactly as before; it now
// require_once's this file. The Purchase Request module is the second user.
//
// Not moved, on purpose: parseDate()/parseDateOnly() and formatPoNumber(),
// which are PO-specific or trivial, and the Competitive Bidding copies (cb*),
// which that module owns.

// True when the column is actually on the table.
//
// Date_PR is added by database/tracking_dates_and_remarks.sql, and that
// migration REBUILDS tbl_purchase_order — 26,000+ rows, write-locked for the
// duration — so it has to wait for a maintenance window. Without this guard,
// deploying the code before that window breaks every Purchase Order screen
// with "Unknown column 'Date_PR'", which is a far worse outcome than the field
// simply not being there yet. Once the migration has run the guard is true
// forever and costs one cached SHOW COLUMNS per request.
//
// $table is a compile-time constant at every call site, never request data —
// which matters, because a table name cannot be a bound parameter and so is
// interpolated.
//
// The whole column list is fetched and matched in PHP rather than asking the
// server to filter it with `SHOW COLUMNS ... LIKE ?`. That form looks tidier
// and does not work here: MySQL 5.5 will not accept a placeholder in a SHOW
// statement, and with PDO::ATTR_EMULATE_PREPARES off there is no client-side
// substitution to paper over it, so prepare() throws a 1064 syntax error. The
// catch below then returned false for a column that was plainly there, which
// silently switched Date_PR off across the whole screen AFTER the migration
// adding it had already been run — a failure with no symptom except the field
// quietly never saving.
//
// Matched case-insensitively because MySQL column names are, and one cached
// round trip per table per request is the entire cost.
function tableHasColumn(PDO $db, string $table, string $column): bool {
    static $cache = [];
    if (!isset($cache[$table])) {
        try {
            $cols = $db->query("SHOW COLUMNS FROM `{$table}`")->fetchAll(PDO::FETCH_COLUMN, 0);
            $cache[$table] = array_change_key_case(array_flip($cols), CASE_LOWER);
        } catch (Throwable $e) {
            error_log('tableHasColumn failed for ' . $table . ': ' . $e->getMessage());
            $cache[$table] = [];
        }
    }
    return isset($cache[$table][strtolower($column)]);
}

// Rejects item rows that are neither a title nor a priced item — see the note
// on invalidItemRows() in purchase_orders.php for why Total Cost is the test
// and Quantity/Unit Cost are not. Enforced here as well as in the browser
// because the browser is not a place to enforce anything.
// Returns ['item' => [row numbers], 'title' => [row numbers]] for rows that do
// not match the type they were saved as. See invalidItemRows() in
// purchase_orders.php for why the declared type is used and not inferred.
//
// is_title arrives from the item grid. When it is absent — an older client, or
// any caller that does not send it — the shape is inferred as before, so
// nothing that used to save starts failing.
function invalidItemRowNumbers(array $items): array {
    $bad = ['item' => [], 'title' => []];
    foreach ($items as $i => $item) {
        if (trim((string)($item['items_description'] ?? '')) === '') continue;
        $num  = fn($k) => (float)($item[$k] ?? 0);
        $unit = trim((string)($item['unit'] ?? ''));
        $blank = $unit === '' && $num('quantity') == 0 && $num('unit_cost') == 0 && $num('total_cost') == 0;

        $isTitle = array_key_exists('is_title', $item) ? (bool)$item['is_title'] : $blank;
        if ($isTitle) {
            if (!$blank) $bad['title'][] = $i + 1;
        } elseif ($num('total_cost') == 0) {
            $bad['item'][] = $i + 1;
        }
    }
    return $bad;
}

function rejectInvalidItems(array $items): void {
    $bad = invalidItemRowNumbers($items);
    if (!$bad['item'] && !$bad['title']) return;
    $list = function (array $rows) {
        $shown = array_slice($rows, 0, 6);
        return '#' . implode(', #', $shown)
             . (count($rows) > 6 ? ' and ' . (count($rows) - 6) . ' more' : '');
    };
    $parts = [];
    if ($bad['item']) {
        $parts[] = 'Row ' . $list($bad['item']) . ' has no Total Cost. Fill in Qty and Unit Cost, '
                 . 'enter a Total Cost directly, or make it a LOT / title row.';
    }
    if ($bad['title']) {
        $parts[] = 'Row ' . $list($bad['title']) . ' is a LOT / title row but still carries a unit or a cost.';
    }
    jsonResponse(false, implode(' ', $parts));
}

function ensureCatalogItem(PDO $db, array $item) {
    $stmt = $db->prepare('SELECT Item_ID FROM tbl_ppmp_bac_items WHERE Items_Description = ? AND isDeleted = 0 LIMIT 1');
    $stmt->execute([$item['items_description']]);
    $existing = $stmt->fetch(PDO::FETCH_ASSOC);
    if ($existing) {
        return $existing['Item_ID'];
    }
    $stmt = $db->prepare('INSERT INTO tbl_ppmp_bac_items (Unit, Items_Description, Unit_Cost, isDeleted) VALUES (?, ?, ?, 0)');
    $stmt->execute([$item['unit'], $item['items_description'], $item['unit_cost']]);
    return $db->lastInsertId();
}
