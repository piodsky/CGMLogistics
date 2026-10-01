<?php
// CLI-only checks and a local browser fixture made from the actual report code.
if (PHP_SAPI !== 'cli') { http_response_code(403); exit; }
require_once __DIR__ . '/../system/lib/competitive_bidding_report.php';

function cbCheck($pass, $message) {
    if (!$pass) throw new RuntimeException($message);
}
$guide = json_decode(file_get_contents(__DIR__ . '/../system/database/sample_competitive_bidding.json'), true);
$guide['bid_number'] = '2026-055';
$blank = cbWithReportDefaults(['contract_amount' => '27,020,435.50']);
cbCheck($blank['cash_bond_amount'] === 1351021.78, 'Cash bond calculation/rounding');
cbCheck($blank['surety_bond_amount'] === 8106130.65, 'Surety bond calculation/rounding');
cbCheck($blank['signatory_name'] === CB_REPORT_DEFAULTS['signatory_name'], 'Mayor default');
cbCheck($blank['witness_entity_name'] === CB_REPORT_DEFAULTS['witness_entity_name'], 'Witness default');
$saved = cbWithReportDefaults(['contract_amount' => 10000, 'cash_bond_amount' => 0,
    'surety_bond_amount' => '2500.00', 'signatory_name' => 'Recorded signatory']);
cbCheck($saved['cash_bond_amount'] === 0 && $saved['surety_bond_amount'] === '2500.00', 'Keep recorded bond amounts');
cbCheck($saved['signatory_name'] === 'Recorded signatory', 'Keep recorded signatory');
$fallback = cbWithReportDefaults(['items' => [['total_cost' => '2,000.00']]]);
cbCheck($fallback['cash_bond_amount'] === 100.0, 'Items-total fallback');
$empty = cbWithReportDefaults([]);
cbCheck(!isset($empty['cash_bond_amount']), 'Unknown price must not invent a bond amount');

$photo = [
    'bid_number' => '2026-130', 'contract_number' => '2026-167',
    'contract_name' => 'PROCUREMENT OF VARIOUS GROCERY ITEM FOR PAMASKONG HANDOG 2026',
    'contract_location' => "CITY MAYOR'S OFFICE", 'supplier_name' => 'CENTRO SUPERSALES, INCORPORATED',
    'supplier_address' => 'Agrosite, South Poblacion, Maramag, Bukidnon',
    'supplier_org_place' => 'Maramag, Bukidnon', 'representative_name' => 'MAELENE M. TING',
    'representative_title' => 'Authorized Representative', 'contract_amount' => '27020435.50',
    'bid_date' => '2026-08-26', 'place_of_delivery' => 'Various Barangays', 'delivery_term' => '30 C.D.',
    'items' => [[
        'quantity' => '36750', 'unit' => 'Package', 'unit_cost' => '735.25', 'total_cost' => '27020437.50',
        'items_description' => "1 LOT\nPamaskong Handog Package\n\nEach package contains:\n5 Kilos Well Milled Rice 90% long grain (Local Rice) (Pack in a customized sack)\n2-cans Beef Loaf 150-155 grams (Premium Quality)\n2-cans Corned beef 150-155 grams (Premium Quality)\n1-pack Instant Pure Coffee powder 40 grams\n1-pack Brown Sugar 1 kilo well-packed\n8-pieces Instant Noodles (4 beef, 4 chicken flavor)\n2-cans Sardines 155 grams (Premium)\n1-pack Powdered milk Fortified 135 grams\nNote: Each package includes the costs of markings, packing, and delivery. See the attached box lay-out\nDelivery Site: Various Barangay",
    ]],
];
$source = file_get_contents(__DIR__ . '/../system/competitive_bidding.php');
preg_match('/<style>(.*?)<\/style>/s', $source, $style);
function cbJsFunction($source, $name) {
    if (!preg_match('/^function ' . preg_quote($name, '/') . '\([^\r\n]*\).*?^\}/ms', $source, $match)) {
        throw new RuntimeException('Function not found: ' . $name);
    }
    return $match[0];
}
$start = strpos($source, 'function numberToWords(');
$end = strpos($source, '// ── Preview / PDF', $start);
$script = 'const CB_BOND_RATES = ' . json_encode(CB_REPORT_BOND_RATES) . ';' . "\n"
    . 'const CB_REPORT_DEFAULTS = ' . json_encode(CB_REPORT_DEFAULTS) . ';' . "\n";
