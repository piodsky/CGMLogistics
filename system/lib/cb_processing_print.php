<?php
require_once __DIR__ . '/competitive_bidding_report.php';

// A processing copy uses the saved step's snapshots, never the full PO's
// items or the quantities currently remaining on the bid.
function cbpBuildPoPrintOrder(array $bid, array $step, array $linkedPo = []): array {
    $order = [
        'po_number' => $bid['po_number'] ?? '',
        'supplier_name' => $bid['supplier_name'] ?? '',
        'supplier_address' => trim(implode(', ', array_filter([
            $bid['supplier_address'] ?? '', $bid['supplier_address2'] ?? '',
        ]))),
        'mode_of_procurement' => 'Public Bidding',
        'pr_number' => '',
        'reference_no' => $bid['contract_number'] ?? '',
        'bac_resolution_no' => '',
        'place_of_delivery' => $bid['place_of_delivery'] ?? '',
        'po_working_days' => $bid['delivery_term'] ?? '',
        'po_payment_term' => '',
        'purpose' => $bid['contract_name'] ?? '',
        'requested_by' => $bid['representative_name'] ?? '',
    ];
    foreach (array_keys($order) as $field) {
        if (trim((string)($linkedPo[$field] ?? '')) !== '') $order[$field] = $linkedPo[$field];
    }
    $order['note'] = '';
    $order['items'] = array_map(static fn($item) => [
        'items_description' => $item['items_description'] ?? '',
        'unit' => $item['unit'] ?? '',
        'quantity' => $item['quantity'],
        'unit_cost' => $item['unit_cost'],
        'total_cost' => $item['total_cost'],
    ], $step['items'] ?? []);
    return $order;
}

function cbpGetPoPrintData(PDO $db, int $cbId, array $step): array {
    $stmt = $db->prepare('SELECT * FROM tbl_competitive_bidding WHERE ID = ? AND isDeleted = 0');
    $stmt->execute([$cbId]);
    $row = $stmt->fetch(PDO::FETCH_ASSOC);
    if (!$row) throw new RuntimeException('Bid document not found.');
    $bid = array_change_key_case($row, CASE_LOWER);
    $bidNumber = (string)($bid['bid_number'] ?? '');

    $po = [];
    $poNumber = trim((string)($bid['po_number'] ?? ''));
    if ($poNumber !== '') {
        // Only the header is reused. A step is a partial processing copy, so
        // the full PO's items and verification QR must not be attached to it.
        $stmt = $db->prepare(
            'SELECT PO_Number AS po_number, Supplier_Name AS supplier_name,
                    Supplier_Address AS supplier_address, Mode_of_Procurement AS mode_of_procurement,
                    PR_Number AS pr_number, Reference_No AS reference_no, BAC_Resolution_No AS bac_resolution_no,
                    Place_of_Delivery AS place_of_delivery, PO_Working_Days AS po_working_days,
                    PO_Payment_Term AS po_payment_term, Purpose AS purpose, Requested_by AS requested_by
             FROM tbl_purchase_order WHERE PO_Number = ? AND isDeleted = 0'
        );
        $stmt->execute([$poNumber]);
        $po = $stmt->fetch(PDO::FETCH_ASSOC) ?: [];
    }
    return fixLegacyEncodingDeep([
        'order' => cbpBuildPoPrintOrder($bid, $step, $po),
        'bid_number' => $bidNumber,
        'step_no' => (int)$step['step_no'],
        'processing_date' => $step['processing_date'] ?? '',
        'signatory_name' => trim((string)($bid['signatory_name'] ?? '')) ?: CB_REPORT_DEFAULTS['signatory_name'],
        'signatory_title' => trim((string)($bid['signatory_title'] ?? '')) ?: CB_REPORT_DEFAULTS['signatory_title'],
    ]);
}
