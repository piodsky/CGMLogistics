const CB_BOND_RATES = {"cash_bond_amount":0.05,"surety_bond_amount":0.3};
const CB_REPORT_DEFAULTS = {"signatory_name":"JAY WARREN R. PABILLARAN","signatory_title":"City Mayor","witness_entity_name":"BELLA STEPHANIE B. SAAVEDRA","witness_entity_title":"City Treasurer"};
function escapeHtml(value) {
    if (value === null || value === undefined) return '';
    return String(value)
        .replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;').replace(/'/g, '&#39;');
}
function parseMoney(value) {
    if (value === null || value === undefined) return 0;
    const clean = String(value).replace(/[^0-9.\-]/g, '');
    return clean === '' ? 0 : (parseFloat(clean) || 0);
}
function formatMoney(value) {
    return Number(value || 0).toLocaleString('en-PH', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
}
function bidWithReportDefaults(bid) {
    const result = Object.assign({}, bid);
    Object.entries(CB_REPORT_DEFAULTS).forEach(([key, value]) => {
        if (!String(result[key] ?? '').trim()) result[key] = value;
    });
    const price = parseMoney(result.contract_amount)
        || (result.items || []).reduce((sum, item) => sum + parseMoney(item.total_cost), 0);
    Object.entries(CB_BOND_RATES).forEach(([key, rate]) => {
        if (!String(result[key] ?? '').trim() && price > 0) {
            result[key] = Math.round(price * rate * 100) / 100;
        }
    });
    return result;
}
function numberToWords(num) {
    const ones  = ['', 'One', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine'];
    const teens = ['Ten', 'Eleven', 'Twelve', 'Thirteen', 'Fourteen', 'Fifteen', 'Sixteen', 'Seventeen', 'Eighteen', 'Nineteen'];
    const tens  = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
    const scales = ['', 'Thousand', 'Million', 'Billion'];
    function chunk(n) {
        let str = '';
        if (n >= 100) { str += ones[Math.floor(n / 100)] + ' Hundred '; n %= 100; }
        if (n >= 20) { str += tens[Math.floor(n / 10)] + ' '; n %= 10; }
        else if (n >= 10) { str += teens[n - 10] + ' '; n = 0; }
        if (n > 0) str += ones[n] + ' ';
        return str.trim();
    }
    let intPart = Math.floor(num);
    if (intPart === 0) return 'Zero';
    const parts = [];
    let scaleIndex = 0;
    while (intPart > 0) {
        const c = intPart % 1000;
        if (c > 0) parts.unshift(chunk(c) + (scales[scaleIndex] ? ' ' + scales[scaleIndex] : ''));
        intPart = Math.floor(intPart / 1000);
        scaleIndex++;
    }
    return parts.join(' ').trim();
}

// The contract writes the price out in full capitals followed by the figure —
// "TEN MILLION SIX HUNDRED FIFTY THOUSAND PESOS ONLY (P 10,650,000.00)".
function amountInWords(amount) {
    const cents = Math.round(parseMoney(amount) * 100);
    const pesos = Math.floor(cents / 100), remainder = cents % 100;
    return numberToWords(pesos).toUpperCase() + (pesos === 1 ? ' PESO' : ' PESOS')
        + (remainder ? ' AND ' + numberToWords(remainder).toUpperCase() + (remainder === 1 ? ' CENTAVO' : ' CENTAVOS') : '')
        + ' ONLY';
}

// A blank prints as the ruled line the parties fill in by hand, which is what
// the original does for every date left open at signing.
function printedOr(value, blankWidth) {
    const v = String(value == null ? '' : value).trim();
    return v ? escapeHtml(v) : `<span class="cp-blank" style="min-width:${blankWidth || 60}px">&nbsp;</span>`;
}

function longDate(value) {
    if (!value) return '';
    const d = new Date(String(value).replace(' ', 'T'));
    return isNaN(d) ? String(value) : d.toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' });
}

// Item numbering is derived here, not stored. It is purely positional — priced
// rows count 1, 2, 3… and description-only lines carry no number — so deriving
// it at print time is the only way it cannot fall out of step with row order.
function buildContractItemRows(items) {
    let n = 0;
    return items.map(item => {
        const amount = parseMoney(item.total_cost);
        const priced = amount > 0;
        // A stored "0.000" is a non-empty string and therefore truthy, so the
        // obvious `value ? … : ''` prints a zero on every heading row. Compare
        // numerically instead.
        const qty  = parseMoney(item.quantity);
        const cost = parseMoney(item.unit_cost);
        return `<tr class="${priced ? 'cp-priced' : ''}">
            <td class="center">${priced ? ++n : ''}</td>
            <td class="center">${qty ? formatQty(qty) : ''}</td>
            <td class="center">${escapeHtml(item.unit) || ''}</td>
            <td class="desc">${cleanDescription(item.items_description).split('\n').map(line => `<div class="cp-desc-line">${escapeHtml(line) || '&nbsp;'}</div>`).join('')}</td>
            <td class="num">${cost ? formatMoney(cost) : ''}</td>
            <td class="num">${priced ? formatMoney(amount) : ''}</td>
        </tr>`;
    }).join('');
}

// Quantities are whole numbers far more often than not, and "1.000 Unit" reads
// like a rounding artefact on a contract. Trailing zeros are dropped, real
// fractions kept.
function formatQty(n) {
    return Number(n).toLocaleString('en-PH', { maximumFractionDigits: 3 });
}

// Drops trailing spaces on each line and blank lines at the end. Leading
// spaces are left alone — they are the indentation the buyer typed, and
// pre-wrap renders them.
function cleanDescription(v) {
    return String(v == null ? '' : v).replace(/[ \t]+$/gm, '').replace(/\s+$/, '');
}

function buildContractHtml(bid) {
    bid = bidWithReportDefaults(bid);
    const amount = parseMoney(bid.contract_amount);
    const itemsTotal = (bid.items || []).reduce((n, i) => n + parseMoney(i.total_cost), 0);
    // The figure the contract is executed for is the stored contract amount,
    // falling back to the items only when nobody set one — an executed price
    // is not something the item grid gets to restate.
    const price = amount || itemsTotal;
    const priceText = `<strong>${amountInWords(price)} (P ${formatMoney(price)})</strong>`;
    const name = escapeHtml(bid.contract_name || '');
    const nameLoc = name + (bid.contract_location ? ', ' + escapeHtml(bid.contract_location) : '');

    return `
    <div class="cp-page">
      <div class="cp-frame"><div class="cp-frame-inner">

        <table class="cp-letterhead">
          <tr>
            <td class="cp-seal"><img src="image/citylogo.png" alt=""></td>
            <td class="cp-lh-text">
              <div class="cp-lh-rep">Republic of the Philippines</div>
              <div class="cp-lh-rep">Province of Bukidnon</div>
              <div class="cp-lh-city">City of Malaybalay</div>
              <div>New City Hall, Barangay Casisang. 8700</div>
              <div>Tel (088) 813-4761-2</div>
            </td>
          </tr>
        </table>
        <hr class="cp-rule">

        <div class="cp-contract-no">Contract No.: <span class="cp-val">${printedOr(bid.contract_number, 120)}</span></div>

        <div class="cp-title-box">
          <div class="cp-title-kicker">CONTRACT FOR</div>
          <div class="cp-title-name">${nameLoc}</div>
        </div>

        <div class="cp-h">KNOW ALL MEN BY THESE PRESENTS:</div>

        <p class="cp-p">This CONTRACT executed on the ${printedOr(contractDay(bid.contract_date), 50)} day of
          ${printedOr(contractMonth(bid.contract_date), 90)} ${escapeHtml(contractYear(bid.contract_date))} by and between:</p>

        <p class="cp-p"><strong>CITY GOVERNMENT OF MALAYBALAY</strong>, a government agency of the Republic of the
          Philippines, hereinafter called <strong>&ldquo;the Entity&rdquo;</strong>;</p>

        <div class="cp-center">-and-</div>

        <p class="cp-p"><strong>${escapeHtml(bid.supplier_name || '')}</strong> a company duly organized and existing
          under the laws of <strong>${printedOr(bid.supplier_org_place, 90)}</strong>, with principal office at
          <strong>${escapeHtml(bid.supplier_address || '')}</strong>, hereinafter called
          <strong>&ldquo;the Supplier&rdquo;</strong>.</p>

        <p class="cp-p"><strong>WHEREAS,</strong> the Entity invited Bids for certain goods,
          <strong>${name}</strong>${bid.contract_location ? ', <strong>' + escapeHtml(bid.contract_location) + '</strong>' : ''};</p>

        <p class="cp-p"><strong>WHEREAS</strong>, the Supplier submitted a responsive bid and was awarded the contract
          for the procurement in the total amount of ${priceText}, hereinafter referred to as the
          <strong>&ldquo;Contract Price.&rdquo;</strong></p>

        <p class="cp-p"><strong>NOW, THEREFORE,</strong> for and in consideration of the foregoing premises, the parties
          hereby agree as follows:</p>

        <div class="cp-list">
          <div><span class="cp-marker">1.</span>Unless otherwise stated, terms and expressions used in this Contract
            shall have the same meanings as those assigned to them in the Conditions of Contract, which form an integral
            part of this Contract.</div>
          <div><span class="cp-marker">2.</span>The following documents as required by the Implementing Rules and
            Regulations of Republic Act No. 12009 shall form part and be read and construed as integral parts of this
            Contract, <em>viz.</em>:
            <div class="cp-sublist">
              <div><strong><em>a.&nbsp;&nbsp;Philippine Bidding Documents (PBD);</em></strong></div>
              <div style="padding-left:22px"><strong><em>&bull; For Procurement of Goods</em></strong></div>
              <div style="padding-left:22px">i.&nbsp;&nbsp;&nbsp;&nbsp;Invitation to Bid;</div>
              <div style="padding-left:22px">ii.&nbsp;&nbsp;&nbsp;Instruction to Bidders;</div>
              <div style="padding-left:22px">iii.&nbsp;&nbsp;Bid Form, including all the documents/statements contained in the Bidder&rsquo;s bidding envelopes, as annexes, and all other documents submitted (e.g., Bidder&rsquo;s response to request for clarifications on the bid), including corrections to the bid, if any, resulting from the Procuring Entity&rsquo;s bid evaluation;</div>
              <div style="padding-left:22px">iv.&nbsp;&nbsp;&nbsp;Bid Data Sheet;</div>
              <div style="padding-left:22px">v.&nbsp;&nbsp;&nbsp;&nbsp;Schedule of Requirements;</div>
              <div style="padding-left:22px">vi.&nbsp;&nbsp;&nbsp;Technical Specifications;</div>
              <div style="padding-left:22px">vii.&nbsp;&nbsp;General and Special Conditions of Contract;</div>
              <div style="padding-left:22px">viii.&nbsp;Supplemental Bid Bulletins, if any; and</div>
              <div style="padding-left:22px">ix.&nbsp;&nbsp;&nbsp;Other contract documents that may be required by existing laws and/or the Entity.</div>
              <div><strong><em>b.&nbsp;&nbsp;Winning bidder&rsquo;s bid, including the Eligibility Requirements, Technical and Financial Proposals, and all other documents or statements submitted;</em></strong></div>
              <div><strong><em>c.&nbsp;&nbsp;Performance Security;</em></strong></div>
              <div><strong><em>d.&nbsp;&nbsp;Notice of Award of Contract; and the Bidder&rsquo;s Conforme thereto; and</em></strong></div>
              <div><strong><em>e.&nbsp;&nbsp;Other contract documents that may be required by existing laws and/or the Procuring Entity concerned in the PBD, such as but not limited to the Notice to Proceed and Warranty Security.</em></strong></div>
            </div>
          </div>
          <div><span class="cp-marker">3.</span>In consideration of the Contract Price of ${priceText}, or such other
            sums as may be determined in accordance with the terms of the Contract, the Supplier agrees to deliver and
            perform the items and related services for the <strong>${nameLoc}</strong> described herein in accordance
            with the terms and conditions specified in the Contract and its annexed documents.</div>
        </div>

        <table class="cp-items">
          <thead>
            <tr>
              <th style="width:6%">Item No.</th>
              <th style="width:7%">QTY</th>
              <th style="width:9%">Unit</th>
              <th style="width:44%">Description</th>
              <th style="width:17%">Unit Cost</th>
              <th style="width:17%">Amount</th>
            </tr>
          </thead>
          <tbody>
            ${buildContractItemRows(bid.items || [])}
            <tr><td colspan="4" class="cp-nothing">***NOTHING AS FOLLOWS***</td><td></td><td></td></tr>
            <tr class="cp-grand">
              <td colspan="4" class="cp-grand-label">${amountInWords(price)}</td>
              <td class="center">GRAND TOTAL</td>
              <td class="num">${formatMoney(price)}</td>
            </tr>
            <tr class="cp-delivery">
              <td colspan="2" class="center">Place of Delivery:</td>
              <td colspan="2" class="center">${escapeHtml(bid.place_of_delivery || '')}</td>
              <td class="center">Delivery Term:</td>
              <td class="center">${escapeHtml(bid.delivery_term || '')}</td>
            </tr>
          </tbody>
        </table>

        <p class="cp-penalty">In case of failure to make the full delivery within the time specified above, a penalty of
          one-tenth (1/10) of one percent for every day of delay shall be imposed.</p>

        <div class="cp-list">
          <div><span class="cp-marker">4.</span>The <strong>CITY GOVERNMENT OF MALAYBALAY</strong> agrees to pay the
            above-mentioned sum to the Supplier in accordance with the schedule and manner provided in the Bidding
            Documents and its annexes.</div>
          <div><span class="cp-marker">5.</span>Any dispute, difference, or claim arising out of or relating to this
            Contract, including its existence, validity, interpretation, breach, or termination thereof, may be
            submitted to arbitration or other form of alternative dispute resolution in accordance with the applicable
            law, such as Republic Act No. 9285 (Alternative Dispute Resolution Act of 2004) or Executive Order No 1008,
            series 1985 (Construction Industry Arbitration Law).</div>
        </div>

        <div class="cp-signatures">
        <p class="cp-p" style="margin-top:18px"><strong>IN WITNESS WHEREOF,</strong> the parties hereto have caused this
          Contract to be executed in accordance with the laws of the Republic of the Philippines on the day and year
          first above written.</p>

        <table class="cp-sign">
          <tr>
            <td>
              <div class="cp-sign-role">Head of the Procuring Entity or Duly Authorized Representative</div>
              <div class="cp-sign-name">${escapeHtml(bid.signatory_name || '')}</div>
              <div class="cp-sign-title">${escapeHtml(bid.signatory_title || '')}</div>
              <div class="cp-sign-date">Date:<span class="cp-blank" style="min-width:130px">&nbsp;</span></div>
            </td>
            <td>
              <div class="cp-sign-role">Duly authorized to sign the Contract for and behalf of
                <strong>${escapeHtml(bid.supplier_name || '')}</strong></div>
              <div class="cp-sign-name">${escapeHtml(bid.representative_name || '')}</div>
              <div class="cp-sign-title">${escapeHtml(bid.representative_title || '')}</div>
              <div class="cp-sign-date">Date:<span class="cp-blank" style="min-width:130px">&nbsp;</span></div>
            </td>
          </tr>
        </table>

        <div class="cp-center" style="font-weight:700;font-style:italic;margin-top:6px">Signed in the presence of:</div>

        <table class="cp-sign">
          <tr>
            <td style="padding-top:34px">
              <div class="cp-sign-name">${escapeHtml(bid.witness_entity_name || '')}</div>
              <div class="cp-sign-title">${escapeHtml(bid.witness_entity_title || '')}</div>
              <div class="cp-sign-title">Witness &ndash; Procuring Entity</div>
            </td>
            <td style="padding-top:34px">
              <div class="cp-sign-name cp-witness-line">${escapeHtml(bid.witness_supplier_name || '') || '&nbsp;'}</div>
              <div class="cp-sign-title">Name and Signature</div>
              <div class="cp-sign-title">Witness&ndash; Supplier</div>
            </td>
          </tr>
        </table>

        </div>
        <div class="cp-notary">
        <div class="cp-h" style="text-align:left;margin-top:26px">ACKNOWLEDGMENT</div>

        <p class="cp-p">BEFORE ME, A Notary Public for and in the ${printedOr(bid.notary_place, 190)}, City/Province of
          <span class="cp-blank" style="min-width:150px">&nbsp;</span>, this
          ${printedOr(contractDay(bid.contract_date), 44)} day of
          ${printedOr(contractMonth(bid.contract_date), 110)}, ${escapeHtml(contractYear(bid.contract_date))},
          personally appeared the above-named persons who have satisfactorily proven to me their identity, through their
          identifying documents written below their names and signatures, that they are the same persons who executed
          and voluntarily signed the foregoing instrument consisting of <strong class="cp-page-count">ONE (1)</strong>
          pages, including this page where this Acknowledgement is written, which they acknowledged before me as their free and voluntary
          act and deed.</p>

        <p class="cp-p">WITNESS MY HAND AND SEAL this <span class="cp-blank" style="min-width:44px">&nbsp;</span> day of
          <span class="cp-blank" style="min-width:130px">&nbsp;</span>, <em>${escapeHtml(contractYear(bid.contract_date))}</em>.</p>

        <div class="cp-notary-grid">
          <div>NAME OF NOTARY PUBLIC&nbsp;&nbsp;${printedOr(bid.notary_name, 200)}</div>
          <div>Notarial Commission No.&nbsp;&nbsp;<span class="cp-blank" style="min-width:150px">&nbsp;</span></div>
          <div>Notary Public for&nbsp;&nbsp;<span class="cp-blank" style="min-width:110px">&nbsp;</span>
               until&nbsp;&nbsp;<span class="cp-blank" style="min-width:110px">&nbsp;</span></div>
          <div>Roll of Attorneys No.&nbsp;&nbsp;<span class="cp-blank" style="min-width:140px">&nbsp;</span></div>
          <div>PTR No.&nbsp;&nbsp;<span class="cp-blank" style="min-width:150px">&nbsp;</span></div>
          <div>IBP No.&nbsp;&nbsp;<span class="cp-blank" style="min-width:150px">&nbsp;</span></div>
        </div>

        <div class="cp-notary-grid" style="margin-top:20px">
          <div>Doc. No.&nbsp;&nbsp;${printedOr(bid.notary_doc_no, 70)}</div>
          <div>Page No.&nbsp;&nbsp;${printedOr(bid.notary_page_no, 70)}</div>
          <div>Book No.&nbsp;&nbsp;${printedOr(bid.notary_book_no, 70)}</div>
          <div>Series of.&nbsp;&nbsp;${printedOr(bid.notary_series, 70)}</div>
        </div>
        </div>

      </div></div>
    </div>`;
}

// The contract's opening line reads "executed on the ___ day of ___ 2026", so
// the date is split rather than formatted whole. An unset contract date leaves
// the day and month as ruled lines and falls back to the current year, which
// is what the blank forms are pre-printed with.
function contractDay(value) {
    if (!value) return '';
    const d = new Date(String(value).replace(' ', 'T'));
    return isNaN(d) ? '' : String(d.getDate());
}
function contractMonth(value) {
    if (!value) return '';
    const d = new Date(String(value).replace(' ', 'T'));
    return isNaN(d) ? '' : d.toLocaleDateString('en-US', { month: 'long' });
}
function contractYear(value) {
    if (!value) return String(new Date().getFullYear());
    const d = new Date(String(value).replace(' ', 'T'));
    return isNaN(d) ? String(new Date().getFullYear()) : String(d.getFullYear());
}


// ── Notice of Award / Notice to Proceed ─────────────────────────────────
// Both sit on the BAC's letterhead and share their opening block, so that much
// is built once. `sfNumber` is the standard-form identity printed above the
// rule — the Notice of Award carries SF-GOOD-49, the Notice to Proceed none.
function buildBacLetterhead(sfNumber) {
    return `
        <table class="cl-letterhead">
          <tr>
            <td class="cl-seal"><img src="image/citylogo.png" alt=""></td>
            <td class="cl-lh-text">
              <div class="cl-lh-rep">Republic of the Philippines</div>
              <div class="cl-lh-rep">Province of Bukidnon</div>
              <div class="cl-lh-city">City of Malaybalay</div>
              <div class="cl-lh-bac">Bids and Awards Committee</div>
              <div>New City Hall, Barangay Casisang</div>
              <div>Malaybalay City, Bukidnon</div>
              <div>Tel (088) 813-4761</div>
              <div class="cl-lh-mail">bacmalaybalay@gmail.com</div>
            </td>
            <td class="cl-seal"><img src="image/City%20BAC.png" alt=""></td>
          </tr>
        </table>
        ${sfNumber ? `<div class="cl-sf">Standard Form Number: ${escapeHtml(sfNumber)}<br>Revised on: May 24, 2004</div>` : ''}
        <hr class="cl-rule">`;
}

// The three-line reference block both letters open with.
function buildBacReferenceBlock(bid) {
    return `
        <table class="cl-ref">
          <tr><td class="cl-ref-label">Contract Reference Number:</td><td class="cl-ref-value">${escapeHtml(bid.bid_number || '')}</td></tr>
          <tr><td class="cl-ref-label">Name of the Contract:</td><td class="cl-ref-value">${escapeHtml(bid.contract_name || '')}</td></tr>
          <tr><td class="cl-ref-label">Location of the Contract:</td><td class="cl-ref-value">${escapeHtml(bid.contract_location || '')}</td></tr>
        </table>`;
}

// Who the letter is addressed to. Identical on both documents.
function buildBidderAddressBlock(bid) {
    return `
        <div class="cl-addr">
          <div class="cl-addr-name">${escapeHtml(bid.representative_name || '')}</div>
          <div>${escapeHtml(bid.representative_title || '')}</div>
          <div class="cl-addr-company">${escapeHtml(bid.supplier_name || '')}</div>
          <div>${escapeHtml(bid.supplier_address || '')}</div>
          ${bid.supplier_address2 ? `<div>${escapeHtml(bid.supplier_address2)}</div>` : ''}
        </div>`;
}

function buildNoticeOfAwardHtml(bid) {
    bid = bidWithReportDefaults(bid);
    const price = parseMoney(bid.contract_amount)
        || (bid.items || []).reduce((n, i) => n + parseMoney(i.total_cost), 0);
    const nameLoc = escapeHtml(bid.contract_name || '')
        + (bid.contract_location ? ', ' + escapeHtml(bid.contract_location) : '');

    return `
    <div class="cl-page">
      ${buildBacLetterhead('SF-GOOD-49')}
      ${buildBacReferenceBlock(bid)}

      <div class="cl-title">NOTICE OF AWARD</div>
      <div class="cl-date">${bid.noa_date ? escapeHtml(longDate(bid.noa_date)) : '&nbsp;'}</div>

      ${buildBidderAddressBlock(bid)}

      <p class="cl-p">Dear Sir:</p>

      <p class="cl-p">We are informing you that your Bid dated <strong>${bid.bid_date ? escapeHtml(longDate(bid.bid_date)) : '__________'}</strong>
        for execution of <strong>BID NO. ${escapeHtml(bid.bid_number || '')} ${nameLoc}</strong> with a total contract
        price of <strong>${amountInWords(price)} (P ${formatMoney(price)})</strong> is hereby accepted.</p>

      <p class="cl-p">You are hereby required to provide within ten (10) calendar days the performance security in the
        form and the amount stipulated below:</p>

      <p class="cl-p">Failure to provide the performance security shall constitute sufficient ground for cancellation of
        the award and forfeiture of the bid security.</p>

      <table class="cl-bond">
        <tr>
          <td><strong>Cash Bond, cashier's /manager's check, Bank Draft/guarantee: or Irrevocable Letter of Credit; or</strong></td>
          <td class="cl-bond-amt">${String(bid.cash_bond_amount ?? '').trim() ? 'P ' + formatMoney(parseMoney(bid.cash_bond_amount)) : '&nbsp;'}</td>
        </tr>
        <tr>
          <td><strong>Surety Bond:</strong></td>
          <td class="cl-bond-amt">${String(bid.surety_bond_amount ?? '').trim() ? 'P ' + formatMoney(parseMoney(bid.surety_bond_amount)) : '&nbsp;'}</td>
        </tr>
      </table>

      <div class="cl-note">*** Note: Expiry should be &ldquo;Until Issuance of final acceptance&rdquo;</div>

      <p class="cl-p">Very truly yours,</p>
      <div class="cl-sign-block">
        <div class="cl-sign-name">${escapeHtml(bid.signatory_name || '')}</div>
        <div>${escapeHtml(bid.signatory_title || '')}</div>
      </div>

      <div style="margin-top:26px">Conformed:</div>
      <div class="cl-sign-block">
        <div class="cl-sign-name">${escapeHtml(bid.representative_name || '')}</div>
        <div>${escapeHtml(bid.supplier_name || '')}</div>
        <div style="margin-top:6px">Date:<span class="cl-blank" style="min-width:150px">&nbsp;</span></div>
      </div>
    </div>`;
}

function buildNoticeToProceedHtml(bid) {
    bid = bidWithReportDefaults(bid);
    const nameLoc = escapeHtml(bid.contract_name || '')
        + (bid.contract_location ? ', ' + escapeHtml(bid.contract_location) : '');

    return `
    <div class="cl-page">
      ${buildBacLetterhead('')}
      ${buildBacReferenceBlock(bid)}

      <div class="cl-title">NOTICE TO PROCEED</div>
      <div class="cl-date">${bid.ntp_date ? escapeHtml(longDate(bid.ntp_date)) : '&nbsp;'}</div>

      ${buildBidderAddressBlock(bid)}

      <p class="cl-p">Dear Sir:</p>

      <p class="cl-p">The attached Contract Agreement having been approved, notice is hereby given to
        <strong>${escapeHtml(bid.supplier_name || '')}</strong> that work may commence on the
        <strong>${nameLoc}.</strong></p>

      <p class="cl-p">Upon receipt of this notice, you are responsible for performing the services under the terms and
        conditions of the Agreement and in accordance with the Implementation and Payment Schedule:</p>

      <p class="cl-p">Please acknowledge receipt and acceptance of this notice by signing both copies in the space
        provided below, keep one copy and return the other to the <strong><em>City of Malaybalay.</em></strong></p>

      <p class="cl-p" style="margin-top:34px">Very truly yours,</p>
      <div class="cl-sign-block">
        <div class="cl-sign-name">${escapeHtml(bid.signatory_name || '')}</div>
        <div>${escapeHtml(bid.signatory_title || '')}</div>
      </div>

      <div class="cl-ack">
        <div>I acknowledge receipt of this Notice on <span class="cl-receipt-date"><span class="cl-blank">&nbsp;</span><span class="cl-sub">(Date of receipt)</span></span></div>
        <div>Name of the Representative of the Bidder
          <span style="border-bottom:1px solid #000;padding:0 12px;font-weight:700">${escapeHtml(bid.representative_name || '') || '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;'}</span></div>
        <div>Authorized Signature: <span class="cl-blank" style="min-width:250px">&nbsp;</span></div>
      </div>
    </div>`;
}

// The three documents, in the order the BAC issues them. Each says how to
// build it, what to call the file, and what furniture the printed original
// carries — the contract has a running header and the marginal initialling
// strip, the two letters have neither.
const CB_DOCUMENTS = {
    noa: {
        label: 'Notice of Award',
        build: buildNoticeOfAwardHtml,
        selector: '.cl-page',
        file: 'Notice_of_Award',
        singlePage: true,
        breakSelector: '.cl-p, .cl-bond, .cl-sign-block, .cl-ack > div',
        gridSelector: '.cl-bond',
        headerBandPt: 0,
        furniture: 'plain',
    },
    contract: {
        label: 'Contract Agreement',
        build: buildContractHtml,
        selector: '.cp-page',
        file: 'Contract',
        breakSelector: '.cp-items tr, .cp-desc-line, .cp-frame-inner > .cp-p, .cp-list > div, .cp-sublist > div, .cp-title-box, .cp-signatures, .cp-notary',
        gridSelector: '.cp-items',
        headerBandPt: 22,
        furniture: 'contract',
        // The longest of the three prints by far (20+ pages once a bid covers
        // this many items) — the shared single-canvas pipeline has to shrink
        // a document that long below full resolution to stay under the
        // browser's canvas size cap, which is what made this one specifically
        // look soft. Rasterising it page-by-page instead keeps every page at
        // full scale regardless of how many there are.
        perPage: true,
    },
    ntp: {
        label: 'Notice to Proceed',
        build: buildNoticeToProceedHtml,
        selector: '.cl-page',
        file: 'Notice_to_Proceed',
        singlePage: true,
        breakSelector: '.cl-p, .cl-sign-block, .cl-ack > div',
        gridSelector: '',
        headerBandPt: 0,
        furniture: 'plain',
    },
};

// The two letters print with nothing but the date/page line at the foot. A
// page number is still worth printing: a one-page letter says "Page 1 of 1",
// which is how a reader knows nothing is missing from the envelope.
function drawPlainFurniture(pdf, page, pageCount, geo, stamp) {
    const baseline = geo.pageH - geo.margin - 4;
    pdf.setTextColor(0, 0, 0);
    pdf.setFont('helvetica', 'normal');
    pdf.setFontSize(7.5);
    pdf.text(`Date Printed: ${stamp.date}   ${stamp.time}`, geo.margin, baseline);
    pdf.setFont('helvetica', 'bold');
    pdf.setFontSize(9);
    pdf.text(`Page ${page} of ${pageCount}`, geo.pageW - geo.margin, baseline, { align: 'right' });
}

function drawContractFurniture(pdf, page, pageCount, geo, bid, stamp) {
    bid = bidWithReportDefaults(bid);
    pdf.setTextColor(0, 0, 0);

    // The pre-bordered-stock double rule, closed on every page — around the
    // content box only, not the running header/footer bands above and below
    // it, matching where the form's own raster is placed.
    const frameX = geo.margin;
    const frameY = geo.margin + geo.headerBandPt - 8;
    const frameW = geo.contentW;
    const frameH = geo.pageH - geo.margin - geo.footerBandPt - frameY;
    pdf.setDrawColor(31, 63, 143);
    pdf.setLineWidth(1.4);
    pdf.rect(frameX, frameY, frameW, frameH);
    pdf.setLineWidth(0.75);
    pdf.rect(frameX + 3, frameY + 3, frameW - 6, frameH - 6);
    pdf.setDrawColor(0, 0, 0);

    // Running header, on the baseline of the reserved top band.
    pdf.setFont('helvetica', 'bold');
    pdf.setFontSize(8.5);
    pdf.text('Bid No. ' + (bid.bid_number || ''), geo.margin, geo.margin + 10);
    pdf.text('Page ' + page + ' of ' + pageCount, geo.pageW - geo.margin, geo.margin + 10, { align: 'right' });
    pdf.setTextColor(0, 0, 0);

    // Foot of the sheet.
    const baseline = geo.pageH - geo.margin - 4;
    pdf.setFont('helvetica', 'normal');
    pdf.setFontSize(7.5);
    pdf.text(`Date Printed: ${stamp.date}   ${stamp.time}`, geo.margin, baseline);
    pdf.setFont('helvetica', 'bold');
    pdf.setFontSize(9);
    pdf.text(`Page ${page} of ${pageCount}`, geo.pageW - geo.margin, baseline, { align: 'right' });

    // Keep all four signing positions, including a blank supplier-witness
    // line. Center along the rotated baseline: jsPDF's align:'center' shifts
    // the unrotated x coordinate and would push long names off the paper.
    const strip = [
        [bid.witness_supplier_name, 'Supplier Witness'],
        [bid.representative_name, bid.representative_title],
        [bid.witness_entity_name, (bid.witness_entity_title || '') + ' Witness'],
        [bid.signatory_name, bid.signatory_title],
    ];

    const usable = frameH - 132;
    const step = usable / strip.length;
    strip.forEach((pair, i) => {
        const y = frameY + 122 + step * i + step / 2;
        pdf.setFont('helvetica', 'bold');
        pdf.setFontSize(9);
        const name = String(pair[0] || '').trim().toUpperCase() || '________________________';
        pdf.setFontSize(Math.min(9, 9 * (step - 12) / Math.max(pdf.getTextWidth(name), 1)));
        pdf.text(name, frameX + 24, y + pdf.getTextWidth(name) / 2, { angle: 90 });
        if (String(pair[1] || '').trim()) {
            pdf.setFont('helvetica', 'normal');
            pdf.setFontSize(7.5);
            const role = String(pair[1]);
            pdf.setFontSize(Math.min(7.5, 7.5 * (step - 12) / Math.max(pdf.getTextWidth(role), 1)));
            pdf.text(role, frameX + 34, y + pdf.getTextWidth(role) / 2, { angle: 90 });
        }
    });
}
const CB_FOOTER_BAND_PT = 20;
async function generateBidPdf(bid, doc) {
    const loading = document.getElementById('cbPdfLoading');
    const frame = document.getElementById('cbPdfFrame');
    const buttons = ['cbPdfDownload', 'cbPdfPrint', 'cbPdfOpenTab'].map(id => document.getElementById(id));

    frame.classList.add('d-none');
    buttons.forEach(b => { if (b) b.disabled = true; });
    loading.classList.remove('d-none');
    loading.innerHTML = '<div class="spinner-border text-primary" role="status"></div><div class="small mt-2">Generating PDF preview…</div>';

    try {
        const source = document.querySelector('#cbPrintSource ' + doc.selector);
        if (!source) throw new Error('Print content did not render — try closing and reopening the preview.');

        const stamp = PrintPdf.printedStamp();
        const renderFn = doc.perPage ? PrintPdf.renderPerPageToPdf : PrintPdf.renderToPagedPdf;
        const pdf = await renderFn(source, {
            paper: 'long',
            // A break candidate at every table row and every paragraph, so a cut
            // never lands mid-sentence or mid-item.
            breakSelector: doc.breakSelector,
            gridSelector: doc.gridSelector,
            headerBandPt: doc.headerBandPt,
            footerBandPt: CB_FOOTER_BAND_PT,
            singlePage: !!doc.singlePage,
            restoreTo: document.getElementById('cbPrintSource'),
            onLayout: pageCount => {
                const count = source.querySelector('.cp-page-count');
                if (count) count.textContent = numberToWords(pageCount).toUpperCase() + ' (' + pageCount + ')';
            },
            onPage: (pdf, page, pageCount, geo) => doc.furniture === 'contract'
                ? drawContractFurniture(pdf, page, pageCount, geo, bid, stamp)
                : drawPlainFurniture(pdf, page, pageCount, geo, stamp),
        });

        const blob = pdf.output('blob');
        if (cbPdfBlobUrl) URL.revokeObjectURL(cbPdfBlobUrl);
        cbPdfBlobUrl = URL.createObjectURL(blob);
        cbPdfFileName = `${doc.file}_${bid.bid_number}.pdf`;

        frame.src = cbPdfBlobUrl + CB_PDF_VIEW_PARAMS;
        loading.classList.add('d-none');
        frame.classList.remove('d-none');
        buttons.forEach(b => { if (b) b.disabled = false; });
    } catch (err) {
        // Surfaced directly rather than as a generic message, so a failure is
        // diagnosable instead of a dead end.
        loading.innerHTML = '<div class="text-danger small px-3"><i class="fas fa-triangle-exclamation me-1"></i>Unable to generate the PDF: '
            + escapeHtml(err.message || String(err)) + '</div>';
        console.error(err);
    }
}
let cbPdfBlobUrl = null, cbPdfFileName = ''; const CB_PDF_VIEW_PARAMS = '';
const fixtures = {"guide":{"id":1,"bid_number":"2026-055","contract_number":"2026-130","contract_name":"PURCHASE OF 1 UNIT WHEEL LOADER","contract_location":"CDRRMO, MALAYBALAY CITY","supplier_name":"MONARK EQUIPMENT CORPORATION","supplier_address":"13 Economia St. Bagumbayan, Quezon City","supplier_address2":"w\/ Branch office at El Salvador, Misamis Oriental","supplier_org_place":"Quezon City","representative_name":"REYNARD MORENO","representative_title":"Account Manager","contract_amount":"10650000.00","cash_bond_amount":"532500.00","surety_bond_amount":"3195000.00","place_of_delivery":"NEW CITY HALL, BARANGAY CASISANG","delivery_term":"30 C.D.","bid_date":"2026-04-29","contract_date":"","noa_date":"2026-07-29","ntp_date":"","signatory_name":"JAY WARREN R. PABILLARAN","signatory_title":"City Mayor","witness_entity_name":"BELLA STEPHANIE B. SAAVEDRA","witness_entity_title":"City Treasurer","witness_supplier_name":"","notary_name":"ATTY. JAY S. ALBARECE","notary_place":"MANOLO FORTICH, BUKIDNON","notary_doc_no":"104","notary_page_no":"21","notary_book_no":"33","notary_series":"2026","remarks":"SAMPLE record, created to demonstrate the module. Not genuine BAC paperwork - the real bid is 2026-055, this one carries a -SAMPLE suffix so the real number stays free. Safe to delete.","items":[{"quantity":"1","unit":"Unit","unit_cost":"10650000.00","total_cost":"10650000.00","is_title":false,"items_description":"WHEEL LOADER\n\nBrand\/ Model: Caterpillar 950GC\n\nSpecifications:\nEngine: Turbocharged Diesel Engine\nRated Engine Horsepower: 243 hp @ designed rpm\nRated Net Torque: 1092 N-m @ designed rpm\nEnvironmental Compliance: Tier IV emission standards\nTransmission: 4 forward \u0026 3 reverse transmission\n\nOperating Specifications:\nOperating Weight: 18.849 metric tons\nBucket Size: 3.2 Cu.m\nBreakout force: 154 kN\nStatic Tipping Load Full turn (no tire deflection): \u2265 11,906 kg\nDumping height at max lift \u0026 45\u00b0 discharge: \u2265 4,188 mm\nGround clearance: 460 mm\n\nOperator Environment: Ladders with guardrails \u0026 handrails\nFully air-conditioned enclosed cab\nAudio-entertainment system\nMechanical suspension seats\n\nSafety Features: ROPS\/FOPS certified cabin\nSeat belts\nBack-up alarm\nAdequate lighting system\n\nInclusions: Standard tools with toolbox\nGrease gun\nDecals \u0026 Logos\nOperator's Manual (Hard and Soft Copy)\nService \u0026 Maintenance Manual (Hard \u0026 Soft Copy)\nParts Manual (Hard \u0026 Soft Copy)\n\nOther Conditions: Machine warranty for 3 years unlimited running hours for factory defects.\nFree preventive maintenance servicing up to 4000 hours with free filters, oils and oil sampling\nFree orientation and training for Operators and Maintenance Personnel\nDealer must have an Exclusive distributorship\/dealership of the equipment being offered by the manufacturing company and must submit proof of the same\nMust have a service center and parts warehouse and service support within Region X \u0026 XI\nAfter sales support team must be available 24\/7\nDealers must provide a 1-year warranty for spare parts purchased as part of their after-sales support.\nSupplier must submit a notarized copy of the machine's waybill or bill of lading\nUnit must be equipped with Remote Management System or Telematics, and company must provide free access to the same\nCompany must have a 24\/7 Online parts store and online parts manual support system for the machine offered and provide free access"}]},"photo":{"bid_number":"2026-130","contract_number":"2026-167","contract_name":"PROCUREMENT OF VARIOUS GROCERY ITEM FOR PAMASKONG HANDOG 2026","contract_location":"CITY MAYOR'S OFFICE","supplier_name":"CENTRO SUPERSALES, INCORPORATED","supplier_address":"Agrosite, South Poblacion, Maramag, Bukidnon","supplier_org_place":"Maramag, Bukidnon","representative_name":"MAELENE M. TING","representative_title":"Authorized Representative","contract_amount":"27020435.50","bid_date":"2026-08-26","place_of_delivery":"Various Barangays","delivery_term":"30 C.D.","items":[{"quantity":"36750","unit":"Package","unit_cost":"735.25","total_cost":"27020437.50","items_description":"1 LOT\nPamaskong Handog Package\n\nEach package contains:\n5 Kilos Well Milled Rice 90% long grain (Local Rice) (Pack in a customized sack)\n2-cans Beef Loaf 150-155 grams (Premium Quality)\n2-cans Corned beef 150-155 grams (Premium Quality)\n1-pack Instant Pure Coffee powder 40 grams\n1-pack Brown Sugar 1 kilo well-packed\n8-pieces Instant Noodles (4 beef, 4 chicken flavor)\n2-cans Sardines 155 grams (Premium)\n1-pack Powdered milk Fortified 135 grams\nNote: Each package includes the costs of markings, packing, and delivery. See the attached box lay-out\nDelivery Site: Various Barangay"}]}};
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