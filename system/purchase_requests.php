<?php
require_once 'config.php';
requireAuth();
requireModule('purchase_requests');
$currentUser = getCurrentUser();
$activePage  = 'purchase_requests';
?>
<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Purchase Requests | <?= e(APP_NAME) ?></title>
<link rel="icon" type="image/png" href="image/cgm-32.png">
<link href="vendor/css/bootstrap.min.css" rel="stylesheet">
<link rel="stylesheet" href="vendor/css/all.min.css">
<link rel="stylesheet" href="vendor/css/dataTables.bootstrap5.min.css">
<link rel="stylesheet" href="css/base.css">
<link rel="stylesheet" href="css/item-grid.css">
<link rel="stylesheet" href="<?= asset('css/pr-print.css') ?>">
<style>
/* Same card / field / chip vocabulary as Purchase Orders and Competitive
   Bidding, so the three screens read as one system. Only what this page adds
   is here. */
.table-actions { display: flex; gap: .35rem; flex-wrap: nowrap; }
.po-card { background: #fff; border: 1px solid #e2e8f0; border-radius: 14px; padding: 1.25rem 1.4rem; margin-bottom: 1.1rem; }
.po-card-title { display: flex; align-items: center; gap: .55rem; font-weight: 700; color: var(--navy); font-size: 1rem; margin-bottom: 1.1rem; }
.po-card-title i { color: var(--primary); font-size: 1.05rem; }
.po-field-group { margin-bottom: 1rem; }
.po-field-group > label { font-size: .8rem; font-weight: 600; color: var(--muted); margin-bottom: .3rem; display: block; }
.po-field-group .form-control, .po-field-group .form-select { font-size: .87rem; min-height: 38px; }
.pr-autogrow { resize: none; overflow: hidden; }
.po-field-hint { font-size: .72rem; color: var(--muted); margin-top: .25rem; line-height: 1.35; }
.req::after { content: ' *'; color: #dc2626; font-weight: 700; }

.badge-status.st-draft     { background: linear-gradient(135deg,#64748b,#94a3b8); color: #fff; }
.badge-status.st-submitted { background: linear-gradient(135deg,#d97706,#f59e0b); color: #fff; }
.badge-status.st-returned  { background: linear-gradient(135deg,#c2410c,#ea580c); color: #fff; }
.badge-status.st-approved  { background: linear-gradient(135deg,#15803d,#22c55e); color: #fff; }
.badge-status.st-cancelled { background: linear-gradient(135deg,#991b1b,#dc2626); color: #fff; }

/* Count chips — readouts and controls at once, exactly as on the PO list. */
.po-count-chips { display: flex; gap: .4rem; flex-wrap: wrap; }
.po-chip {
    display: inline-flex; align-items: center; gap: .4rem; padding: .3rem .7rem; border-radius: 20px;
    font-size: .75rem; font-weight: 600; white-space: nowrap; border: 1px solid var(--border); background: #fff; color: var(--muted);
    cursor: pointer; transition: background .15s, border-color .15s, color .15s;
}
.po-chip:hover { background: #f8fafc; }
.po-chip-num { font-weight: 700; color: var(--text); font-variant-numeric: tabular-nums; }
.po-chip-draft     { border-color: #cbd5e1; color: #475569; }
.po-chip-submitted { border-color: #fcd34d; color: #b45309; }
.po-chip-returned  { border-color: #fdba74; color: #c2410c; }
.po-chip-approved  { border-color: #86efac; color: #15803d; }
.po-chip-cancelled { border-color: #fca5a5; color: #b91c1c; }
.po-chip-draft .po-chip-num { color: #475569; } .po-chip-submitted .po-chip-num { color: #b45309; }
.po-chip-returned .po-chip-num { color: #c2410c; } .po-chip-approved .po-chip-num { color: #15803d; }
.po-chip-cancelled .po-chip-num { color: #b91c1c; }
.po-chip.is-active { background: linear-gradient(135deg,#1e3a8a,#2563eb); border-color: transparent; color: #fff; }
.po-chip.is-active .po-chip-num { color: #fff; }
.po-chip-draft.is-active     { background: linear-gradient(135deg,#64748b,#94a3b8); }
.po-chip-submitted.is-active { background: linear-gradient(135deg,#d97706,#f59e0b); }
.po-chip-returned.is-active  { background: linear-gradient(135deg,#c2410c,#ea580c); }
.po-chip-approved.is-active  { background: linear-gradient(135deg,#15803d,#22c55e); }
.po-chip-cancelled.is-active { background: linear-gradient(135deg,#991b1b,#dc2626); }

.pr-num-pill { display: inline-block; padding: .18rem .55rem; border-radius: 6px; font-size: .8rem; font-weight: 700; white-space: nowrap;
               color: #1d4ed8; background: #e8f0ff; border: 1px solid #bfdbfe; }
#prTable tbody td { vertical-align: top; }
/* Nine columns have to fit beside the sidebar without the Actions column being
   pushed off the right edge, so cell padding and the two wrapping columns are
   kept tight. */
#prTable th, #prTable td { padding-left: .55rem; padding-right: .55rem; }
#prTable td:last-child, #prTable th:last-child { padding-right: .4rem; }
.pr-wrap-cell { display: block; max-width: 185px; min-width: 110px; white-space: normal; word-break: break-word; }
.pr-sub { display: block; font-size: .72rem; color: var(--muted); }
.pr-amount { font-variant-numeric: tabular-nums; white-space: nowrap; text-align: right; font-weight: 600; }
.pr-empty { color: var(--muted); }

.modal-pr-wide { max-width: min(1600px, 96vw); }
.po-modal-header-v2 { background: linear-gradient(135deg, var(--navy), #1e40af); color: #fff; border-bottom: none; padding: 1.25rem 1.5rem; display: flex; align-items: center; gap: 1rem; }
.po-header-icon { width: 46px; height: 46px; border-radius: 14px; flex: 0 0 auto; background: rgba(255,255,255,.18); display: flex; align-items: center; justify-content: center; font-size: 1.25rem; color: #fff; }
.po-header-text { flex: 1 1 auto; min-width: 0; }
.po-header-text .modal-title { color: #fff; font-weight: 700; font-size: 1.15rem; margin: 0; }
.po-header-text small { display: block; color: rgba(255,255,255,.72); font-size: .8rem; margin-top: .1rem; }
.po-header-close { width: 38px; height: 38px; flex: 0 0 auto; border-radius: 12px; border: 1px solid rgba(255,255,255,.3); background: rgba(255,255,255,.12); color: #fff; display: flex; align-items: center; justify-content: center; transition: background .15s; }
.po-header-close:hover { background: rgba(255,255,255,.25); color: #fff; }
#prModal .modal-content, #prReasonModal .modal-content, #prImportModal .modal-content { color: #1e293b; }

.pr-form-scroll { overflow-y: auto; max-height: 62vh; padding-right: .4rem; }
.po-card-compact { padding: .95rem 1.15rem; }
.po-card-compact .po-card-title { margin-bottom: .65rem; font-size: .92rem; }
.po-card-compact .po-field-group { margin-bottom: .4rem; }
.po-card-compact .po-field-group > label { font-size: .72rem; margin-bottom: .15rem; }
.tight-field-row .po-field-group { margin-bottom: .6rem; }
.po-input-wrap { position: relative; }
.po-input-wrap > .po-input-icon { position: absolute; left: 5px; top: 50%; transform: translateY(-50%); width: 28px; height: 28px; border-radius: 8px; background: rgba(37,99,235,.1); color: var(--primary); display: flex; align-items: center; justify-content: center; font-size: .78rem; pointer-events: none; }
.po-input-wrap > .form-control, .po-input-wrap > .form-select { padding-left: 40px; }
.po-input-wrap > textarea.form-control { padding-top: .55rem; }
.po-input-wrap > textarea.form-control ~ .po-input-icon { top: 19px; transform: none; }
.po-card-compact .po-input-wrap > .po-input-icon { width: 22px; height: 22px; font-size: .68rem; }
.po-card-compact .po-input-wrap > .form-control, .po-card-compact .po-input-wrap > .form-select { padding: .3rem .5rem .3rem 32px; min-height: 32px; font-size: .8rem; }
.po-card-compact .po-input-wrap > textarea.form-control ~ .po-input-icon { top: 5px; }
.pr-numberbox { font-weight: 700; color: #1d4ed8; background: #f1f5f9 !important; }

.po-items-empty { display: flex; flex-direction: column; align-items: center; justify-content: center; text-align: center; padding: 3rem 1.5rem; }
.po-items-empty-icon { width: 74px; height: 74px; border-radius: 50%; background: rgba(37,99,235,.1); color: var(--primary); display: flex; align-items: center; justify-content: center; font-size: 1.7rem; margin-bottom: 1rem; }
.po-items-empty-title { font-weight: 700; color: #1e293b; margin-bottom: .2rem; }
.po-items-empty-sub { color: #64748b; font-size: .88rem; }
.po-add-row-link { display: block; text-align: center; padding: .6rem; font-size: .85rem; font-weight: 600; color: var(--primary); border: 1px dashed var(--border); border-radius: 8px; margin-top: .6rem; background: none; width: 100%; cursor: pointer; }
.po-add-row-link:hover { background: rgba(37,99,235,.05); }
.po-items-footer-bar { display: flex; align-items: center; justify-content: space-between; flex-wrap: wrap; gap: .5rem; background: rgba(37,99,235,.05); border: 1px solid #e2e8f0; border-radius: 10px; padding: .7rem 1.1rem; margin-top: .75rem; }
.po-items-footer-bar .po-items-count { color: #475569; font-size: .9rem; }
.po-items-footer-bar .po-items-count strong { color: #1e293b; }
#prSubtotal { font-size: 1.4rem; font-weight: 700; color: var(--navy); }
/* A viewed PR has no row buttons and no add-row controls. */
#prModal.pr-viewing #prItemsTable th:last-child, #prModal.pr-viewing #prItemsTable td:last-child { display: none; }
#prModal.pr-viewing .pr-edit-only { display: none !important; }

.import-guide { background: rgba(37,99,235,.06); border: 1px solid rgba(37,99,235,.2); border-radius: 8px; padding: .65rem .9rem; font-size: .82rem; color: #1e293b; }
.import-guide code { font-size: .8rem; }
.imp-eg { width: 100%; border-collapse: collapse; font-size: .74rem; margin: .45rem 0 .2rem; }
.imp-eg th, .imp-eg td { border: 1px solid #d7dee8; padding: .22rem .45rem; white-space: nowrap; }
.imp-eg th { background: #eef2f7; font-weight: 700; color: var(--navy); font-size: .68rem; text-transform: uppercase; letter-spacing: .03em; }
.imp-eg td.blank { background: #f8fafc; }
.imp-eg tr.eg-title td { background: #eef6ff; font-weight: 700; color: #1d4ed8; }
.imp-eg tr.eg-bad td { background: #fff5f5; }
.imp-eg .tag { font-size: .66rem; font-weight: 700; padding: .04rem .3rem; border-radius: 20px; white-space: nowrap; }
.imp-eg .tag.ok  { background: #dcfce7; color: #15803d; }
.imp-eg .tag.no  { background: #fee2e2; color: #b91c1c; }
.imp-eg td.note  { white-space: normal; border: 0; background: none; font-size: .7rem; color: var(--muted); padding-left: .5rem; }
.pr-imp-preview { max-height: 34vh; overflow: auto; border: 1px solid #e2e8f0; border-radius: 8px; }
.pr-imp-preview table { font-size: .78rem; margin: 0; }
.pr-imp-preview td.num { text-align: right; font-variant-numeric: tabular-nums; }

/* History timeline in the View dialog. */
.pr-timeline { list-style: none; margin: 0; padding: 0 0 0 .3rem; }
.pr-timeline li { position: relative; padding: 0 0 .75rem 1.3rem; border-left: 2px solid #e2e8f0; font-size: .8rem; }
.pr-timeline li:last-child { border-left-color: transparent; padding-bottom: 0; }
.pr-timeline li::before { content: ''; position: absolute; left: -6px; top: .25rem; width: 10px; height: 10px; border-radius: 50%; background: var(--primary); }
.pr-timeline .who { font-weight: 700; color: #1e293b; }
.pr-timeline .when { color: var(--muted); font-size: .72rem; }
.pr-timeline .why { color: #475569; font-style: italic; margin-top: .1rem; }

/* Loading overlays with a progress bar (modal opens first, bar sits on top). */
.pr-loader { display: flex; align-items: center; justify-content: center; background: rgba(255, 255, 255, .78); }
.pr-loader.d-none { display: none !important; }
#prBusy { position: fixed; inset: 0; z-index: 2000; }
#prModalLoading { position: absolute; inset: 0; z-index: 20; border-radius: inherit; }
.pr-loader .pr-busy-box { width: min(360px, 86vw); padding: 20px 22px; background: #fff; border-radius: 10px; box-shadow: 0 8px 30px rgba(0,0,0,.18); text-align: center; color: #333; font-size: 14px; }
.pr-loader .pr-busy-text, .pr-pdf-progress .pr-busy-text { font-weight: 600; margin-bottom: 12px; }
.pr-loader .pr-bar { height: 8px; background: #e6e9ee; border-radius: 4px; overflow: hidden; }
.pr-loader .pr-bar > span { display: block; height: 100%; width: 0; background: currentColor; border-radius: 4px; transition: width .15s linear; }
.pr-loader.indeterminate .pr-bar > span { width: 40%; transition: none; animation: prBusySlide 1.1s ease-in-out infinite; }
@keyframes prBusySlide { from { transform: translateX(-100%); } to { transform: translateX(250%); } }
.pr-loader .pr-busy-pct { margin-top: 8px; font-size: 12px; color: #777; min-height: 16px; }

/* Print preview */
.pp-preview-body { background: #e2e8f0; padding: 24px; min-height: 60vh; }
.pp-offscreen-source { position: absolute; top: 0; left: 0; height: 0; overflow: hidden; }
.pdf-preview-frame { width: 100%; height: 70vh; border: none; background: #fff; }
.pp-toolbar { display: flex; flex-wrap: wrap; align-items: center; gap: .75rem; background: #fff; border: 1px solid #cbd5e1; border-radius: 10px; padding: .5rem .75rem; margin-bottom: .75rem; }
.pp-tool { display: flex; align-items: center; gap: .4rem; }
.pp-tool label { margin: 0; font-size: .72rem; font-weight: 700; text-transform: uppercase; letter-spacing: .03em; color: #64748b; white-space: nowrap; }
.pp-tool select { width: auto; min-width: 12rem; font-size: .82rem; }
.pp-layout-status { margin-left: auto; font-size: .8rem; color: #334155; display: flex; align-items: center; gap: .35rem; }
.pp-layout-status .pp-sheets { font-weight: 700; color: var(--navy, #1e3a8a); }
.pp-layout-status .pp-layout-note { color: #b45309; }
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
                <div class="page-title">Purchase Requests</div>
                <div class="page-subtitle">Raise, approve and print a request &mdash; the first step before canvass, abstract and purchase order.</div>
            </div>
        </div>
        <div class="d-flex align-items-center gap-2">
            <div class="d-flex gap-2 no-print">
                <button class="btn btn-primary-app btn-sm" id="btnAddPr"><i class="fas fa-plus me-1"></i> New Purchase Request</button>
            </div>
            <?php include 'topbar.php'; ?>
        </div>
    </div>

    <div class="content-area">
        <div class="panel mb-3 no-print">
            <div class="panel-body">
                <div class="row g-2 align-items-end">
                    <div class="col-sm-6 col-md-3">
                        <label class="form-label">Search</label>
                        <input type="text" id="searchText" class="form-control" placeholder="PR no., office, purpose, requested by"
                               onkeydown="if(event.key==='Enter')loadPRs()">
                    </div>
                    <div class="col-sm-6 col-md-2"><label class="form-label">From</label><input type="date" id="filterFrom" class="form-control"></div>
                    <div class="col-sm-6 col-md-2"><label class="form-label">To</label><input type="date" id="filterTo" class="form-control"></div>
                    <div class="col-sm-6 col-md-2">
                        <label class="form-label">Status</label>
                        <select id="filterStatus" class="form-select" onchange="loadPRs()">
                            <option value="">All</option>
                            <option>Draft</option><option>Submitted</option><option>Returned</option><option>Approved</option><option>Cancelled</option>
                        </select>
                    </div>
                    <div class="col-sm-6 col-md-3 d-flex gap-2">
                        <button class="btn btn-primary-app w-100" onclick="loadPRs()"><i class="fas fa-search me-1"></i> Filter</button>
                        <button class="btn btn-outline-secondary w-100" onclick="resetFilters()"><i class="fas fa-eraser me-1"></i> Clear</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel">
            <div class="panel-header d-flex align-items-center justify-content-between flex-wrap gap-2">
                <span class="panel-title"><i class="fas fa-file-pen me-2 text-primary"></i>Purchase Requests</span>
                <div class="po-count-chips no-print">
                    <button type="button" class="po-chip" data-status="" onclick="setStatusFilter('')">Total <span class="po-chip-num" id="countTotal">&mdash;</span></button>
                    <span id="prStatusChips"></span>
                </div>
            </div>
            <div class="panel-body">
                <div id="prListLoading" class="text-center text-muted py-5">
                    <div class="spinner-border text-primary" role="status"></div>
                    <div class="small mt-2">Loading purchase requests&hellip;</div>
                </div>
                <table id="prTable" class="app-table table table-hover table-bordered d-none" style="width:100%">
                    <thead>
                        <tr>
                            <th>PR No.</th><th>Date</th><th>Office</th><th>Purpose</th><th>Requested by</th>
                            <th>Items</th><th>Estimated Total</th><th>Status</th><th>Actions</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>
</div>
</div>

<div class="toast-container position-fixed top-0 end-0 p-3" id="toastContainer"></div>

<!-- Purchase Request modal: create, edit and view are the same dialog. It opens
     FIRST and empty, with the loading bar over it, and fills in behind the bar. -->
<div class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" id="prModal" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-xl modal-pr-wide modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header po-modal-header-v2">
        <div class="po-header-icon"><i class="fas fa-file-pen"></i></div>
        <div class="po-header-text">
            <h5 class="modal-title" id="prModalTitle">New Purchase Request</h5>
            <small id="prModalSubtitle">Fill in the details below. It is saved as a draft until you submit it.</small>
        </div>
        <button type="button" class="po-header-close" data-bs-dismiss="modal" aria-label="Close"><i class="fas fa-times"></i></button>
      </div>
      <div id="prModalLoading" class="pr-loader d-none" role="status" aria-live="polite">
        <div class="pr-busy-box">
          <div class="pr-busy-text">Loading the request, please wait&hellip;</div>
          <div class="pr-bar text-primary"><span></span></div>
          <div class="pr-busy-pct"></div>
        </div>
      </div>
      <div class="modal-body">
        <form id="prForm" onsubmit="return false;">
          <input type="hidden" id="prId"><input type="hidden" id="prVersion">
          <div class="row g-4">
            <div class="col-lg-5">
              <div class="pr-form-scroll">

                <div class="po-card po-card-compact">
                  <div class="po-card-title"><i class="fas fa-file-signature"></i> Request Details</div>
                  <div class="row g-2 tight-field-row">
                    <div class="col-md-6 po-field-group">
                      <label>PR Number</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-hashtag"></i></span><input type="text" id="prNumberShown" class="form-control pr-numberbox" readonly value="Issued when saved"></div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label class="req">PR Date</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-calendar-day"></i></span><input type="date" id="pr_date" class="form-control"></div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>Fund Cluster / Source of Fund</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-coins"></i></span><input type="text" id="fund_cluster" class="form-control" placeholder="e.g. General Fund" autocomplete="off"></div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>Category</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-tags"></i></span><input type="text" id="category" class="form-control" list="dlCategories" autocomplete="off"></div>
                    </div>
                  </div>
                </div>

                <div class="po-card po-card-compact">
                  <div class="po-card-title"><i class="fas fa-building"></i> Requesting Office</div>
                  <div class="row g-2 tight-field-row">
                    <div class="col-12 po-field-group">
                      <label class="req">Department / Office</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-building-columns"></i></span><input type="text" id="department" class="form-control" list="dlDepartments" autocomplete="off"></div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>Division</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-sitemap"></i></span><input type="text" id="division" class="form-control" list="dlDivisions" autocomplete="off"></div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>Section</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-diagram-project"></i></span><input type="text" id="section" class="form-control" list="dlSections" autocomplete="off"></div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>Responsibility Center Code</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-barcode"></i></span><input type="text" id="responsibility_center_code" class="form-control" autocomplete="off"></div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>Proposed Mode of Procurement</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-scale-balanced"></i></span><input type="text" id="mode_of_procurement" class="form-control" list="dlModes" autocomplete="off"></div>
                    </div>
                  </div>
                </div>

                <div class="po-card po-card-compact">
                  <div class="po-card-title"><i class="fas fa-bullseye"></i> Purpose</div>
                  <div class="po-field-group">
                    <label class="req">Purpose of the request</label>
                    <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-align-left"></i></span><textarea id="purpose" class="form-control pr-autogrow" rows="2" placeholder="What the items are for"></textarea></div>
                  </div>
                  <div class="po-field-group">
                    <label>Remarks</label>
                    <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-comment-dots"></i></span><textarea id="remarks" class="form-control pr-autogrow" rows="1"></textarea></div>
                  </div>
                </div>

                <div class="po-card po-card-compact">
                  <div class="po-card-title"><i class="fas fa-signature"></i> Signatories</div>
                  <div class="row g-2 tight-field-row">
                    <div class="col-md-6 po-field-group"><label>Requested by</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-user"></i></span><input type="text" id="requested_by" class="form-control" list="dlSignatories" autocomplete="off" data-title="requested_by_position"></div></div>
                    <div class="col-md-6 po-field-group"><label>Designation</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-id-badge"></i></span><input type="text" id="requested_by_position" class="form-control" autocomplete="off"></div></div>
                    <div class="col-md-6 po-field-group"><label>Funds certified by</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-user-check"></i></span><input type="text" id="funds_certified_by" class="form-control" list="dlSignatories" autocomplete="off" data-title="funds_certified_position"></div></div>
                    <div class="col-md-6 po-field-group"><label>Designation</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-id-badge"></i></span><input type="text" id="funds_certified_position" class="form-control" autocomplete="off"></div></div>
                    <div class="col-md-6 po-field-group"><label>Approved by</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-user-tie"></i></span><input type="text" id="approved_by" class="form-control" list="dlSignatories" autocomplete="off" data-title="approved_by_position"></div></div>
                    <div class="col-md-6 po-field-group"><label>Designation</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-id-badge"></i></span><input type="text" id="approved_by_position" class="form-control" autocomplete="off"></div></div>
                  </div>
                  <div class="po-field-hint">Pick a name and the designation fills in from Master Data &rarr; Signatories. Approving the request in this system is a separate step and does not sign the form.</div>
                </div>

                <div class="po-card po-card-compact d-none" id="prHistoryCard">
                  <div class="po-card-title"><i class="fas fa-clock-rotate-left"></i> History</div>
                  <ul class="pr-timeline" id="prTimeline"></ul>
                </div>

              </div>
            </div>

            <div class="col-lg-7">
              <div class="po-card h-100 d-flex flex-column">
                <div class="d-flex align-items-center justify-content-between mb-2 flex-wrap gap-2">
                    <div class="po-card-title mb-0"><i class="fas fa-boxes-stacked"></i> Items <span class="text-muted fw-normal small ms-1">&mdash; estimated costs</span></div>
                    <button type="button" class="btn btn-sm btn-outline-success rounded-pill pr-edit-only" onclick="addItemRow({}, null, true)"><i class="fas fa-plus me-1"></i> Add Item</button>
                </div>
                <div class="d-flex flex-wrap gap-2 mb-3 pr-edit-only">
                    <input type="file" id="itemImportInput" accept=".xlsx,.csv" class="d-none">
                    <button type="button" class="btn btn-sm btn-outline-secondary rounded-pill" onclick="document.getElementById('itemImportInput').click()"><i class="fas fa-file-import me-1"></i> Import Items</button>
                    <button type="button" class="btn btn-sm btn-outline-secondary rounded-pill" onclick="toggleItemGuide()" title="What does an import file need?"><i class="fas fa-circle-question"></i></button>
                </div>

                <div id="prItemGuide" class="import-guide d-none mb-3">
                  <div class="fw-semibold mb-1"><i class="fas fa-circle-info me-1"></i>Rows, and the import file format</div>
                  The first row of an import file must be column headers. <strong>Description</strong> is the only one you have to include.
                  Add any of <code>Unit, Qty, Estimated Unit Cost, Estimated Total Cost, Stock No, Remarks</code>, in any order. An <code>Item No.</code> column is ignored.
                  The file is <strong>read and shown to you first</strong> &mdash; nothing is saved until you press Save.
                  <div class="mt-2 mb-1"><strong>Every row must be one of two shapes:</strong></div>
                  <table class="imp-eg">
                    <thead><tr><th>Unit</th><th>Qty</th><th>Description</th><th>Est. Unit Cost</th><th>Est. Total</th><th></th></tr></thead>
                    <tbody>
                      <tr><td>box</td><td>10</td><td>Ballpen, black</td><td>120.00</td><td>1,200.00</td><td class="note"><span class="tag ok">ITEM</span> Unit, Qty and Unit Cost filled &mdash; the total is worked out</td></tr>
                      <tr class="eg-title"><td class="blank"></td><td class="blank"></td><td>LOT 1 &mdash; OFFICE SUPPLIES</td><td class="blank"></td><td class="blank"></td><td class="note"><span class="tag ok">TITLE</span> a heading &mdash; every other column blank</td></tr>
                      <tr><td>lot</td><td class="blank"></td><td>Repair of roofing</td><td class="blank"></td><td>85,000.00</td><td class="note"><span class="tag ok">ITEM</span> lump sum &mdash; a Total alone is fine</td></tr>
                      <tr class="eg-bad"><td>pc</td><td>5</td><td>Brake cleaner</td><td class="blank"></td><td class="blank"></td><td class="note"><span class="tag no">REJECTED</span> priced like an item but no cost</td></tr>
                    </tbody>
                  </table>
                  A Total that does not equal Qty &times; Unit Cost is rejected rather than quietly changed. Accepted file types: .xlsx, .csv.
                  <a href="purchase_request_item_import_template.csv" download>Download a template</a>.
                </div>

                <div id="prItemsEmpty" class="po-items-empty">
                    <div class="po-items-empty-icon"><i class="fas fa-bag-shopping"></i></div>
                    <div class="po-items-empty-title">No items added yet.</div>
                    <div class="po-items-empty-sub">Click "Add Item" or import a list to get started.</div>
                </div>

                <div id="prItemsWrap" class="table-responsive items-scroll-wrap d-none">
                  <table class="app-table item-grid table table-bordered mb-0" id="prItemsTable">
                    <thead>
                      <tr>
                        <th style="width:3%">#</th>
                        <th style="width:10%">QTY</th>
                        <th style="width:10%">Unit</th>
                        <th style="width:26%">Description</th>
                        <th style="width:13%">Unit Cost</th>
                        <th style="width:14%">Total</th>
                        <th style="width:11%">Remarks</th>
                        <th style="width:13%">Action</th>
                      </tr>
                    </thead>
                    <tbody></tbody>
                  </table>
                </div>

                <button type="button" class="po-add-row-link pr-edit-only" onclick="addItemRow({}, null, true)"><i class="fas fa-plus me-1"></i> Add another row</button>

                <div class="po-items-footer-bar">
                    <div class="po-items-count">Total Items: <strong id="prRowCount">0</strong></div>
                    <div><span class="text-muted small me-1">Estimated Total:</span><span id="prSubtotal">&#8369;0.00</span></div>
                </div>
              </div>
            </div>
          </div>
        </form>
      </div>
      <div class="modal-footer d-flex align-items-center flex-wrap gap-2">
        <div class="me-auto">Status: <span id="prModalStatusBadge" class="badge-status st-draft">Draft</span></div>
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal"><i class="fas fa-times me-1"></i> Close</button>
        <span id="prFooterActions" class="d-flex gap-2 flex-wrap"></span>
      </div>
    </div>
  </div>
</div>

<!-- Reason for Return / Reopen / Cancel -->
<div class="modal fade" data-bs-backdrop="static" id="prReasonModal" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header"><h5 class="modal-title" id="prReasonTitle">Reason</h5><button type="button" class="btn-close" data-bs-dismiss="modal"></button></div>
      <div class="modal-body">
        <div id="prReasonPrompt" class="small text-muted mb-2"></div>
        <textarea id="prReasonText" class="form-control" rows="3" maxlength="500" placeholder="Say why, in a few words"></textarea>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Back</button>
        <button type="button" class="btn btn-primary-app" id="prReasonConfirm">Confirm</button>
      </div>
    </div>
  </div>
</div>

<!-- Import preview: the file has been READ, nothing saved. -->
<div class="modal fade" data-bs-backdrop="static" id="prImportModal" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header"><h5 class="modal-title"><i class="fas fa-file-import me-2"></i>Import preview</h5><button type="button" class="btn-close" data-bs-dismiss="modal"></button></div>
      <div class="modal-body">
        <div id="prImportSummary" class="mb-2"></div>
        <div id="prImportWarnings"></div>
        <div class="pr-imp-preview"><table class="table table-sm table-bordered" id="prImportTable"><thead><tr><th>Unit</th><th>Qty</th><th>Description</th><th>Est. Unit Cost</th><th>Est. Total</th></tr></thead><tbody></tbody></table></div>
        <div class="small text-muted mt-2">Nothing has been saved. The rows go into the item grid; the request is saved only when you press Save.</div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
        <button type="button" class="btn btn-outline-primary" id="prImportAdd">Add to the existing rows</button>
        <button type="button" class="btn btn-primary-app" id="prImportReplace">Replace all rows</button>
      </div>
    </div>
  </div>
</div>

<!-- Print preview -->
<div class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" id="prPrintModal" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-xl modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header po-modal-header-v2">
        <div class="po-header-icon"><i class="fas fa-print"></i></div>
        <div class="po-header-text"><h5 class="modal-title">Purchase Request</h5><small id="prPrintSubtitle">&nbsp;</small></div>
        <button type="button" class="po-header-close" data-bs-dismiss="modal" aria-label="Close"><i class="fas fa-times"></i></button>
      </div>
      <div class="modal-body pp-preview-body">
        <div id="prPrintSource" class="pp-offscreen-source"></div>
        <div class="pp-toolbar no-print">
            <div class="pp-tool">
                <label for="pdfPaperSize"><i class="fas fa-file-lines me-1"></i>Paper</label>
                <select id="pdfPaperSize" class="form-select form-select-sm">
                    <option value="auto">Auto &mdash; smallest sheet that fits</option>
                    <option value="a4">A4 &middot; 8.27 &times; 11.69 in</option>
                    <option value="long">Long / Folio &middot; 8.5 &times; 13 in</option>
                    <option value="legal">Legal &middot; 8.5 &times; 14 in</option>
                    <option value="letter">Letter &middot; 8.5 &times; 11 in</option>
                </select>
            </div>
            <div class="pp-tool">
                <label for="pdfTextSize"><i class="fas fa-text-height me-1"></i>Item text</label>
                <select id="pdfTextSize" class="form-select form-select-sm">
                    <option value="0">Normal</option><option value="1">Larger (+1)</option><option value="2">Largest (+2)</option>
                </select>
            </div>
            <div class="pp-layout-status" id="pdfLayoutStatus"></div>
        </div>
        <div id="prPdfLoading" class="text-center text-muted py-5">
            <div class="spinner-border text-primary" role="status"></div>
            <div class="small mt-2">Generating PDF preview&hellip;</div>
        </div>
        <iframe id="prPdfFrame" class="pdf-preview-frame d-none" title="Purchase Request PDF preview"></iframe>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-outline-secondary" id="prPdfOpenTab" disabled><i class="fas fa-up-right-from-square me-1"></i> Open in Tab</button>
        <button type="button" class="btn btn-outline-primary" id="prPdfDownload" disabled><i class="fas fa-download me-1"></i> Download PDF</button>
        <button type="button" class="btn btn-primary-app" id="prPdfPrint" disabled><i class="fas fa-print me-1"></i> Print</button>
      </div>
    </div>
  </div>
</div>

<div id="prBusy" class="pr-loader d-none" role="status" aria-live="polite">
    <div class="pr-busy-box">
        <div class="pr-busy-text">Loading, please wait&hellip;</div>
        <div class="pr-bar text-primary"><span></span></div>
        <div class="pr-busy-pct"></div>
    </div>
</div>

<datalist id="dlDepartments"></datalist><datalist id="dlDivisions"></datalist><datalist id="dlSections"></datalist>
<datalist id="dlCategories"></datalist><datalist id="dlModes"></datalist><datalist id="dlSignatories"></datalist><datalist id="dlUnits"></datalist>

<?php include 'profile_modal.php'; ?>

<script src="vendor/js/jquery.min.js"></script>
<script src="vendor/js/jquery.dataTables.min.js"></script>
<script src="vendor/js/dataTables.bootstrap5.min.js"></script>
<script src="vendor/js/bootstrap.bundle.min.js"></script>
<script src="vendor/js/html2canvas.min.js"></script>
<script src="vendor/js/jspdf.umd.min.js"></script>
<script src="<?= asset('js/print-pdf.js') ?>"></script>
<script src="<?= asset('js/iar-print.js') ?>"></script>
<script src="<?= asset('js/pr-print.js') ?>"></script>
<script>
const apiUrl    = 'api/purchase_requests.php';
const statusUrl = 'api/purchase_request_status.php';
const parseUrl  = 'api/purchase_request_item_parse.php';
let prTable = null;
let firstLoadDone = false;
let prCurrent = null;            // the request open in the modal (null for a new one)
let prMode = 'new';              // new | edit | view

// ── Small shared helpers ────────────────────────────────────────────────
function escapeHtml(value) {
    if (value === null || value === undefined) return '';
    return String(value).replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;').replace(/'/g, '&#39;');
}
function showToast(msg, type = 'success') {
    const id = 'toast-' + Date.now();
    const bg = type === 'success' ? 'bg-success' : (type === 'warning' ? 'bg-warning text-dark' : 'bg-danger');
    document.getElementById('toastContainer').insertAdjacentHTML('beforeend',
        `<div id="${id}" class="toast align-items-center text-white ${bg} border-0" role="alert"><div class="d-flex"><div class="toast-body">${escapeHtml(msg)}</div>
        <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button></div></div>`);
    const el = document.getElementById(id);
    new bootstrap.Toast(el, { delay: type === 'error' ? 8000 : 4000 }).show();
    el.addEventListener('hidden.bs.toast', () => el.remove());
}
function openModal(id) { const el = document.getElementById(id); const m = bootstrap.Modal.getInstance(el) || new bootstrap.Modal(el); m.show(); return m; }
function hideModal(id) { const m = bootstrap.Modal.getInstance(document.getElementById(id)); if (m) m.hide(); }
function parseMoney(value) {
    if (value === null || value === undefined) return 0;
    const clean = String(value).replace(/[^0-9.\-]/g, '');
    return clean === '' ? 0 : (parseFloat(clean) || 0);
}
function formatMoney(value) { return Number(value || 0).toLocaleString('en-PH', { minimumFractionDigits: 2, maximumFractionDigits: 2 }); }
function todayValue() { const d = new Date(); return d.getFullYear() + '-' + String(d.getMonth() + 1).padStart(2, '0') + '-' + String(d.getDate()).padStart(2, '0'); }
function longDate(v) {
    const d = new Date(String(v || '').replace(' ', 'T'));
    return isNaN(d) ? '' : d.toLocaleDateString('en-PH', { year: 'numeric', month: 'short', day: 'numeric' });
}
function longDateTime(v) {
    const d = new Date(String(v || '').replace(' ', 'T'));
    return isNaN(d) ? '' : d.toLocaleString('en-PH', { year: 'numeric', month: 'short', day: 'numeric', hour: 'numeric', minute: '2-digit' });
}

// Loading overlay with a progress bar. Always pair start() with end() in a
// finally, or a failed request would leave the screen covered. Indeterminate
// while waiting on the server, determinate once rows are being drawn.
function prLoader(idOrElement) {
    const root = typeof idOrElement === 'string' ? document.getElementById(idOrElement) : idOrElement;
    const text = root.querySelector('.pr-busy-text'), bar = root.querySelector('.pr-bar > span'), pct = root.querySelector('.pr-busy-pct');
    const idle = text.textContent;
    return {
        start(message) { text.textContent = message || idle; bar.style.width = ''; pct.textContent = ''; root.classList.add('indeterminate'); root.classList.remove('d-none'); },
        progress(fraction, detail) {
            const v = Math.max(0, Math.min(100, Math.round(fraction * 100)));
            root.classList.remove('indeterminate'); bar.style.width = v + '%'; pct.textContent = v + '%' + (detail ? '  ·  ' + detail : '');
        },
        end() { root.classList.add('d-none'); },
    };
}
const prBusy = prLoader('prBusy');
const prLoad = prLoader('prModalLoading');
let prLoadToken = 0;
function nextPaint() { return new Promise(r => requestAnimationFrame(() => setTimeout(r, 0))); }

// The approval lifecycle. The server decides the status; this is only how each
// one is labelled and coloured, in one place so badge, chips and filter agree.
const PR_STATUS_KEYS = { Draft: 'draft', Submitted: 'submitted', Returned: 'returned', Approved: 'approved', Cancelled: 'cancelled' };
const statusBadge = s => `<span class="badge-status st-${PR_STATUS_KEYS[s] || 'draft'}">${escapeHtml(s)}</span>`;

const PR_FIELDS = ['pr_date', 'fund_cluster', 'category', 'department', 'division', 'section', 'responsibility_center_code',
    'purpose', 'mode_of_procurement', 'requested_by', 'requested_by_position', 'funds_certified_by', 'funds_certified_position',
    'approved_by', 'approved_by_position', 'remarks'];

// ── List ────────────────────────────────────────────────────────────────
function initTable() {
    prTable = $('#prTable').DataTable({
        columns: [
            { data: 'pr_number', render: (d, type) => type === 'display' ? `<span class="pr-num-pill">${d}</span>` : d },
            { data: 'pr_date_disp', className: 'text-nowrap', render: (d, type, row) => type === 'display' ? d : row.pr_date },
            { data: 'office', render: (d, type) => type === 'display' ? `<span class="pr-wrap-cell">${d}</span>` : d },
            { data: 'purpose', render: (d, type) => type === 'display' ? `<span class="pr-wrap-cell">${d}</span>` : d },
            { data: 'requested_by' },
            { data: 'item_count', className: 'text-center' },
            { data: 'total', render: (d, type) => type === 'display' ? `<span class="pr-amount">${formatMoney(d)}</span>` : d },
            { data: 'status_html', className: 'text-center' },
            { data: 'actions', orderable: false, searchable: false },
        ],
        order: [[0, 'desc']],
        pageLength: 15,
    });
}
function formatRow(p) {
    // Department on top; division and section, when there are any, small beneath it.
    const sub = [p.division, p.section].map(s => (s || '').trim()).filter(Boolean).join(' / ');
    const office = escapeHtml((p.department || '').trim()) + (sub ? `<span class="pr-sub">${escapeHtml(sub)}</span>` : '');
    return {
        pr_number: escapeHtml(p.pr_number), pr_date: p.pr_date, pr_date_disp: escapeHtml(longDate(p.pr_date)),
        office: office || '—', purpose: escapeHtml(p.purpose) || '—', requested_by: escapeHtml(p.requested_by) || '—',
        item_count: p.item_count, total: p.total, status_html: statusBadge(p.status), actions: buildActions(p),
    };
}
function buildActions(p) {
    const editable = p.status === 'Draft' || p.status === 'Returned';
    const canDelete = p.status === 'Draft' && window.PR_CAN_DELETE;
    return `<div class="table-actions justify-content-center">
        <button class="btn btn-sm btn-outline-info" title="View" onclick="openPr(${p.id}, 'view')"><i class="fas fa-eye"></i></button>
        <button class="btn btn-sm btn-outline-secondary" title="Print" onclick="openPrPrint(${p.id})"><i class="fas fa-print"></i></button>
        <button class="btn btn-sm btn-outline-primary" title="${editable ? 'Edit' : 'Only a draft or returned request can be edited'}" onclick="openPr(${p.id}, 'edit')" ${editable ? '' : 'disabled'}><i class="fas fa-pen-to-square"></i></button>
        ${canDelete ? `<button class="btn btn-sm btn-outline-danger" title="Delete this draft" onclick="deletePr(${p.id}, '${escapeHtml(p.pr_number)}')"><i class="fas fa-trash"></i></button>` : ''}
    </div>`;
}
function renderCounts(counts) {
    if (!counts) return;
    const fmt = n => Number(n || 0).toLocaleString('en-PH');
    const active = document.getElementById('filterStatus').value;
    document.getElementById('countTotal').textContent = fmt(counts.total);
    document.getElementById('prStatusChips').innerHTML = Object.keys(PR_STATUS_KEYS).map(s =>
        `<button type="button" class="po-chip po-chip-${PR_STATUS_KEYS[s]} ${active === s ? 'is-active' : ''}" data-status="${s}" onclick="setStatusFilter('${s}')">${s} <span class="po-chip-num">${fmt(counts[s])}</span></button>`).join('');
    document.querySelectorAll('.po-chip[data-status=""]').forEach(c => c.classList.toggle('is-active', active === ''));
}
function setStatusFilter(s) { document.getElementById('filterStatus').value = s; loadPRs(); }
function resetFilters() { document.getElementById('searchText').value = ''; document.getElementById('filterStatus').value = ''; document.getElementById('filterFrom').value = ''; document.getElementById('filterTo').value = ''; loadPRs(); }
function initDateFilters() {
    const tomorrow = new Date(); tomorrow.setDate(tomorrow.getDate() + 1);
    document.getElementById('filterTo').value = tomorrow.toISOString().slice(0, 10);
    document.getElementById('filterFrom').value = new Date().getFullYear() + '-01-01';
}
async function loadPRs() {
    const q = new URLSearchParams({ search: document.getElementById('searchText').value.trim(), from: document.getElementById('filterFrom').value,
        to: document.getElementById('filterTo').value, status: document.getElementById('filterStatus').value });
    try {
        const res = await fetch(`${apiUrl}?${q}`);
        const data = await res.json();
        if (!data.success) {
            document.getElementById('prListLoading').innerHTML = `<div class="text-danger small px-3"><i class="fas fa-triangle-exclamation me-1"></i>${escapeHtml(data.message || 'Unable to load purchase requests.')}</div>`;
            return showToast(data.message || 'Unable to load purchase requests.', 'error');
        }
        window.PR_CAN_DELETE = !!data.can_delete;
        renderCounts(data.counts);
        if (!firstLoadDone) {
            document.getElementById('prListLoading').classList.add('d-none');
            document.getElementById('prTable').classList.remove('d-none');
            prTable.columns.adjust();
            firstLoadDone = true;
        }
        prTable.clear().rows.add((data.prs || []).map(formatRow)).draw();
    } catch (err) {
        if (!firstLoadDone) document.getElementById('prListLoading').innerHTML = '<div class="text-danger small"><i class="fas fa-triangle-exclamation me-1"></i>Unable to load purchase requests. <a href="#" onclick="loadPRs();return false;">Retry</a></div>';
        showToast('Unable to load purchase requests.', 'error');
        console.error(err);
    }
}

// ── Item grid ───────────────────────────────────────────────────────────
// A row is either a priced ITEM or a TITLE (description only, everything else
// blank). Total Cost is the test, not Unit + Qty + Unit Cost: a lump-sum line
// has a total with no quantity behind it. The server enforces the same rule and
// works the total out itself — this only explains it before a round trip.
function itemHasValue(v) { return v !== null && v !== undefined && String(v).trim() !== '' && Number(v) !== 0; }
function looksLikeTitleRow(item) {
    return String(item.items_description || '').trim() !== '' && !itemHasValue(item.quantity) && !itemHasValue(item.unit_cost)
        && !itemHasValue(item.total_cost) && !String(item.unit || '').trim();
}
function itemNum(v) { if (v === null || v === undefined || v === '') return ''; const n = Number(v); return isFinite(n) ? String(n) : String(v); }
function setRowType(row, isTitle, refresh = true) {
    row.classList.toggle('item-row-desc', !!isTitle);
    row.dataset.descOnly = isTitle ? '1' : '0';
    const toggle = row.querySelector('.type-toggle');
    if (toggle) toggle.classList.toggle('active', !!isTitle);
    if (isTitle) ['unit', 'quantity', 'unit_cost', 'total_cost'].forEach(f => { const i = row.querySelector(`[data-field="${f}"]`); if (i) i.value = ''; });
    if (refresh) refreshItemsSummary();
}
function toggleRowType(row) { setRowType(row, row.dataset.descOnly !== '1'); }

// bulk = true is for loading a saved request: it skips the per-row renumber and
// per-row height measurement, both of which re-read EVERY row and made drawing n
// rows O(n²). The caller runs refreshItemsSummary() once at the end.
function addItemRow(item = {}, afterRow = null, focus = false, isTitle = null, bulk = false) {
    const tbody = document.querySelector('#prItemsTable tbody');
    const row = document.createElement('tr');
    // Kept on the row, not in a visible field: the ids are what let an edit update
    // an item in place, and stock number is carried through import and edit.
    if (item.pr_item_id) row.dataset.itemId = item.pr_item_id;
    row.dataset.stockNo = item.stock_no || '';
    row.innerHTML = `
        <td class="item-grid-idx"></td>
        <td><input type="number" min="0" step="any" class="form-control form-control-sm" data-field="quantity" value="${itemHasValue(item.quantity) ? itemNum(item.quantity) : ''}"><span class="item-row-dash">&mdash;</span></td>
        <td><input type="text" class="form-control form-control-sm" data-field="unit" list="dlUnits" autocomplete="off" value="${escapeHtml(item.unit)}"><span class="item-row-dash">&mdash;</span></td>
        <td><textarea class="form-control form-control-sm item-desc-input" data-field="items_description" rows="1">${escapeHtml(item.items_description)}</textarea></td>
        <td><input type="number" min="0" step="0.01" class="form-control form-control-sm" data-field="unit_cost" value="${itemHasValue(item.unit_cost) ? itemNum(item.unit_cost) : ''}"><span class="item-row-dash">&mdash;</span></td>
        <td><input type="number" min="0" step="0.01" class="form-control form-control-sm" data-field="total_cost" value="${itemHasValue(item.total_cost) ? itemNum(item.total_cost) : ''}"><span class="item-row-dash">&mdash;</span></td>
        <td><input type="text" class="form-control form-control-sm" data-field="remarks" maxlength="255" value="${escapeHtml(item.remarks)}"></td>
        <td class="text-nowrap"><div class="d-flex gap-1">
            <button type="button" class="item-grid-action-btn add" title="Insert row below" onclick="addItemRow({}, this.closest('tr'), true)"><i class="fas fa-plus"></i></button>
            <button type="button" class="item-grid-action-btn type-toggle" title="Turn into a title / description-only line" onclick="toggleRowType(this.closest('tr'))"><i class="fas fa-align-left"></i></button>
            <button type="button" class="item-grid-action-btn remove" title="Remove row" onclick="this.closest('tr').remove(); refreshItemsSummary();"><i class="fas fa-trash"></i></button>
        </div></td>`;
    const qty = row.querySelector('[data-field="quantity"]'), cost = row.querySelector('[data-field="unit_cost"]'), total = row.querySelector('[data-field="total_cost"]');
    // Total follows QTY x Unit Cost but stays typeable, for a lump sum.
    const recalc = () => { const q = parseFloat(qty.value) || 0, c = parseFloat(cost.value) || 0; if (q && c) total.value = (q * c).toFixed(2); refreshItemsSummary(); };
    qty.addEventListener('input', recalc); cost.addEventListener('input', recalc); total.addEventListener('input', refreshItemsSummary);
    const desc = row.querySelector('.item-desc-input');
    const grow = () => { desc.style.height = 'auto'; desc.style.height = desc.scrollHeight + 'px'; };
    desc.addEventListener('input', grow);
    if (afterRow && afterRow.parentNode === tbody) afterRow.insertAdjacentElement('afterend', row); else tbody.appendChild(row);
    setRowType(row, isTitle === null ? looksLikeTitleRow(item) : isTitle, !bulk);
    if (!bulk) { refreshItemsSummary(); grow(); }
    if (focus) (row.classList.contains('item-row-desc') ? desc : row.querySelector('input'))?.focus();
    return row;
}
function refreshItemsSummary() {
    const rows = [...document.querySelectorAll('#prItemsTable tbody tr')];
    document.getElementById('prItemsWrap').classList.toggle('d-none', rows.length === 0);
    document.getElementById('prItemsEmpty').classList.toggle('d-none', rows.length > 0);
    let total = 0, items = 0;
    rows.forEach((row, i) => {
        row.querySelector('.item-grid-idx').textContent = i + 1;
        if (row.dataset.descOnly !== '1') { total += parseMoney(row.querySelector('[data-field="total_cost"]').value); items++; }
    });
    document.getElementById('prSubtotal').textContent = '₱' + formatMoney(total);
    document.getElementById('prRowCount').textContent = items;
    return total;
}
function collectItems() {
    return [...document.querySelectorAll('#prItemsTable tbody tr')].map(row => {
        const v = f => (row.querySelector(`[data-field="${f}"]`)?.value ?? '').trim();
        return {
            pr_item_id: row.dataset.itemId ? Number(row.dataset.itemId) : undefined,
            stock_no: row.dataset.stockNo || '',
            quantity: v('quantity'), unit: v('unit'), items_description: v('items_description'),
            unit_cost: v('unit_cost'), total_cost: v('total_cost'), remarks: v('remarks'),
            is_title: row.dataset.descOnly === '1',
        };
    }).filter(i => i.items_description !== '');
}
// Mirrors the server's item rules so the same problem is explained here first.
function invalidItemRows() {
    const bad = { item: [], title: [] };
    collectItems().forEach((it, i) => {
        const n = k => parseMoney(it[k]);
        const blank = !it.unit && !n('quantity') && !n('unit_cost') && !n('total_cost');
        if (it.is_title) { if (!blank) bad.title.push(i + 1); } else if (!n('total_cost')) bad.item.push(i + 1);
    });
    return bad;
}
function rowList(rows) { const s = rows.slice(0, 6); return (rows.length === 1 ? 'Row ' : 'Rows ') + '#' + s.join(', #') + (rows.length > 6 ? ` and ${rows.length - 6} more` : ''); }
function toggleItemGuide() { document.getElementById('prItemGuide').classList.toggle('d-none'); }

// ── Import: read the file, PREVIEW it, then put it in the grid ───────────
let prImportPending = null;
async function readImportFile(file) {
    const fd = new FormData(); fd.append('file', file);
    try {
        const res = await fetch(parseUrl, { method: 'POST', body: fd });
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Unable to read that file.', 'error');
        prImportPending = data.items;
        const items = data.items.filter(i => !i.is_title), total = items.reduce((s, i) => s + parseMoney(i.total_cost), 0);
        document.getElementById('prImportSummary').innerHTML =
            `<strong>${data.items.length}</strong> row(s) read &mdash; ${items.length} item(s), ${data.items.length - items.length} title row(s), estimated total <strong>₱${formatMoney(total)}</strong>.`;
        document.getElementById('prImportWarnings').innerHTML = (data.warnings || []).map(w => `<div class="alert alert-warning py-1 px-2 small mb-2"><i class="fas fa-triangle-exclamation me-1"></i>${escapeHtml(w)}</div>`).join('');
        document.querySelector('#prImportTable tbody').innerHTML = data.items.slice(0, 200).map(i =>
            `<tr class="${i.is_title ? 'table-primary' : ''}"><td>${escapeHtml(i.unit)}</td><td class="num">${escapeHtml(i.quantity)}</td><td>${escapeHtml(i.items_description).replace(/\n/g, '<br>')}</td><td class="num">${i.unit_cost ? formatMoney(i.unit_cost) : ''}</td><td class="num">${i.total_cost ? formatMoney(i.total_cost) : ''}</td></tr>`).join('')
            + (data.items.length > 200 ? `<tr><td colspan="5" class="text-muted small">…and ${data.items.length - 200} more rows.</td></tr>` : '');
        const hasRows = [...document.querySelectorAll('#prItemsTable tbody tr')].some(r => r.querySelector('.item-desc-input').value.trim() !== '');
        document.getElementById('prImportAdd').classList.toggle('d-none', !hasRows);
        document.getElementById('prImportReplace').textContent = hasRows ? 'Replace all rows' : 'Add to the grid';
        openModal('prImportModal');
    } catch (err) { showToast('Unable to read that file.', 'error'); console.error(err); }
}
function applyImport(replace) {
    if (!prImportPending) return;
    const tbody = document.querySelector('#prItemsTable tbody');
    if (replace) {
        // Rows that were loaded from the server keep their id only if they are
        // re-posted; a replaced row simply is not, and the server soft-deletes it.
        tbody.innerHTML = '';
    } else {
        // A single still-blank starter row is dropped rather than kept above the import.
        const rows = [...tbody.querySelectorAll('tr')];
        if (rows.length === 1 && [...rows[0].querySelectorAll('input, textarea')].every(i => i.value.trim() === '')) rows[0].remove();
    }
    prImportPending.forEach(item => addItemRow(item, null, false, !!item.is_title, true));
    refreshItemsSummary(); regrowModalFields();
    hideModal('prImportModal');
    showToast(prImportPending.length + ' row(s) added to the grid. Press Save to keep them.');
    prImportPending = null;
}

// ── Form ────────────────────────────────────────────────────────────────
function autoGrowField(el) { el.style.height = 'auto'; el.style.height = el.scrollHeight + 'px'; }
// Batched — every reset, then every read, then every write — because a per-element
// scrollHeight read forces a layout each time, and across hundreds of description
// boxes that froze the page.
function regrowModalFields() {
    const fields = [...document.querySelectorAll('#prModal .pr-autogrow, #prItemsTable .item-desc-input')];
    fields.forEach(el => { el.style.height = 'auto'; });
    const h = fields.map(el => el.scrollHeight);
    fields.forEach((el, i) => { el.style.height = h[i] + 'px'; });
}
function clearForm() {
    document.getElementById('prId').value = ''; document.getElementById('prVersion').value = '';
    PR_FIELDS.forEach(f => { const el = document.getElementById(f); if (el) el.value = ''; });
    document.getElementById('prNumberShown').value = 'Issued when saved';
    document.querySelector('#prItemsTable tbody').innerHTML = '';
    document.getElementById('prHistoryCard').classList.add('d-none');
    refreshItemsSummary();
    document.querySelectorAll('.pr-autogrow').forEach(autoGrowField);
}
function fillForm(pr) {
    document.getElementById('prId').value = pr.id; document.getElementById('prVersion').value = pr.row_version;
    document.getElementById('prNumberShown').value = pr.pr_number;
    PR_FIELDS.forEach(f => { const el = document.getElementById(f); if (el) el.value = String(pr[f] ?? '').slice(0, el.type === 'date' ? 10 : undefined); });
    document.querySelector('#prItemsTable tbody').innerHTML = '';
    refreshItemsSummary();
    document.querySelectorAll('.pr-autogrow').forEach(autoGrowField);
}
function setFormReadOnly(ro) {
    document.querySelectorAll('#prForm input, #prForm textarea, #prForm select').forEach(el => { if (el.id !== 'prNumberShown' && el.type !== 'hidden') el.disabled = ro; });
    document.getElementById('prModal').classList.toggle('pr-viewing', ro);
}
function setModalChrome(title, subtitle, status) {
    document.getElementById('prModalTitle').textContent = title;
    document.getElementById('prModalSubtitle').textContent = subtitle;
    const b = document.getElementById('prModalStatusBadge');
    b.textContent = status; b.className = 'badge-status st-' + (PR_STATUS_KEYS[status] || 'draft');
}
const HISTORY_WORDS = { create: 'Created as a draft', submit: 'Submitted for approval', approve: 'Approved', return: 'Returned to the requester', reopen: 'Reopened as a draft', cancel: 'Cancelled' };
function renderHistory(history) {
    const card = document.getElementById('prHistoryCard');
    card.classList.toggle('d-none', !history || !history.length);
    document.getElementById('prTimeline').innerHTML = (history || []).map(h =>
        `<li><div class="who">${escapeHtml(HISTORY_WORDS[h.Action] || h.Action)}${h.Action !== 'create' ? ` <span class="text-muted fw-normal">(${escapeHtml(h.From_Status)} → ${escapeHtml(h.To_Status)})</span>` : ''}</div>
        <div class="when">${escapeHtml(h.Actor_Name || '')} · ${escapeHtml(longDateTime(h.Date_Created))}</div>
        ${h.Reason ? `<div class="why">“${escapeHtml(h.Reason)}”</div>` : ''}</li>`).join('');
}

// Time-boxed batches with the bar moving between them, so the percentage follows
// how many items the request actually has. The rows go into a table that is still
// hidden, so a batch costs no layout; the browser lays the grid out once, at the end.
const ROW_SLICE_MS = 24;
async function loadItemRows(items, token) {
    const total = items.length; let i = 0;
    while (i < total) {
        if (token !== prLoadToken) return false;
        const start = performance.now();
        while (i < total && performance.now() - start < ROW_SLICE_MS) addItemRow(items[i++], null, false, null, true);
        prLoad.progress(i / total, i + ' of ' + total + ' items');
        await nextPaint();
    }
    if (token !== prLoadToken) return false;
    prLoad.progress(1, 'Finishing…'); await nextPaint();
    refreshItemsSummary(); regrowModalFields();
    return true;
}

// Footer buttons come from what the SERVER says this person may do right now.
function renderFooter() {
    const box = document.getElementById('prFooterActions');
    const btn = (cls, icon, label, fn) => `<button type="button" class="btn ${cls}" onclick="${fn}"><i class="fas fa-${icon} me-1"></i>${label}</button>`;
    let html = '';
    if (prMode === 'new' || prMode === 'edit') {
        html += btn('btn-outline-primary', 'floppy-disk', prMode === 'new' ? 'Save as Draft' : 'Save', 'savePr(false)');
        html += btn('btn-primary-app', 'paper-plane', 'Save &amp; Submit', 'savePr(true)');
    } else if (prCurrent) {
        html += btn('btn-outline-secondary', 'print', 'Print', `openPrPrint(${prCurrent.id})`);
        if (prCurrent.can_edit) html += btn('btn-outline-primary', 'pen-to-square', 'Edit', "switchToEdit()");
        const a = prCurrent.actions || [];
        if (a.includes('submit'))  html += btn('btn-primary-app', 'paper-plane', 'Submit for approval', "runAction('submit')");
        if (a.includes('approve')) html += btn('btn-success', 'circle-check', 'Approve', "runAction('approve')");
        if (a.includes('return'))  html += btn('btn-warning', 'rotate-left', 'Return', "runAction('return')");
        if (a.includes('reopen'))  html += btn('btn-outline-secondary', 'lock-open', 'Reopen', "runAction('reopen')");
        if (a.includes('cancel'))  html += btn('btn-outline-danger', 'ban', 'Cancel request', "runAction('cancel')");
    }
    box.innerHTML = html;
}

function newPr() {
    prLoadToken++; prLoad.end();
    prCurrent = null; prMode = 'new';
    clearForm();
    document.getElementById('pr_date').value = todayValue();
    setFormReadOnly(false);
    setModalChrome('New Purchase Request', 'Fill in the details below. It is saved as a draft until you submit it.', 'Draft');
    renderFooter();
    openModal('prModal');
    addItemRow({}, null, false);
}

// The modal opens FIRST, empty and inert, with the loading bar over it; the fetch
// and the item rows then fill it in behind the bar. Opening it only after the
// data arrived meant nothing happened on screen for as long as a big request took.
async function openPr(ref, mode) {
    const token = ++prLoadToken;
    prCurrent = null; prMode = 'view';
    clearForm(); setFormReadOnly(true);
    setModalChrome(mode === 'edit' ? 'Edit Purchase Request' : 'Purchase Request', typeof ref === 'number' ? 'Loading…' : 'PR ' + ref, 'Draft');
    renderFooter();
    prLoad.start('Loading the request, please wait…');
    openModal('prModal');
    try {
        const url = typeof ref === 'number' ? `${apiUrl}?id=${ref}` : `${apiUrl}?pr_number=${encodeURIComponent(ref)}`;
        const res = await fetch(url); const data = await res.json();
        if (token !== prLoadToken) return;
        if (!data.success) throw new Error(data.message || 'Unable to load the purchase request.');
        const pr = data.pr;
        fillForm(pr);
        // A request that is no longer editable opens read-only whatever was clicked.
        const editing = mode === 'edit' && pr.can_edit;
        setModalChrome(editing ? 'Edit Purchase Request' : 'Purchase Request',
            pr.pr_number + (pr.purpose ? ' — ' + pr.purpose : ''), pr.status);
        renderHistory(pr.history);
        if (!await loadItemRows(pr.items || [], token)) return;
        prCurrent = pr; prMode = editing ? 'edit' : 'view';
        setFormReadOnly(!editing);
        renderFooter();
        prLoad.end();
        if (mode === 'edit' && !pr.can_edit) showToast('A ' + pr.status.toLowerCase() + ' request cannot be edited.', 'warning');
    } catch (err) {
        if (token !== prLoadToken) return;
        setModalChrome('Unable to load', '', 'Draft'); prLoad.end();
        showToast(err.message || 'Unable to load the purchase request.', 'error'); console.error(err);
    }
}
function switchToEdit() { if (prCurrent && prCurrent.can_edit) { prMode = 'edit'; setFormReadOnly(false); setModalChrome('Edit Purchase Request', document.getElementById('prModalSubtitle').textContent, prCurrent.status); renderFooter(); regrowModalFields(); } }

let prSaving = false;
async function savePr(thenSubmit) {
    if (prSaving) return;
    const id = document.getElementById('prId').value;
    const payload = { id: id ? Number(id) : undefined, row_version: Number(document.getElementById('prVersion').value) || undefined, items: collectItems() };
    PR_FIELDS.forEach(f => { payload[f] = (document.getElementById(f)?.value ?? '').trim(); });
    if (!payload.pr_date)    return showToast('PR Date is required.', 'warning');
    if (!payload.department) return showToast('Department / Office is required.', 'warning');
    if (!payload.purpose)    return showToast('Purpose is required.', 'warning');
    if (payload.items.length === 0) return showToast('Add at least one item.', 'warning');
    const bad = invalidItemRows();
    if (bad.item.length)  return showToast(`${rowList(bad.item)} ${bad.item.length === 1 ? 'has' : 'have'} no Total Cost. Fill in Qty and Unit Cost, enter a Total directly, or make it a title row.`, 'warning');
    if (bad.title.length) return showToast(`${rowList(bad.title)} ${bad.title.length === 1 ? 'is a title row but still carries' : 'are title rows but still carry'} a unit or a cost.`, 'warning');

    prSaving = true;
    document.querySelectorAll('#prFooterActions button').forEach(b => b.disabled = true);
    try {
        const res = await fetch(apiUrl, { method: id ? 'PUT' : 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(payload) });
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Unable to save.', 'error');
        const savedId = data.id;
        if (!thenSubmit) { showToast(data.message || 'Saved.'); hideModal('prModal'); loadPRs(); return; }
        const r2 = await fetch(statusUrl, { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify({ id: savedId, action: 'submit' }) });
        const d2 = await r2.json();
        loadPRs();
        if (d2.success) { showToast(d2.message || 'Submitted.'); hideModal('prModal'); return; }
        // Saved, but not submittable yet: keep it open on the saved copy so the fix is one edit away.
        showToast('Saved as a draft, but not submitted — ' + (d2.message || 'it cannot be submitted yet.'), 'warning');
        openPr(savedId, 'edit');
    } catch (err) { showToast('Unable to save the purchase request.', 'error'); console.error(err); }
    finally { prSaving = false; document.querySelectorAll('#prFooterActions button').forEach(b => b.disabled = false); }
}

// ── Workflow ────────────────────────────────────────────────────────────
const REASON_TEXT = {
    return: ['Return this request', 'Tell the requester what to fix. It goes back to them as editable.'],
    reopen: ['Reopen this request', 'It goes back to a draft and needs approving again.'],
    cancel: ['Cancel this request', 'A cancelled request keeps its number and history but cannot be revived.'],
};
async function postStatus(action, reason) {
    const res = await fetch(statusUrl, { method: 'POST', headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ id: prCurrent.id, action, reason: reason || '' }) });
    return res.json();
}
async function finishAction(data) {
    showToast(data.message || (data.success ? 'Done.' : 'Not done.'), data.success ? 'success' : 'error');
    if (!data.success) return false;
    loadPRs(); hideModal('prModal'); return true;
}
function runAction(action) {
    if (!prCurrent) return;
    if (REASON_TEXT[action]) {
        document.getElementById('prReasonTitle').textContent = REASON_TEXT[action][0];
        document.getElementById('prReasonPrompt').textContent = REASON_TEXT[action][1];
        document.getElementById('prReasonText').value = '';
        const confirmBtn = document.getElementById('prReasonConfirm');
        confirmBtn.onclick = async () => {
            const reason = document.getElementById('prReasonText').value.trim();
            if (reason.length < 5) return showToast('Please give a reason (at least a few words).', 'warning');
            confirmBtn.disabled = true;
            try { const d = await postStatus(action, reason); if (d.success) hideModal('prReasonModal'); await finishAction(d); }
            catch (e) { showToast('Nothing was changed: the request failed.', 'error'); }
            finally { confirmBtn.disabled = false; }
        };
        openModal('prReasonModal');
        return;
    }
    const q = action === 'approve' ? `Approve ${prCurrent.pr_number}? It can no longer be edited after this.` : `Submit ${prCurrent.pr_number} for approval? It cannot be edited while it waits.`;
    if (!confirm(q)) return;
    postStatus(action).then(finishAction).catch(() => showToast('Nothing was changed: the request failed.', 'error'));
}
async function deletePr(id, number) {
    if (!confirm(`Delete draft ${number}? This cannot be undone.`)) return;
    try {
        const res = await fetch(`${apiUrl}?id=${id}`, { method: 'DELETE' }); const data = await res.json();
        showToast(data.message || (data.success ? 'Deleted.' : 'Unable to delete.'), data.success ? 'success' : 'error');
        if (data.success) loadPRs();
    } catch (err) { showToast('Unable to delete the purchase request.', 'error'); console.error(err); }
}

// ── Master data (suggestions) ───────────────────────────────────────────
let signatoryTitleByName = {};
async function loadSuggestions() {
    try {
        const res = await fetch('api/master_data.php'); const data = await res.json();
        if (!data.success) return;
        const c = data.categories || {};
        const fill = (id, rows) => { document.getElementById(id).innerHTML = (rows || []).map(r => `<option value="${escapeHtml(r.name)}">`).join(''); };
        fill('dlDepartments', c.departments); fill('dlDivisions', c.divisions); fill('dlSections', c.sections);
        fill('dlCategories', c.categories); fill('dlModes', c.modes); fill('dlSignatories', c.signatories); fill('dlUnits', c.units);
        signatoryTitleByName = {}; (c.signatories || []).forEach(s => { signatoryTitleByName[s.name] = s.extra || ''; });
    } catch (err) { console.error(err); }
}

// ── Print ───────────────────────────────────────────────────────────────
// The preview is a real PDF, built the way the Purchase Order's is: the form is
// rasterised by html2canvas and laid into a jsPDF document on the smallest sheet
// that fits (js/print-pdf.js), with "Date Printed / Page N of M" drawn on each page.
let prPrintData = null, prPdfUrl = null, prPdfName = 'purchase_request.pdf';
const PDF_VIEW_PARAMS = '#navpanes=0&toolbar=1&view=FitH';
function readPref(k, fb) { try { const v = localStorage.getItem(k); return v === null ? fb : v; } catch (e) { return fb; } }
function writePref(k, v) { try { localStorage.setItem(k, v); } catch (e) { /* a preference that cannot persist must not stop a form printing */ } }

async function openPrPrint(id) {
    prBusy.start('Loading the request, please wait…');
    let pr;
    try {
        const res = await fetch(`${apiUrl}?id=${id}`); const data = await res.json();
        if (!data.success) throw new Error(data.message || 'Unable to load the purchase request.');
        pr = data.pr;
    } catch (err) { prBusy.end(); return showToast(err.message || 'Unable to load the purchase request.', 'error'); }
    prBusy.end();
    prPrintData = pr;
    document.getElementById('prPrintSubtitle').textContent = pr.pr_number;
    document.getElementById('pdfPaperSize').value = readPref('pr.paper', 'auto');
    document.getElementById('pdfTextSize').value = readPref('pr.text', '0');
    document.getElementById('prPrintSource').innerHTML = PrPrint.build(pr);
    const el = document.getElementById('prPrintModal');
    // Wait for the fade-in to finish: html2canvas reading layout mid-transition
    // can capture an inconsistent (sometimes zero-size) box.
    el.addEventListener('shown.bs.modal', generatePrPdf, { once: true });
    openModal('prPrintModal');
}
function setToolbarBusy(b) { ['pdfPaperSize', 'pdfTextSize'].forEach(i => document.getElementById(i).disabled = b); }
async function generatePrPdf() {
    const loading = document.getElementById('prPdfLoading'), frame = document.getElementById('prPdfFrame');
    const btns = ['prPdfOpenTab', 'prPdfDownload', 'prPdfPrint'].map(i => document.getElementById(i));
    frame.classList.add('d-none'); btns.forEach(b => b.disabled = true);
    loading.classList.remove('d-none');
    loading.innerHTML = '<div class="spinner-border text-primary" role="status"></div><div class="small mt-2">Generating PDF preview…</div>';
    document.getElementById('pdfLayoutStatus').innerHTML = '';
    setToolbarBusy(true);
    try {
        const source = document.querySelector('#prPrintSource .prp-page');
        if (!source) throw new Error('Print content did not render — close and reopen the preview.');
        const step = document.getElementById('pdfTextSize').value;
        source.classList.remove('prp-text-1', 'prp-text-2');
        if (step !== '0') source.classList.add('prp-text-' + step);
        const out = await PrintPdf.renderToPdf(source, {
            paper: document.getElementById('pdfPaperSize').value,
            breakSelector: 'tr, .prp-dl, .prp-summary-box, .prp-sign-row',
            gridSelector: '.prp-items',
        });
        const d = PrintPdf.describeLayout(out.layout);
        document.getElementById('pdfLayoutStatus').innerHTML = `<span>${escapeHtml(d.paper)}</span><span class="pp-sheets">${escapeHtml(d.sheetsLabel)}</span>` + (d.note ? `<span class="pp-layout-note">${escapeHtml(d.note)}</span>` : '');
        if (prPdfUrl) URL.revokeObjectURL(prPdfUrl);
        prPdfUrl = URL.createObjectURL(out.blob);
        prPdfName = 'PR_' + prPrintData.pr_number + '.pdf';
        frame.src = prPdfUrl + PDF_VIEW_PARAMS;
        loading.classList.add('d-none'); frame.classList.remove('d-none'); btns.forEach(b => b.disabled = false);
    } catch (err) {
        loading.innerHTML = '<div class="text-danger small px-3"><i class="fas fa-triangle-exclamation me-1"></i>Unable to generate PDF preview: ' + escapeHtml(err.message || String(err)) + '</div>';
        console.error(err);
    } finally { setToolbarBusy(false); }
}

// ── Init ────────────────────────────────────────────────────────────────
document.addEventListener('DOMContentLoaded', () => {
    initTable(); initDateFilters(); loadSuggestions();
    document.getElementById('btnAddPr').addEventListener('click', newPr);
    document.getElementById('itemImportInput').addEventListener('change', e => { const f = e.target.files[0]; e.target.value = ''; if (f) readImportFile(f); });
    document.getElementById('prImportReplace').addEventListener('click', () => applyImport(true));
    document.getElementById('prImportAdd').addEventListener('click', () => applyImport(false));
    // Picking a name fills its designation from Master Data — but never over one the person typed.
    document.querySelectorAll('[data-title]').forEach(el => el.addEventListener('input', () => {
        const t = signatoryTitleByName[el.value.trim()], target = document.getElementById(el.dataset.title);
        if (t && target && target.value.trim() === '') target.value = t;
    }));
    document.querySelectorAll('.pr-autogrow').forEach(el => el.addEventListener('input', () => autoGrowField(el)));
    document.getElementById('prModal').addEventListener('shown.bs.modal', regrowModalFields);
    document.getElementById('pdfPaperSize').addEventListener('change', e => { writePref('pr.paper', e.target.value); generatePrPdf(); });
    document.getElementById('pdfTextSize').addEventListener('change', e => { writePref('pr.text', e.target.value); generatePrPdf(); });
    document.getElementById('prPdfDownload').addEventListener('click', () => { if (!prPdfUrl) return; const a = document.createElement('a'); a.href = prPdfUrl; a.download = prPdfName; document.body.appendChild(a); a.click(); a.remove(); });
    document.getElementById('prPdfOpenTab').addEventListener('click', () => { if (prPdfUrl) window.open(prPdfUrl, '_blank'); });
    document.getElementById('prPdfPrint').addEventListener('click', () => {
        try { const f = document.getElementById('prPdfFrame'); f.contentWindow.focus(); f.contentWindow.print(); } catch (e) { if (prPdfUrl) window.open(prPdfUrl, '_blank'); }
    });
    loadPRs().then(() => {
        // A notification's link: open that request straight away.
        const n = new URLSearchParams(location.search).get('pr_number');
        if (n) openPr(n, 'view');
    });
});
</script>
<?php include 'assistant.php'; ?>
</body>
</html>
