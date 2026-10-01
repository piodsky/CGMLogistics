<?php
// Names and bond percentages supplied in database/competitive bidding.pdf.
// representative_salutation is the neutral greeting for the Notice of Award and
// Notice to Proceed, so bids saved before that column existed still read right.
const CB_REPORT_DEFAULTS = [
    'representative_salutation' => 'Sir/Madam',
    'signatory_name' => 'JAY WARREN R. PABILLARAN',
    'signatory_title' => 'City Mayor',
    'witness_entity_name' => 'BELLA STEPHANIE B. SAAVEDRA',
    'witness_entity_title' => 'City Treasurer',
];
const CB_REPORT_BOND_RATES = ['cash_bond_amount' => 0.05, 'surety_bond_amount' => 0.30];

function cbReportNumber($value): float {
    return (float)preg_replace('/[^0-9.\-]/', '', (string)$value);
}

function cbWithReportDefaults(array $bid): array {
    // Fill missing names; retain explicitly recorded signatories on older bids.
    foreach (CB_REPORT_DEFAULTS as $field => $value) {
        if (trim((string)($bid[$field] ?? '')) === '') $bid[$field] = $value;
    }
    $price = cbReportNumber($bid['contract_amount'] ?? 0);
    if ($price <= 0) {
        $price = array_sum(array_map(
            fn($item) => cbReportNumber($item['total_cost'] ?? 0), $bid['items'] ?? []
        ));
    }
    foreach (CB_REPORT_BOND_RATES as $field => $rate) {
        if (trim((string)($bid[$field] ?? '')) === '' && $price > 0) {
            $bid[$field] = round($price * $rate, 2);
        }
    }
    return $bid;
}
