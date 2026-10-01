<?php
// Item-list import for the Purchase Request grid.
//
// Takes the ROWS of a CSV/.xlsx (already read by readSpreadsheetRows(), so this
// file never touches the filesystem and can be tested with plain arrays) and
// returns clean item rows plus any warnings, or throws one message naming every
// row that is wrong.
//
// The same shape rules as the Purchase Order and Competitive Bidding importers —
// header row, only Description required, columns matched by name in any order,
// and every row is either a TITLE row (LOT / heading, every number blank) or an
// ITEM row (priced) — written once here instead of pasted a third time into
// another 168-line parser. Those two are left alone; move them onto this when
// there is a reason to touch them.
//
// What a Purchase Request adds: it is an ESTIMATE, so there is no Number Series,
// and a row whose Total does not equal Qty x Unit Cost is rejected rather than
// quietly reconciled — a stale total pasted from a spreadsheet is exactly the
// mistake an estimate must not carry into an approval.
require_once __DIR__ . '/pr_workflow.php';

// Recognised header (lower-cased) => item field. '_ignore' is accepted and
// dropped: an "Item No." column is common in a spreadsheet and harmless, since
// the number on the form is the row's own position.
const ITEM_IMPORT_HEADERS = [
    'description' => 'items_description', 'item description' => 'items_description',
    'items description' => 'items_description', 'specification' => 'items_description',
    'specifications' => 'items_description', 'item description / specifications' => 'items_description',
    'unit' => 'unit', 'units' => 'unit', 'uom' => 'unit',
    'qty' => 'quantity', 'quantity' => 'quantity',
    'unit cost' => 'unit_cost', 'estimated unit cost' => 'unit_cost', 'est. unit cost' => 'unit_cost',
    'est unit cost' => 'unit_cost', 'unit price' => 'unit_cost',
    'total cost' => 'total_cost', 'total' => 'total_cost', 'estimated total' => 'total_cost',
    'estimated total cost' => 'total_cost', 'est. total cost' => 'total_cost', 'amount' => 'total_cost',
    'stock no' => 'stock_no', 'stock no.' => 'stock_no', 'stock number' => 'stock_no', 'stock/property no.' => 'stock_no',
    'remarks' => 'remarks', 'remark' => 'remarks',
    'item no' => '_ignore', 'item no.' => '_ignore', 'no' => '_ignore', 'no.' => '_ignore', '#' => '_ignore',
];

