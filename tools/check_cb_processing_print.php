<?php
// Read-only checks: no configuration, database connection or saved PO needed.
require_once __DIR__ . '/../system/lib/cb_processing_print.php';

function checkStepPrint(bool $condition, string $message): void {
    if (!$condition) throw new RuntimeException($message);
}

$bid = [
    'bid_number' => 'BID-TEST-001', 'po_number' => 'PO-TEST-001',
    'contract_number' => 'CONTRACT-TEST-001', 'contract_name' => 'Office equipment',
    'supplier_name' => 'Sample Supplier', 'supplier_address' => 'Address 1',
    'supplier_address2' => 'Address 2', 'place_of_delivery' => 'City Hall',
    'delivery_term' => '30', 'representative_name' => 'Supplier Representative',
    'contract_amount' => 999999,
    'items' => [['quantity' => 100, 'unit_cost' => 999, 'total_cost' => 99900]],
];
$step = [
    'step_no' => 2, 'remarks' => 'Second delivery',
    'items' => [
        ['items_description' => "Printer & accessories\n  With warranty", 'unit' => 'unit',
         'quantity' => '2.000', 'unit_cost' => '1500.25', 'total_cost' => '3000.50'],
        ['items_description' => '<Special> cable', 'unit' => 'meter',
         'quantity' => '0.500', 'unit_cost' => '100.00', 'total_cost' => '50.00'],
    ],
];
$linkedPo = [
    'po_number' => 'PO-TEST-001', 'pr_number' => 'PR-TEST-001', 'po_payment_term' => '30 Calendar Days',
    'note' => 'Handle with care', 'supplier_name' => 'Linked Supplier',
    'purpose' => 'For the records office', 'place_of_delivery' => '',
    'items' => [['quantity' => 999, 'total_cost' => 999999]],
    'qr_png' => 'full-po-verification', 'barcode_png' => 'full-po-barcode',
];

$order = cbpBuildPoPrintOrder($bid, $step, $linkedPo);
checkStepPrint(count($order['items']) === 2, 'A step print must contain only its own saved lines.');
checkStepPrint(array_sum(array_column($order['items'], 'total_cost')) === 3050.5, 'Total must come from this step.');
checkStepPrint($order['items'][0]['quantity'] === '2.000', 'Saved quantities must not become contract quantities.');
checkStepPrint($order['items'][1]['quantity'] === '0.500', 'Fractional quantities must be preserved.');
checkStepPrint($order['supplier_name'] === 'Linked Supplier' && $order['pr_number'] === 'PR-TEST-001', 'Linked PO header was lost.');
checkStepPrint($order['place_of_delivery'] === 'City Hall', 'A blank PO field must retain the bid fallback.');
checkStepPrint(!isset($order['qr_png']) && !isset($order['barcode_png']), 'A partial copy must not claim full-PO verification.');
checkStepPrint($order['note'] === '', 'Processing-step PO print must not show an extra note or identifier line.');
checkStepPrint($order['items'][1]['items_description'] === '<Special> cable', 'Descriptions must be preserved for HTML escaping.');

$unlinked = $bid;
$unlinked['po_number'] = '';
$standalone = cbpBuildPoPrintOrder($unlinked, $step);
checkStepPrint($standalone['po_number'] === '', 'Printing must not invent a PO number.');
checkStepPrint($standalone['mode_of_procurement'] === 'Public Bidding', 'Missing bidding procurement mode.');
checkStepPrint($standalone['supplier_address'] === 'Address 1, Address 2', 'Bid address fallback must retain both lines.');

$otherStep = ['step_no' => 3, 'items' => [
    ['items_description' => 'Lump sum', 'unit' => 'lot', 'quantity' => 1, 'unit_cost' => 5000, 'total_cost' => 5000],
]];
$otherOrder = cbpBuildPoPrintOrder($bid, $otherStep, $linkedPo);
checkStepPrint(count($otherOrder['items']) === 1 && $otherOrder['items'][0]['total_cost'] === 5000, 'Another step must not include earlier deliveries.');
checkStepPrint($step['items'][0]['quantity'] === '2.000' && $bid['items'][0]['quantity'] === 100, 'Printing must not modify source data.');

echo "Processing-step PO data checks passed.\n";