foreach (['escapeHtml', 'parseMoney', 'formatMoney', 'bidWithReportDefaults'] as $name) {
    $script .= cbJsFunction($source, $name) . "\n";
}
$script .= substr($source, $start, $end - $start) . "\n";
$script .= cbJsFunction($source, 'drawContractFurniture') . "\n";
preg_match('/const CB_FOOTER_BAND_PT = [^;]+;/', $source, $footerBand);
$script .= $footerBand[0] . "\n";
preg_match('/^async function generateBidPdf\(.*?^\}/ms', $source, $generator);
cbCheck(!empty($generator[0]), 'PDF generator must be present');
$script .= $generator[0] . "\n";
$script .= "let cbPdfBlobUrl = null, cbPdfFileName = ''; const CB_PDF_VIEW_PARAMS = '';\n";
$script .= 'const fixtures = ' . json_encode(['guide' => $guide, 'photo' => $photo], JSON_HEX_TAG | JSON_HEX_AMP) . ';';
$script .= <<<'JS'

window.reportResults = [];
window.reportFailure = '';
window.runReportChecks = async function() {
    try {
        for (const [fixture, bid] of Object.entries(fixtures)) {
            for (const type of ['noa', 'contract', 'ntp']) {
                const doc = CB_DOCUMENTS[type];
                const host = document.getElementById('cbPrintSource');
                host.innerHTML = doc.build(bid);
                await document.fonts.ready;
                await Promise.all([...host.querySelectorAll('img')].map(img => img.decode()));
                const source = host.querySelector(doc.selector);
                const beforeHeight = source.offsetHeight;
                const width = source.offsetWidth;
                const fontSize = getComputedStyle(source).fontSize;
                const bodyText = source.textContent;
                cbPdfBlobUrl = null;
                await generateBidPdf(bid, doc);
                if (!cbPdfBlobUrl) throw new Error(document.getElementById('cbPdfLoading').textContent);
                const blob = await fetch(cbPdfBlobUrl).then(r => r.blob());
                const data = await new Promise(resolve => { const reader = new FileReader(); reader.onload = () => resolve(reader.result.split(',')[1]); reader.readAsDataURL(blob); });
                reportResults.push({fixture, type, width, beforeHeight, fontSize, bodyText,
                    pageCountText: source.querySelector('.cp-page-count')?.textContent || '', data});
            }
        }
        return reportResults.map(({data, bodyText, ...metadata}) => metadata);
    } catch (error) {
        reportFailure = error.stack || error.message;
        throw error;
    }
};
JS;
$out = __DIR__ . '/rpt_reports/competitive-check';
if (!is_dir($out)) mkdir($out, 0777, true);
$base = 'file:///' . str_replace('\\', '/', realpath(__DIR__ . '/../system')) . '/';
$html = '<!doctype html><html><head><meta charset="UTF-8"><base href="' . htmlspecialchars($base) . '">'
    . '<link rel="stylesheet" href="vendor/css/bootstrap.min.css"><style>' . $style[1] . '</style></head><body>'
    . '<div id="cbPrintSource"></div><div id="cbPdfLoading"></div><iframe id="cbPdfFrame"></iframe>'
    . '<button id="cbPdfDownload"></button><button id="cbPdfPrint"></button><button id="cbPdfOpenTab"></button>'
    . '<script src="vendor/js/html2canvas.min.js"></script><script src="vendor/js/jspdf.umd.min.js"></script>'
    . '<script src="js/print-pdf.js"></script><script>' . $script . '</script></body></html>';
file_put_contents($out . '/preview.html', $html);
file_put_contents($out . '/report-code.js', $script);
echo "8 PHP report checks passed. Browser fixture: $out/preview.html\n";
