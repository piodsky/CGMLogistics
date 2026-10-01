/* Record History — the timeline behind the History button on Inventory,
 * Inspection & Acceptance and Competitive Bidding.
 *
 * Same look and behaviour as the Purchase Order's own History modal
 * (#poHistoryModal in purchase_orders.php): newest first, what was done, the
 * log's own wording, who and when. That page keeps its own copy — it is live
 * and is not refactored onto this file.
 *
 * The modal is built on first use and appended to <body>, never inside a
 * .top-navbar (see the stacking-context note in CLAUDE.md).
 *
 *   RecordHistory.open({ title: 'Inventory History', label: 'PO-sys2026…',
 *                        url: 'api/inventory.php?po_number=…&history=1' })
 *
 * The endpoint answers { success, history: [{ Username, Action, Details,
 * Date_Created }] } — see lib/audit_history.php.
 */
(function (global) {
    'use strict';

    // Action codes worth a friendlier word than the title-cased code.
    const WORDS = {
        CREATE_INVENTORY: 'Received into Inventory', REVISE_INVENTORY: 'Revised', UNDO_INVENTORY: 'Undone',
        UPDATE_INVENTORY_SUB_DETAILS: 'Unit Breakdown Updated',
        PAR_NEW: 'PAR Issued', PAR_RENEW: 'PAR Renewed', ICS_NEW: 'ICS Issued', ICS_RENEW: 'ICS Renewed',
        ITR_TRANSFER: 'Transferred (ITR)', PTR_TRANSFER: 'Transferred (PTR)', PRS_RETURN: 'Returned to Stock (PRS)',
        CREATE_INSPECTION: 'Inspection Recorded', UPDATE_INSPECTION: 'Inspection Updated', UNDO_INSPECTION: 'Inspection Undone',
        CREATE_PROGRESS_BILLING: 'Progress Billing Recorded', UPDATE_PROGRESS_BILLING: 'Progress Billing Revised',
        UNDO_PROGRESS_BILLING: 'Progress Billing Undone',
        CREATE_BID: 'Created', UPDATE_BID: 'Updated', DELETE_BID: 'Deleted', LOCK_BID: 'Locked', UNLOCK_BID: 'Unlocked',
        UPDATE_BID_FIELD: 'Tracking Updated', CREATE_CB_STEP: 'Processing Step Recorded',
        REVISE_CB_STEP: 'Processing Step Revised', UNDO_CB_STEP: 'Processing Step Undone',
    };
    const ACRONYMS = { po: 'PO', par: 'PAR', ics: 'ICS', itr: 'ITR', ptr: 'PTR', prs: 'PRS', cb: 'CB' };

    function esc(v) {
        return String(v == null ? '' : v).replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;').replace(/'/g, '&#39;');
    }
    function label(action) {
        if (WORDS[action]) return WORDS[action];
        return String(action || '').toLowerCase().split('_')
            .map(w => ACRONYMS[w] || (w.charAt(0).toUpperCase() + w.slice(1))).join(' ');
    }
    function when(v) {
        if (!v) return '';
        const d = new Date(String(v).replace(' ', 'T'));
        if (isNaN(d)) return esc(v);
        return d.toLocaleDateString('en-PH', { year: 'numeric', month: 'short', day: 'numeric' })
             + ' ' + d.toLocaleTimeString('en-PH', { hour: 'numeric', minute: '2-digit' });
    }

    const EMPTY_HTML = '<i class="fas fa-inbox mb-2 d-block" style="font-size:1.4rem;"></i>'
        + 'No recorded activity yet for this record.'
        + '<div class="small mt-1">Activity has only been logged since this system went live &mdash; '
        + 'an older record shows nothing here until it is next saved, issued, or changed.</div>';

    function ensureModal() {
        let el = document.getElementById('recordHistoryModal');
        if (el) return el;
        const css = document.createElement('style');
        css.textContent = ''
            + '#recordHistoryModal .rh-header { background: linear-gradient(135deg, var(--navy, #1e3a8a), #1e40af); color: #fff; border-bottom: none; padding: 1.1rem 1.5rem; }'
            + '#recordHistoryModal .rh-header .modal-title { color: #fff; font-weight: 700; letter-spacing: .01em; }'
            + '#recordHistoryModal .rh-header .btn-close { filter: invert(1); opacity: .85; }'
            + '#recordHistoryModal .rh-header .btn-close:hover { opacity: 1; }'
            + '.rh-timeline { list-style: none; margin: 0; padding: 0 0 0 .3rem; }'
            + '.rh-timeline li { position: relative; padding: 0 0 .9rem 1.3rem; border-left: 2px solid #e2e8f0; font-size: .82rem; }'
            + '.rh-timeline li:last-child { border-left-color: transparent; padding-bottom: 0; }'
            + ".rh-timeline li::before { content: ''; position: absolute; left: -6px; top: .25rem; width: 10px; height: 10px; border-radius: 50%; background: var(--primary, #2563eb); }"
            + '.rh-timeline .who { font-weight: 700; color: #1e293b; }'
            + '.rh-timeline .when { color: var(--muted, #64748b); font-size: .74rem; margin-top: .1rem; }'
            + '.rh-empty { color: var(--muted, #64748b); font-size: .85rem; text-align: center; padding: 1.5rem 0; }';
        document.head.appendChild(css);

        document.body.insertAdjacentHTML('beforeend', ''
            + '<div class="modal fade" id="recordHistoryModal" tabindex="-1" aria-hidden="true">'
            + '<div class="modal-dialog modal-dialog-centered"><div class="modal-content">'
            + '<div class="modal-header rh-header"><h5 class="modal-title"><i class="fas fa-clock-rotate-left me-2"></i>'
            + '<span id="rhTitle">History</span><span id="rhLabel" class="fw-normal opacity-75"></span></h5>'
            + '<button type="button" class="btn-close" data-bs-dismiss="modal"></button></div>'
            + '<div class="modal-body" style="max-height: 60vh; overflow-y: auto;">'
            + '<div id="rhLoading" class="text-center text-muted small py-4"><div class="spinner-border spinner-border-sm me-2"></div>Loading history…</div>'
            + '<ul class="rh-timeline d-none" id="rhList"></ul>'
            + '<div class="rh-empty d-none" id="rhEmpty"></div>'
            + '</div>'
            + '<div class="modal-footer"><button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button></div>'
            + '</div></div></div>');
        el = document.getElementById('recordHistoryModal');
        // Opened over the record's own modal. Closing this one clears Bootstrap's
        // modal-open from <body>, which would let the page behind scroll while
        // the record modal is still up — put it back if one is.
        el.addEventListener('hidden.bs.modal', () => {
            if (document.querySelector('.modal.show')) document.body.classList.add('modal-open');
        });
        return el;
    }

    let requestId = 0;
    async function open(opts) {
        const el = ensureModal();
        const current = ++requestId;
        document.getElementById('rhTitle').textContent = opts.title || 'History';
        document.getElementById('rhLabel').textContent = opts.label ? ' — ' + opts.label : '';
        const loading = document.getElementById('rhLoading');
        const list = document.getElementById('rhList');
        const empty = document.getElementById('rhEmpty');
        list.classList.add('d-none');
        empty.classList.add('d-none');
        loading.classList.remove('d-none');
        list.innerHTML = '';
        (bootstrap.Modal.getInstance(el) || new bootstrap.Modal(el)).show();
        try {
            const res = await fetch(opts.url);
            const data = await res.json();
            if (current !== requestId) return;
            loading.classList.add('d-none');
            const rows = (data.success && Array.isArray(data.history)) ? data.history : [];
            if (!rows.length) { empty.innerHTML = EMPTY_HTML; empty.classList.remove('d-none'); return; }
            list.classList.remove('d-none');
            list.innerHTML = rows.map(r => '<li>'
                + '<div class="who">' + esc(label(r.Action))
                + (r.Details ? ' <span class="text-muted fw-normal">— ' + esc(r.Details) + '</span>' : '') + '</div>'
                + '<div class="when">' + esc(r.Username || 'Unknown user') + ' · ' + when(r.Date_Created) + '</div>'
                + '</li>').join('');
        } catch (err) {
            if (current !== requestId) return;
            loading.classList.add('d-none');
            empty.innerHTML = '<i class="fas fa-triangle-exclamation mb-2 d-block" style="font-size:1.4rem;"></i>Unable to load history.';
            empty.classList.remove('d-none');
            console.error(err);
        }
    }

    global.RecordHistory = { open };
})(window);
