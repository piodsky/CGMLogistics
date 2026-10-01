/* The PO report layout shared by Purchase Orders and Competitive Bidding steps. */
(function (global) {
'use strict';
function escapeHtml(value) {
    if (value === null || value === undefined) return '';
    return String(value)
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;');
}

function fmtMoney2(v) {
    return Number(v || 0).toLocaleString('en-PH', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
}

function fmtQty(v) {
    const num = Number(v);
    if (v === null || v === undefined || String(v).trim() === '' || !isFinite(num)) return '';
    return String(parseFloat(num.toFixed(3)));
}

function numberToWords(num) {
    const ones = ['', 'One', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine'];
    const teens = ['Ten', 'Eleven', 'Twelve', 'Thirteen', 'Fourteen', 'Fifteen', 'Sixteen', 'Seventeen', 'Eighteen', 'Nineteen'];
    const tens = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
    const scales = ['', 'Thousand', 'Million', 'Billion'];

    function chunkToWords(n) {
        let str = '';
        if (n >= 100) { str += ones[Math.floor(n / 100)] + ' Hundred '; n %= 100; }
        if (n >= 20) { str += tens[Math.floor(n / 10)] + ' '; n %= 10; }
        else if (n >= 10) { str += teens[n - 10] + ' '; n = 0; }
        if (n > 0) { str += ones[n] + ' '; }
        return str.trim();
    }

    let intPart = Math.floor(num);
    if (intPart === 0) return 'Zero';
    const parts = [];
    let scaleIndex = 0;
    while (intPart > 0) {
        const chunk = intPart % 1000;
        if (chunk > 0) parts.unshift(chunkToWords(chunk) + (scales[scaleIndex] ? ' ' + scales[scaleIndex] : ''));
        intPart = Math.floor(intPart / 1000);
        scaleIndex++;
    }
    return parts.join(' ').trim();
}

function amountInWords(amount) {
    const pesos = Math.floor(amount);
    const centavos = Math.round((amount - pesos) * 100);
    return `${numberToWords(pesos)} Pesos And ${String(centavos).padStart(2, '0')}/100 Only`;
}

function cleanDescription(v) {
    return String(v == null ? '' : v).replace(/[ 	]+$/gm, '').replace(/\s+$/, '');
}

function descriptionLines(v) {
    return cleanDescription(v).split('\n')
        .map(line => `<div class="pp-dl">${escapeHtml(line) || '&nbsp;'}</div>`)
        .join('');
}

function itemHasValue(v) {
    return v !== null && v !== undefined && String(v).trim() !== '' && parseFloat(v) !== 0;
}

function buildPrintItemRows(items, startNo, useNumberSeries) {
    return items.map((item, i) => `
        <tr>
            <td class="center">${useNumberSeries ? escapeHtml((item.number_series || '').toString().trim()) : (startNo + i)}</td>
            <td class="center">${itemHasValue(item.quantity) ? escapeHtml(fmtQty(item.quantity)) : ''}</td>
            <td class="center">${escapeHtml(item.unit)}</td>
            <td class="desc">${descriptionLines(item.items_description)}</td>
            <td class="num">${itemHasValue(item.unit_cost) ? fmtMoney2(item.unit_cost) : ''}</td>
            <td class="num">${itemHasValue(item.total_cost) ? fmtMoney2(item.total_cost) : ''}</td>
        </tr>
    `).join('');
}

function buildPrintPreviewHtml(order, type, signatoryName, signatoryTitle, initial, dateStr, supplierContact = '') {
    const items = order.items || [];
    const grandTotal = items.reduce((sum, i) => sum + (parseFloat(i.total_cost) || 0), 0);

    // Both variants print the SAME single continuous table — the by-LOT form
    // differs only in where the "No." column gets its numbers. The lot
    // structure is already in the data as divider rows the user typed
    // ("LOT 1", "LOT 2") and the Number Series against each item, so there is
    // nothing to regroup: rebuilding it as one table per lot produced a
    // heading and a repeated header row the real form does not have.
    const itemsHtml = `<table class="pp-items"><thead><tr><th>No.</th><th>QTY</th><th>Units</th><th>Item Description</th><th>Unit Cost</th><th>Total</th></tr></thead>
        <tbody>${buildPrintItemRows(items, 1, type === 'lot')}
        <tr><td colspan="6" class="pp-nothing-follows">***nothing follows***${(initial || '').trim() ? `<span class="pp-initial">${escapeHtml(initial.trim())}</span>` : ''}</td></tr>
        </tbody></table>`;

    const workingDays = (order.po_working_days || '').toString();
    const paymentTerm = (order.po_payment_term || '').toString();
    const workingDaysSuffix = /^\d+$/.test(workingDays.trim()) ? ' Working Days' : '';
    // Payment Term prints EXACTLY what Master Data holds — no unit is appended
    // here. It used to gain a hardcoded " Calendar Days" whenever the value was
    // a bare number, which made the ref_payment_term list unable to express
    // anything else: an entry meaning five WORKING days still printed as
    // calendar days. The unit belongs in the list entry ("30 Calendar Days",
    // "5 Working Days"), where the office can edit it without a code change.
    // Delivery Term above still appends, deliberately — it was not part of this
    // change and its list is a different one.
    // The "Date" beside PO No. is left to whatever the preparer typed into the
    // print dialog's Date field (empty by default for a document with no one
    // meaningful date, like a meals/catering order) — not silently pulled from
    // Date_PO or from today. The Crystal source filled it from ?Date_Printed,
    // a print-time parameter, which meant a reprint two months later showed a
    // different date on an otherwise identical document; anchoring it to
    // Date_PO instead once had the opposite problem, forcing a date onto POs
    // where none applies. When this copy was printed is still on the form —
    // in the footer, where it belongs.
    // Supplier phone number, looked up in the master list rather than stored on
    // the purchase order (there is no column for it there).
    //
    // Printed only when it looks like a phone number. Of the 45 numbers on file,
    // 23 are one to three digits — truncated entries like "857" — and putting
    // those on a purchase order as a supplier's contact is worse than leaving
    // the line off. Seven digits is the shortest real local landline.

    const supplierContactHtml = supplierContact.replace(/\D/g, '').length >= 7
        ? `<div class="pp-supplier-contact">Tel. No.: ${escapeHtml(supplierContact)}</div>`
        : '';

    // Verification QR. Scanning it yields the PO number, its current lifecycle
    // status, and whether this copy is the original or a later revision — the
    // same facts printed underneath, so the paper stays self-checking even if
    // the reader has no scanner. Omitted entirely if the server couldn't
    // build it, rather than printing a broken image on an audited document.
    // Code 128 of the PO number, sitting in the letterhead just left of the
    // BAC seal. Complements the QR at the bottom: this one scans straight into
    // a "PO number" field on any warehouse/COA scanner, where the QR carries a
    // whole verification URL. Omitted rather than shown broken if the server
    // couldn't build it.
    const barcodeHtml = order.barcode_png ? `
            <div class="pp-barcode-float">
                <img class="pp-barcode" src="${order.barcode_png}" style="width:${order.barcode_width || 176}px" alt="Purchase Order barcode">
                <div class="pp-barcode-caption">${escapeHtml(order.barcode_text || order.po_number)}</div>
            </div>` : '';

    const v = order.verification || {};
    const qrHtml = order.qr_png ? `
                <td class="pp-qr-cell">
                    <img class="pp-qr" src="${order.qr_png}" alt="Purchase Order verification QR code">
                    <!-- The lifecycle status ("Pending Approval" etc.) used to
                         print here and was removed: it is only true at the
                         moment of printing, so an approved PO would keep
                         carrying "Pending Approval" on paper forever. The live
                         status is on the verifier page the QR opens, which is
                         always current. ORIGINAL/EDITED stays because that is a
                         property of this printed copy, not of the record. -->
                    <div class="pp-qr-caption">
                        <strong>${v.original ? 'ORIGINAL' : 'EDITED'}</strong><br>
                        ${escapeHtml(v.code || '')}
                        <div class="pp-qr-scan">Scan to verify</div>
                    </div>
                </td>` : '';

    return `
    <div class="print-page">
      <div class="pp-frame pp-frame-full">
        <div class="pp-letterhead-wrap">
        <table class="pp-letterhead">
            <tr>
                <td class="pp-seal"><img src="image/citylogo.png" alt=""></td>
                <td class="pp-letterhead-text">
                    Republic of the Philippines<br>
                    Province of Bukidnon<br>
                    <strong>City of Malaybalay</strong><br>
                    <strong>Bids and Awards Committee</strong><br>
                    New City Hall, Barangay Casisang<br>
                    Malaybalay City, Bukidnon<br>
                    Tel (088) 813-476 1-2<br>
                    <a href="#">bacmalaybalay@gmail.com</a>
                </td>
                <td class="pp-seal"><img src="image/City%20BAC.png" alt=""></td>
            </tr>
        </table>
        ${barcodeHtml}
        </div>
        <hr class="pp-rule">

        <div class="pp-title-center">PURCHASE ORDER</div>

        <table class="pp-header-grid">
            <tr>
                <td class="pp-hg-label">SUPPLIER</td>
                <td class="pp-hg-value">${escapeHtml(order.supplier_name)}</td>
                <td class="pp-hg-label">PO NO.</td>
                <td class="pp-hg-value pp-po-number">${escapeHtml(order.po_number)}</td>
            </tr>
            <tr>
                <td class="pp-hg-label">ADDRESS</td>
                <td class="pp-hg-value">${escapeHtml(order.supplier_address)}${supplierContactHtml}</td>
                <td class="pp-hg-label">DATE</td>
                <td class="pp-hg-value">${(dateStr || '').trim() ? escapeHtml(dateStr.trim()) : '&nbsp;'}</td>
            </tr>
            <tr>
                <td class="pp-hg-label">MODE OF<br>PROCUREMENT</td>
                <td class="pp-hg-value">${escapeHtml(order.mode_of_procurement)}</td>
                <td class="pp-hg-label">PR NO.</td>
                <td class="pp-hg-value">${escapeHtml(order.pr_number)}</td>
            </tr>
            <tr>
                <td class="pp-hg-label">REF #</td>
                <td class="pp-hg-value">${escapeHtml(order.reference_no)}</td>
                <td class="pp-hg-label">BAC REF#</td>
                <td class="pp-hg-value">${escapeHtml(order.bac_resolution_no)}</td>
            </tr>
        </table>

        <div class="pp-gentlemen">
            Gentlemen<br>
            <span class="pp-indent">Please furnish this office the following articles subject to the terms and conditions contained herein :</span>
        </div>

        <table class="pp-block3">
            <tr>
                <td class="col1">Place of Delivery : <span class="pp-underline">${escapeHtml(order.place_of_delivery)}</span></td>
                <td class="col3"><div>Delivery Term <span class="pp-underline pp-underline-sm">${escapeHtml(workingDays)}</span>${workingDaysSuffix}</div></td>
            </tr>
            <tr>
                <td class="col1">Date of Delivery: <span class="pp-underline">&nbsp;</span></td>
                <td class="col3"><div>Payment Term <span class="pp-underline pp-underline-sm">${escapeHtml(paymentTerm)}</span></div></td>
            </tr>
        </table>

        ${itemsHtml}

        ${order.note && order.note.trim() !== '' ? `<div class="pp-note"><strong>Note:</strong> ${escapeHtml(order.note)}</div>` : ''}

        <div class="pp-summary-box">
            <table class="pp-totals">
                <tr>
                    <td>Amount in Words : <span class="pp-underline" style="min-width:420px;">${amountInWords(grandTotal)}</span></td>
                    <td class="pp-total-label">TOTAL:</td>
                    <td class="pp-total-value">${fmtMoney2(grandTotal)}</td>
                </tr>
            </table>
            <table class="pp-purpose-row">
                <tr>
                    <td class="pp-purpose-label"><em>PURPOSE :</em></td>
                    <td>${escapeHtml(order.purpose)}</td>
                </tr>
            </table>
        </div>

        <div class="pp-terms">
            In case of failure to make the full delivery within the time specified above, a penalty of one-tenth (1/10) of one (1) percent for every day of delay shall be imposed.<br>
            I have read and agree with the terms and condition stipulated at the back of the Purchase Order.
        </div>

        <table class="pp-conforme-band">
            <tr>
                <td class="pp-conforme-cell">Conforme:</td>
                ${qrHtml}
            </tr>
        </table>
        <table class="pp-sign-row">
            <tr>
                <td class="pp-sign-left">
                    <div class="pp-sign-line"></div>
                    ${order.requested_by && order.requested_by.trim() !== '' ? `<div class="pp-sign-name">${escapeHtml(order.requested_by)}</div>` : ''}
                    <div class="pp-sign-caption">(Signature over printed name )</div>
                    <div class="pp-sign-line" style="margin-top:18px;"></div>
                    <div class="pp-sign-caption">(Date)</div>
                </td>
                <td class="pp-sign-right">
                    <div class="pp-sign-line"></div>
                    <div class="pp-sign-name">${escapeHtml(signatoryName) || '&nbsp;'}</div>
                    <div class="pp-sign-title">${escapeHtml(signatoryTitle)}</div>
                </td>
            </tr>
        </table>
      </div>
    </div>`;
}
global.PoPrint = { build: buildPrintPreviewHtml };
})(window);
