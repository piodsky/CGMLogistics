/* ── Purchase Request printout — build the form's HTML ──────────────────────
 *
 * Builds the printed Purchase Request from the object api/purchase_requests.php
 * returns for one PR. Layout and look are in css/pr-print.css, which follows the
 * Purchase Order printout; this file only decides what goes in each slot.
 * Rasterising and paginating is js/print-pdf.js, the pipeline the PO print uses.
 *
 * Amount in words is delegated to IarPrint.amountInWords (js/iar-print.js), the
 * same way js/dv-print.js does it, so there is one implementation of it.
 * ──────────────────────────────────────────────────────────────────────────── */
(function (global) {
    'use strict';

    function esc(s) {
        return String(s == null ? '' : s).replace(/[&<>"']/g, c =>
            ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[c]));
    }

    function money(v) {
        const n = parseFloat(v);
        return isFinite(n) ? n.toLocaleString('en-PH', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) : '';
    }

    // The item columns are decimal(12,3), so four boxes arrive as "4.000".
    // Trailing zeros drop, a genuine fraction stays (30.500 -> 30.5). No
    // thousands separator: quantities never had one on the PO form either.
    function qty(v) {
        const n = parseFloat(v);
        return (v === null || v === undefined || String(v).trim() === '' || !isFinite(n)) ? '' : String(parseFloat(n.toFixed(3)));
    }

    function longDate(v) {
        if (!v) return '';
        const d = new Date(String(v).replace(' ', 'T'));
        return isNaN(d) ? '' : d.toLocaleDateString('en-PH', { year: 'numeric', month: 'long', day: 'numeric' });
    }

    // A zero in a title row's numeric cells means "nothing here", and the form
    // leaves the cell empty rather than papering the page with 0.00. Values
    // arrive as strings, and "0.000" is a truthy string — so compare the number.
    function has(v) {
        return v !== null && v !== undefined && String(v).trim() !== '' && parseFloat(v) !== 0;
    }

    function descriptionLines(v) {
        const clean = String(v == null ? '' : v).replace(/[ \t]+$/gm, '').replace(/\s+$/, '');
        return clean.split('\n').map(line => `<div class="prp-dl">${esc(line) || '&nbsp;'}</div>`).join('');
    }

    // The department line reads "OFFICE" or "OFFICE / DIVISION / SECTION",
    // whichever parts exist.
    function officeLine(pr) {
        return [pr.department, pr.division, pr.section].map(s => String(s || '').trim()).filter(Boolean).join(' / ');
    }

    function itemRows(items, showStock) {
        let no = 0;
        return items.map(it => {
            if (it.is_title) {
                return `<tr class="prp-title-row"><td class="center"></td><td></td><td></td>`
                     + (showStock ? '<td></td>' : '')
                     + `<td class="desc">${descriptionLines(it.items_description)}</td><td></td><td></td><td></td></tr>`;
            }
            no++;
            return `<tr>
                <td class="center">${no}</td>
                <td class="center">${esc(qty(it.quantity))}</td>
                <td class="center">${esc(it.unit)}</td>
                ${showStock ? `<td class="center">${esc(it.stock_no)}</td>` : ''}
                <td class="desc">${descriptionLines(it.items_description)}</td>
                <td class="num">${has(it.unit_cost) ? money(it.unit_cost) : ''}</td>
                <td class="num">${has(it.total_cost) ? money(it.total_cost) : ''}</td>
                <td class="remarks">${esc(it.remarks)}</td>
            </tr>`;
        }).join('');
    }

    function signBlock(role, name, title) {
        return `<td>
            <div class="prp-sign-role">${esc(role)}</div>
            <div class="prp-sign-line"></div>
            <div class="prp-sign-name">${esc(name) || '&nbsp;'}</div>
            <div class="prp-sign-title">${esc(title) || '&nbsp;'}</div>
            <div class="prp-sign-date">Date:</div>
        </td>`;
    }

    // pr: the object from api/purchase_requests.php (items, barcode_png, ...).
    function build(pr) {
        const items = pr.items || [];
        const total = items.reduce((s, i) => s + (i.is_title ? 0 : (parseFloat(i.total_cost) || 0)), 0);
        const showStock = items.some(i => !i.is_title && String(i.stock_no || '').trim() !== '');
        const words = (global.IarPrint && global.IarPrint.amountInWords) ? global.IarPrint.amountInWords(total) : '';

        // Omitted rather than shown broken if the server could not build it — a
        // broken image on a controlled document is worse than no barcode.
        const barcode = pr.barcode_png ? `
            <div class="prp-barcode-float">
                <img class="prp-barcode" src="${pr.barcode_png}" style="width:${pr.barcode_width || 176}px" alt="Barcode of ${esc(pr.pr_number)}">
                <div class="prp-barcode-caption">${esc(pr.barcode_text || pr.pr_number)}</div>
            </div>` : '';

        const cols = showStock ? 8 : 7;
        return `
    <div class="prp-page">
      <div class="prp-frame">
        <div class="prp-letterhead-wrap">
        <table class="prp-letterhead">
            <tr>
                <td class="prp-seal"><img src="image/citylogo.png" alt=""></td>
                <td class="prp-letterhead-text">
                    Republic of the Philippines<br>
                    Province of Bukidnon<br>
                    <strong>City of Malaybalay</strong><br>
                    <strong>Bids and Awards Committee</strong><br>
                    New City Hall, Barangay Casisang<br>
                    Malaybalay City, Bukidnon<br>
                    Tel (088) 813-476 1-2<br>
                    <a href="#">bacmalaybalay@gmail.com</a>
                </td>
                <td class="prp-seal"><img src="image/City%20BAC.png" alt=""></td>
            </tr>
        </table>
        ${barcode}
        </div>
        <hr class="prp-rule">

        <div class="prp-title">PURCHASE REQUEST</div>

        <table class="prp-header-grid">
            <tr>
                <td class="prp-hg-label">OFFICE / DEPARTMENT</td>
                <td class="prp-hg-value">${esc(officeLine(pr))}</td>
                <td class="prp-hg-label">PR NO.</td>
                <td class="prp-hg-value prp-number">${esc(pr.pr_number)}</td>
            </tr>
            <tr>
                <td class="prp-hg-label">FUND CLUSTER</td>
                <td class="prp-hg-value">${esc(pr.fund_cluster) || '&nbsp;'}</td>
                <td class="prp-hg-label">DATE</td>
                <td class="prp-hg-value">${esc(longDate(pr.pr_date))}</td>
            </tr>
            <tr>
                <td class="prp-hg-label">CATEGORY</td>
                <td class="prp-hg-value">${esc(pr.category) || '&nbsp;'}</td>
                <td class="prp-hg-label">RESP. CENTER CODE</td>
                <td class="prp-hg-value">${esc(pr.responsibility_center_code) || '&nbsp;'}</td>
            </tr>
            <tr>
                <td class="prp-hg-label">MODE OF<br>PROCUREMENT</td>
                <td class="prp-hg-value" colspan="3">${esc(pr.mode_of_procurement) || '&nbsp;'}</td>
            </tr>
        </table>

        <table class="prp-items"><thead><tr>
            <th>No.</th><th>QTY</th><th>Units</th>${showStock ? '<th>Stock No.</th>' : ''}<th>Item Description</th><th>Estimated Unit Cost</th><th>Estimated Total Cost</th><th>Remarks</th>
        </tr></thead>
        <tbody>${itemRows(items, showStock)}
        <tr><td colspan="${cols}" class="prp-nothing-follows">***nothing follows***</td></tr>
        </tbody></table>

        <div class="prp-summary-box">
            <table class="prp-totals"><tr>
                <td>Amount in Words : <span class="prp-underline" style="min-width:420px;">${esc(words)}</span></td>
                <td class="prp-total-label">TOTAL:</td>
                <td class="prp-total-value">${money(total)}</td>
            </tr></table>
            <table class="prp-purpose-row"><tr>
                <td class="prp-purpose-label"><em>PURPOSE :</em></td>
                <td>${esc(pr.purpose)}</td>
            </tr></table>
        </div>
        ${String(pr.remarks || '').trim() !== '' ? `<div class="prp-note"><strong>Remarks:</strong> ${esc(pr.remarks)}</div>` : ''}

        <table class="prp-sign-row"><tr>
            ${signBlock('Requested by:', pr.requested_by, pr.requested_by_position)}
            ${signBlock('Funds certified by:', pr.funds_certified_by, pr.funds_certified_position)}
            ${signBlock('Approved by:', pr.approved_by, pr.approved_by_position)}
        </tr></table>
      </div>
    </div>`;
    }

    global.PrPrint = { build };
})(window);
