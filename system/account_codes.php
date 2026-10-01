<?php
require_once 'config.php';
// Same audience as Master Data — the System tools flag. The chart of accounts
// is reference data other modules will lean on (Purchase Request first), so
// editing it stays with the people who already look after the picklists.
requireSystemToolsAdmin();
$currentUser = getCurrentUser();
$activePage  = 'account_codes';
?>
<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Account Codes | <?= e(APP_NAME) ?></title>
<link rel="icon" type="image/png" href="image/cgm-32.png">
<link href="vendor/css/bootstrap.min.css" rel="stylesheet">
<link rel="stylesheet" href="vendor/css/all.min.css">
<link rel="stylesheet" href="css/base.css">
<style>
.ac-stat { cursor: default; }
.ac-stat.clickable { cursor: pointer; }
.ac-stat.clickable.on { outline: 2px solid #f59e0b; outline-offset: -2px; }

.ac-tabs { border-bottom: 1px solid var(--border); padding: 0 1.25rem; gap: .25rem; }
.ac-tabs .nav-link { border: none; border-bottom: 3px solid transparent; border-radius: 0; color: var(--muted);
                     font-weight: 600; font-size: .86rem; padding: .8rem .9rem; }
.ac-tabs .nav-link.active { color: var(--navy); border-bottom-color: var(--primary); background: transparent; }
.ac-tabs .nav-link .ac-tab-count { font-size: .7rem; background: #e2e8f0; color: #334155; border-radius: 999px;
                                   padding: .05rem .45rem; margin-left: .35rem; }

.ac-toolbar { display: flex; flex-wrap: wrap; gap: .5rem; align-items: center; margin-bottom: .9rem; }
.ac-toolbar .form-select, .ac-toolbar .form-control { font-size: .84rem; }
.ac-toolbar .ac-search { flex: 1 1 220px; min-width: 180px; }
.ac-toolbar .ac-filter { flex: 0 1 230px; min-width: 150px; }
/* Left-aligned: the assistant bubble sits over the bottom-right corner. */
.ac-shown { font-size: .78rem; color: var(--muted); white-space: nowrap; }

/* Long lists scroll inside the card with the header pinned, so the column
   names stay in view across 665 rows. */
.ac-table-wrap { max-height: calc(100vh - 380px); min-height: 280px; overflow-y: auto;
                 border: 1px solid var(--border); border-radius: 10px; }
/* Fixed layout so the column widths set on the <th>s hold; left to auto, the
   short code column stretched and pushed every title to the middle. */
.ac-table-wrap table { margin: 0; font-size: .85rem; table-layout: fixed; width: 100%; }
.ac-table-wrap td { overflow-wrap: anywhere; }
.ac-table-wrap thead th { position: sticky; top: 0; z-index: 1; background: #f8fafc; color: #475569;
                          font-size: .72rem; text-transform: uppercase; letter-spacing: .04em;
                          border-bottom: 1px solid var(--border); white-space: nowrap; }
.ac-table-wrap td { vertical-align: middle; }

.ac-code { display: inline-block; font-family: Consolas, 'Courier New', monospace; font-weight: 700;
           font-size: .8rem; color: #1d4ed8; background: #eff6ff; border: 1px solid #bfdbfe;
           border-radius: 6px; padding: .05rem .45rem; max-width: 100%; overflow-wrap: anywhere; }
.ac-table-wrap .ac-title-cell { color: #0f172a; }
.ac-table-wrap .ac-parent-cell { color: var(--muted); font-size: .8rem; }
.ac-table-wrap .ac-count-cell { color: #334155; font-variant-numeric: tabular-nums; text-align: center; }

/* A heading (5-02-03) groups the accounts under it (5-02-03-010…): drawn as a
   band, with its accounts indented beneath, so the two levels that share one
   table read as two levels. */
.ac-table-wrap tr.ac-heading td { background: #f1f5f9; }
.ac-table-wrap tr.ac-heading .ac-title-cell { font-weight: 700; color: var(--navy); }
.ac-table-wrap tr.ac-account .ac-title-cell { padding-left: 1.6rem; }
.ac-kind { font-size: .66rem; font-weight: 700; letter-spacing: .04em; text-transform: uppercase;
           border-radius: 999px; padding: .08rem .45rem; margin-left: .4rem; background: #e2e8f0; color: #334155; }

.ac-issue { display: block; margin-top: .2rem; font-size: .74rem; font-weight: 600; color: #92400e; }
.ac-issue i { color: #d97706; }
.ac-table-wrap tr.ac-flagged td:first-child { box-shadow: inset 3px 0 0 #f59e0b; }

.ac-actions { white-space: nowrap; text-align: center; }
.ac-actions .btn { width: 30px; height: 30px; padding: 0; display: inline-flex; align-items: center;
                   justify-content: center; font-size: .78rem; border-radius: 7px; border-width: 1px; }
.ac-btn-edit { color: #2563eb; background: #eff6ff; border-color: #60a5fa; }
.ac-btn-edit:hover { color: #fff; background: #2563eb; border-color: #2563eb; }
.ac-btn-delete { color: #dc2626; background: #fef2f2; border-color: #f87171; }
.ac-btn-delete:hover { color: #fff; background: #dc2626; border-color: #dc2626; }

.ac-fix-bar { display: flex; align-items: center; gap: .9rem; flex-wrap: wrap; margin-bottom: 1rem;
              background: #fffbeb; border: 1px solid #fcd34d; border-left: 5px solid #f59e0b;
              border-radius: 12px; padding: .8rem 1rem; }
.ac-fix-icon { font-size: 1.35rem; color: #d97706; }
.ac-fix-text { flex: 1 1 280px; display: flex; flex-direction: column; line-height: 1.3; }
.ac-fix-text strong { color: #92400e; font-size: .92rem; }
.ac-fix-text span { color: #a16207; font-size: .8rem; }
.ac-fix-btn { background: #d97706; border: 1px solid #d97706; color: #fff; font-weight: 600; font-size: .84rem;
              border-radius: 8px; padding: .45rem 1rem; white-space: nowrap; }
.ac-fix-btn:hover { background: #b45309; border-color: #b45309; color: #fff; }
/* While the list is filtered, the button reads as the way back out. */
.ac-fix-btn.on { background: #fff; color: #b45309; }
.ac-fix-btn.on:hover { background: #fef3c7; color: #92400e; }

.ac-empty { text-align: center; color: var(--muted); padding: 2.5rem 1rem !important; }
.ac-hint { font-size: .74rem; color: var(--muted); margin-top: .3rem; }
.ac-hint.bad { color: #b45309; font-weight: 600; }
</style>
</head>
<body>
<div class="wrapper">
<?php include 'sidebar.php'; ?>

<div class="main-content">
    <div class="top-navbar">
        <div class="d-flex align-items-center gap-3">
            <button class="btn btn-sm d-lg-none" onclick="toggleSidebar()"><i class="fas fa-bars"></i></button>
            <div>
                <div class="page-title">Account Codes</div>
                <div class="page-subtitle">Chart of accounts — major groups, subcategories and account codes</div>
            </div>
        </div>
        <?php include 'topbar.php'; ?>
    </div>

    <div class="content-area">
        <div class="row g-3 mb-3">
            <div class="col-6 col-xl-3">
                <div class="stat-card ac-stat">
                    <div class="stat-icon blue"><i class="fas fa-layer-group"></i></div>
                    <div><div class="stat-value" id="statMajors">—</div><div class="stat-label">Major Groups</div></div>
                </div>
            </div>
            <div class="col-6 col-xl-3">
                <div class="stat-card ac-stat">
                    <div class="stat-icon teal"><i class="fas fa-folder-tree"></i></div>
                    <div><div class="stat-value" id="statSubs">—</div><div class="stat-label">Subcategories</div></div>
                </div>
            </div>
            <div class="col-6 col-xl-3">
                <div class="stat-card ac-stat">
                    <div class="stat-icon green"><i class="fas fa-hashtag"></i></div>
                    <div><div class="stat-value" id="statAccounts">—</div><div class="stat-label" id="statAccountsLabel">Account Codes</div></div>
                </div>
            </div>
            <div class="col-6 col-xl-3">
                <div class="stat-card ac-stat clickable" id="statIssuesCard" onclick="toggleIssuesFilter()"
                     title="Show only the entries that need attention">
                    <div class="stat-icon amber"><i class="fas fa-triangle-exclamation"></i></div>
                    <div><div class="stat-value" id="statIssues">—</div><div class="stat-label">Need Attention</div></div>
                </div>
            </div>
        </div>

        <!-- Shown only while something needs fixing. The amber card above does
             the same thing, but a card does not look like a button; this does. -->
        <div class="ac-fix-bar d-none" id="acFixBar">
            <i class="fas fa-triangle-exclamation ac-fix-icon"></i>
            <div class="ac-fix-text">
                <strong id="acFixCount">0 entries need fixing</strong>
                <span>Wrong code format, a code used twice, or filed under the wrong subcategory.</span>
            </div>
            <button type="button" class="btn ac-fix-btn" id="acFixBtn" onclick="toggleIssuesFilter()">
                <i class="fas fa-wrench me-1"></i> Show entries to fix
            </button>
        </div>

        <div class="panel">
            <ul class="nav ac-tabs" role="tablist">
                <li class="nav-item"><button class="nav-link active" data-tab="detail" onclick="showTab('detail')"><i class="fas fa-hashtag me-1"></i>Account Codes<span class="ac-tab-count" id="tabCountDetail">0</span></button></li>
                <li class="nav-item"><button class="nav-link" data-tab="sub" onclick="showTab('sub')"><i class="fas fa-folder-tree me-1"></i>Subcategories<span class="ac-tab-count" id="tabCountSub">0</span></button></li>
                <li class="nav-item"><button class="nav-link" data-tab="major" onclick="showTab('major')"><i class="fas fa-layer-group me-1"></i>Major Groups<span class="ac-tab-count" id="tabCountMajor">0</span></button></li>
            </ul>

            <div class="panel-body">
                <!-- Account Codes -->
                <div class="ac-pane" id="pane-detail">
                    <div class="ac-toolbar">
                        <select class="form-select ac-filter" id="fMajor" onchange="onMajorFilter()"><option value="">All major groups</option></select>
                        <select class="form-select ac-filter" id="fSub" onchange="renderDetails()"><option value="">All subcategories</option></select>
                        <select class="form-select ac-filter" id="fKind" onchange="renderDetails()">
                            <option value="">Headings and accounts</option>
                            <option value="account">Accounts only</option>
                            <option value="heading">Headings only</option>
                            <option value="issues">Needs attention</option>
                        </select>
                        <input type="search" class="form-control ac-search" id="fSearch" placeholder="Search code or title…" oninput="renderDetails()">
                        <button class="btn btn-primary-app btn-sm" onclick="openEditor('detail')"><i class="fas fa-plus me-1"></i>Add Account Code</button>
                    </div>
                    <div class="ac-table-wrap">
                        <table class="table table-hover">
                            <thead><tr><th style="width:150px">Code</th><th>Account Title</th><th style="width:260px">Subcategory</th><th style="width:90px" class="text-center">Actions</th></tr></thead>
                            <tbody id="detailBody"><tr><td colspan="4" class="ac-empty"><div class="spinner-border spinner-border-sm text-primary me-2"></div>Loading…</td></tr></tbody>
                        </table>
                    </div>
                    <div class="d-flex mt-2"><span class="ac-shown" id="detailShown"></span></div>
                </div>

                <!-- Subcategories -->
                <div class="ac-pane d-none" id="pane-sub">
                    <div class="ac-toolbar">
                        <select class="form-select ac-filter" id="fSubMajor" onchange="renderSubs()"><option value="">All major groups</option></select>
                        <input type="search" class="form-control ac-search" id="fSubSearch" placeholder="Search code or title…" oninput="renderSubs()">
                        <button class="btn btn-primary-app btn-sm" onclick="openEditor('sub')"><i class="fas fa-plus me-1"></i>Add Subcategory</button>
                    </div>
                    <div class="ac-table-wrap">
                        <table class="table table-hover">
                            <thead><tr><th style="width:120px">Code</th><th>Subcategory</th><th style="width:220px">Major Group</th><th style="width:110px" class="text-center">Account Codes</th><th style="width:90px" class="text-center">Actions</th></tr></thead>
                            <tbody id="subBody"></tbody>
                        </table>
                    </div>
                </div>

                <!-- Major Groups -->
                <div class="ac-pane d-none" id="pane-major">
                    <div class="ac-toolbar">
                        <span class="text-muted small">The top level of the chart. Rarely changed.</span>
                        <button class="btn btn-primary-app btn-sm ms-auto" onclick="openEditor('major')"><i class="fas fa-plus me-1"></i>Add Major Group</button>
                    </div>
                    <div class="ac-table-wrap">
                        <table class="table table-hover">
                            <thead><tr><th style="width:120px">Code</th><th>Major Group</th><th style="width:130px" class="text-center">Subcategories</th><th style="width:90px" class="text-center">Actions</th></tr></thead>
                            <tbody id="majorBody"></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</div>

<!-- One editor for all three levels; the parent picker shows for the two that
     have a parent. -->
<div class="modal fade" data-bs-backdrop="static" id="acModal" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title fw-bold" id="acModalTitle">Add</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
      </div>
      <div class="modal-body">
        <input type="hidden" id="acLevel">
        <input type="hidden" id="acId">
        <div class="mb-3" id="acParentGroup">
          <label class="form-label small fw-semibold" id="acParentLabel">Filed under</label>
          <select class="form-select" id="acParent" onchange="refreshCodeHint()"></select>
        </div>
        <div class="mb-3">
          <label class="form-label small fw-semibold">Account Code</label>
          <input type="text" class="form-control" id="acCode" autocomplete="off" oninput="refreshCodeHint()"
                 style="font-family: Consolas, 'Courier New', monospace;">
          <div class="ac-hint" id="acCodeHint"></div>
        </div>
        <div class="mb-1">
          <label class="form-label small fw-semibold">Account Title</label>
          <input type="text" class="form-control" id="acTitle" autocomplete="off" maxlength="255"
                 onkeydown="if(event.key==='Enter'){event.preventDefault();saveEntry();}">
        </div>
        <div class="ac-issue d-none mt-3" id="acIssues"></div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
        <button type="button" class="btn btn-primary-app" id="acSaveBtn" onclick="saveEntry()"><i class="fas fa-floppy-disk me-1"></i> Save</button>
      </div>
    </div>
  </div>
</div>

<div class="toast-container position-fixed top-0 end-0 p-3" id="toastContainer"></div>
<script src="vendor/js/bootstrap.bundle.min.js"></script>
<script>
const apiUrl = 'api/account_codes.php';
// Same shapes the API enforces on save — used here only for the live hint.
const LEVELS = {
    major:  { label: 'Major Group',  pattern: /^\d+$/,                       example: '5' },
    sub:    { label: 'Subcategory',  pattern: /^\d+-\d{2}$/,                 example: '5-02' },
    detail: { label: 'Account Code', pattern: /^\d+-\d{2}-\d{2}(-\d{3})?$/,  example: '5-02-03 (heading) or 5-02-03-010 (account)' },
};
let ac = { majors: [], subs: [], details: [] };
let majorById = {}, subById = {};

function showToast(msg, type = 'success') {
    const id = 't' + Date.now();
    const bg = type === 'success' ? 'bg-success' : type === 'error' ? 'bg-danger' : 'bg-warning text-dark';
    document.getElementById('toastContainer').insertAdjacentHTML('beforeend',
        `<div id="${id}" class="toast align-items-center text-white ${bg} border-0"><div class="d-flex"><div class="toast-body">${escapeHtml(msg)}</div><button class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button></div></div>`);
    const el = document.getElementById(id);
    new bootstrap.Toast(el, { delay: 4000 }).show();
    el.addEventListener('hidden.bs.toast', () => el.remove());
}
function escapeHtml(v) {
    if (v === null || v === undefined) return '';
    return String(v).replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;').replace(/'/g, '&#39;');
}
const codeTag = code => `<span class="ac-code">${escapeHtml(code || '—')}</span>`;
const issuesHtml = row => (row.issues || []).map(i =>
    `<span class="ac-issue"><i class="fas fa-triangle-exclamation me-1"></i>${escapeHtml(i)}</span>`).join('');
const actionsHtml = (level, id) => `<td class="ac-actions">
    <button class="btn btn-sm ac-btn-edit" title="Edit" onclick="openEditor('${level}', ${id})"><i class="fas fa-pen-to-square"></i></button>
    <button class="btn btn-sm ac-btn-delete" title="Remove" onclick="removeEntry('${level}', ${id})"><i class="fas fa-trash"></i></button>
</td>`;
const subLabel = s => s ? `${s.code} · ${s.title}` : '';
const matches = (row, q) => !q || row.code.toLowerCase().includes(q) || row.title.toLowerCase().includes(q);

async function loadAll() {
    try {
        const res = await fetch(apiUrl);
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Unable to load the account codes.', 'error');
        ac = { majors: data.majors, subs: data.subs, details: data.details };
        majorById = Object.fromEntries(ac.majors.map(m => [m.id, m]));
        subById = Object.fromEntries(ac.subs.map(s => [s.id, s]));
        renderStats();
        fillFilters();
        renderDetails();
        renderSubs();
        renderMajors();
    } catch (err) {
        console.error(err);
        showToast('Unable to load the account codes.', 'error');
    }
}

function renderStats() {
    const accounts = ac.details.filter(d => d.kind === 'account').length;
    const headings = ac.details.length - accounts;
    const issues = [...ac.majors, ...ac.subs, ...ac.details].filter(r => r.issues.length).length;
    document.getElementById('statMajors').textContent = ac.majors.length;
    document.getElementById('statSubs').textContent = ac.subs.length;
    document.getElementById('statAccounts').textContent = accounts;
    document.getElementById('statAccountsLabel').textContent = `Account Codes (+${headings} headings)`;
    document.getElementById('statIssues').textContent = issues;
    document.getElementById('acFixBar').classList.toggle('d-none', issues === 0);
    document.getElementById('acFixCount').textContent = `${issues} ${issues === 1 ? 'entry needs' : 'entries need'} fixing`;
    document.getElementById('tabCountDetail').textContent = ac.details.length;
    document.getElementById('tabCountSub').textContent = ac.subs.length;
    document.getElementById('tabCountMajor').textContent = ac.majors.length;
}

// Filters keep what was picked across a reload (after a save), as long as the
// picked entry still exists.
function fillSelect(id, rows, allLabel, labelOf) {
    const el = document.getElementById(id);
    const keep = el.value;
    el.innerHTML = `<option value="">${allLabel}</option>` +
        rows.map(r => `<option value="${r.id}">${escapeHtml(labelOf(r))}</option>`).join('');
    if (rows.some(r => String(r.id) === keep)) el.value = keep;
}
function fillFilters() {
    fillSelect('fMajor', ac.majors, 'All major groups', m => `${m.code} · ${m.title}`);
    fillSelect('fSubMajor', ac.majors, 'All major groups', m => `${m.code} · ${m.title}`);
    onMajorFilter(false);
}
function onMajorFilter(render = true) {
    const major = document.getElementById('fMajor').value;
    fillSelect('fSub', ac.subs.filter(s => !major || String(s.parent_id) === major), 'All subcategories', subLabel);
    if (render) renderDetails();
}

function showTab(tab) {
    document.querySelectorAll('.ac-tabs .nav-link').forEach(b => b.classList.toggle('active', b.dataset.tab === tab));
    document.querySelectorAll('.ac-pane').forEach(p => p.classList.toggle('d-none', p.id !== 'pane-' + tab));
}

function toggleIssuesFilter() {
    const kind = document.getElementById('fKind');
    kind.value = kind.value === 'issues' ? '' : 'issues';
    if (kind.value === 'issues') {
        document.getElementById('fMajor').value = '';
        onMajorFilter(false);
        document.getElementById('fSearch').value = '';
    }
    showTab('detail');
    renderDetails();
}

function renderDetails() {
    const major = document.getElementById('fMajor').value;
    const sub = document.getElementById('fSub').value;
    const kind = document.getElementById('fKind').value;
    const q = document.getElementById('fSearch').value.trim().toLowerCase();
    document.getElementById('statIssuesCard').classList.toggle('on', kind === 'issues');
    const fixBtn = document.getElementById('acFixBtn');
    fixBtn.classList.toggle('on', kind === 'issues');
    fixBtn.innerHTML = kind === 'issues'
        ? '<i class="fas fa-list me-1"></i> Show all entries'
        : '<i class="fas fa-wrench me-1"></i> Show entries to fix';

    const rows = ac.details.filter(d => {
        const s = subById[d.parent_id];
        if (major && (!s || String(s.parent_id) !== major)) return false;
        if (sub && String(d.parent_id) !== sub) return false;
        if (kind === 'issues') { if (!d.issues.length) return false; }
        else if (kind && d.kind !== kind) return false;
        return matches(d, q);
    });
    // The Needs-attention filter also covers the two upper levels, which live
    // on their own tabs; say so rather than letting them go unseen.
    const upper = [...ac.majors, ...ac.subs].filter(r => r.issues.length).length;
    document.getElementById('detailShown').textContent = `Showing ${rows.length} of ${ac.details.length}`
        + (kind === 'issues' && upper ? ` · ${upper} more on the Subcategories / Major Groups tabs` : '');

    document.getElementById('detailBody').innerHTML = rows.length ? rows.map(d => {
        const s = subById[d.parent_id];
        return `<tr class="ac-${d.kind} ${d.issues.length ? 'ac-flagged' : ''}">
            <td>${codeTag(d.code)}</td>
            <td class="ac-title-cell">${escapeHtml(d.title)}${d.kind === 'heading' ? '<span class="ac-kind">Heading</span>' : ''}${issuesHtml(d)}</td>
            <td class="ac-parent-cell">${s ? escapeHtml(subLabel(s)) : '<span class="ac-issue">No subcategory</span>'}</td>
            ${actionsHtml('detail', d.id)}
        </tr>`;
    }).join('') : `<tr><td colspan="4" class="ac-empty">${kind === 'issues' ? 'Nothing here needs attention.' : 'No account codes match.'}</td></tr>`;
}

function renderSubs() {
    const major = document.getElementById('fSubMajor').value;
    const q = document.getElementById('fSubSearch').value.trim().toLowerCase();
    const counts = {};
    ac.details.forEach(d => { if (d.kind === 'account') counts[d.parent_id] = (counts[d.parent_id] || 0) + 1; });
    const rows = ac.subs.filter(s => (!major || String(s.parent_id) === major) && matches(s, q));
    document.getElementById('subBody').innerHTML = rows.length ? rows.map(s => {
        const m = majorById[s.parent_id];
        return `<tr class="${s.issues.length ? 'ac-flagged' : ''}">
            <td>${codeTag(s.code)}</td>
            <td class="ac-title-cell">${escapeHtml(s.title)}${issuesHtml(s)}</td>
            <td class="ac-parent-cell">${m ? escapeHtml(`${m.code} · ${m.title}`) : '<span class="ac-issue">No major group</span>'}</td>
            <td class="ac-count-cell">${counts[s.id] || 0}</td>
            ${actionsHtml('sub', s.id)}
        </tr>`;
    }).join('') : `<tr><td colspan="5" class="ac-empty">No subcategories match.</td></tr>`;
}

function renderMajors() {
    const counts = {};
    ac.subs.forEach(s => { counts[s.parent_id] = (counts[s.parent_id] || 0) + 1; });
    document.getElementById('majorBody').innerHTML = ac.majors.length ? ac.majors.map(m => `<tr class="${m.issues.length ? 'ac-flagged' : ''}">
            <td>${codeTag(m.code)}</td>
            <td class="ac-title-cell">${escapeHtml(m.title)}${issuesHtml(m)}</td>
            <td class="ac-count-cell">${counts[m.id] || 0}</td>
            ${actionsHtml('major', m.id)}
        </tr>`).join('') : `<tr><td colspan="4" class="ac-empty">No major groups yet.</td></tr>`;
}

// ── Add / edit ──────────────────────────────────────────────────────────
const rowsOf = level => level === 'major' ? ac.majors : level === 'sub' ? ac.subs : ac.details;

function openEditor(level, id = null) {
    const row = id ? rowsOf(level).find(r => r.id === id) : null;
    document.getElementById('acLevel').value = level;
    document.getElementById('acId').value = row ? row.id : '';
    document.getElementById('acModalTitle').textContent = (row ? 'Edit ' : 'Add ') + LEVELS[level].label;

    const parentGroup = document.getElementById('acParentGroup');
    const parent = document.getElementById('acParent');
    parentGroup.classList.toggle('d-none', level === 'major');
    if (level === 'sub') {
        document.getElementById('acParentLabel').textContent = 'Major Group';
        parent.innerHTML = '<option value="">Choose…</option>' + ac.majors.map(m =>
            `<option value="${m.id}">${escapeHtml(`${m.code} · ${m.title}`)}</option>`).join('');
        parent.value = row ? (row.parent_id ?? '') : document.getElementById('fSubMajor').value;
    } else if (level === 'detail') {
        document.getElementById('acParentLabel').textContent = 'Subcategory';
        parent.innerHTML = '<option value="">Choose…</option>' + ac.subs.map(s =>
            `<option value="${s.id}">${escapeHtml(subLabel(s))}</option>`).join('');
        parent.value = row ? (row.parent_id ?? '') : document.getElementById('fSub').value;
    }
    // A new code starts with its group's prefix typed in, since it must.
    const prefix = !row && level !== 'major' && parent.value ? parentCode() + '-' : '';
    document.getElementById('acCode').value = row ? row.code : prefix;
    document.getElementById('acTitle').value = row ? row.title : '';
    const issues = document.getElementById('acIssues');
    issues.innerHTML = row && row.issues.length
        ? row.issues.map(i => `<div><i class="fas fa-triangle-exclamation me-1"></i>${escapeHtml(i)}</div>`).join('') : '';
    issues.classList.toggle('d-none', !(row && row.issues.length));
    refreshCodeHint();
    bootstrap.Modal.getOrCreateInstance(document.getElementById('acModal')).show();
    setTimeout(() => document.getElementById(row ? 'acTitle' : 'acCode').focus(), 350);
}

function parentCode() {
    const level = document.getElementById('acLevel').value;
    const id = parseInt(document.getElementById('acParent').value, 10);
    const p = level === 'sub' ? majorById[id] : level === 'detail' ? subById[id] : null;
    return p ? p.code : '';
}

function refreshCodeHint() {
    const level = document.getElementById('acLevel').value;
    const code = document.getElementById('acCode').value.trim();
    const hint = document.getElementById('acCodeHint');
    const pc = parentCode();
    let text = 'Format: ' + LEVELS[level].example + '.', bad = false;
    if (code && !LEVELS[level].pattern.test(code)) { text = 'Not in the ' + LEVELS[level].example + ' format.'; bad = true; }
    else if (code && pc && !code.startsWith(pc + '-')) { text = `Should start with ${pc}- to sit under the chosen group.`; bad = true; }
    hint.textContent = text;
    hint.classList.toggle('bad', bad);
}

async function saveEntry() {
    const level = document.getElementById('acLevel').value;
    const id = document.getElementById('acId').value;
    const payload = {
        level, id: id || undefined,
        code: document.getElementById('acCode').value.trim(),
        title: document.getElementById('acTitle').value.trim(),
        parent_id: level === 'major' ? undefined : document.getElementById('acParent').value,
    };
    if (!payload.code) return showToast('Account Code is required.', 'warning');
    if (!payload.title) return showToast('Account Title is required.', 'warning');
    if (level !== 'major' && !payload.parent_id) return showToast('Choose where this is filed.', 'warning');
    const btn = document.getElementById('acSaveBtn');
    btn.disabled = true;
    try {
        const res = await fetch(apiUrl, {
            method: id ? 'PUT' : 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload),
        });
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Unable to save.', 'error');
        showToast(data.message || 'Saved.');
        bootstrap.Modal.getInstance(document.getElementById('acModal')).hide();
        loadAll();
    } catch (err) {
        console.error(err);
        showToast('Unable to save.', 'error');
    } finally {
        btn.disabled = false;
    }
}

async function removeEntry(level, id) {
    const row = rowsOf(level).find(r => r.id === id);
    if (!row || !confirm(`Remove ${LEVELS[level].label.toLowerCase()} ${row.code} "${row.title}"?`)) return;
    try {
        const res = await fetch(`${apiUrl}?level=${level}&id=${id}`, { method: 'DELETE' });
        const data = await res.json();
        showToast(data.message || (data.success ? 'Removed.' : 'Unable to remove.'), data.success ? 'success' : 'error');
        if (data.success) loadAll();
    } catch (err) {
        console.error(err);
        showToast('Unable to remove.', 'error');
    }
}

document.addEventListener('DOMContentLoaded', () => {
    // account_codes.php?show=issues opens straight on the entries to fix.
    if (new URLSearchParams(location.search).get('show') === 'issues') document.getElementById('fKind').value = 'issues';
    loadAll();
});
</script>
<?php include 'profile_modal.php'; ?>
<?php include 'assistant.php'; ?>
</body>
</html>
