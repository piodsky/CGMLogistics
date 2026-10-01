<?php
// Reads a CSV/.xlsx of item rows for the Purchase Request grid and hands them
// back for PREVIEW — nothing is saved here. The person sees the rows in the
// grid, with any warnings, and only saves when they are happy with them. The
// rules live in lib/item_import.php.
require_once '../config.php';
require_once __DIR__ . '/lib_spreadsheet.php';
require_once '../lib/item_import.php';
requireAuth();
requireModule('purchase_requests');
header('Content-Type: application/json');

if ($_SERVER['REQUEST_METHOD'] !== 'POST') jsonResponse(false, 'Only POST is allowed.');
if (empty($_FILES['file']) || $_FILES['file']['error'] !== UPLOAD_ERR_OK) {
    jsonResponse(false, 'Please upload a valid Excel or CSV file.');
}
// A list of a thousand items is well under this; anything bigger is not an item list.
if ($_FILES['file']['size'] > 2 * 1024 * 1024) jsonResponse(false, 'That file is too large (2 MB at most).');

$rows = readSpreadsheetRows($_FILES['file']);
if ($rows === null) jsonResponse(false, 'Only .xlsx and .csv files are supported, and the file must be readable.');

try {
    $units = getPoDB()->query('SELECT Unit_Name FROM ref_unit WHERE isDeleted = 0')->fetchAll(PDO::FETCH_COLUMN);
} catch (Throwable $e) {
    $units = [];       // a missing list only costs the unit warning
}

try {
    $result = itemImportParse($rows, fixLegacyEncodingDeep($units));
} catch (InvalidArgumentException $e) {
    jsonResponse(false, $e->getMessage());
}
jsonResponse(true, count($result['items']) . ' item(s) read.', $result);