// $rows: the file's rows, header first. $knownUnits: ref_unit names, to warn
// (never reject) on a unit nobody has used before. Returns
// ['items' => [...], 'warnings' => [...]].
function itemImportParse(array $rows, array $knownUnits = []): array {
    if (count($rows) < 1) throw new InvalidArgumentException('The file appears to be empty.');

    // Excel writes a spare trailing comma often enough that a good file arrives one column wider than it looks.
    $header = array_map('normalizeHeader', trimTrailingEmptyCells($rows[0]));
    $map = [];
    $unknown = [];
    foreach ($header as $i => $name) {
        if ($name === '') continue;
        $field = ITEM_IMPORT_HEADERS[strtolower($name)] ?? null;
        if ($field === null) { $unknown[] = $name; continue; }
        if ($field !== '_ignore') $map[$field] = $i;
    }
    $quote = fn(array $a) => implode(', ', array_map(fn($v) => '"' . $v . '"', $a));
    if (!isset($map['items_description'])) {
        $found = $quote(array_values(array_filter($header, fn($v) => $v !== '')));
        throw new InvalidArgumentException('The file needs a "Description" column — that is the only one required. '
            . ($found !== '' ? 'Found instead: ' . $found . '.' : 'The first row was empty.')
            . ' Unit, Qty, Unit Cost, Total Cost, Stock No and Remarks are optional and can be in any order.');
    }
    if ($unknown) {
        throw new InvalidArgumentException('These column headers were not recognised: ' . $quote($unknown)
            . '. Use any of: "Description", "Unit", "Qty", "Unit Cost", "Total Cost", "Stock No", "Remarks".');
    }
    if (count($rows) < 2) throw new InvalidArgumentException('The file has a header row but no item rows below it.');

    $cell = function (array $row, string $field) use ($map): string {
        return isset($map[$field]) ? trim((string)($row[$map[$field]] ?? '')) : '';
    };
    $known = [];
    foreach ($knownUnits as $u) $known[mb_strtolower(trim((string)$u))] = true;

    $items = [];
    $errors = [];
    $warnUnits = [];
    $seen = [];
    $dupes = [];
    foreach (array_slice($rows, 1) as $index => $row) {
        $row = trimTrailingEmptyCells($row);
        if (!$row) continue;                                           // a fully blank row
        $n = $index + 2;                                               // as numbered in the spreadsheet

        $desc = $cell($row, 'items_description');
        if ($desc === '') { $errors[] = "Row {$n} has no Description."; continue; }

        $unit = $cell($row, 'unit');
        $qtyRaw = $cell($row, 'quantity');
        $costRaw = $cell($row, 'unit_cost');
        $totRaw = $cell($row, 'total_cost');
        $qty = prNum($qtyRaw); $cost = prNum($costRaw); $tot = prNum($totRaw);
        if ($qty === false || $cost === false || $tot === false) { $errors[] = "Row {$n} has a Qty, Unit Cost or Total that is not a number."; continue; }
        if (($qty !== null && $qty < 0) || ($cost !== null && $cost < 0) || ($tot !== null && $tot < 0)) { $errors[] = "Row {$n} has a negative number."; continue; }

        $isTitle = $unit === '' && $qtyRaw === '' && $costRaw === '' && $totRaw === '';
        if (!$isTitle) {
            // Only demand a column the file provides: a deliberately Unit-less
            // file is the author's choice, a half-empty Unit column is not.
            $lump = $totRaw !== '' && $qtyRaw === '' && $costRaw === '';
            $missing = [];
            if (!$lump) {
                foreach (['unit' => 'Unit', 'quantity' => 'Qty', 'unit_cost' => 'Unit Cost'] as $field => $label) {
                    if (isset($map[$field]) && $cell($row, $field) === '') $missing[] = $label;
                }
            }
            if ($missing) {
                $errors[] = "Row {$n} is missing " . implode(', ', $missing)
                    . '. An item row needs Unit, Qty and Unit Cost (or just a Total Cost for a lump sum) — leave every one of them blank if it is a title row like "LOT 1".';
                continue;
            }
            if ($qty && $cost && $tot && abs($tot - round($qty * $cost, 2)) > 0.01) {
                $errors[] = "Row {$n}: Total Cost " . number_format($tot, 2) . ' is not Qty x Unit Cost (' . number_format(round($qty * $cost, 2), 2) . '). Fix the figure, or leave Total Cost blank and it is worked out for you.';
                continue;
            }
            if ($qty && $cost) $tot = round($qty * $cost, 2);
            if (!$tot) { $errors[] = "Row {$n} has no Total Cost."; continue; }
            if ($known && $unit !== '' && !isset($known[mb_strtolower($unit)])) $warnUnits[$unit] = true;
            $key = mb_strtolower($desc) . '|' . mb_strtolower($unit);
            if (isset($seen[$key])) $dupes[] = $n; else $seen[$key] = true;
        }

        $money = fn($v) => $v === null ? '' : number_format($v, 2, '.', '');
        $items[] = [
            'is_title'          => $isTitle,
            'stock_no'          => $cell($row, 'stock_no'),
            'unit'              => $unit,
            'items_description' => $desc,
            'quantity'          => $qty === null ? '' : rtrim(rtrim(number_format($qty, 3, '.', ''), '0'), '.'),
            'unit_cost'         => $money($cost),
            'total_cost'        => $isTitle ? '' : $money($tot),
            'remarks'           => $cell($row, 'remarks'),
        ];
        if (count($items) > PR_MAX_ITEMS) { $errors[] = 'The file has more than ' . PR_MAX_ITEMS . ' item rows.'; break; }
    }

    if ($errors) {
        $shown = array_slice($errors, 0, 8);
        throw new InvalidArgumentException('Nothing was imported. ' . implode(' ', $shown)
            . (count($errors) > 8 ? ' …and ' . (count($errors) - 8) . ' more problem(s).' : ''));
    }
    if (!$items) throw new InvalidArgumentException('No item rows were found in the file.');

    $warnings = [];
    if ($warnUnits) $warnings[] = 'Unit not seen before: ' . implode(', ', array_keys($warnUnits)) . '. Check the spelling.';
    if ($dupes)     $warnings[] = prRowList($dupes) . ' repeat an earlier row (same description and unit).';
    return ['items' => $items, 'warnings' => $warnings];
}
