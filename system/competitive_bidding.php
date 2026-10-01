<?php
require_once 'config.php';
require_once 'lib/competitive_bidding_report.php';
requireAuth();
requireModule('competitive_bidding');
$currentUser = getCurrentUser();
$activePage  = 'competitive_bidding';
?>
<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Competitive Bidding | <?= e(APP_NAME) ?></title>
<link rel="icon" type="image/png" href="image/cgm-32.png">
<link href="vendor/css/bootstrap.min.css" rel="stylesheet">
<link rel="stylesheet" href="vendor/css/all.min.css">
<link rel="stylesheet" href="vendor/css/dataTables.bootstrap5.min.css">
<link rel="stylesheet" href="css/base.css">
<link rel="stylesheet" href="css/item-grid.css">
<link rel="stylesheet" href="<?= asset('css/po-print.css') ?>">
<style>
/* Same card / field / chip vocabulary as Purchase Orders and Document
   Tracking, so all three screens read as one system. Only what this page
   adds is here. */
.table-actions { display: flex; gap: .35rem; flex-wrap: wrap; }
.po-card { background: #fff; border: 1px solid #e2e8f0; border-radius: 14px; padding: 1.25rem 1.4rem; margin-bottom: 1.1rem; }
.po-card-title { display: flex; align-items: center; gap: .55rem; font-weight: 700; color: var(--navy); font-size: 1rem; margin-bottom: 1.1rem; }
.po-card-title i { color: var(--primary); font-size: 1.05rem; }
.po-field-group { margin-bottom: 1rem; }
.po-field-group > label { font-size: .8rem; font-weight: 600; color: var(--muted); margin-bottom: .3rem; display: block; }
.po-field-group .form-control, .po-field-group .form-select { font-size: .87rem; min-height: 38px; }
/* These grow to fit their own content (autoGrowField), so the manual resize
   grabber is both redundant and a way to leave a field stuck at the wrong
   height. overflow:hidden stops a scrollbar flashing up mid-grow. */
.cb-autogrow { resize: none; overflow: hidden; }
.po-field-hint { font-size: .72rem; color: var(--muted); margin-top: .25rem; line-height: 1.35; }
.req::after { content: ' *'; color: #dc2626; font-weight: 700; }
/* The statutory rate, sitting on the label so it reads as part of the field's
   name rather than as advice that can be scrolled past. */
.cb-pct-tag {
    font-weight: 700; font-size: .64rem; text-transform: none; letter-spacing: 0;
    color: #1d4ed8; background: #e8f0ff; border: 1px solid #bfdbfe;
    border-radius: 20px; padding: .04rem .4rem; margin-left: .3rem; white-space: nowrap;
}
/* Amber when the typed figure is not the statutory rate — a look-twice, not
   an error: a bond agreed at another rate is legitimate, just worth seeing. */
.cb-pct-off { color: #92400e; font-weight: 600; }

/* css/base.css defines .badge-status but not its state modifiers — the PO
   page carries those inline. Same values here so a status reads identically
   on both screens. */
.badge-status.pending  { background: linear-gradient(135deg,#d97706,#f59e0b); color: #fff; }
.badge-status.complete { background: linear-gradient(135deg,#16a34a,#22c55e); color: #fff; }
/* The stage ladder. Deliberately one hue walking from amber through blue to
   green rather than six unrelated colours: the point of the badge is that a
   glance down the column shows how far along each set is, and six arbitrary
   colours would have to be memorised before they meant anything.
   Grey = nothing has happened yet, green = it has left this office. */
/* The subtitle carries the full contract name, which on a consolidated bid
   is several hundred characters. Two lines, then an ellipsis — the whole
   string is on the element's title attribute for anyone who needs it. */
#cbModal.cb-viewing #cbItemsTable th:last-child,
#cbModal.cb-viewing #cbItemsTable td:last-child { display: none; }
.cb-modal-subtitle {
    display: -webkit-box;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
    overflow: hidden;
}
.badge-status.st-draft   { background: linear-gradient(135deg,#64748b,#94a3b8); color: #fff; }
.badge-status.st-awarded { background: linear-gradient(135deg,#d97706,#f59e0b); color: #fff; }
.badge-status.st-bond    { background: linear-gradient(135deg,#c2410c,#ea580c); color: #fff; }
.badge-status.st-signing { background: linear-gradient(135deg,#7c3aed,#a78bfa); color: #fff; }
.badge-status.st-signed  { background: linear-gradient(135deg,#1d4ed8,#3b82f6); color: #fff; }
.badge-status.st-ntp     { background: linear-gradient(135deg,#0e7490,#06b6d4); color: #fff; }
.badge-status.st-cgso    { background: linear-gradient(135deg,#15803d,#22c55e); color: #fff; }
/* Six stages will not sit on one line on a laptop; let them wrap. */
.po-count-chips { flex-wrap: wrap; }
.po-chip.chip-on { outline: 2px solid var(--primary); outline-offset: 1px; }
/* .badge-status is nowrap by default, which suits a one-word status and cuts
   "Submission of Performance Bond" off mid-word. Capped and wrapped instead:
   two short lines read fine in a table cell, a truncated one does not. */
#cbTable .badge-status {
    white-space: normal; display: inline-block; max-width: 150px;
    line-height: 1.25; text-align: center;
}

/* Count chips — readouts and controls at once, exactly as on the PO list. */
.po-count-chips { display: flex; gap: .4rem; flex-wrap: wrap; }
.po-chip {
    display: inline-flex; align-items: center; gap: .4rem;
    padding: .3rem .7rem; border-radius: 20px;
    font-size: .75rem; font-weight: 600; white-space: nowrap;
    border: 1px solid var(--border); background: #fff; color: var(--muted);
    cursor: pointer; transition: background .15s, border-color .15s, color .15s;
}
.po-chip:hover { background: #f8fafc; }
.po-chip-num { font-weight: 700; color: var(--text); font-variant-numeric: tabular-nums; }
/* One chip per stage, each tinted with its badge's hue so a chip and the
   badges it filters to are recognisably the same thing. Same walk from grey
   through amber and blue to green as .badge-status.st-* below. */
.po-chip-draft   { border-color: #cbd5e1; color: #475569; }
.po-chip-awarded { border-color: #fcd34d; color: #b45309; }
.po-chip-bond    { border-color: #fdba74; color: #c2410c; }
.po-chip-signing { border-color: #ddd6fe; color: #6d28d9; }
.po-chip-signed  { border-color: #bfdbfe; color: #1d4ed8; }
.po-chip-ntp     { border-color: #a5f3fc; color: #0e7490; }
.po-chip-cgso    { border-color: #86efac; color: #15803d; }
.po-chip-draft   .po-chip-num { color: #475569; }
.po-chip-awarded .po-chip-num { color: #b45309; }
.po-chip-bond    .po-chip-num { color: #c2410c; }
.po-chip-signing .po-chip-num { color: #6d28d9; }
.po-chip-signed  .po-chip-num { color: #1d4ed8; }
.po-chip-ntp     .po-chip-num { color: #0e7490; }
.po-chip-cgso    .po-chip-num { color: #15803d; }

.po-chip.is-active { background: linear-gradient(135deg,#1e3a8a,#2563eb); border-color: transparent; color: #fff; }
.po-chip.is-active .po-chip-num { color: #fff; }
.po-chip-draft.is-active   { background: linear-gradient(135deg,#64748b,#94a3b8); }
.po-chip-awarded.is-active { background: linear-gradient(135deg,#d97706,#f59e0b); }
.po-chip-bond.is-active    { background: linear-gradient(135deg,#c2410c,#ea580c); }
.po-chip-signing.is-active { background: linear-gradient(135deg,#7c3aed,#a78bfa); }
.po-chip-signed.is-active  { background: linear-gradient(135deg,#1d4ed8,#3b82f6); }
.po-chip-ntp.is-active     { background: linear-gradient(135deg,#0e7490,#06b6d4); }
.po-chip-cgso.is-active    { background: linear-gradient(135deg,#15803d,#22c55e); }

/* Bid / contract numbers, tinted the way the PO list tints its own. */
.bid-num-pill, .contract-num-pill {
    display: inline-block; padding: .18rem .55rem; border-radius: 6px;
    font-size: .8rem; font-weight: 700; white-space: nowrap; border: 1px solid transparent;
}
.bid-num-pill      { color: #6d28d9; background: #f3ebff; border-color: #ddd0fb; }
.contract-num-pill { color: #1d4ed8; background: #e8f0ff; border-color: #bfdbfe; }
.cb-empty { color: var(--muted); }

/* Contract name and location are free text and often a full sentence — shown
   in full, wrapping onto as many lines as they need, capped in width so they
   wrap instead of pushing the rest of the columns off to the right. */
#cbTable th, #cbTable td { padding-left: .4rem; padding-right: .4rem; }
#cbTable thead th { white-space: normal; }
#cbTable tbody td { vertical-align: top; }
.cb-wrap-cell { display: block; max-width: 240px; min-width: 0; white-space: normal; word-break: break-word; }
/* Keep all seven actions visible in a compact grid instead of widening the
   table with one unbroken row of buttons. */
#cbTable .table-actions {
    display: grid; grid-template-columns: repeat(3, 30px); gap: 4px;
    width: max-content; margin-inline: auto;
}
#cbTable .table-actions .btn {
    display: inline-flex; align-items: center; justify-content: center;
    width: 30px; height: 30px; padding: 0; font-size: .8rem;
    border-radius: 7px; border-width: 1px; box-shadow: 0 1px 2px rgba(15, 23, 42, .08);
}
#cbTable .table-actions .btn i { color: inherit; }
/* Same per-action colours as the Purchase Orders list, so a given action
   looks the same on both screens. Copied rather than shared: the PO page's
   own classes are left alone. */
.cb-action-view { color: #0891b2; background: #ecfeff; border-color: #22d3ee; }
.cb-action-view:hover { color: #fff; background: #0891b2; border-color: #0891b2; }
.cb-action-edit { color: #2563eb; background: #eff6ff; border-color: #60a5fa; }
.cb-action-edit:hover { color: #fff; background: #2563eb; border-color: #2563eb; }
.cb-action-delete { color: #dc2626; background: #fef2f2; border-color: #f87171; }
.cb-action-delete:hover { color: #fff; background: #dc2626; border-color: #dc2626; }
.cb-action-print { color: #1d4ed8; background: #eff6ff; border-color: #93c5fd; }
.cb-action-print:hover { color: #fff; background: #1d4ed8; border-color: #1d4ed8; }
.cb-action-obligation { color: #c2410c; background: #fff7ed; border-color: #fdba74; }
.cb-action-obligation:hover { color: #fff; background: #c2410c; border-color: #c2410c; }
.cb-action-dts { color: #7c3aed; background: #f5f3ff; border-color: #c4b5fd; }
.cb-action-dts:hover { color: #fff; background: #7c3aed; border-color: #7c3aed; }
.cb-action-lock { color: #6b7280; background: #f3f4f6; border-color: #9ca3af; }
.cb-action-lock:hover { color: #fff; background: #6b7280; border-color: #6b7280; }
.cb-action-unlock { color: #b45309; background: #fffbeb; border-color: #f59e0b; }
.cb-action-unlock:hover { color: #fff; background: #b45309; border-color: #b45309; }
.cb-amount { font-variant-numeric: tabular-nums; white-space: nowrap; text-align: right; font-weight: 700; color: #1d4ed8; }
.cb-amount.muted { font-weight: 500; color: var(--muted); }
/* Savings in the list. Green reads as money kept, amber as money over — and
   the sub-line carries the percentage, because ₱40,000 saved means nothing
   until you know whether it came off ₱50,000 or off ₱50,000,000. */
.cb-save      { display: block; text-align: right; font-variant-numeric: tabular-nums; white-space: nowrap; }
.cb-save-val  { font-weight: 700; color: #15803d; }
.cb-save-pct  { display: block; font-size: .68rem; color: var(--muted); font-weight: 600; }
.cb-save.over .cb-save-val { color: #b45309; }
.cb-save.none .cb-save-val { color: var(--muted); font-weight: 500; }

/* The savings band under the two amounts in the form: the whole reason both
   figures are on this screen, so it is stated rather than left to be worked
   out from the two numbers above it. */
.cb-savings-band {
    border-radius: 10px; padding: .55rem .75rem; border: 1px solid #bbf7d0;
    background: #f0fdf4; display: flex; align-items: baseline; gap: .55rem; flex-wrap: wrap;
}
.cb-savings-band .lab { font-size: .72rem; font-weight: 700; text-transform: uppercase;
                        letter-spacing: .04em; color: #166534; }
.cb-savings-band .val { font-size: 1.05rem; font-weight: 800; color: #15803d;
                        font-variant-numeric: tabular-nums; }
.cb-savings-band .sub { font-size: .74rem; color: #166534; }
.cb-savings-band.over { border-color: #fde68a; background: #fffbeb; }
.cb-savings-band.over .lab, .cb-savings-band.over .sub { color: #92400e; }
.cb-savings-band.over .val { color: #b45309; }
.cb-savings-band.idle { border-color: var(--border); background: #f8fafc; }
.cb-savings-band.idle .lab, .cb-savings-band.idle .sub { color: var(--muted); }
.cb-savings-band.idle .val { color: var(--muted); font-size: .9rem; font-weight: 600; }

/* The modal's left column and the item grid each scroll on their own, so a
   long specification list never pushes Save off the bottom. */
.cb-form-scroll { overflow-y: auto; max-height: 62vh; padding-right: .4rem; }
.cb-section-sep { border-top: 1px dashed var(--border); margin: .2rem 0 1.1rem; }
#cbSubtotal { font-size: 1.4rem; font-weight: 700; color: var(--navy); }

/* ── Modal chrome, lifted from the Create Purchase Order modal so the two
      screens are the same piece of software. Values are copied rather than
      shared: purchase_orders.php keeps its own inline copy (see the note in
      css/item-grid.css for why that page is left alone), and these are pinned
      to fixed colours rather than --text/--muted so the cards stay readable on
      the white modal background however those vars are retuned. ── */
.modal-cb-wide { max-width: min(1600px, 96vw); }

.po-modal-header-v2 {
    background: linear-gradient(135deg, var(--navy), #1e40af);
    color: #fff; border-bottom: none;
    padding: 1.25rem 1.5rem;
    display: flex; align-items: center; gap: 1rem;
}
.po-header-icon {
    width: 46px; height: 46px; border-radius: 14px; flex: 0 0 auto;
    background: rgba(255,255,255,.18);
    display: flex; align-items: center; justify-content: center; font-size: 1.25rem; color: #fff;
}
.po-header-text { flex: 1 1 auto; min-width: 0; }
.po-header-text .modal-title { color: #fff; font-weight: 700; font-size: 1.15rem; margin: 0; }
.po-header-text small { display: block; color: rgba(255,255,255,.72); font-size: .8rem; margin-top: .1rem; }
.po-header-close {
    width: 38px; height: 38px; flex: 0 0 auto; border-radius: 12px;
    border: 1px solid rgba(255,255,255,.3); background: rgba(255,255,255,.12); color: #fff;
    display: flex; align-items: center; justify-content: center; transition: background .15s;
}
.po-header-close:hover { background: rgba(255,255,255,.25); color: #fff; }
#cbModal .modal-content { color: #1e293b; }

/* Compact card + field density, so 31 fields fit without the modal turning
   into a scroll marathon. */
.po-card-compact { padding: .95rem 1.15rem; }
.po-card-compact .po-card-title { margin-bottom: .65rem; font-size: .92rem; }
.po-card-compact .po-field-group { margin-bottom: .4rem; }
.po-card-compact .po-field-group > label { font-size: .72rem; margin-bottom: .15rem; }
.tight-field-row .po-field-group { margin-bottom: .6rem; }

/* Icon-prefixed inputs. The icon is pointer-events:none so clicking it still
   focuses the field underneath rather than swallowing the click. */
.po-input-wrap { position: relative; }
.po-input-wrap > .po-input-icon {
    position: absolute; left: 5px; top: 50%; transform: translateY(-50%);
    width: 28px; height: 28px; border-radius: 8px; background: rgba(37,99,235,.1); color: var(--primary);
    display: flex; align-items: center; justify-content: center; font-size: .78rem; pointer-events: none;
}
.po-input-wrap > .form-control, .po-input-wrap > .form-select { padding-left: 40px; }
.po-input-wrap > textarea.form-control { padding-top: .55rem; }
/* A textarea grows downward, so its icon is pinned near the top line instead
   of centred on a box whose height keeps changing. */
.po-input-wrap > textarea.form-control ~ .po-input-icon { top: 19px; transform: none; }
.po-card-compact .po-input-wrap > .po-input-icon { width: 22px; height: 22px; font-size: .68rem; }
.po-card-compact .po-input-wrap > .form-control,
.po-card-compact .po-input-wrap > .form-select { padding: .3rem .5rem .3rem 32px; min-height: 32px; font-size: .8rem; }
.po-card-compact .po-input-wrap > textarea.form-control ~ .po-input-icon { top: 5px; }

/* Empty item grid. */
.po-items-empty {
    display: flex; flex-direction: column; align-items: center; justify-content: center;
    text-align: center; padding: 3rem 1.5rem;
}
.po-items-empty-icon {
    width: 74px; height: 74px; border-radius: 50%; background: rgba(37,99,235,.1); color: var(--primary);
    display: flex; align-items: center; justify-content: center; font-size: 1.7rem; margin-bottom: 1rem;
}
.po-items-empty-title { font-weight: 700; color: #1e293b; margin-bottom: .2rem; }
.po-items-empty-sub { color: #64748b; font-size: .88rem; }

.po-add-row-link {
    display: block; text-align: center; padding: .6rem; font-size: .85rem; font-weight: 600;
    color: var(--primary); border: 1px dashed var(--border); border-radius: 8px; margin-top: .6rem;
    background: none; width: 100%; cursor: pointer;
}
.po-add-row-link:hover { background: rgba(37,99,235,.05); }

.po-items-footer-bar {
    display: flex; align-items: center; justify-content: space-between; flex-wrap: wrap; gap: .5rem;
    background: rgba(37,99,235,.05); border: 1px solid #e2e8f0; border-radius: 10px;
    padding: .7rem 1.1rem; margin-top: .75rem;
}
.po-items-footer-bar .po-items-count { color: #475569; font-size: .9rem; }
.po-items-footer-bar .po-items-count strong { color: #1e293b; }

/* Import guide — same vocabulary as the Purchase Order modal's, so the file
   format is explained the same way on both screens. */
.import-guide {
    background: rgba(37,99,235,.06); border: 1px solid rgba(37,99,235,.2);
    border-radius: 8px; padding: .65rem .9rem; font-size: .82rem; color: #1e293b;
}
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

/* ── Contract Agreement printout ──────────────────────────────────────
   A recreation of the BAC's actual printed contract. Everything here is
   measured off the scanned original rather than invented, so the generated
   copy sits alongside the paper ones without looking like a different form.
   Rasterised by html2canvas (see js/print-pdf.js), so plain box offsets are
   used throughout — transforms do not rasterise identically everywhere. */
.cp-preview-body { background: #e2e8f0; padding: 24px; min-height: 60vh; }
/* Off-screen but still laid out. NOT display:none — html2canvas needs a real
   render box, and scrollHeight/getBoundingClientRect are meaningless without
   one. */
.cp-offscreen-source { position: absolute; top: 0; left: 0; height: 0; overflow: hidden; }
.pdf-preview-frame { width: 100%; height: 72vh; border: 1px solid #cbd5e1; border-radius: 8px; background: #fff; }

.cp-page {
    width: 768px; background: #fff; color: #000;
    font-family: 'Times New Roman', Times, serif; font-size: 16px; line-height: 1.3;
}
/* No border here — the source is one continuous flow sliced into 20+ PDF
   pages (see js/print-pdf.js), so a border baked into this HTML only ever
   drew as open-ended vertical lines running down every page with no top/bottom
   rule closing it off. The blue double rule the original's pre-bordered stock
   has is drawn instead as real PDF furniture, once per page, in
   drawContractFurniture() below — that's the only place a "per page" border
   can actually be closed on each sheet. */
.cp-frame { padding: 0; margin: 0; }
.cp-frame-inner { padding: 12px 20px 22px 84px; }

.cp-letterhead { width: 88%; border-collapse: collapse; margin: 0 auto 4px; }
.cp-letterhead td { vertical-align: middle; text-align: center; }
.cp-seal { width: 100px; }
.cp-seal img { width: 88px; height: 88px; object-fit: contain; }
.cp-lh-text { font-size: 12px; line-height: 1.3; color: #000; }
.cp-lh-text .cp-lh-city { font-family: Arial, Helvetica, sans-serif; font-size: 20px; font-weight: 700; color: #000; }
.cp-lh-text .cp-lh-rep { font-style: italic; }
.cp-rule { border: none; border-top: 3px double #1f3f8f; opacity: 1; margin: 5px 0 10px; }

.cp-contract-no { text-align: center; font-weight: 700; margin: 10px 0 22px; }
.cp-contract-no .cp-val { border-bottom: 1px solid #000; padding: 0 26px; display: inline-block; min-width: 120px; }

/* Boxed title block. */
.cp-title-box { border: 2px solid #1f3f8f; padding: 12px 16px 18px; text-align: center; margin: 0 10px 24px; }
.cp-title-kicker { font-weight: 700; text-decoration: underline; margin-bottom: 14px; }
.cp-title-name { font-family: Arial, Helvetica, sans-serif; font-size: 18px; font-weight: 700; color: #000; line-height: 1.3; }

.cp-h { text-align: center; font-weight: 700; margin: 16px 0 12px; letter-spacing: .02em; }
.cp-p { margin: 0 0 10px; text-align: justify; }
.cp-blank { border-bottom: 1px solid #000; display: inline-block; min-width: 60px; }
.cp-center { text-align: center; margin: 6px 0; }
/* The numbered clauses and their sub-lists. Padding rather than list markers,
   so the numbering matches the original exactly (1., 2., a., i., ii., …). */
.cp-list { margin: 0 0 10px; padding-left: 26px; }
.cp-list > div { margin-bottom: 6px; text-align: justify; }
.cp-sublist { padding-left: 26px; margin-top: 6px; }
.cp-sublist > div { margin-bottom: 3px; }
.cp-marker { display: inline-block; width: 22px; margin-left: -22px; }

/* Item grid — same two-shape rule the app enforces, printed the way the
   original prints it. */
.cp-items { width: 100%; border-collapse: collapse; margin: 12px 0 0; }
.cp-items th, .cp-items td { border: 1px solid #000; padding: 4px 6px; font-size: 14px; }
.cp-items th { background: #fff; text-align: center; font-weight: 700; color: #000; }
.cp-items td { vertical-align: top; }
.cp-items td.num { text-align: right; }
.cp-items td.center { text-align: center; }
/* pre-wrap: a description now routinely holds a whole specification block on
   one row, and its line breaks are the buyer's own formatting. */
.cp-items td.desc { white-space: pre-wrap; }
.cp-items tr.cp-priced td.desc { font-weight: 700; }
.cp-nothing { text-align: center; font-style: italic; font-weight: 700; background: #fdf3d3; }
.cp-grand td { background: #fdf3d3; font-weight: 700; }
.cp-grand .cp-grand-label { text-align: center; }
.cp-delivery td { font-weight: 700; }
.cp-penalty { font-size: 14px; margin: 8px 0 14px; text-align: justify; }

/* Signature and notary blocks. */
.cp-sign { width: 100%; border-collapse: collapse; margin-top: 26px; }
.cp-sign td { width: 50%; vertical-align: top; padding: 0 10px 26px; }
.cp-sign-role { min-height: 3.9em; margin-bottom: 40px; }
.cp-sign-name { font-weight: 700; }
.cp-sign-title { font-style: italic; }
.cp-sign-date { margin-top: 4px; }
.cp-ack-line { margin: 4px 0; }
.cp-notary-grid { margin-top: 26px; line-height: 1.9; }
.cp-signatures, .cp-notary { break-inside: avoid; }
.cp-witness-line { display: block; min-height: 22px; border-bottom: 1px solid #000; margin-right: 24px; }
.cp-page-count { white-space: nowrap; }

/* ── Notice of Award / Notice to Proceed ──────────────────────────────
   Both are single-page letters on the BAC's letterhead — two seals, an email
   line, and no frame. Distinct enough from the contract to keep their own
   block rather than being bent out of .cp-*. */
/* The guide is 8.5 x 13 inches. At 768px printable width, 16px prints at
   12 points without shrinking; leave a writing gap below the letter date. */
.cl-page {
    width: 768px; background: #fff; color: #000; padding: 12px 42px 20px;
    font-family: Arial, Helvetica, sans-serif; font-size: 16px; line-height: 1.3;
}
.cl-letterhead { width: 100%; border-collapse: collapse; }
.cl-letterhead td { vertical-align: middle; text-align: center; }
.cl-seal { width: 88px; }
.cl-seal img { width: 80px; height: 80px; object-fit: contain; }
.cl-lh-text { font-size: 11px; line-height: 1.35; color: #000; }
.cl-lh-rep { font-style: italic; font-family: 'Times New Roman', Times, serif; font-size: 13px; }
.cl-lh-city { font-size: 17px; font-weight: 700; }
.cl-lh-bac { font-size: 18px; font-weight: 700; font-style: italic; font-family: 'Times New Roman', Times, serif; }
.cl-lh-mail { color: #000; }
/* The standard-form number sits above the rule on the right, in grey — it is
   the form's identity, not part of the letter. */
.cl-sf { text-align: right; font-size: 8.5px; color: #333; line-height: 1.3; margin-top: -14px; }
.cl-rule { border: none; border-top: 3px double #000; opacity: 1; margin: 4px 0 12px; }

/* The three-line reference block under the rule: italic labels, bold values. */
.cl-ref { width: 100%; border-collapse: collapse; margin-bottom: 16px; }
.cl-ref td { vertical-align: top; padding: 1px 0; }
.cl-ref .cl-ref-label { font-style: italic; font-size: 12px; width: 178px; white-space: nowrap; }
.cl-ref .cl-ref-value { font-size: 14px; }
.cl-ref .cl-ref-value { font-weight: 700; }

.cl-title { text-align: center; font-weight: 700; letter-spacing: .04em; margin: 20px 0 24px; }
.cl-date { border-bottom: 1px solid #000; display: inline-block; min-width: 210px; min-height: 24px; padding: 0 4px 2px; margin-bottom: 40px; }
.cl-addr { margin-bottom: 14px; line-height: 1.4; }
.cl-addr .cl-addr-name { font-weight: 700; }
.cl-addr .cl-addr-company { font-weight: 700; }
.cl-p { margin: 0 0 10px; text-align: justify; }
.cl-blank { border-bottom: 1px solid #000; display: inline-block; min-width: 60px; }

/* Performance-security table on the Notice of Award. */
.cl-bond { border-collapse: collapse; margin: 12px auto; width: 76%; }
.cl-bond td { border: 1px solid #000; padding: 5px 9px; font-size: 14px; vertical-align: middle; }
.cl-bond td.cl-bond-amt { text-align: center; font-weight: 700; white-space: nowrap; width: 40%; }
.cl-note { text-align: center; font-weight: 700; color: #c00; font-size: 11px; margin: 8px 0 14px; }

.cl-sign-block { margin-top: 16px; }
.cl-sign-name { font-weight: 700; margin-top: 34px; }
.cl-ack { margin-top: 26px; line-height: 2.1; }
.cl-ack .cl-sub { font-size: 10.5px; font-style: italic; }
.cl-receipt-date { display: inline-block; vertical-align: top; text-align: center; line-height: 1.3; }
.cl-receipt-date .cl-blank { display: block; min-width: 220px; height: 24px; }

/* Loading overlays with a progress bar. Two homes, one look:
     #cbBusy         full screen, above Bootstrap's modals (1055) — the wait for
                     the print preview's data, before any modal exists.
     #cbModalLoading covers the Bid Document modal itself, so the modal opens
                     first and the bar sits on top of it while the items load.
   Both swallow clicks, so a row cannot be opened twice mid-load. */
.cb-loader { display: flex; align-items: center; justify-content: center;
             background: rgba(255, 255, 255, .78); }
.cb-loader.d-none { display: none !important; }
#cbBusy { position: fixed; inset: 0; z-index: 2000; }
#cbModalLoading { position: absolute; inset: 0; z-index: 20; border-radius: inherit; }
.cb-loader .cb-busy-box { width: min(360px, 86vw); padding: 20px 22px; background: #fff; border-radius: 10px;
          box-shadow: 0 8px 30px rgba(0, 0, 0, .18); text-align: center; color: #333; font-size: 14px; }
/* The PDF preview's own bar, shown inside its pane rather than over the screen. */
.cb-pdf-progress { width: min(360px, 86vw); margin: 0 auto; text-align: center; color: #333; font-size: 14px; }
.cb-loader .cb-busy-text, .cb-pdf-progress .cb-busy-text { font-weight: 600; margin-bottom: 12px; }
.cb-loader .cb-bar, .cb-pdf-progress .cb-bar { height: 8px; background: #e6e9ee; border-radius: 4px; overflow: hidden; }
.cb-loader .cb-bar > span, .cb-pdf-progress .cb-bar > span { display: block; height: 100%; width: 0; background: currentColor;
          border-radius: 4px; transition: width .15s linear; }
/* No known total yet (waiting on the server): a sliding segment. Uses transform,
   which keeps animating while the page is busy, unlike a striped background. */
.cb-loader.indeterminate .cb-bar > span, .cb-pdf-progress.indeterminate .cb-bar > span {
    width: 40%; transition: none; animation: cbBusySlide 1.1s ease-in-out infinite; }
@keyframes cbBusySlide { from { transform: translateX(-100%); } to { transform: translateX(250%); } }
.cb-loader .cb-busy-pct, .cb-pdf-progress .cb-busy-pct { margin-top: 8px; font-size: 12px; color: #777; min-height: 16px; }

/* ── Progressive Item Processing ──────────────────────────────────────────
   Adapted from inspection.php's #pbModal (Progress Billing) — same two-pane
   shape, own cbp- prefix and own minimal CSS since this page does not load
   inspection.php's stylesheet. Reuses .po-card / .po-field-group /
   .po-input-wrap / .po-modal-header-v2 already defined above. */
.cbp-summary-strip { display: grid; grid-template-columns: repeat(auto-fit, minmax(11rem, 1fr)); gap: .8rem; }
.cbp-summary-strip .cell {
    background: linear-gradient(180deg, #f8fbff, #f1f5f9);
    border: 1px solid #dbe3ee; border-left: 3px solid #93b4f5;
    border-radius: 10px; padding: .55rem .75rem;
}
.cbp-summary-strip .cell .k { font-size: .68rem; text-transform: uppercase; letter-spacing: .05em; color: var(--muted); font-weight: 700; }
.cbp-summary-strip .cell .v { font-size: .87rem; font-weight: 600; color: var(--text); word-break: break-word; }
.cbp-hero { display: grid; grid-template-columns: repeat(auto-fit, minmax(9rem, 1fr)); gap: .8rem; margin-bottom: .6rem; }
.cbp-hero .cell { text-align: center; }
.cbp-hero .k { font-size: .7rem; text-transform: uppercase; letter-spacing: .04em; color: var(--muted); font-weight: 700; }
.cbp-hero .v { font-size: 1.15rem; font-weight: 700; color: var(--navy); }
.cbp-hero .v.processed { color: #1d4ed8; }
.cbp-hero .v.remaining { color: #15803d; }
.cbp-bar { height: 10px; background: #e6e9ee; border-radius: 5px; overflow: hidden; }
.cbp-bar > span { display: block; height: 100%; background: linear-gradient(90deg, #2563eb, #22c55e); border-radius: 5px; transition: width .2s ease; }
.cbp-hint { font-size: .78rem; color: var(--muted); }
.cbp-hint.over { color: #b91c1c; font-weight: 600; }
.cbp-status-pill {
    display: inline-flex; align-items: center; gap: .3rem; font-size: .7rem; font-weight: 700;
    border-radius: 999px; padding: .15rem .6rem; margin-left: .5rem; vertical-align: middle;
}
.cbp-status-pill.idle       { background: #f1f5f9; color: #475569; }
.cbp-status-pill.partial    { background: #fef3c7; color: #92400e; }
.cbp-status-pill.complete   { background: #dcfce7; color: #15803d; }
.cbp-status-pill.over       { background: #fee2e2; color: #b91c1c; }
.cbp-status-pill.unmeasured { background: #eff6ff; color: #1d4ed8; }
.cbp-progress-scroll { max-height: 260px; overflow: auto; }
.cbp-step-card { border: 1px solid #e2e8f0; border-radius: 10px; padding: .75rem .9rem; margin-bottom: .75rem; }
.cbp-step-card .head { display: flex; justify-content: space-between; align-items: center; gap: .5rem; flex-wrap: wrap; margin-bottom: .35rem; }
.cbp-step-card .step-no { font-weight: 700; color: var(--navy); }
.cbp-step-card .meta { font-size: .74rem; color: var(--muted); }
.cbp-step-card .remarks { font-size: .82rem; color: #334155; margin: .3rem 0; }
.cbp-step-card .actions { display: flex; gap: .4rem; flex-wrap: wrap; }
#cbpEditItems { width: 100%; border-collapse: collapse; font-size: .82rem; }
#cbpEditItems th, #cbpEditItems td { padding: .4rem .5rem; border-bottom: 1px solid #eef1f5; vertical-align: middle; }
#cbpEditItems th { text-align: left; color: var(--muted); font-size: .72rem; text-transform: uppercase; letter-spacing: .03em; }
#cbpEditItems td.num, #cbpEditItems th.num { text-align: right; }
#cbpEditItems input[type="number"] { width: 100%; max-width: 110px; text-align: right; }
#cbpEditItems tr.cbp-caption td { background: #f8fafc; font-weight: 700; color: var(--navy); }
#cbpEditItems input.cbp-over { border-color: #b91c1c !important; background: #fef2f2; }
.cbp-title-row { display: flex; align-items: center; gap: .4rem; }
.cbp-po-source { width: 816px; }
.cbp-po-source .print-page { max-width: none; }

/* Obligation Request print — copied field-for-field from
   purchase_orders.php's own .orq-* rules (its Obligation Request slip),
   which are themselves an honest, non-pixel-verified rendering (no real
   printed CB obligation sample exists to check against either). Kept as
   its own copy rather than a shared include, same reasoning CLAUDE.md
   already gives for purchase_orders.php's print pipeline living on its own. */
.print-page { font-family: 'Times New Roman', Times, serif; font-size: 12px; color: #000;
    background: #fff; width: 816px; max-width: 100%; margin: 0 auto; box-shadow: 0 4px 18px rgba(0,0,0,.18); }
.pp-frame { border: 1.5px solid #000; margin: 8px; padding: 10px 14px 8px; }
.orq-letterhead-wrap { position: relative; }
.orq-letterhead { width: 100%; border-collapse: collapse; margin-bottom: 4px; }
.orq-seal { width: 90px; text-align: center; vertical-align: middle; }
.orq-seal img { width: 76px; height: 76px; object-fit: contain; }
.orq-letterhead-text { text-align: center; font-size: 10.5px; line-height: 1.4; vertical-align: middle; }
.orq-letterhead-text .blue { color: #1d4ed8; }
.orq-letterhead-text .blue.bold { font-weight: 700; }
.orq-rule { border: none; border-top: 1px solid #000; margin: 2px 0 6px; }
.orq-grid { width: 100%; border-collapse: collapse; border: 1px solid #000; margin-bottom: 0; }
.orq-grid td { border: 1px solid #000; padding: 3px 7px; vertical-align: middle; font-size: 12px; }
.orq-title-cell { font-size: 20px; font-weight: 700; color: #1d4ed8; text-decoration: underline; text-align: center; }
.orq-label { font-weight: 600; white-space: nowrap; width: 16%; }
.orq-value { width: 34%; }
.orq-particular-header { text-align: center; font-weight: 700; font-size: 11px; }
.orq-particular-body { text-align: left; vertical-align: top; font-size: 12px; }
.orq-amount-cell { text-align: right; vertical-align: top; font-weight: 700; white-space: nowrap; }
.orq-pesos-label { font-weight: 600; }
.orq-words { color: #c2410c; font-style: italic; }
.orq-total-label, .orq-total-value { font-weight: 700; text-align: right; white-space: nowrap; }
.orq-cert-grid { width: 100%; border-collapse: collapse; }
.orq-cert-grid td { border: 1px solid #000; border-top: none; padding: 6px 8px; vertical-align: top; font-size: 11px; width: 50%; }
.orq-cert-letter { display: inline-block; border: 1px solid #000; width: 15px; height: 15px; text-align: center; line-height: 14px; font-weight: 700; margin-right: 4px; }
.orq-checkbox-row { display: flex; align-items: flex-start; gap: 5px; margin-top: 4px; }
.orq-checkbox { width: 10px; height: 10px; border: 1px solid #000; flex: 0 0 auto; margin-top: 2px; }
.orq-sign-grid { width: 100%; border-collapse: collapse; }
.orq-sign-grid td { border: 1px solid #000; border-top: none; padding: 4px 8px; font-size: 11px; }
.orq-sign-grid .lbl { font-weight: 600; width: 15%; white-space: nowrap; }
.orq-sign-grid .val { width: 35%; }
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
                <div class="page-title">Competitive Bidding</div>
                <div class="page-subtitle">Notice of Award, Contract Agreement and Notice to Proceed &mdash; one record, three documents.</div>
            </div>
        </div>
        <div class="d-flex align-items-center gap-2">
            <div class="d-flex gap-2 no-print">
                <button class="btn btn-primary-app btn-sm" id="btnAddBid"><i class="fas fa-plus me-1"></i> New Bid Document</button>
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
                        <input type="text" id="searchText" class="form-control"
                               placeholder="Bid no., contract no., name, supplier"
                               onkeydown="if(event.key==='Enter')loadBids()">
                    </div>
                    <div class="col-sm-6 col-md-2">
                        <label class="form-label">From</label>
                        <input type="date" id="filterFrom" class="form-control">
                    </div>
                    <div class="col-sm-6 col-md-2">
                        <label class="form-label">To</label>
                        <input type="date" id="filterTo" class="form-control">
                    </div>
                    <div class="col-sm-6 col-md-2">
                        <label class="form-label">Status</label>
                        <!-- Every stage is derived from the tracking dates on
                             Additional Details — there is no status column on
                             the table, so a set cannot be marked one thing
                             while its dates say another. -->
                        <select id="filterStatus" class="form-select" onchange="loadBids()">
                            <option value="">All</option>
                            <option value="draft">Draft</option>
                            <option value="awarded">Awarded</option>
                            <option value="bond">Submission of Performance Bond</option>
                            <option value="signing">For Contract Signing</option>
                            <option value="signed">Signed</option>
                            <option value="ntp">Notice to Proceed</option>
                            <option value="cgso">Forwarded to CGSO</option>
                        </select>
                    </div>
                    <div class="col-sm-6 col-md-3 d-flex gap-2">
                        <button class="btn btn-primary-app w-100" onclick="loadBids()"><i class="fas fa-search me-1"></i> Filter</button>
                        <button class="btn btn-outline-secondary w-100" onclick="resetFilters()"><i class="fas fa-eraser me-1"></i> Clear</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel">
            <div class="panel-header d-flex align-items-center justify-content-between flex-wrap gap-2">
                <span class="panel-title"><i class="fas fa-gavel me-2 text-primary"></i>Bid Documents</span>
                <div class="po-count-chips no-print">
                    <button type="button" class="po-chip" data-status="" onclick="setStatusFilter('')">
                        Total <span class="po-chip-num" id="countTotal">&mdash;</span>
                    </button>
                    <!-- One chip per stage, rendered from CB_STAGES so the
                         chips cannot drift out of step with the badges. -->
                    <span id="cbStageChips"></span>
                </div>
            </div>
            <div class="panel-body">
                <div id="cbListLoading" class="text-center text-muted py-5">
                    <div class="spinner-border text-primary" role="status"></div>
                    <div class="small mt-2">Loading bid documents&hellip;</div>
                </div>
                <table id="cbTable" class="app-table table table-hover table-bordered d-none" style="width:100%">
                    <thead>
                        <tr>
                            <th>Bid No.</th>
                            <th>Contract No.</th>
                            <th>Name of the Contract</th>
                            <th>Location</th>
                            <th>Supplier</th>
                            <th>PR Amount</th>
                            <th>Contract Amount</th>
                            <th>Savings</th>
                            <th>Status</th>
                            <th>Actions</th>
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

<!-- Bid document modal. Deliberately the same shape as the Create Purchase
     Order modal: branded header with an icon badge, a compact icon-prefixed
     field grid on the left, the item grid on the right, and a status/actions
     footer. Same software, same vocabulary. -->
<div class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" id="cbModal" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-xl modal-cb-wide modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header po-modal-header-v2">
        <div class="po-header-icon"><i class="fas fa-gavel"></i></div>
        <div class="po-header-text">
            <h5 class="modal-title" id="cbModalTitle">Create Bid Document</h5>
            <small id="cbModalSubtitle" class="cb-modal-subtitle">Fill in the details below to create a new Bid Document.</small>
        </div>
        <button type="button" class="po-header-close" data-bs-dismiss="modal" aria-label="Close"><i class="fas fa-times"></i></button>
      </div>
      <div id="cbModalLoading" class="cb-loader d-none" role="status" aria-live="polite">
        <div class="cb-busy-box">
          <div class="cb-busy-text">Loading the document, please wait&hellip;</div>
          <div class="cb-bar text-primary"><span></span></div>
          <div class="cb-busy-pct"></div>
        </div>
      </div>
      <div class="modal-body">
        <form id="cbForm" onsubmit="return false;">
          <input type="hidden" id="cbId">

          <div class="row g-4">
            <!-- Left column — the form. Its own scroll region, so a document
                 with a lot of filled-in fields scrolls internally instead of
                 growing the whole modal and pushing Save off the bottom. -->
            <div class="col-lg-5">
              <div class="cb-form-scroll">

                <div class="po-card po-card-compact">
                  <div class="po-card-title"><i class="fas fa-file-signature"></i> Contract Identity</div>
                  <div class="row g-2 tight-field-row">
                    <div class="col-md-6 po-field-group">
                      <label class="req">Bid Number</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-hashtag"></i></span><input type="text" id="bid_number" class="form-control" placeholder="2026-055" autocomplete="off" oninput="checkBidNumberDuplicate()"></div>
                      <div class="po-field-hint">BAC's own numbering, from their paperwork &mdash; typed in, not generated here.</div>
                      <!-- Checked as you type, not just at Save. One Bid Number
                           can carry several contracts, so only the same Bid
                           Number AND Contract Number is a duplicate; the
                           second hint just says the number is shared. See
                           checkBidNumberDuplicate(). -->
                      <div class="po-field-hint text-danger d-none" id="bidNumberDupHint"><i class="fas fa-triangle-exclamation me-1"></i><span></span></div>
                      <div class="po-field-hint d-none" id="bidNumberShareHint"><i class="fas fa-circle-info me-1"></i><span></span></div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>Contract Number</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-file-contract"></i></span><input type="text" id="contract_number" class="form-control" placeholder="2026-130" autocomplete="off" oninput="checkBidNumberDuplicate()"></div>
                    </div>
                    <div class="col-12 po-field-group">
                      <label class="req">Name of the Contract</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-file-lines"></i></span><textarea id="contract_name" class="form-control cb-autogrow" rows="1" placeholder="PURCHASE OF 1 UNIT WHEEL LOADER"></textarea></div>
                    </div>
                    <div class="col-12 po-field-group">
                      <label>Location of the Contract</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-location-dot"></i></span><textarea id="contract_location" class="form-control cb-autogrow" rows="1" placeholder="CDRRMO, MALAYBALAY CITY"></textarea></div>
                    </div>
                  </div>
                </div>

                <div class="po-card po-card-compact">
                  <div class="po-card-title"><i class="fas fa-building"></i> Supplier / Winning Bidder</div>
                  <div class="row g-2 tight-field-row">
                    <div class="col-12 po-field-group">
                      <label>Supplier Name</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-users"></i></span><textarea id="supplier_name" class="form-control cb-autogrow" rows="1" placeholder="MONARK EQUIPMENT CORPORATION"></textarea></div>
                    </div>
                    <div class="col-12 po-field-group">
                      <label>Address</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-location-dot"></i></span><textarea id="supplier_address" class="form-control cb-autogrow" rows="1" placeholder="13 Economia St. Bagumbayan, Quezon City"></textarea></div>
                    </div>
                    <div class="col-12 po-field-group">
                      <label>Second Address Line</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-map-pin"></i></span><input type="text" id="supplier_address2" class="form-control" placeholder="w/ Branch office at El Salvador, Misamis Oriental"></div>
                      <div class="po-field-hint">Printed under the address on the Notice of Award and Notice to Proceed. Leave blank if there is none.</div>
                    </div>
                    <div class="col-12 po-field-group">
                      <label>Organized Under the Laws Of</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-scale-balanced"></i></span><input type="text" id="supplier_org_place" class="form-control" placeholder="Quezon City"></div>
                      <div class="po-field-hint">Fills the &ldquo;duly organized and existing under the laws of ___&rdquo; clause in the Contract.</div>
                    </div>
                    <div class="col-md-7 po-field-group">
                      <label>Representative</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-user-tie"></i></span><input type="text" id="representative_name" class="form-control" placeholder="REYNARD MORENO"></div>
                    </div>
                    <div class="col-md-5 po-field-group">
                      <label>Position</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-id-badge"></i></span><input type="text" id="representative_title" class="form-control" placeholder="Account Manager"></div>
                    </div>
                    <div class="col-md-5 po-field-group">
                      <label>Salutation</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-envelope-open-text"></i></span>
                        <select id="representative_salutation" class="form-select">
                          <option value="Sir/Madam">Sir/Madam</option>
                          <option value="Sir">Sir</option>
                          <option value="Madam">Madam</option>
                          <option value="Maam">Maam</option>
                          <option value="Sir/Maam">Sir/Maam</option>
                        </select></div>
                      <div class="po-field-hint">Printed as &ldquo;Dear ___:&rdquo; on the Notice of Award and Notice to Proceed.</div>
                    </div>
                  </div>
                </div>

                <div class="po-card po-card-compact">
                  <div class="po-card-title"><i class="fas fa-peso-sign"></i> Amounts &amp; Delivery</div>
                  <div class="row g-2 tight-field-row">
                    <!-- The Purchase Request side of the pair. Kept next to the
                         contract amount rather than filed under Dates, because
                         the two figures only mean anything read together: the
                         difference between them is what the bidding saved. -->
                    <div class="col-md-6 po-field-group">
                      <label>PR Amount <span class="cb-pct-tag">approved budget</span></label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-clipboard-list"></i></span><input type="text" id="pr_amount" class="form-control" placeholder="12,000,000.00" inputmode="decimal"></div>
                      <div class="po-field-hint">What the Purchase Request was budgeted at &mdash; the approved budget the bidding was run against.</div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>PR Date</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-calendar-day"></i></span><input type="date" id="date_pr" class="form-control"></div>
                      <div class="po-field-hint">When the request was raised &mdash; the start of the clock every lead-time figure is measured from.</div>
                    </div>
                    <div class="col-12 po-field-group">
                      <label>Contract Amount</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-peso-sign"></i></span><input type="text" id="contract_amount" class="form-control" placeholder="10,650,000.00" inputmode="decimal"></div>
                      <div class="po-field-hint">
                        The awarded total. Stored on the record rather than summed from the items, so editing an item
                        can't silently restate an executed contract.
                        <a href="#" onclick="useItemsTotal();return false;">Use the items total instead</a>.
                      </div>
                    </div>
                    <!-- Stated, not left to be worked out from the two numbers
                         above it. Blank until both are filled in: a saving
                         computed against a PR amount nobody entered would read
                         as a 100% saving on every draft in the system. -->
                    <div class="col-12"><div class="cb-savings-band idle" id="cbSavings">
                        <span class="lab">Savings</span><span class="val">&mdash;</span>
                        <span class="sub">Fill in both the PR amount and the contract amount.</span>
                    </div></div>
                    <div class="col-md-6 po-field-group">
                      <label>Cash Bond <span class="cb-pct-tag">5% of contract</span></label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-money-bill-wave"></i></span><input type="text" id="cash_bond_amount" class="form-control" placeholder="532,500.00" inputmode="decimal"></div>
                      <div class="po-field-hint" id="cashBondPct">&nbsp;</div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>Surety Bond <span class="cb-pct-tag">30% of contract</span></label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-shield-halved"></i></span><input type="text" id="surety_bond_amount" class="form-control" placeholder="3,195,000.00" inputmode="decimal"></div>
                      <div class="po-field-hint" id="suretyBondPct">&nbsp;</div>
                    </div>
                    <div class="col-12">
                      <div class="po-field-hint mb-2">
                        Cash Bond (<strong>5%</strong>) and Surety Bond (<strong>30%</strong>) calculate automatically
                        when you enter or change the contract amount. These are the percentages in the supplied report guide.
                        You can still enter a different approved amount.
                        <a href="#" onclick="computeBonds();return false;">Recalculate from the contract amount</a>.
                      </div>
                    </div>
                    <div class="col-md-7 po-field-group">
                      <label>Place of Delivery</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-truck"></i></span><input type="text" id="place_of_delivery" class="form-control" placeholder="NEW CITY HALL, BARANGAY CASISANG"></div>
                    </div>
                    <div class="col-md-5 po-field-group">
                      <label>Delivery Term</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-clock"></i></span><input type="text" id="delivery_term" class="form-control" placeholder="30 C.D."></div>
                    </div>
                  </div>
                </div>

                <div class="po-card po-card-compact">
                  <div class="po-card-title"><i class="fas fa-calendar-days"></i> Dates</div>
                  <div class="row g-2 tight-field-row">
                    <div class="col-md-6 po-field-group">
                      <label>Bid Date</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-calendar-check"></i></span><input type="date" id="bid_date" class="form-control"></div>
                      <div class="po-field-hint">The bid being accepted, quoted on the Notice of Award.</div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>Notice of Award Date</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-award"></i></span><input type="date" id="noa_date" class="form-control"></div>
                      <div class="po-field-hint">The date printed on the award. When the supplier <em>received</em> it is under Additional Details.</div>
                    </div>
                  </div>
                </div>

                <div class="po-card po-card-compact">
                  <div class="po-card-title"><i class="fas fa-pen-nib"></i> Signatories</div>
                  <div class="row g-2 tight-field-row">
                    <div class="col-md-7 po-field-group">
                      <label>Head of the Procuring Entity</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-user-shield"></i></span><input type="text" id="signatory_name" class="form-control" list="dlSignatories" autocomplete="off" placeholder="JAY WARREN R. PABILLARAN" oninput="fillSignatoryTitle('signatory_name','signatory_title')"></div>
                    </div>
                    <div class="col-md-5 po-field-group">
                      <label>Title</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-id-badge"></i></span><input type="text" id="signatory_title" class="form-control" placeholder="City Mayor"></div>
                    </div>
                    <div class="col-md-7 po-field-group">
                      <label>Witness &mdash; Procuring Entity</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-user-check"></i></span><input type="text" id="witness_entity_name" class="form-control" list="dlSignatories" autocomplete="off" placeholder="BELLA STEPHANIE B. SAAVEDRA" oninput="fillSignatoryTitle('witness_entity_name','witness_entity_title')"></div>
                    </div>
                    <div class="col-md-5 po-field-group">
                      <label>Title</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-id-badge"></i></span><input type="text" id="witness_entity_title" class="form-control" placeholder="City Treasurer"></div>
                    </div>
                    <div class="col-12 po-field-group">
                      <label>Witness &mdash; Supplier</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-user-pen"></i></span><input type="text" id="witness_supplier_name" class="form-control"></div>
                      <div class="po-field-hint">Often signed on the day and left blank here &mdash; the printout rules a line for it either way.</div>
                    </div>
                  </div>
                </div>

                <div class="po-card po-card-compact">
                  <div class="po-card-title"><i class="fas fa-stamp"></i> Notary (Acknowledgment page)</div>
                  <div class="po-field-hint mb-2">
                    Every one of these is written by hand on the paper copy. Anything left blank prints as the ruled line
                    the notary fills in, so it is safe to save this section empty.
                  </div>
                  <div class="row g-2 tight-field-row">
                    <div class="col-md-7 po-field-group">
                      <label>Notary Public</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-user-tie"></i></span><input type="text" id="notary_name" class="form-control" placeholder="ATTY. JAY S. ALBARECE"></div>
                    </div>
                    <div class="col-md-5 po-field-group">
                      <label>Place</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-location-dot"></i></span><input type="text" id="notary_place" class="form-control" placeholder="MANOLO FORTICH, BUKIDNON"></div>
                    </div>
                    <div class="col-md-3 po-field-group">
                      <label>Doc. No.</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-hashtag"></i></span><input type="text" id="notary_doc_no" class="form-control"></div>
                    </div>
                    <div class="col-md-3 po-field-group">
                      <label>Page No.</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-hashtag"></i></span><input type="text" id="notary_page_no" class="form-control"></div>
                    </div>
                    <div class="col-md-3 po-field-group">
                      <label>Book No.</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-hashtag"></i></span><input type="text" id="notary_book_no" class="form-control"></div>
                    </div>
                    <div class="col-md-3 po-field-group">
                      <label>Series of</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-calendar"></i></span><input type="text" id="notary_series" class="form-control"></div>
                    </div>
                  </div>
                </div>

                <div class="po-card po-card-compact">
                  <div class="po-card-title"><i class="fas fa-list-check"></i> Additional Details</div>
                  <div class="po-field-hint mb-2">
                    Filled in over the weeks the paperwork moves, so each one saves on its own the moment you change it
                    &mdash; independently of this form's Save button, and still editable once the set is locked.
                  </div>
                  <div class="row g-2 tight-field-row">
                    <div class="col-md-6 po-field-group">
                      <label>Date Received Notice of Award</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-inbox"></i></span><input type="date" id="date_received_noa" class="form-control cb-quick" data-quick="date_received_noa"></div>
                      <div class="po-field-hint">Starts the ten-calendar-day performance security clock.</div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>Date Performance Bond</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-shield-halved"></i></span><input type="date" id="date_performance_bond" class="form-control cb-quick" data-quick="date_performance_bond"></div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>Date of Contract</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-file-signature"></i></span><input type="date" id="contract_date" class="form-control cb-quick" data-quick="contract_date"></div>
                      <div class="po-field-hint">Blank prints as the ruled line the parties fill in by hand.</div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>Date Notice to Proceed</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-flag-checkered"></i></span><input type="date" id="ntp_date" class="form-control cb-quick" data-quick="ntp_date"></div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>Document Tracking No.</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-barcode"></i></span><input type="text" id="barcode_number" class="form-control cb-quick" data-quick="barcode_number" placeholder="From the routing slip"></div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>Linked Purchase Order</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-cart-shopping"></i></span><input type="text" id="po_number" class="form-control cb-quick" data-quick="po_number" list="dlCbPoNumbers" autocomplete="off" placeholder="PO Number, once one is raised for this award" oninput="searchCbPoNumbers(this.value)"></div>
                      <div class="po-field-hint">Set once a Purchase Order exists for this award &mdash; Inspection &amp; Acceptance and Inventory then show how much of it this bid's Processing Steps have covered.</div>
                    </div>
                    <div class="col-md-6 po-field-group">
                      <label>Remarks</label>
                      <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-comment-dots"></i></span><input type="text" id="remarks" class="form-control cb-quick" data-quick="remarks" list="dlRemarks" autocomplete="off" placeholder="e.g. Forwarded to CGSO"></div>
                      <div class="po-field-hint">Pick a common wording or type your own. Manage the list in Master Data &rarr; Remarks.</div>
                    </div>
                  </div>
                  <div class="po-field-hint mt-1" id="cbQuickStatus">&nbsp;</div>
                </div>

              </div>
            </div>

            <!-- Right column — the item grid. -->
            <div class="col-lg-7">
              <div class="po-card h-100 d-flex flex-column">
                <div class="d-flex align-items-center justify-content-between mb-2 flex-wrap gap-2">
                    <div class="po-card-title mb-0"><i class="fas fa-boxes-stacked"></i> Item Details</div>
                    <button type="button" class="btn btn-sm btn-outline-success rounded-pill cb-edit-only" onclick="addItemRow({}, null, true)"><i class="fas fa-plus me-1"></i> Add Item</button>
                </div>
                <div class="d-flex flex-wrap gap-2 mb-3 cb-edit-only">
                    <input type="file" id="itemImportInput" accept=".xlsx,.csv" class="d-none">
                    <button type="button" class="btn btn-sm btn-outline-secondary rounded-pill" onclick="document.getElementById('itemImportInput').click()"><i class="fas fa-file-import me-1"></i> Import Items</button>
                    <button type="button" class="btn btn-sm btn-outline-secondary rounded-pill" onclick="toggleItemGuide()" title="What counts as a valid row, and what does an import file need?"><i class="fas fa-circle-question"></i></button>
                </div>

                <div id="cbItemGuide" class="import-guide d-none mb-3">
                  <div class="fw-semibold mb-1"><i class="fas fa-circle-info me-1"></i>Rows, and the import file format</div>
                  The first row of an import file must be column headers. <strong>Description</strong> is the only one
                  you have to include &mdash; a file with just that single column works fine, which is what a scope of
                  work pasted out of a bid document looks like. Add any of
                  <code>QTY, Unit, Unit Cost, Amount</code> as well, in any order.
                  <div class="mt-2 mb-1"><strong>Keep specifications on one row, not one row each.</strong></div>
                  Description is a multi-line field: press Enter inside it and the whole specification block 
                  lives on the priced row it belongs to. One item is then one row in the database instead of forty,
                  and it prints the same either way. In a CSV, wrap the cell in quotes and put real line breaks inside it.
                  <div class="mt-2 mb-1"><strong>Every row must be one of two shapes:</strong></div>
                  <table class="imp-eg">
                    <thead><tr>
                      <th>QTY</th><th>Unit</th><th>Description</th><th>Unit Cost</th><th>Amount</th><th></th>
                    </tr></thead>
                    <tbody>
                      <tr>
                        <td>1</td><td>Unit</td><td>WHEEL LOADER<br>Brand/ Model: Caterpillar 950GC<br>Engine: Turbocharged Diesel<br>&hellip;</td><td>10,650,000.00</td><td>10,650,000.00</td>
                        <td class="note"><span class="tag ok">PRICED</span> it has an Amount &mdash; specs on the same row</td>
                      </tr>
                      <tr class="eg-title">
                        <td class="blank"></td><td class="blank"></td>
                        <td>SCOPE OF WORK:<br>Mobilization<br>Site clearing</td><td class="blank"></td><td class="blank"></td>
                        <td class="note"><span class="tag ok">DESCRIPTION</span> a heading with no price &mdash; every other column left blank</td>
                      </tr>
                      <tr>
                        <td class="blank"></td><td>lot</td><td>Mobilization and accommodation</td>
                        <td class="blank"></td><td>250,000.00</td>
                        <td class="note"><span class="tag ok">PRICED</span> lump sum &mdash; an Amount alone is fine</td>
                      </tr>
                      <tr class="eg-bad">
                        <td>5</td><td>pc</td><td>Brake cleaner</td><td class="blank"></td><td class="blank"></td>
                        <td class="note"><span class="tag no">REJECTED</span> priced like an item but no Amount</td>
                      </tr>
                    </tbody>
                  </table>
                  On the sample contract &ldquo;WHEEL LOADER&rdquo; is the one priced row and the thirty-odd
                  specification lines beneath it are description-only &mdash; they print as text across the grid with the
                  numeric columns empty. <strong>Amount</strong> is worked out from QTY &times; Unit Cost when you leave
                  it out; enter it directly for a lump sum with no quantity behind it.
                  Accepted file types: .xlsx, .csv.
                  <a href="competitive_bidding_item_import_template.csv" download>Download a template</a> &mdash; it
                  already contains the priced line and specification rows shown above.
                </div>

                <div id="cbItemsEmpty" class="po-items-empty">
                    <div class="po-items-empty-icon"><i class="fas fa-bag-shopping"></i></div>
                    <div class="po-items-empty-title">No items added yet.</div>
                    <div class="po-items-empty-sub">Click "Add Item" to get started.</div>
                </div>

                <div id="cbItemsWrap" class="table-responsive items-scroll-wrap d-none">
                  <table class="app-table item-grid table table-bordered mb-0" id="cbItemsTable">
                    <thead>
                      <tr>
                        <!-- QTY was 8% and Amount 13%, which on a real bid gave
                             a 30px box for a 52px number: "6.000" read as "6.0"
                             and "32200.000" as "32200.00(". The three points come
                             off Description, which wraps and can afford them. -->
                        <th style="width:4%">#</th>
                        <th style="width:10%">QTY</th>
                        <th style="width:10%">Unit</th>
                        <th style="width:40%">Description</th>
                        <th style="width:13%">Unit Cost</th>
                        <th style="width:15%">Amount</th>
                        <th style="width:8%">Action</th>
                      </tr>
                    </thead>
                    <tbody></tbody>
                  </table>
                </div>

                <button type="button" class="po-add-row-link cb-edit-only" onclick="addItemRow({}, null, true)"><i class="fas fa-plus me-1"></i> Add another row</button>

                <div class="po-items-footer-bar">
                    <div class="po-items-count">Total Items: <strong id="cbRowCount">0</strong></div>
                    <div><span class="text-muted small me-1">Grand Total:</span><span id="cbSubtotal">&#8369;0.00</span></div>
                </div>
              </div>
            </div>
          </div>
        </form>
      </div>
      <div class="modal-footer d-flex align-items-center">
        <div class="me-auto">Status: <span id="cbModalStatusBadge" class="badge-status st-draft">Draft</span></div>
        <!-- Progressive Item Processing — a bid's items processed/delivered in
             batches, without recreating the bid itself. See api/cb_processing.php
             and #cbProcessingModal. Hidden only before the bid has been saved
             once (no Bid Number to attach a step to yet). -->
        <!-- Everything logged against this bid — created, edited, locked,
             tracking dates, processing steps — like the Purchase Order's own
             History button. Shown on the same terms as Processing Steps. -->
        <button type="button" class="btn btn-outline-secondary d-none" id="cbHistoryButton" onclick="openBidHistory()"><i class="fas fa-clock-rotate-left me-1"></i> History</button>
        <button type="button" class="btn btn-outline-secondary d-none" id="cbProcessingButton" onclick="openCbProcessing(document.getElementById('cbId').value)"><i class="fas fa-layer-group me-1"></i> Processing Steps</button>
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal"><i class="fas fa-times me-1"></i> Cancel</button>
        <button type="button" class="btn btn-primary-app" id="cbSaveButton" onclick="saveBid()">
          <i class="fas fa-floppy-disk me-1"></i> Save Bid Document
        </button>
      </div>
    </div>
  </div>
</div>

<!-- Progressive Item Processing — adapted from inspection.php's #pbModal
     (Progress Billing). Two panes: a history of every step recorded against
     this bid, and an editor for recording or revising one. Never recreates
     the bid document — see api/cb_processing.php, which only ever writes to
     tbl_cb_processing / tbl_cb_processing_items. -->
<div class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" id="cbProcessingModal" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-xl modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header po-modal-header-v2">
        <div class="po-header-icon"><i class="fas fa-layer-group"></i></div>
        <div class="po-header-text">
            <h5 class="modal-title cbp-title-row"><span id="cbpTitle">Processing Steps</span><span class="cbp-status-pill d-none" id="cbpStatusPill"></span></h5>
            <small id="cbpSubtitle">Items processed/delivered against this bid, recorded in batches.</small>
        </div>
        <button type="button" class="po-header-close" data-bs-dismiss="modal" aria-label="Close"><i class="fas fa-times"></i></button>
      </div>
      <div class="modal-body">
        <input type="hidden" id="cbpBidId">
        <input type="hidden" id="cbpBidNumber">

        <!-- ── Pane 1: history ─────────────────────────────────────── -->
        <div id="cbpHistoryPane">
          <div class="po-card po-card-compact">
            <div class="po-card-title"><i class="fas fa-file-contract"></i> Bid Document</div>
            <div class="cbp-summary-strip">
              <div class="cell"><div class="k">Bid Number</div><div class="v" id="cbpSumBid">—</div></div>
              <div class="cell"><div class="k">Contract</div><div class="v" id="cbpSumContract">—</div></div>
              <div class="cell"><div class="k">Supplier</div><div class="v" id="cbpSumSupplier">—</div></div>
              <div class="cell"><div class="k">Linked PO</div><div class="v" id="cbpSumPo">—</div></div>
            </div>
          </div>

          <div class="po-card po-card-compact">
            <div class="po-card-title"><i class="fas fa-chart-line"></i> Processed to date</div>
            <div class="cbp-hero">
              <div class="cell"><div class="k">Contract Amount</div><div class="v" id="cbpTotOrdered">—</div></div>
              <div class="cell"><div class="k">Processed</div><div class="v processed" id="cbpTotProcessed">—</div></div>
              <div class="cell"><div class="k">Remaining</div><div class="v remaining" id="cbpTotRemaining">—</div></div>
              <div class="cell"><div class="k">Steps</div><div class="v" id="cbpTotCount">—</div></div>
            </div>
            <div class="cbp-bar"><span id="cbpBar" style="width:0%"></span></div>
            <div class="cbp-hint mt-1" id="cbpBarHint"></div>
          </div>

          <div class="po-card po-card-compact">
            <div class="po-card-title"><i class="fas fa-list-ol"></i> Quantity per item</div>
            <div class="cbp-progress-scroll" id="cbpProgressWrap"></div>
          </div>

          <div class="po-card po-card-compact">
            <div class="po-card-title"><i class="fas fa-clock-rotate-left"></i> Steps recorded</div>
            <div id="cbpSteps"></div>
          </div>
        </div>

        <!-- ── Pane 2: record / revise one step ────────────────────── -->
        <div id="cbpEditorPane" class="d-none">
          <div class="po-card po-card-compact">
            <div class="po-card-title"><i class="fas fa-calendar-week"></i> This step</div>
            <div class="row g-2 tight-field-row">
              <div class="col-md-4 po-field-group">
                <label>Step No. <span class="text-danger">*</span></label>
                <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-hashtag"></i></span>
                <select id="cbpEditNo" class="form-select"></select></div>
              </div>
              <div class="col-md-4 po-field-group">
                <label>Processing Date</label>
                <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-calendar"></i></span>
                <input type="date" id="cbpEditDate" class="form-control"></div>
              </div>
              <div class="col-md-4 po-field-group">
                <label>Remarks</label>
                <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-comment-dots"></i></span>
                <input type="text" id="cbpEditRemarks" class="form-control" list="dlRemarks" autocomplete="off"></div>
              </div>
            </div>
          </div>

          <div class="po-card po-card-compact">
            <div class="po-card-title"><i class="fas fa-boxes-stacked"></i> Items processed</div>
            <div class="po-field-hint mb-2">Every bid item is listed, whether or not this step covers it. Leaving a quantity
              at zero leaves that item out of this step — nothing is deleted. A quantity cannot exceed what remains.</div>
            <div class="cbp-progress-scroll">
              <table id="cbpEditItems">
                <thead><tr>
                  <th>Item</th><th>Unit</th>
                  <th class="num">Ordered</th><th class="num">Remaining</th>
                  <th class="num">Qty this step</th><th class="num">Unit Cost</th><th class="num">Total</th>
                </tr></thead>
                <tbody id="cbpEditItemsBody"></tbody>
                <tfoot><tr>
                  <td colspan="6" class="num"><strong>Total this step</strong></td>
                  <td class="num"><strong id="cbpEditTotal">0.00</strong></td>
                </tr></tfoot>
              </table>
            </div>
            <div class="cbp-hint mt-1" id="cbpEditHint"></div>
          </div>
        </div>
      </div>
      <div class="modal-footer">
        <span class="me-auto cbp-hint" id="cbpFooterNote"></span>
        <button type="button" class="btn btn-outline-secondary d-none" id="cbpBtnBack"><i class="fas fa-arrow-left me-1"></i> Back to history</button>
        <button type="button" class="btn btn-outline-secondary" id="cbpBtnClose" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary-app" id="cbpBtnNew"><i class="fas fa-plus me-1"></i> Record step</button>
        <button type="button" class="btn btn-primary-app d-none" id="cbpBtnSave"><i class="fas fa-floppy-disk me-1"></i> Save step</button>
      </div>
    </div>
  </div>
</div>

<div class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" id="cbpPoPrintModal" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-xl modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header po-modal-header-v2">
        <div class="po-header-icon"><i class="fas fa-print"></i></div>
        <div class="po-header-text">
          <h5 class="modal-title">PO Print</h5>
          <small id="cbpPoPrintSubtitle">Processing step</small>
        </div>
        <button type="button" class="po-header-close" data-bs-dismiss="modal" aria-label="Close"><i class="fas fa-times"></i></button>
      </div>
      <div class="modal-body cp-preview-body">
        <form id="cbpPoPrintForm" class="po-card mb-3">
          <fieldset id="cbpPoPrintOptions" disabled>
            <div class="row g-2">
              <div class="col-md-5">
                <label class="form-label" for="cbpPoSignatory">Signatory Name</label>
                <input id="cbpPoSignatory" class="form-control" list="dlSignatories" autocomplete="off">
              </div>
              <div class="col-md-4">
                <label class="form-label" for="cbpPoSignatoryTitle">Signatory Title</label>
                <input id="cbpPoSignatoryTitle" class="form-control">
              </div>
              <div class="col-md-3">
                <label class="form-label" for="cbpPoInitial">Initial <span class="text-danger">*</span></label>
                <input id="cbpPoInitial" class="form-control" maxlength="12" autocomplete="off" required>
              </div>
              <div class="col-md-4">
                <label class="form-label" for="cbpPoDate">Date</label>
                <input type="date" id="cbpPoDate" class="form-control">
              </div>
              <div class="col-md-4">
                <label class="form-label" for="cbpPoPaper">Paper</label>
                <select id="cbpPoPaper" class="form-select">
                  <option value="auto">Auto</option><option value="a4">A4</option>
                  <option value="long">Long / Folio</option><option value="legal">Legal</option>
                  <option value="letter">Letter</option>
                </select>
              </div>
              <div class="col-md-4 d-flex align-items-end">
                <button type="submit" class="btn btn-primary-app w-100"><i class="fas fa-file-pdf me-1"></i>Generate Preview</button>
              </div>
            </div>
          </fieldset>
          <div class="form-text mt-2" id="cbpPoPrintContext"></div>
        </form>
        <div id="cbpPoPrintSource" class="cp-offscreen-source cbp-po-source"></div>
        <div id="cbpPoPrintStatus" class="text-center small py-3" role="status" aria-live="polite"></div>
        <iframe id="cbpPoPdfFrame" class="pdf-preview-frame d-none" title="Processing step Purchase Order PDF preview"></iframe>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-outline-secondary" id="cbpPoOpenTab" disabled><i class="fas fa-up-right-from-square me-1"></i>Open in Tab</button>
        <button type="button" class="btn btn-outline-primary" id="cbpPoDownload" disabled><i class="fas fa-download me-1"></i>Download PDF</button>
        <button type="button" class="btn btn-primary-app" id="cbpPoPrint" disabled><i class="fas fa-print me-1"></i>Print</button>
      </div>
    </div>
  </div>
</div>

<!-- Obligation Request — adapted field-for-field from purchase_orders.php's
     #poObligationModal (replacing the legacy Obligation_Print.vb), for
     Competitive Bidding's own fields. See the plan note on Department/
     Category having no CB equivalent — left free-typed here, same as they
     already behave on a PO where either happens to be blank. -->
<div class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" id="cbObligationModal" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header po-modal-header-v2">
        <div class="po-header-icon"><i class="fas fa-file-invoice-dollar"></i></div>
        <div class="po-header-text">
            <h5 class="modal-title">Obligation Request</h5>
            <small>Please fill up the details for the Obligation Request.</small>
        </div>
        <button type="button" class="po-header-close" data-bs-dismiss="modal" aria-label="Close"><i class="fas fa-times"></i></button>
      </div>
      <div class="modal-body">
        <input type="hidden" id="obligationBidId">
        <input type="hidden" id="obligationBidNumber">
        <div class="po-card">
            <div class="row g-2 tight-field-row">
                <div class="col-12 po-field-group"><label>Barcode Number</label><div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-barcode"></i></span><input type="text" id="obBidBarcodeNumber" class="form-control"></div><div class="po-field-hint" id="obBidBarcodeHint"></div></div>
                <div class="col-12 po-field-group"><label>Supplier Name</label><div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-users"></i></span><input type="text" id="obBidSupplierName" class="form-control"></div></div>
                <div class="col-12 po-field-group"><label>Supplier Address</label><div class="po-input-wrap"><span class="po-input-icon" style="top:19px;transform:none;"><i class="fas fa-location-dot"></i></span><textarea id="obBidSupplierAddress" class="form-control" rows="2"></textarea></div></div>
                <div class="col-md-6 po-field-group"><label>Department</label><div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-building"></i></span><input type="text" id="obBidDepartment" class="form-control" placeholder="Not tracked on a bid — type if needed"></div></div>
                <div class="col-md-6 po-field-group"><label>Category</label><div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-tags"></i></span><input type="text" id="obBidCategory" class="form-control" placeholder="Not tracked on a bid — type if needed"></div></div>
                <div class="col-md-6 po-field-group"><label>City Budget</label><div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-user"></i></span><input type="text" id="obBidCityBudget" class="form-control" list="dlSignatories" autocomplete="off"></div></div>
                <div class="col-md-6 po-field-group"><label>City Budget Position</label><div class="po-input-wrap"><span class="po-input-icon" style="top:19px;transform:none;"><i class="fas fa-id-badge"></i></span><textarea id="obBidCityBudgetPosition" class="form-control" rows="4"></textarea></div></div>
                <div class="col-md-6 po-field-group"><label>End User</label><div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-user"></i></span><input type="text" id="obBidEndUser" class="form-control" list="dlSignatories" autocomplete="off"></div></div>
                <div class="col-md-6 po-field-group"><label>End User Position</label><div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-id-badge"></i></span><input type="text" id="obBidEndUserPosition" class="form-control"></div></div>
                <div class="col-12 po-field-group"><label class="text-danger">Total Amount</label><div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-peso-sign"></i></span><input type="text" id="obBidTotalAmount" class="form-control" readonly></div></div>
            </div>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
        <button type="button" class="btn btn-outline-danger" id="obBidReimbursementButton"><i class="fas fa-receipt me-1"></i> Reimbursement Print</button>
        <button type="button" class="btn btn-outline-danger" id="obBidReimbursementNoFooterButton" title="Preview the reimbursement request without the printed date and page footer"><i class="fas fa-receipt me-1"></i> Reimbursement (No Footer)</button>
        <button type="button" class="btn btn-dark" id="obBidPaymentButton"><i class="fas fa-money-check-dollar me-1"></i> Payment Print</button>
        <button type="button" class="btn btn-outline-secondary" id="obBidPaymentNoFooterButton" title="Preview the payment request without the printed date and page footer"><i class="fas fa-money-check-dollar me-1"></i> Payment (No Footer)</button>
      </div>
    </div>
  </div>
</div>

<div class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" id="cbObligationPreviewModal" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header po-modal-header-v2 no-print">
        <div class="po-header-icon"><i class="fas fa-print"></i></div>
        <div class="po-header-text"><h5 class="modal-title" id="obBidPreviewTitle">Obligation Request Preview</h5></div>
        <button type="button" class="po-header-close" data-bs-dismiss="modal" aria-label="Close"><i class="fas fa-times"></i></button>
      </div>
      <div class="modal-body cp-preview-body">
        <div id="obBidPrintSource" class="cp-offscreen-source"></div>
        <div id="obBidPdfLoading" class="text-center text-muted py-5">
            <div class="spinner-border text-primary" role="status"></div>
            <div class="small mt-2">Generating PDF preview...</div>
        </div>
        <iframe id="obBidPdfFrame" class="pdf-preview-frame d-none" title="Obligation Request PDF preview"></iframe>
      </div>
      <div class="modal-footer no-print">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-outline-secondary" id="obBidPdfOpenTab" disabled><i class="fas fa-up-right-from-square me-1"></i> Open in Tab</button>
        <button type="button" class="btn btn-outline-primary" id="obBidPdfDownload" disabled><i class="fas fa-download me-1"></i> Download PDF</button>
        <button type="button" class="btn btn-primary-app" id="obBidPdfPrint" disabled><i class="fas fa-print me-1"></i> Print</button>
      </div>
    </div>
  </div>
</div>

<!-- Contract Agreement print preview. Same two-part shape as the Purchase
     Order's: an off-screen source element that html2canvas rasterises, and an
     iframe showing the real PDF that comes out of it. -->
<div class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" id="cbPrintModal" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-xl modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header po-modal-header-v2">
        <div class="po-header-icon"><i class="fas fa-print"></i></div>
        <div class="po-header-text">
            <h5 class="modal-title">Contract Agreement</h5>
            <small id="cbPrintSubtitle">&nbsp;</small>
        </div>
        <button type="button" class="po-header-close" data-bs-dismiss="modal" aria-label="Close"><i class="fas fa-times"></i></button>
      </div>
      <div class="modal-body cp-preview-body">
        <div id="cbDocTabs" class="d-flex gap-2 justify-content-center mb-3 flex-wrap"></div>
        <div id="cbPrintSource" class="cp-offscreen-source"></div>
        <div id="cbPdfLoading" class="text-center text-muted py-5">
            <div class="spinner-border text-primary" role="status"></div>
            <div class="small mt-2">Generating PDF preview&hellip;</div>
        </div>
        <iframe id="cbPdfFrame" class="pdf-preview-frame d-none" title="Contract Agreement PDF preview"></iframe>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-outline-secondary" id="cbPdfOpenTab" disabled><i class="fas fa-up-right-from-square me-1"></i> Open in Tab</button>
        <button type="button" class="btn btn-outline-primary" id="cbPdfDownload" disabled><i class="fas fa-download me-1"></i> Download PDF</button>
        <button type="button" class="btn btn-primary-app" id="cbPdfPrint" disabled><i class="fas fa-print me-1"></i> Print</button>
      </div>
    </div>
  </div>
</div>
<div id="cbBusy" class="cb-loader d-none" role="status" aria-live="polite">
    <div class="cb-busy-box">
        <div class="cb-busy-text">Loading the document, please wait&hellip;</div>
        <div class="cb-bar text-primary"><span></span></div>
        <div class="cb-busy-pct"></div>
    </div>
</div>
<datalist id="dlSignatories"></datalist>
<datalist id="dlRemarks"></datalist>
<datalist id="dlCbPoNumbers"></datalist>

<?php /* Shared Document Tracking trail popup — see dts_trail.php. Included
         at the END of <body>, outside .top-navbar, for the same stacking
         reason as profile_modal.php. */ ?>
<?php include 'dts_trail.php'; ?>

<?php include 'profile_modal.php'; ?>

<script src="vendor/js/jquery.min.js"></script>
<script src="vendor/js/jquery.dataTables.min.js"></script>
<script src="vendor/js/dataTables.bootstrap5.min.js"></script>
<script src="vendor/js/bootstrap.bundle.min.js"></script>
<script src="vendor/js/html2canvas.min.js"></script>
<script src="vendor/js/jspdf.umd.min.js"></script>
<script src="<?= asset('js/print-pdf.js') ?>"></script>
<script src="<?= asset('js/po-print.js') ?>"></script>
<script src="<?= asset('js/cb-processing-print.js') ?>"></script>
<script src="<?= asset('js/record-history.js') ?>"></script>
<script>
const apiUrl = 'api/competitive_bidding.php';
let cbTable = null;
let firstLoadDone = false;
// The last list load, by record ID, so a row action can name its document in
// a confirm box without another round trip.
let cbBidsById = {};

// A Bid Number can carry several contracts, so on its own it no longer names
// one document. Wherever a document is named to the user, both go together.
function bidLabel(bid) {
    if (!bid) return '';
    const number = String(bid.bid_number || '').trim();
    const contract = String(bid.contract_number || '').trim();
    return contract ? `${number} (Contract ${contract})` : number;
}

// ── Small shared helpers ────────────────────────────────────────────────
function escapeHtml(value) {
    if (value === null || value === undefined) return '';
    return String(value)
        .replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;').replace(/'/g, '&#39;');
}

function showToast(msg, type = 'success') {
    const id = 'toast-' + Date.now();
    const bg = type === 'success' ? 'bg-success' : (type === 'warning' ? 'bg-warning text-dark' : 'bg-danger');
    const html = `<div id="${id}" class="toast align-items-center text-white ${bg} border-0" role="alert">
        <div class="d-flex"><div class="toast-body">${escapeHtml(msg)}</div>
        <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button></div></div>`;
    document.getElementById('toastContainer').insertAdjacentHTML('beforeend', html);
    const el = document.getElementById(id);
    new bootstrap.Toast(el, { delay: 4000 }).show();
    el.addEventListener('hidden.bs.toast', () => el.remove());
}

function openModal(id) {
    const el = document.getElementById(id);
    (bootstrap.Modal.getInstance(el) || new bootstrap.Modal(el)).show();
}

// "Loading the document, please wait" overlay with a progress bar. Always pair
// start() with end() in a finally, or a failed request would leave the screen
// covered.
//
// Indeterminate (a sliding segment) while waiting on the server, where there is
// no total to measure; determinate once the rows are being drawn and the
// fraction done is known — progress() switches it over.
function cbLoader(idOrElement) {
    const root = typeof idOrElement === 'string' ? document.getElementById(idOrElement) : idOrElement;
    const text = root.querySelector('.cb-busy-text');
    const bar  = root.querySelector('.cb-bar > span');
    const pct  = root.querySelector('.cb-busy-pct');
    const idle = text.textContent;
    return {
        start(message) {
            text.textContent = message || idle;
            bar.style.width = '';
            pct.textContent = '';
            root.classList.add('indeterminate');
            root.classList.remove('d-none');
        },
        progress(fraction, detail) {
            const value = Math.max(0, Math.min(100, Math.round(fraction * 100)));
            root.classList.remove('indeterminate');
            bar.style.width = value + '%';
            pct.textContent = value + '%' + (detail ? '  ·  ' + detail : '');
        },
        end() { root.classList.add('d-none'); },
    };
}

const cbBusy = cbLoader('cbBusy');               // full screen, before any modal exists
const cbLoad = cbLoader('cbModalLoading');       // over the Bid Document modal
let cbLoadToken = 0;                             // identifies the latest open, so a stale load can bail

// Resolves after the browser has actually painted, so the overlay and the bar
// are on screen before the next slab of synchronous work blocks the page.
function cbNextPaint() {
    return new Promise(resolve => requestAnimationFrame(() => setTimeout(resolve, 0)));
}

// Money in, number out. Strips thousands separators and any currency mark
// first, so a pasted "10,650,000.00" does not silently become 10.
function parseMoney(value) {
    if (value === null || value === undefined) return 0;
    const clean = String(value).replace(/[^0-9.\-]/g, '');
    return clean === '' ? 0 : (parseFloat(clean) || 0);
}

function formatMoney(value) {
    return Number(value || 0).toLocaleString('en-PH', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
}

// What the bidding saved: the approved budget the request was raised for, less
// what the winning bidder was actually awarded.
//
// Returns null — not 0 — unless BOTH figures are on file. The distinction is
// the whole point: most of the live records carry no PR amount yet, and
// treating a missing budget as zero would report every one of them as having
// saved the entire contract price. "Not known" has to stay visibly different
// from "nothing saved".
function savingsOf(prAmount, contractAmount) {
    const pr = parseMoney(prAmount), award = parseMoney(contractAmount);
    if (pr <= 0 || award <= 0) return null;
    return pr - award;
}

// The percentage needs the budget it came off, which is why this takes the PR
// amount as well — ₱40,000 saved reads very differently against ₱50,000 than
// against ₱50,000,000.
function renderSavingsCell(value, prAmount) {
    if (value === null || value === undefined) {
        return '<span class="cb-save none" title="No PR amount on file, so there is nothing to compare the award against">'
             + '<span class="cb-save-val">—</span></span>';
    }
    const pr = parseMoney(prAmount);
    const pct = pr ? (Math.abs(value) / pr * 100).toFixed(1) + '%' : '';
    const over = value < 0;
    return `<span class="cb-save ${over ? 'over' : ''}">
        <span class="cb-save-val">${over ? '+' : ''}${formatMoney(Math.abs(value))}</span>
        <span class="cb-save-pct">${over ? 'over budget' : 'saved'}${pct ? ' · ' + pct : ''}</span></span>`;
}

// The stage ladder, mirroring CB_STAGES in api/competitive_bidding.php. The
// server decides which stage a row is in — this is only how each one is
// labelled and coloured, kept in one place so the badge, the chips and the
// filter cannot drift apart.
const CB_STAGES = [
    ['draft',   'Draft'],
    ['awarded', 'Awarded'],
    ['bond',    'Submission of Performance Bond'],
    ['signing', 'For Contract Signing'],
    ['signed',  'Signed'],
    ['ntp',     'Notice to Proceed'],
    ['cgso',    'Forwarded to CGSO'],
];
const CB_STAGE_LABEL = Object.fromEntries(CB_STAGES);

// Shortened for the chip row only. The badge in the table keeps the full
// wording, because that is the column somebody reads to answer "where is it".
const CB_STAGE_SHORT = {
    draft: 'Draft', awarded: 'Awarded', bond: 'For Bond', signing: 'For Signing',
    signed: 'Signed', ntp: 'NTP', cgso: 'To CGSO',
};

function stageBadge(bid) {
    const key = bid.stage || 'draft';
    return '<span class="badge-status st-' + key + '">'
         + escapeHtml(bid.stage_label || CB_STAGE_LABEL[key] || key) + '</span>';
}

// ── List ────────────────────────────────────────────────────────────────
function initTable() {
    cbTable = $('#cbTable').DataTable({
        columns: [
            // render() only dresses up the 'display' pass, so filtering, sorting
            // and export all still see the bare value.
            { data: 'bid_number', render: (d, type) =>
                type === 'display' && d && d !== '—' ? `<span class="bid-num-pill">${d}</span>` : d },
            { data: 'contract_number', render: (d, type) => {
                if (type !== 'display' || !d) return d;
                return d === '—' ? '<span class="cb-empty">—</span>' : `<span class="contract-num-pill">${d}</span>`;
            } },
            { data: 'contract_name', render: (d, type) =>
                type === 'display' && d && d !== '—' ? `<span class="cb-wrap-cell">${d}</span>` : d },
            { data: 'contract_location', render: (d, type) =>
                type === 'display' && d && d !== '—' ? `<span class="cb-wrap-cell">${d}</span>` : d },
            { data: 'supplier_name', render: (d, type) =>
                type === 'display' && d && d !== '—' ? `<span class="cb-wrap-cell">${d}</span>` : d },
            // Sorts on the number, displays the formatted string — hence the
            // split by `type` rather than formatting in formatRow().
            { data: 'pr_amount_raw', render: (d, type) =>
                type === 'display'
                    ? (d ? `<span class="cb-amount muted">${formatMoney(d)}</span>` : '<span class="cb-empty">—</span>')
                    : (d || 0) },
            { data: 'contract_amount_raw', render: (d, type) =>
                type === 'display'
                    ? (d ? `<span class="cb-amount">${formatMoney(d)}</span>` : '<span class="cb-empty">—</span>')
                    : (d || 0) },
            // Sorts on the peso figure so the biggest saving comes to the top,
            // which is the only sort order anyone wants on this column.
            { data: 'savings_raw', render: (d, type, row) =>
                type === 'display' ? renderSavingsCell(d, row.pr_amount_raw) : (d || 0) },
            { data: 'status' },
            { data: 'actions', orderable: false, searchable: false },
        ],
        order: [[0, 'desc']],
        pageLength: 15,
        createdRow: (row) => { $(row).find('td:eq(8), td:eq(9)').addClass('text-center'); },
    });
}

function formatRow(bid) {
    return {
        id: bid.id,
        bid_number: escapeHtml(bid.bid_number) || '—',
        contract_number: escapeHtml(bid.contract_number) || '—',
        contract_name: escapeHtml(bid.contract_name) || '—',
        contract_location: escapeHtml(bid.contract_location) || '—',
        supplier_name: escapeHtml((bid.supplier_name || '').trim()) || '—',
        pr_amount_raw: parseMoney(bid.pr_amount),
        contract_amount_raw: parseMoney(bid.contract_amount),
        // null, not 0, when either figure is missing. A saving of zero and a
        // saving nobody can compute are different answers, and the cell says
        // so rather than showing ₱0.00 for both.
        savings_raw: savingsOf(bid.pr_amount, bid.contract_amount),
        locked: !!bid.locked,
        status: stageBadge(bid),
        actions: buildActions(bid),
    };
}

// Every action addresses the record by its ID, not its Bid Number: two
// contracts under one bid share the number, and must not share a button.
function buildActions(bid) {
    const id = parseInt(bid.id, 10) || 0;
    const locked = !!bid.locked;
    // A locked set is issued paperwork: Edit and Delete are disabled outright
    // rather than left to fail at the server, so the state is visible before
    // anyone types into a form they cannot save.
    const disabled = locked ? 'disabled' : '';
    const lockTitle = locked ? 'Unlock — allow edits again' : 'Lock — the set has been issued';
    return `<div class="table-actions justify-content-center">
        <button class="btn btn-sm cb-action-view" title="View" onclick="viewBid(${id})"><i class="fas fa-eye"></i></button>
        <button class="btn btn-sm cb-action-print" title="Print" onclick="openBidPrint(${id}, 'contract')"><i class="fas fa-print"></i></button>
        <button class="btn btn-sm cb-action-dts" title="Track in the Document Tracking System" onclick="trackBid(${id})"><i class="fas fa-route"></i></button>
        <button class="btn btn-sm cb-action-obligation" title="Obligation Request" onclick="openCbObligationModal(${id})"><i class="fas fa-file-invoice-dollar"></i></button>
        <button class="btn btn-sm ${locked ? 'cb-action-unlock' : 'cb-action-lock'}" title="${lockTitle}" onclick="toggleBidLock(${id}, ${locked ? 'false' : 'true'})"><i class="fas fa-${locked ? 'lock' : 'lock-open'}"></i></button>
        <button class="btn btn-sm cb-action-edit" title="Edit" onclick="editBid(${id})" ${disabled}><i class="fas fa-pen-to-square"></i></button>
        <button class="btn btn-sm cb-action-delete" title="Delete" onclick="deleteBid(${id})" ${disabled}><i class="fas fa-trash"></i></button>
    </div>`;
}

// One chip per stage. Stages with nothing in them are still drawn, because a
// zero is information — "no set is waiting on a performance bond" is worth
// seeing, and a chip row that changes shape as the work moves is hard to aim at.
function renderStageChips(counts) {
    const active = document.getElementById('filterStatus').value;
    document.getElementById('cbStageChips').innerHTML = CB_STAGES.map(([key]) =>
        `<button type="button" class="po-chip po-chip-${key} ${active === key ? 'chip-on' : ''}"
                 data-status="${key}" title="${escapeHtml(CB_STAGE_LABEL[key])}"
                 onclick="setStatusFilter('${key}')">
            ${escapeHtml(CB_STAGE_SHORT[key])}
            <span class="po-chip-num">${counts[key] === undefined ? '—' : counts[key]}</span>
        </button>`).join('');
}

function renderCounts(counts) {
    if (!counts) return;
    const fmt = n => Number(n || 0).toLocaleString('en-PH');
    document.getElementById('countTotal').textContent = fmt(counts.total);
    renderStageChips(counts);
    const active = document.getElementById('filterStatus').value;
    document.querySelectorAll('.po-chip').forEach(chip => {
        chip.classList.toggle('is-active', chip.dataset.status === active);
    });
}

// Clicking a count chip is just another way to set the Status dropdown — one
// code path drives the filter, so the two controls can't drift apart.
function setStatusFilter(status) {
    document.getElementById('filterStatus').value = status;
    loadBids();
}

function resetFilters() {
    document.getElementById('searchText').value = '';
    document.getElementById('filterStatus').value = '';
    document.getElementById('filterFrom').value = '';
    document.getElementById('filterTo').value = '';
    loadBids();
}

function initDateFilters() {
    // +1 day, not today: a document created later today would otherwise fall
    // outside a range ending at midnight this morning.
    const tomorrow = new Date(); tomorrow.setDate(tomorrow.getDate() + 1);
    const monthStart = new Date(); monthStart.setDate(1);
    document.getElementById('filterTo').value   = tomorrow.toISOString().slice(0, 10);
    document.getElementById('filterFrom').value = monthStart.toISOString().slice(0, 10);
}

async function loadBids() {
    const search = encodeURIComponent(document.getElementById('searchText').value.trim());
    const from   = document.getElementById('filterFrom').value;
    const to     = document.getElementById('filterTo').value;
    const status = document.getElementById('filterStatus').value;
    try {
        const res  = await fetch(`${apiUrl}?search=${search}&from=${from}&to=${to}&status=${status}`);
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Unable to load bid documents.', 'error');
        renderCounts(data.counts);
        cbBidsById = {};
        (data.bids || []).forEach(b => { cbBidsById[String(b.id)] = b; });
        if (!firstLoadDone) {
            document.getElementById('cbListLoading').classList.add('d-none');
            document.getElementById('cbTable').classList.remove('d-none');
            // The table was initialised while hidden, so DataTables could not
            // measure real column widths yet — recompute now that it is visible.
            cbTable.columns.adjust();
            firstLoadDone = true;
        }
        cbTable.clear().rows.add((data.bids || []).map(formatRow)).draw();
    } catch (err) {
        if (!firstLoadDone) {
            document.getElementById('cbListLoading').innerHTML =
                '<div class="text-danger small"><i class="fas fa-triangle-exclamation me-1"></i>Unable to load bid documents. <a href="#" onclick="loadBids();return false;">Retry</a></div>';
        }
        showToast('Unable to load bid documents.', 'error');
        console.error(err);
    }
}

// ── Item grid ───────────────────────────────────────────────────────────
// Same two-shape rule as the purchase order grid: a row is either a priced
// item (it has an Amount) or a description-only line (everything but the
// description blank). Amount — not QTY or Unit Cost — is the test, because a
// lump-sum line legitimately has a total with no quantity behind it.

const CB_FIELDS = [
    'bid_number', 'contract_number', 'contract_name', 'contract_location',
    'supplier_name', 'supplier_address', 'supplier_address2', 'supplier_org_place',
    'representative_name', 'representative_title', 'representative_salutation',
    'pr_amount', 'contract_amount', 'cash_bond_amount', 'surety_bond_amount',
    'place_of_delivery', 'delivery_term',
    'date_pr', 'bid_date', 'noa_date',
    'signatory_name', 'signatory_title',
    'witness_entity_name', 'witness_entity_title', 'witness_supplier_name',
    'notary_name', 'notary_place', 'notary_doc_no', 'notary_page_no', 'notary_book_no', 'notary_series',
];

// Saved one at a time through their own PATCH, not by this form's Save — see
// CB_QUICK_FIELDS in api/competitive_bidding.php for why they are separate.
const CB_QUICK_FIELD_IDS = [
    'date_received_noa', 'date_performance_bond', 'contract_date', 'ntp_date',
    'barcode_number', 'remarks', 'po_number',
];

function itemHasValue(v) { return v !== null && v !== undefined && String(v).trim() !== '' && Number(v) !== 0; }

// Inferred only when the row does not say what it is — an existing row saved
// before the flag existed, or one pasted in from elsewhere.
//
// A description is required for the guess, which is what keeps a brand-new
// empty row from being read as description-only: every column of {} is blank,
// so without this test the starter row came up dashed out and un-typeable.
function looksLikeDescriptionRow(item) {
    return String(item.items_description || '').trim() !== ''
        && !itemHasValue(item.quantity) && !itemHasValue(item.unit_cost)
        && !itemHasValue(item.total_cost) && !String(item.unit || '').trim();
}

function setRowType(row, isDescriptionOnly, refresh = true) {
    row.classList.toggle('item-row-desc', !!isDescriptionOnly);
    row.dataset.descOnly = isDescriptionOnly ? '1' : '0';
    const toggle = row.querySelector('.type-toggle');
    if (toggle) toggle.classList.toggle('active', !!isDescriptionOnly);
    if (isDescriptionOnly) {
        // Clear the numeric side so a row switched to description-only cannot
        // keep a stale cost that the server would then reject.
        ['unit', 'quantity', 'unit_cost', 'total_cost'].forEach(f => {
            const input = row.querySelector(`[data-field="${f}"]`);
            if (input) input.value = '';
        });
    }
    if (refresh) refreshItemsSummary();
}

function toggleRowType(row) {
    setRowType(row, row.dataset.descOnly !== '1');
}

// A stored amount, as a person would write it. The item columns are
// decimal(11,3), so a quantity of six arrives as "6.000" and a cost of ₱102 as
// "102.000" — three zeros of nothing, in the narrowest columns on the form.
// Number() drops them and keeps every real decimal, so 102.5 stays 102.5.
//
// Safe to re-save: these are <input type="number">, so the field posts the same
// number either way, and the column stores it back at its own scale.
function itemNum(v) {
    if (v === null || v === undefined || v === '') return '';
    const n = Number(v);
    return isFinite(n) ? String(n) : String(v);
}

// bulk = true is for loading a saved bid: it skips the per-row renumber/total
// pass and the per-row height measurement. Both re-read EVERY row already in
// the table, so drawing n rows one by one was O(n²) and froze the page on a
// long contract. The caller runs refreshItemsSummary() once at the end, and
// regrowModalFields() re-measures the descriptions when the modal is shown.
function addItemRow(item = {}, afterRow = null, focus = false, isDescriptionOnly = null, bulk = false) {
    const tbody = document.querySelector('#cbItemsTable tbody');
    const row = document.createElement('tr');
    row.innerHTML = `
        <td class="item-grid-idx"></td>
        <td><input type="number" min="0" step="any" class="form-control form-control-sm" data-field="quantity" value="${itemHasValue(item.quantity) ? itemNum(item.quantity) : ''}"><span class="item-row-dash">&mdash;</span></td>
        <td><input type="text" class="form-control form-control-sm" data-field="unit" value="${escapeHtml(item.unit)}"><span class="item-row-dash">&mdash;</span></td>
        <td><textarea class="form-control form-control-sm item-desc-input" data-field="items_description" rows="1">${escapeHtml(item.items_description)}</textarea></td>
        <td><input type="number" min="0" step="0.01" class="form-control form-control-sm" data-field="unit_cost" value="${itemHasValue(item.unit_cost) ? itemNum(item.unit_cost) : ''}"><span class="item-row-dash">&mdash;</span></td>
        <td><input type="number" min="0" step="0.01" class="form-control form-control-sm" data-field="total_cost" value="${itemHasValue(item.total_cost) ? itemNum(item.total_cost) : ''}"><span class="item-row-dash">&mdash;</span></td>
        <td class="text-nowrap">
            <div class="d-flex gap-1">
                <button type="button" class="item-grid-action-btn add" title="Insert row below" onclick="addItemRow({}, this.closest('tr'), true)"><i class="fas fa-plus"></i></button>
                <button type="button" class="item-grid-action-btn type-toggle" title="Turn into a description-only line" onclick="toggleRowType(this.closest('tr'))"><i class="fas fa-align-left"></i></button>
                <button type="button" class="item-grid-action-btn remove" title="Remove row" onclick="this.closest('tr').remove(); refreshItemsSummary();"><i class="fas fa-trash"></i></button>
            </div>
        </td>`;

    // Amount follows QTY x Unit Cost, but stays typeable: a lump-sum line has
    // an amount and no quantity, so this fills the field rather than locking
    // it, and only when there is actually something to multiply.
    const qtyInput  = row.querySelector('[data-field="quantity"]');
    const costInput = row.querySelector('[data-field="unit_cost"]');
    const totalInput = row.querySelector('[data-field="total_cost"]');
    const recalc = () => {
        const qty  = parseFloat(qtyInput.value) || 0;
        const cost = parseFloat(costInput.value) || 0;
        if (qty && cost) totalInput.value = (qty * cost).toFixed(2);
        refreshItemsSummary();
    };
    qtyInput.addEventListener('input', recalc);
    costInput.addEventListener('input', recalc);
    totalInput.addEventListener('input', refreshItemsSummary);

    // Description grows with its content instead of clipping a long spec line.
    // Resets to 'auto' first so it shrinks back down when text is deleted.
    const descInput = row.querySelector('.item-desc-input');
    const autoGrow = () => { descInput.style.height = 'auto'; descInput.style.height = descInput.scrollHeight + 'px'; };
    descInput.addEventListener('input', autoGrow);

    if (afterRow && afterRow.parentNode === tbody) afterRow.insertAdjacentElement('afterend', row);
    else tbody.appendChild(row);

    // refreshItemsSummary() runs before autoGrow(), not just after: it is what
    // un-hides #cbItemsWrap, and scrollHeight is unreliable inside a
    // display:none ancestor. A programmatically-filled row never gets a
    // keystroke to self-correct, so it would stay clipped to one line forever.
    setRowType(row, isDescriptionOnly === null ? looksLikeDescriptionRow(item) : isDescriptionOnly, !bulk);
    if (!bulk) {
        refreshItemsSummary();
        autoGrow();
    }
    if (focus) (row.classList.contains('item-row-desc') ? descInput : row.querySelector('input'))?.focus();
    return row;
}

function refreshItemsSummary() {
    const rows = [...document.querySelectorAll('#cbItemsTable tbody tr')];
    document.getElementById('cbItemsWrap').classList.toggle('d-none', rows.length === 0);
    document.getElementById('cbItemsEmpty').classList.toggle('d-none', rows.length > 0);
    let total = 0;
    rows.forEach((row, i) => {
        row.querySelector('.item-grid-idx').textContent = i + 1;
        total += parseMoney(row.querySelector('[data-field="total_cost"]').value);
    });
    document.getElementById('cbSubtotal').textContent = '₱' + formatMoney(total);
    document.getElementById('cbRowCount').textContent = rows.length;
    return total;
}

function collectItems() {
    return [...document.querySelectorAll('#cbItemsTable tbody tr')].map(row => {
        const value = f => (row.querySelector(`[data-field="${f}"]`)?.value ?? '').trim();
        return {
            quantity: value('quantity'),
            unit: value('unit'),
            items_description: value('items_description'),
            unit_cost: value('unit_cost'),
            total_cost: value('total_cost'),
            is_title: row.dataset.descOnly === '1',
        };
    }).filter(item => item.items_description !== '');
}

// Mirrors the server's cbInvalidItemRows() so the same rejection is explained
// here, before a round trip. The server still enforces it — the browser is not
// a place to enforce anything.
function invalidItemRows() {
    const bad = { item: [], desc: [] };
    collectItems().forEach((item, i) => {
        const num = k => parseMoney(item[k]);
        const blank = !item.unit && !num('quantity') && !num('unit_cost') && !num('total_cost');
        if (item.is_title) { if (!blank) bad.desc.push(i + 1); }
        else if (!num('total_cost')) bad.item.push(i + 1);
    });
    return bad;
}

// Row numbers for a rejection message, worded exactly as the server words them
// (see cbRejectInvalidItems) so the same problem never gets two descriptions.
// Capped at six: pasting a bad spreadsheet should give a readable sentence,
// not three hundred row numbers.
function rowList(rows) {
    const shown = rows.slice(0, 6);
    return (rows.length === 1 ? 'Row ' : 'Rows ') + '#' + shown.join(', #')
         + (rows.length > 6 ? ` and ${rows.length - 6} more` : '');
}

// Sends the file to api/competitive_bidding_item_parse.php and appends what
// comes back. The parse is server-side because .xlsx is a zip of XML, and
// because the same shape rules then live in one place rather than being
// reimplemented in the browser and drifting.
async function importItemsIntoGrid(file) {
    const formData = new FormData();
    formData.append('file', file);
    try {
        const res  = await fetch('api/competitive_bidding_item_parse.php', { method: 'POST', body: formData });
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Unable to import items.', 'error');
        // A single still-blank starter row is replaced rather than kept
        // alongside the import — otherwise every imported file arrives with an
        // empty row on top that then fails validation on save.
        const tbody = document.querySelector('#cbItemsTable tbody');
        const rows  = [...tbody.querySelectorAll('tr')];
        if (rows.length === 1) {
            const blank = [...rows[0].querySelectorAll('input, textarea')].every(i => i.value.trim() === '');
            if (blank) rows[0].remove();
        }
        // is_title comes from the parser, so a heading that happens to read
        // like an item does not flip type on the way in.
        data.items.forEach(item => addItemRow(item, null, false, !!item.is_title));
        refreshItemsSummary();
        showToast(data.message || 'Items imported.');
    } catch (err) {
        showToast('Unable to import items.', 'error');
        console.error(err);
    }
}

function toggleItemGuide() {



    document.getElementById('cbItemGuide').classList.toggle('d-none');
}

// ── Amount helpers ──────────────────────────────────────────────────────
function useItemsTotal() {
    document.getElementById('contract_amount').value = formatMoney(refreshItemsSummary());
    computeBonds();
    refreshSavingsBand();
}

// The same guide defaults are used by the API when saving or loading a bid.
const CB_BOND_RATES = <?= json_encode(CB_REPORT_BOND_RATES) ?>;
const CB_REPORT_DEFAULTS = <?= json_encode(CB_REPORT_DEFAULTS) ?>;

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

function computeBonds() {
    const amount = parseMoney(document.getElementById('contract_amount').value);
    Object.keys(CB_BOND_RATES).forEach(id => {
        document.getElementById(id).value = amount > 0 ? formatMoney(Math.round(amount * CB_BOND_RATES[id] * 100) / 100) : '';
    });
    refreshBondHints();
}

// What the figure actually typed comes to as a percentage of the contract, so
// a bond entered at the wrong rate — or one left behind when the contract
// amount changed — shows up instead of quietly going out on the award.
function refreshBondHints() {
    const amount = parseMoney(document.getElementById('contract_amount').value);
    [['cash_bond_amount', 'cashBondPct'], ['surety_bond_amount', 'suretyBondPct']].forEach(pair => {
        const hint = document.getElementById(pair[1]);
        if (!hint) return;
        const value = parseMoney(document.getElementById(pair[0]).value);
        if (!amount || !value) { hint.innerHTML = '&nbsp;'; hint.className = 'po-field-hint'; return; }
        const pct = (value / amount) * 100;
        const expected = CB_BOND_RATES[pair[0]] * 100;
        // A hair's tolerance before flagging: a bond rounded to the nearest peso
        // lands fractionally either side of the exact rate, and calling that
        // wrong would train people to ignore the hint.
        const off = Math.abs(pct - expected) > 0.005;
        hint.className = 'po-field-hint' + (off ? ' cb-pct-off' : '');
        hint.textContent = '= ' + pct.toFixed(2) + '% of the contract amount'
            + (off ? ' (expected ' + expected + '%)' : '');
    });
}

// The saving, stated in words under the two figures it comes from.
//
// Three states, not two. "Over the approved budget" is a real and legitimate
// outcome — a re-bid at a higher ABC, a contract awarded above the original
// request — and showing it as a negative saving would bury it. It gets its own
// amber treatment so it is visible without being called an error.
function refreshSavingsBand() {
    const band = document.getElementById('cbSavings');
    if (!band) return;
    const pr    = parseMoney(document.getElementById('pr_amount').value);
    const award = parseMoney(document.getElementById('contract_amount').value);

    if (pr <= 0 || award <= 0) {
        band.className = 'cb-savings-band idle';
        band.innerHTML = '<span class="lab">Savings</span><span class="val">&mdash;</span>'
                       + '<span class="sub">Fill in both the PR amount and the contract amount.</span>';
        return;
    }
    const diff = pr - award;
    const pct  = (Math.abs(diff) / pr * 100).toFixed(2) + '%';
    if (diff < 0) {
        band.className = 'cb-savings-band over';
        band.innerHTML = '<span class="lab">Over budget</span>'
                       + '<span class="val">₱' + formatMoney(-diff) + '</span>'
                       + '<span class="sub">' + pct + ' above the PR amount &mdash; awarded ₱'
                       + formatMoney(award) + ' against a budget of ₱' + formatMoney(pr) + '.</span>';
        return;
    }
    band.className = 'cb-savings-band';
    band.innerHTML = '<span class="lab">Savings</span>'
                   + '<span class="val">₱' + formatMoney(diff) + '</span>'
                   + '<span class="sub">' + pct + ' below the PR amount &mdash; awarded ₱'
                   + formatMoney(award) + ' against a budget of ₱' + formatMoney(pr) + '.</span>';
}

// ── Form ────────────────────────────────────────────────────────────────
function autoGrowField(el) {
    el.style.height = 'auto';
    el.style.height = el.scrollHeight + 'px';
}

// Re-measures every auto-growing field in the modal.
//
// Needed because the form is filled while the modal or the item grid is still
// hidden or animating in, and a scrollHeight read inside a display:none element
// comes back near-zero — so every field was left with a wrong inline height
// baked in, and nothing ever corrected it, because a programmatically-filled
// field never receives the keystroke that would. The measurement is redone
// once the elements have a real layout: on Bootstrap's shown event, and again
// when the item rows have been revealed.
//
// Batched — every height reset first, then every height read, then every
// height written — rather than autoGrowField() per element. That helper reads
// scrollHeight between its own write and the next element's, which forces a
// full layout each time; across the 500-odd description boxes of a long
// contract that is 500 layouts of a 500-row table, and the browser gave up
// ("Page Unresponsive"). Batched it is one layout.
function regrowModalFields() {
    const fields = [...document.querySelectorAll('#cbModal .cb-autogrow, #cbItemsTable .item-desc-input')];
    fields.forEach(el => { el.style.height = 'auto'; });
    const heights = fields.map(el => el.scrollHeight);
    fields.forEach((el, i) => { el.style.height = heights[i] + 'px'; });
}

function clearForm() {
    document.getElementById('cbId').value = '';
    clearTimeout(cbBidNumberCheckTimer);
    cbBidNumberIsDuplicate = false;
    document.getElementById('bidNumberDupHint').classList.add('d-none');
    document.getElementById('bidNumberShareHint').classList.add('d-none');
    CB_FIELDS.concat(CB_QUICK_FIELD_IDS).forEach(f => { const el = document.getElementById(f); if (el) el.value = ''; });
    Object.entries(CB_REPORT_DEFAULTS).forEach(([id, value]) => { document.getElementById(id).value = value; });
    document.querySelector('#cbItemsTable tbody').innerHTML = '';
    refreshItemsSummary();
    document.querySelectorAll('.cb-autogrow').forEach(autoGrowField);
    refreshBondHints();
    refreshSavingsBand();
}

// Header fields only. The item rows go in separately through loadItemRows(), a
// batch at a time, because a long contract has hundreds of them.
function fillForm(bid) {
    bid = bidWithReportDefaults(bid);
    document.getElementById('cbId').value = bid.id || '';
    CB_FIELDS.concat(CB_QUICK_FIELD_IDS).forEach(f => {
        const el = document.getElementById(f);
        if (!el) return;
        let value = bid[f] ?? '';
        // A DATE column comes back as YYYY-MM-DD already; anything longer is
        // trimmed so <input type="date"> accepts it.
        if (el.type === 'date') value = String(value).slice(0, 10);
        if (['pr_amount', 'contract_amount', 'cash_bond_amount', 'surety_bond_amount'].includes(f)) {
            value = value === '' || value === null ? '' : formatMoney(value);
        }
        el.value = value;
    });
    document.querySelector('#cbItemsTable tbody').innerHTML = '';
    refreshItemsSummary();
    // Values were set programmatically, so no 'input' event fired — every
    // auto-grow field needs an explicit resize.
    document.querySelectorAll('.cb-autogrow').forEach(autoGrowField);
    refreshBondHints();
    refreshSavingsBand();
}

// Builds the item rows in time-boxed batches, updating the loading bar between
// them, so the percentage follows how many items the bid actually has.
//
// The rows go into a table that is still hidden (refreshItemsSummary() is what
// un-hides it), so a batch costs no layout at all; the browser lays the whole
// grid out once, at the end. Returns false if a newer open superseded this one.
const CB_ROW_SLICE_MS = 24;

async function loadItemRows(items, token) {
    const total = items.length;
    let i = 0;
    while (i < total) {
        if (token !== cbLoadToken) return false;
        const start = performance.now();
        while (i < total && performance.now() - start < CB_ROW_SLICE_MS) {
            addItemRow(items[i++], null, false, null, true);
        }
        cbLoad.progress(i / total, i + ' of ' + total + ' items');
        await cbNextPaint();
    }
    if (token !== cbLoadToken) return false;
    cbLoad.progress(1, 'Finishing…');
    await cbNextPaint();
    refreshItemsSummary();     // un-hides the grid: the one layout pass
    regrowModalFields();       // then size every description box, batched
    return true;
}

// Title, subtitle and status badge move together — the header and the footer
// describing different documents would be worse than either being wrong alone.
// A tracking date IS the status here, so saving one can move the document to a
// new stage. Without this the badge in the open modal — and the row behind it —
// would keep showing the stage the set was in before the date was entered.
async function refreshStageAfterQuickSave() {
    if (!cbQuickBidId) return;
    try {
        const res = await fetch(`${apiUrl}?id=${encodeURIComponent(cbQuickBidId)}`);
        const data = await res.json();
        if (!data.success || !data.bid) return;
        const badge = document.getElementById('cbModalStatusBadge');
        badge.textContent = data.bid.stage_label || 'Draft';
        badge.className = 'badge-status st-' + (data.bid.stage || 'draft');
        loadBids();
    } catch (err) { console.error(err); }
}

// ═══════════════ Progressive Item Processing ═══════════════
// Adapted from inspection.php's Progress Billing (#pbModal / pb* functions).
// Talks only to api/cb_processing.php, which never writes to
// tbl_competitive_bidding or tbl_competitive_bidding_items_details — see
// that file's own header comment. Two deliberate departures from Progress
// Billing, both enforced here: over-processing is a hard stop (Save is
// disabled, not just warned), and status comes from cbProcessingStatus()
// in config.php rather than progressBillingStatus().
const cbpApiUrl = 'api/cb_processing.php';
let cbpData = null;
let cbpBusy = false;

function cbpQty(v) {
    const n = Number(v || 0);
    return (Math.round(n * 1000) / 1000).toString();
}

function cbpWhen(v) {
    if (!v) return '';
    const d = new Date(String(v).replace(' ', 'T'));
    if (isNaN(d)) return escapeHtml(v);
    return d.toLocaleDateString('en-PH', { year: 'numeric', month: 'short', day: 'numeric' })
         + ' ' + d.toLocaleTimeString('en-PH', { hour: 'numeric', minute: '2-digit' });
}

async function openCbProcessing(bidId) {
    if (!bidId) return showToast('Save the bid document first.', 'warning');
    document.getElementById('cbpBidId').value = bidId;
    document.getElementById('cbpBidNumber').value = '';
    openModal('cbProcessingModal');
    await cbpLoad(bidId);
}

async function cbpLoad(bidId) {
    try {
        const res = await fetch(`${cbpApiUrl}?id=${encodeURIComponent(bidId)}`);
        const data = await res.json();
        if (!data.success) { showToast(data.message || 'Unable to load processing steps.', 'error'); return; }
        cbpData = data;
        // Display only (the PO print's subtitle and file name) — requests go by ID.
        document.getElementById('cbpBidNumber').value = bidLabel(data.bid);
        cbpShowHistory();
        cbpRenderHistory();
    } catch (err) {
        showToast('Unable to load processing steps.', 'error');
        console.error(err);
    }
}

function cbpShowHistory() {
    document.getElementById('cbpHistoryPane').classList.remove('d-none');
    document.getElementById('cbpEditorPane').classList.add('d-none');
    document.getElementById('cbpBtnBack').classList.add('d-none');
    document.getElementById('cbpBtnNew').classList.remove('d-none');
    document.getElementById('cbpBtnSave').classList.add('d-none');
    if (cbpData) document.getElementById('cbpBtnNew').innerHTML =
        `<i class="fas fa-plus me-1"></i> Record step ${cbpData.next_step_no}`;
}

const CBP_STATUS_LABELS = { idle: 'Not started', partial: 'Partially processed', complete: 'Complete', over: 'Over-processed', unmeasured: 'Unmeasured' };

function cbpSetStatusPill(t) {
    const pill = document.getElementById('cbpStatusPill');
    pill.textContent = CBP_STATUS_LABELS[t.status] || t.status;
    pill.className = 'cbp-status-pill ' + t.status;
    pill.classList.remove('d-none');
}

function cbpRenderHistory() {
    if (!cbpData) return;
    const { bid, totals } = cbpData;
    document.getElementById('cbpSumBid').textContent = bidLabel(bid) || '—';
    document.getElementById('cbpSumContract').textContent = bid.contract_name || '—';
    document.getElementById('cbpSumSupplier').textContent = bid.supplier_name || '—';
    document.getElementById('cbpSumPo').textContent = bid.po_number || '— (none linked yet)';
    document.getElementById('cbpTotOrdered').textContent = '₱' + formatMoney(totals.ordered_amount);
    document.getElementById('cbpTotProcessed').textContent = '₱' + formatMoney(totals.processed_amount);
    document.getElementById('cbpTotRemaining').textContent = '₱' + formatMoney(totals.remaining_amount);
    document.getElementById('cbpTotCount').textContent = totals.step_count;
    cbpSetStatusPill(totals);
    const pct = totals.percent === null ? 0 : Math.max(0, Math.min(totals.percent, 100));
    document.getElementById('cbpBar').style.width = pct + '%';
    const hint = document.getElementById('cbpBarHint');
    if (totals.status === 'over') {
        hint.textContent = 'Processed more than the contract amount — check the steps below.';
        hint.className = 'cbp-hint over';
    } else {
        hint.textContent = totals.percent === null ? 'No priced items to measure against.'
            : totals.percent.toFixed(2) + '% processed.';
        hint.className = 'cbp-hint';
    }
    cbpRenderProgressTable();
    cbpRenderSteps();
}

function cbpRenderProgressTable() {
    const wrap = document.getElementById('cbpProgressWrap');
    const { progress, steps } = cbpData;
    if (!progress.length) { wrap.innerHTML = '<div class="text-muted small">No priced items on this bid.</div>'; return; }
    let html = '<table id="cbpEditItems"><thead><tr><th>Item</th><th class="num">Ordered</th>';
    steps.forEach(s => { html += `<th class="num">Step ${s.step_no}</th>`; });
    html += '<th class="num">Remaining</th></tr></thead><tbody>';
    progress.forEach(p => {
        html += `<tr><td>${escapeHtml(p.items_description)}</td><td class="num">${cbpQty(p.ordered_quantity)}</td>`;
        steps.forEach(s => {
            const line = s.items.find(it => it.cb_item_id === p.cb_item_id);
            html += `<td class="num">${line ? cbpQty(line.quantity) : '—'}</td>`;
        });
        const over = p.remaining_quantity < -0.0001;
        html += `<td class="num"${over ? ' style="color:#b91c1c;font-weight:600;"' : ''}>${cbpQty(p.remaining_quantity)}</td></tr>`;
    });
    html += '</tbody></table>';
    wrap.innerHTML = html;
}

function cbpRenderSteps() {
    const wrap = document.getElementById('cbpSteps');
    const steps = cbpData.steps || [];
    if (!steps.length) {
        wrap.innerHTML = '<div class="text-muted small text-center py-3">No processing steps recorded yet.</div>';
        return;
    }
    wrap.innerHTML = steps.slice().reverse().map(s => `
        <div class="cbp-step-card">
            <div class="head">
                <span class="step-no"><i class="fas fa-layer-group me-1"></i>Step ${s.step_no}</span>
                <span class="meta">${escapeHtml(s.created_by || 'Unknown user')} · ${cbpWhen(s.date_created)}</span>
            </div>
            ${s.processing_date ? `<div class="meta">Processed: ${cbpWhen(s.processing_date)}</div>` : ''}
            ${s.remarks ? `<div class="remarks">${escapeHtml(s.remarks)}</div>` : ''}
            <div class="meta mb-2">${s.items.length} item(s) — total ₱${formatMoney(s.total)}</div>
            <div class="actions">
                <button type="button" class="btn btn-sm btn-outline-primary" onclick="cbpOpenPoPrint(${s.step_no})"><i class="fas fa-print me-1"></i>PO Print</button>
                <button type="button" class="btn btn-sm btn-outline-secondary" onclick="cbpOpenEditor(${s.step_no})"><i class="fas fa-pen me-1"></i>Revise</button>
                <button type="button" class="btn btn-sm btn-outline-danger" onclick="cbpUndo(${s.step_no})"><i class="fas fa-rotate-left me-1"></i>Undo</button>
            </div>
        </div>
    `).join('');
}

// stepNo: undefined for a brand-new step, or an existing number to revise.
function cbpOpenEditor(stepNo) {
    if (!cbpData) return;
    document.getElementById('cbpHistoryPane').classList.add('d-none');
    document.getElementById('cbpEditorPane').classList.remove('d-none');
    document.getElementById('cbpBtnBack').classList.remove('d-none');
    document.getElementById('cbpBtnNew').classList.add('d-none');
    document.getElementById('cbpBtnSave').classList.remove('d-none');

    const isNew = stepNo === undefined || stepNo === null;
    const sel = document.getElementById('cbpEditNo');
    if (isNew) {
        const used = cbpData.steps.map(s => s.step_no);
        const opts = [];
        for (let n = 1; n <= 50; n++) if (!used.includes(n)) opts.push(n);
        sel.innerHTML = opts.map(n => `<option value="${n}">${n}</option>`).join('');
        sel.value = cbpData.next_step_no;
        sel.disabled = false;
    } else {
        sel.innerHTML = `<option value="${stepNo}">${stepNo}</option>`;
        sel.value = stepNo;
        // Moving a step from 2 to 3 is not a revision, it is two operations —
        // same rule Progress Billing's own editor enforces on Billing No.
        sel.disabled = true;
    }

    const existingStep = !isNew ? cbpData.steps.find(s => s.step_no === stepNo) : null;
    document.getElementById('cbpEditDate').value = existingStep && existingStep.processing_date
        ? String(existingStep.processing_date).slice(0, 10) : new Date().toISOString().slice(0, 10);
    document.getElementById('cbpEditRemarks').value = existingStep ? (existingStep.remarks || '') : '';

    cbpRenderEditorItems(existingStep);
}

function cbpRenderEditorItems(existingStep) {
    const tbody = document.getElementById('cbpEditItemsBody');
    const existingByItemId = {};
    if (existingStep) existingStep.items.forEach(it => { existingByItemId[it.cb_item_id] = it; });

    tbody.innerHTML = '';
    cbpData.ordered_items.forEach(oi => {
        if (oi.line_kind === 'caption') {
            const tr = document.createElement('tr');
            tr.className = 'cbp-caption';
            tr.innerHTML = `<td colspan="7">${escapeHtml(oi.items_description)}</td>`;
            tbody.appendChild(tr);
            return;
        }
        const prog = cbpData.progress.find(p => p.cb_item_id === oi.cb_item_id) || {};
        const existingLine = existingByItemId[oi.cb_item_id];
        // Excludes THIS step's own current quantity from "remaining" —
        // otherwise revising a step would count its own prior quantity
        // against itself. Mirrors pbRenderEditorItems()'s billedElsewhere.
        const mine = existingLine ? Number(existingLine.quantity) : 0;
        const remainingExcludingMine = Number(prog.remaining_quantity || 0) + mine;

        const tr = document.createElement('tr');
        tr.dataset.cbItemId = oi.cb_item_id;
        tr.dataset.remaining = remainingExcludingMine;
        tr.dataset.unitCost = oi.process_unit_cost;
        tr.innerHTML = `
            <td>${escapeHtml(oi.items_description)}</td>
            <td>${escapeHtml(oi.unit || '')}</td>
            <td class="num">${cbpQty(oi.orderable_quantity)}</td>
            <td class="num">${cbpQty(remainingExcludingMine)}</td>
            <td class="num"><input type="number" min="0" step="0.001" class="cbp-qty-input" value="${mine || ''}" oninput="cbpRecalcRow(this)"></td>
            <td class="num">${formatMoney(oi.process_unit_cost)}</td>
            <td class="num cbp-line-total">${formatMoney(mine * oi.process_unit_cost)}</td>
        `;
        tbody.appendChild(tr);
    });
    cbpRecalcTotal();
}

function cbpRecalcRow(input) {
    const row = input.closest('tr');
    const qty = parseFloat(input.value) || 0;
    const remaining = parseFloat(row.dataset.remaining) || 0;
    const unitCost = parseFloat(row.dataset.unitCost) || 0;
    input.classList.toggle('cbp-over', qty > remaining + 0.0001);
    row.querySelector('.cbp-line-total').textContent = formatMoney(qty * unitCost);
    cbpRecalcTotal();
}

function cbpRecalcTotal() {
    let total = 0;
    document.querySelectorAll('#cbpEditItemsBody tr').forEach(row => {
        const input = row.querySelector('.cbp-qty-input');
        if (!input) return;
        total += (parseFloat(input.value) || 0) * (parseFloat(row.dataset.unitCost) || 0);
    });
    document.getElementById('cbpEditTotal').textContent = formatMoney(total);
    const anyOver = !!document.querySelector('.cbp-over');
    const hint = document.getElementById('cbpEditHint');
    const saveBtn = document.getElementById('cbpBtnSave');
    if (anyOver) {
        hint.textContent = 'One or more items exceed what remains — fix the highlighted quantity before saving.';
        hint.className = 'cbp-hint over';
        saveBtn.disabled = true;
    } else {
        hint.textContent = '';
        hint.className = 'cbp-hint';
        saveBtn.disabled = false;
    }
}

function cbpCollectItems() {
    const items = [];
    document.querySelectorAll('#cbpEditItemsBody tr').forEach(row => {
        const input = row.querySelector('.cbp-qty-input');
        if (!input) return; // caption row, no input at all
        const qty = parseFloat(input.value) || 0;
        if (qty <= 0) return; // not processed in this step — simply omitted, nothing deleted
        items.push({ cb_item_id: parseInt(row.dataset.cbItemId, 10), quantity: qty });
    });
    return items;
}

async function cbpSave() {
    if (cbpBusy) return;
    const bidId = document.getElementById('cbpBidId').value;
    const stepNo = parseInt(document.getElementById('cbpEditNo').value, 10);
    if (!stepNo) return showToast('A step number is required.', 'warning');
    if (document.querySelector('.cbp-over')) return showToast('Fix the highlighted quantity before saving.', 'warning');
    const items = cbpCollectItems();
    if (!items.length) return showToast('No items were processed in this step.', 'warning');

    const isRevise = cbpData.steps.some(s => s.step_no === stepNo);
    const payload = {
        id: bidId, step_no: stepNo,
        processing_date: document.getElementById('cbpEditDate').value,
        remarks: document.getElementById('cbpEditRemarks').value.trim(),
        items,
    };
    const btn = document.getElementById('cbpBtnSave');
    const original = btn.innerHTML;
    btn.disabled = true;
    btn.innerHTML = '<span class="spinner-border spinner-border-sm me-1"></span> Saving…';
    cbpBusy = true;
    try {
        const res = await fetch(cbpApiUrl, {
            method: isRevise ? 'PUT' : 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload),
        });
        const data = await res.json();
        if (!data.success) { showToast(data.message || 'Unable to save this step.', 'error'); return; }
        showToast(data.message || 'Step saved.', 'success');
        cbpData = data; // same shape as the GET load
        cbpShowHistory();
        cbpRenderHistory();
    } catch (err) {
        showToast('Unable to save this step.', 'error');
        console.error(err);
    } finally {
        cbpBusy = false;
        btn.disabled = false;
        btn.innerHTML = original;
    }
}

async function cbpUndo(stepNo) {
    if (!confirm(`Undo step ${stepNo}? This restores the version it replaced, or removes it entirely if it was the only version.`)) return;
    const bidId = document.getElementById('cbpBidId').value;
    try {
        const res = await fetch(`${cbpApiUrl}?id=${encodeURIComponent(bidId)}&step_no=${stepNo}`, { method: 'DELETE' });
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Unable to undo this step.', 'error');
        showToast(data.message || 'Undone.', 'success');
        cbpData = data;
        cbpRenderHistory();
    } catch (err) {
        showToast('Unable to undo this step.', 'error');
        console.error(err);
    }
}

// ═══════════════ Obligation Request (Competitive Bidding) ═══════════════
// Adapted field-for-field from purchase_orders.php's #poObligationModal
// (replacing the legacy Obligation_Print.vb) — see the plan note on why
// Department/Category are free-typed here (a bid carries neither). Renders
// through the same PrintPdf module every other document on this page uses,
// not a copy of the older raw html2canvas+jsPDF calls the PO version has.
let cbObligationBid = null;

function cbAmountInWords(amount) {
    const pesos = Math.floor(amount);
    const centavos = Math.round((amount - pesos) * 100);
    return `${numberToWords(pesos)} Pesos And ${String(centavos).padStart(2, '0')}/100 Only`;
}

async function openCbObligationModal(bidId) {
    if (!bidId) return showToast('Save the bid document first.', 'warning');
    try {
        const res = await fetch(`${apiUrl}?id=${encodeURIComponent(bidId)}`);
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Unable to load the bid document.', 'error');
        cbObligationBid = data.bid;
        const bidNumber = String(data.bid.bid_number || '');
        document.getElementById('obligationBidId').value = data.bid.id;
        const total = parseMoney(cbObligationBid.contract_amount)
            || (cbObligationBid.items || []).reduce((sum, i) => sum + (parseFloat(i.total_cost) || 0), 0);

        document.getElementById('obligationBidNumber').value = bidNumber;
        const storedBarcode = (cbObligationBid.barcode_number || '').trim();
        document.getElementById('obBidBarcodeNumber').value = storedBarcode || bidNumber.replace(/[^0-9A-Za-z]/g, '');
        document.getElementById('obBidBarcodeHint').textContent = storedBarcode
            ? '' : 'Suggested from the Bid Number. Edit it if a different number is needed.';
        document.getElementById('obBidSupplierName').value = cbObligationBid.supplier_name || '';
        document.getElementById('obBidSupplierAddress').value = cbObligationBid.supplier_address || '';
        document.getElementById('obBidDepartment').value = '';
        document.getElementById('obBidCategory').value = '';
        document.getElementById('obBidCityBudget').value = cbCityBudgetSignatory ? cbCityBudgetSignatory.name : 'CITY BUDGET';
        const cbTitle = (cbCityBudgetSignatory && (cbCityBudgetSignatory.extra || '').trim()) || CB_CITY_BUDGET_TITLE_DEFAULT;
        document.getElementById('obBidCityBudgetPosition').value = cbTitle + '\n' + CB_CITY_BUDGET_AUTHORITY_LINE;
        document.getElementById('obBidEndUser').value = cbDefaultSignatory ? cbDefaultSignatory.name : '';
        document.getElementById('obBidEndUserPosition').value = cbDefaultSignatory ? (cbDefaultSignatory.extra || '') : '';
        document.getElementById('obBidTotalAmount').value = formatMoney(total);

        openModal('cbObligationModal');
    } catch (err) {
        showToast('Unable to load the bid document.', 'error');
        console.error(err);
    }
}

function cbObligationValues() {
    return {
        bidId: document.getElementById('obligationBidId').value,
        bidNumber: document.getElementById('obligationBidNumber').value,
        barcodeNumber: document.getElementById('obBidBarcodeNumber').value.trim(),
        supplierName: document.getElementById('obBidSupplierName').value.trim(),
        supplierAddress: document.getElementById('obBidSupplierAddress').value.trim(),
        department: document.getElementById('obBidDepartment').value.trim(),
        category: document.getElementById('obBidCategory').value.trim(),
        cityBudget: document.getElementById('obBidCityBudget').value.trim(),
        cityBudgetPosition: document.getElementById('obBidCityBudgetPosition').value,
        endUser: document.getElementById('obBidEndUser').value.trim(),
        endUserPosition: document.getElementById('obBidEndUserPosition').value.trim(),
        totalAmount: parseMoney(document.getElementById('obBidTotalAmount').value),
    };
}

// No barcode PNG here on purpose: unlike Purchase Orders, Competitive
// Bidding has no barcode-image rendering anywhere on this page today (its
// own Document Tracking No. field is plain text) — adding one is outside
// this feature's scope. The number itself still prints, as text.
function buildCbObligationPreviewHtml(v, reportType) {
    const actionWord = reportType === 'reimbursement' ? 'Reimbursement' : 'Payment';
    return `
    <div class="print-page" style="width:650px;">
      <div class="pp-frame">
        <div class="orq-letterhead-wrap">
        <table class="orq-letterhead">
            <tr>
                <td class="orq-seal"><img src="image/citylogo.png" alt=""></td>
                <td class="orq-letterhead-text">
                    <span class="blue">Province of Bukidnon</span><br>
                    <span class="blue bold">City of Malaybalay</span><br>
                    New City Hall, Barangay Casisang<br>
                    Malaybalay City, Bukidnon<br>
                    Tel&nbsp; (088) 813-4761-2
                </td>
            </tr>
        </table>
        <div style="position:absolute;right:0;top:-2px;text-align:center;font-size:10px;">${escapeHtml(v.barcodeNumber)}</div>
        </div>
        <hr class="orq-rule">

        <table class="orq-grid">
            <tr>
                <td class="orq-title-cell" colspan="2">OBLIGATION REQUEST</td>
                <td class="orq-label">No.</td>
                <td class="orq-value">&nbsp;</td>
            </tr>
            <tr>
                <td class="orq-label">Payee:</td>
                <td class="orq-value">${escapeHtml(v.supplierName) || '&nbsp;'}</td>
                <td class="orq-label">Date:</td>
                <td class="orq-value">&nbsp;</td>
            </tr>
            <tr>
                <td class="orq-label">Address:</td>
                <td class="orq-value">${escapeHtml(v.supplierAddress) || '&nbsp;'}</td>
                <td class="orq-label">Responsibility Center:</td>
                <td class="orq-value">&nbsp;</td>
            </tr>
            <tr>
                <td class="orq-label">Office:</td>
                <td class="orq-value">${escapeHtml(v.department) || '&nbsp;'}</td>
                <td class="orq-label">F/P.P.A:</td>
                <td class="orq-value">&nbsp;</td>
            </tr>
        </table>

        <table class="orq-grid">
            <tr>
                <td class="orq-particular-header" style="width:12%;">Responsible<br>Center</td>
                <td class="orq-particular-header" style="width:48%;">PARTICULAR</td>
                <td class="orq-particular-header" style="width:10%;">F.P.P</td>
                <td class="orq-particular-header" style="width:14%;">Account<br>Code</td>
                <td class="orq-particular-header" style="width:16%;">Amount</td>
            </tr>
            <tr>
                <td class="orq-particular-body" style="height:150px;">&nbsp;</td>
                <td class="orq-particular-body">To ${actionWord} of<br>${escapeHtml(v.category) || '&nbsp;'}<br><br>as per supporting papers, hereto<br>attached in the amount of . . . .</td>
                <td class="orq-particular-body">&nbsp;</td>
                <td class="orq-particular-body">&nbsp;</td>
                <td class="orq-amount-cell">PHP&nbsp;${formatMoney(v.totalAmount)}</td>
            </tr>
            <tr>
                <td colspan="3" class="orq-pesos-label">PESOS: <span class="orq-words">${cbAmountInWords(v.totalAmount)}</span></td>
                <td class="orq-total-label">Total:</td>
                <td class="orq-total-value">&#8369;&nbsp; ${formatMoney(v.totalAmount)}</td>
            </tr>
        </table>

        <table class="orq-cert-grid">
            <tr>
                <td>
                    <span class="orq-cert-letter">A</span>
                    <div class="orq-checkbox-row"><span class="orq-checkbox"></span> Charges to appropriation/ allotment necessary lawful and under my direct supervision.</div>
                    <div class="orq-checkbox-row"><span class="orq-checkbox"></span> Supporting Document valid, proper and legal</div>
                </td>
                <td>
                    <span class="orq-cert-letter">B</span>
                    Existence of available appropriation
                </td>
            </tr>
        </table>

        <table class="orq-sign-grid">
            <tr>
                <td class="lbl">Signature:</td><td class="val">&nbsp;</td>
                <td class="lbl">Signature:</td><td class="val">&nbsp;</td>
            </tr>
            <tr>
                <td class="lbl">Printed Name:</td><td class="val"><strong>${escapeHtml(v.endUser) || '&nbsp;'}</strong></td>
                <td class="lbl">Printed Name:</td><td class="val"><strong>${escapeHtml(v.cityBudget) || '&nbsp;'}</strong></td>
            </tr>
            <tr>
                <td class="lbl">Position:</td><td class="val">${escapeHtml(v.endUserPosition) || '&nbsp;'}</td>
                <td class="lbl">Position:</td><td class="val">${(escapeHtml(v.cityBudgetPosition) || '').replace(/\r?\n/g, '<br>') || '&nbsp;'}</td>
            </tr>
            <tr>
                <td class="lbl">Date:</td><td class="val">&nbsp;</td>
                <td class="lbl">Date:</td><td class="val">&nbsp;</td>
            </tr>
        </table>
      </div>
    </div>`;
}

let obBidPdfBlobUrl = null;
let obBidPdfFileName = 'obligation_request.pdf';

async function saveObligationBarcodeAndPrint(reportType, includeFooter = true) {
    const v = cbObligationValues();
    if (!v.supplierName) return showToast('Supplier Name is required.', 'warning');
    // Barcode Number saves through the existing quick-field mechanism —
    // same one PO_Number/Document Tracking No. already use — not a new
    // write path.
    try {
        await fetch(apiUrl, {
            method: 'PATCH',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id: v.bidId, quick_field: 'barcode_number', value: v.barcodeNumber }),
        });
    } catch (err) { console.error(err); }

    document.getElementById('obBidPrintSource').innerHTML = buildCbObligationPreviewHtml(v, reportType);
    bootstrap.Modal.getInstance(document.getElementById('cbObligationModal'))?.hide();
    const previewModalEl = document.getElementById('cbObligationPreviewModal');
    openModal('cbObligationPreviewModal');
    previewModalEl.addEventListener('shown.bs.modal', () => generateCbObligationPdf(includeFooter), { once: true });
}

async function generateCbObligationPdf(includeFooter = true) {
    document.getElementById('obBidPreviewTitle').textContent = includeFooter
        ? 'Obligation Request Preview' : 'Obligation Request Preview (No Footer)';
    const loading = document.getElementById('obBidPdfLoading');
    const frame = document.getElementById('obBidPdfFrame');
    const buttons = ['obBidPdfDownload', 'obBidPdfPrint', 'obBidPdfOpenTab'].map(id => document.getElementById(id));
    frame.classList.add('d-none');
    buttons.forEach(b => { if (b) b.disabled = true; });
    loading.classList.remove('d-none');
    try {
        const source = document.querySelector('#obBidPrintSource .print-page');
        if (!source) throw new Error('Print content did not render — try closing and reopening the preview.');
        const stamp = includeFooter ? PrintPdf.printedStamp() : null;
        const pdf = await PrintPdf.renderToPagedPdf(source, {
            paper: 'letter', singlePage: true, headerBandPt: 0,
            footerBandPt: includeFooter ? CB_FOOTER_BAND_PT : 0,
            restoreTo: document.getElementById('obBidPrintSource'),
            onPage: includeFooter
                ? (pdf, page, pageCount, geo) => drawPlainFurniture(pdf, page, pageCount, geo, stamp)
                : undefined,
        });
        if (obBidPdfBlobUrl) URL.revokeObjectURL(obBidPdfBlobUrl);
        obBidPdfBlobUrl = URL.createObjectURL(pdf.output('blob'));
        obBidPdfFileName = `Obligation_${document.getElementById('obligationBidNumber').value}${includeFooter ? '' : '_NoFooter'}.pdf`;
        frame.src = obBidPdfBlobUrl + CB_PDF_VIEW_PARAMS;
        loading.classList.add('d-none');
        frame.classList.remove('d-none');
        buttons.forEach(b => { if (b) b.disabled = false; });
    } catch (err) {
        loading.innerHTML = '<div class="text-danger small px-3"><i class="fas fa-triangle-exclamation me-1"></i>Unable to generate the PDF: '
            + escapeHtml(err.message || String(err)) + '</div>';
        console.error(err);
    }
}

function downloadCbObligationPdf() {
    if (!obBidPdfBlobUrl) return;
    const a = document.createElement('a');
    a.href = obBidPdfBlobUrl;
    a.download = obBidPdfFileName;
    a.click();
}

function printCbObligationPdf() {
    if (!obBidPdfBlobUrl) return;
    const frame = document.getElementById('obBidPdfFrame');
    frame.contentWindow?.print();
}

function setModalChrome(title, subtitle, statusText, statusClass) {
    document.getElementById('cbModalTitle').textContent = title;
    // Clamped to two lines in CSS. A consolidated bid's contract name runs to
    // several hundred characters and filled the whole header with it, pushing
    // the form down the screen; the full text stays available on hover.
    const sub = document.getElementById('cbModalSubtitle');
    sub.textContent = subtitle;
    sub.title = subtitle;
    const badge = document.getElementById('cbModalStatusBadge');
    badge.textContent = statusText;
    badge.className = 'badge-status ' + statusClass;
}

function openNewBid() {
    cbLoadToken++;      // abandon any edit still loading its rows
    cbLoad.end();
    clearForm();
    setFormReadOnly(false);
    // Nothing to save against until the document exists.
    cbQuickBidId = null;
    document.querySelectorAll('.cb-quick').forEach(el => { el.disabled = true; });
    document.getElementById('cbQuickStatus').textContent = 'Available once the document has been saved.';
    // Nothing to attach a processing step to until the bid document itself
    // exists. (Obligation Request is a row-level action, not here — it's
    // reached from the list's own Actions column, which a brand-new,
    // unsaved bid has no row for yet.)
    document.getElementById('cbProcessingButton').classList.add('d-none');
    document.getElementById('cbHistoryButton').classList.add('d-none');
    setModalChrome('Create Bid Document',
        'Fill in the details below to create a new Bid Document.', 'Draft', 'st-draft');
    openModal('cbModal');
    // One empty row to start, so the grid is not a dead end.
    addItemRow({}, null, false);
}

// The modal opens FIRST, empty and inert, with the loading bar over it; the
// fetch and the item rows then fill it in behind the bar. Opening it only after
// the data arrived meant nothing happened on screen for as long as the load
// took, which on a 500-item contract was long enough to look like a hang.
async function editBid(bidId, readOnly = false) {
    const token = ++cbLoadToken;     // a newer open supersedes any load still running
    clearForm();
    setFormReadOnly(true);           // inert until the data is in
    cbQuickBidId = null;
    document.querySelectorAll('.cb-quick').forEach(el => { el.disabled = true; });
    // Named from the last list load while the record itself is still coming.
    let bidNumber = bidLabel(cbBidsById[String(bidId)]) || '…';
    setModalChrome(readOnly ? 'Bid Document' : 'Edit Bid Document', 'Bid No. ' + bidNumber, 'Loading…', 'st-draft');
    cbLoad.start('Loading the document, please wait…');
    openModal('cbModal');
    try {
        const res  = await fetch(`${apiUrl}?id=${encodeURIComponent(bidId)}`);
        const data = await res.json();
        if (token !== cbLoadToken) return;
        if (!data.success) throw new Error(data.message || 'Unable to load the bid document.');
        bidNumber = bidLabel(data.bid);

        fillForm(data.bid);
        // A locked document opens read-only whatever was clicked: the row's
        // Edit button is already disabled, but a deep link or a stale list
        // would otherwise walk straight into a form that cannot be saved.
        const locked = !!data.bid.locked;
        const viewing = readOnly || locked;
        setModalChrome(viewing ? 'Bid Document' : 'Edit Bid Document',
            'Bid No. ' + bidNumber + (data.bid.contract_name ? ' — ' + data.bid.contract_name : '')
                + (locked ? '  •  Locked' : ''),
            data.bid.stage_label || 'Draft', 'st-' + (data.bid.stage || 'draft'));

        if (!await loadItemRows(data.bid.items || [], token)) return;

        // After the rows exist, so setFormReadOnly reaches their inputs too.
        setFormReadOnly(viewing);
        // Quick fields stay live even on a locked or view-only document.
        cbQuickBidId = data.bid.id;
        document.querySelectorAll('.cb-quick').forEach(el => { el.disabled = false; });
        // Processing Steps stays available on a locked/view-only document
        // too — recording what was actually processed is tracking, not an
        // edit to the bid document itself.
        document.getElementById('cbProcessingButton').classList.remove('d-none');
        document.getElementById('cbHistoryButton').classList.remove('d-none');
        cbLoad.end();
    } catch (err) {
        if (token !== cbLoadToken) return;
        // The modal stays open, empty and read-only, so Cancel closes it — hiding
        // it here could be ignored while Bootstrap is still animating it in.
        setModalChrome('Unable to load', 'Bid No. ' + bidNumber, 'Error', 'st-draft');
        cbLoad.end();
        showToast(err.message || 'Unable to load the bid document.', 'error');
        console.error(err);
    }
}

// Checked as the user types the Bid Number or the Contract Number, not only
// when they press Save. One Bid Number can carry several contracts, so the
// only duplicate is the same Bid Number AND Contract Number (cbExists() in
// api/competitive_bidding.php, which enforces it again at Save). A shared
// Bid Number on its own gets a quiet note, not a warning. While editing, the
// record's own ID is excluded, so a bid never collides with itself.
let cbBidNumberCheckTimer = null;
let cbBidNumberIsDuplicate = false;

function checkBidNumberDuplicate() {
    const dupHint   = document.getElementById('bidNumberDupHint');
    const shareHint = document.getElementById('bidNumberShareHint');
    clearTimeout(cbBidNumberCheckTimer);
    const bidNumber = document.getElementById('bid_number').value.trim();
    const contract  = document.getElementById('contract_number').value.trim();
    const exceptId  = document.getElementById('cbId').value;
    if (!bidNumber) {
        dupHint.classList.add('d-none');
        shareHint.classList.add('d-none');
        cbBidNumberIsDuplicate = false;
        return;
    }
    cbBidNumberCheckTimer = setTimeout(async () => {
        try {
            const query = new URLSearchParams({ check_bid_number: bidNumber, contract_number: contract, except_id: exceptId });
            const res = await fetch(`${apiUrl}?${query}`);
            const data = await res.json();
            // Still what is in the boxes? Typing fast can land an older
            // response after a newer one — re-read the fields rather than
            // trust the closure.
            if (document.getElementById('bid_number').value.trim() !== bidNumber
                || document.getElementById('contract_number').value.trim() !== contract) return;
            if (!data.success) return;
            cbBidNumberIsDuplicate = !!data.duplicate;
            dupHint.querySelector('span').textContent = contract
                ? `Bid Number ${bidNumber} with Contract Number ${contract} is already in use.`
                : `Bid Number ${bidNumber} is already in use. Enter a Contract Number to add another contract under it.`;
            dupHint.classList.toggle('d-none', !cbBidNumberIsDuplicate);

            const others = (data.siblings || []).length;
            shareHint.querySelector('span').textContent =
                `Bid Number shared with ${others} other contract${others === 1 ? '' : 's'}`
                + ` (${data.siblings.map(s => s.contract_number || 'no Contract No.').join(', ')}).`;
            shareHint.classList.toggle('d-none', cbBidNumberIsDuplicate || others === 0);
        } catch (err) {
            console.error(err);
        }
    }, 350);
}

// The open bid's History. By record ID, like every other action here — the
// API matches the log on Bid Number + Contract Number (lib/audit_history.php).
function openBidHistory() {
    const id = document.getElementById('cbId').value;
    if (!id) return showToast('Save the bid document first.', 'warning');
    const bid = cbBidsById[String(id)];
    RecordHistory.open({ title: 'Bid History',
        label: bid ? bidLabel(bid) : document.getElementById('bid_number').value.trim(),
        url: `${apiUrl}?id=${encodeURIComponent(id)}&history=1` });
}

// An older ?bid_number= link (notification, assistant, Competitive Insights).
// One document under that number opens directly; several are filtered into the
// list, because guessing which contract was meant would open the wrong one.
async function openBidNumberLink(bidNumber) {
    try {
        const res = await fetch(`${apiUrl}?bid_number=${encodeURIComponent(bidNumber)}`);
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Bid document not found.', 'error');
        if (data.bid) return editBid(data.bid.id);
        document.getElementById('searchText').value = bidNumber;
        loadBids();
        showToast(`Bid Number ${bidNumber} has ${data.matches.length} contracts. Pick one from the list.`, 'warning');
    } catch (err) {
        showToast('Unable to open that bid document.', 'error');
        console.error(err);
    }
}

async function saveBid() {
    const id = document.getElementById('cbId').value;
    const payload = { id: id || undefined, items: collectItems() };
    CB_FIELDS.forEach(f => { payload[f] = (document.getElementById(f)?.value ?? '').trim(); });

    if (!payload.bid_number)    return showToast('Bid Number is required.', 'warning');
    if (cbBidNumberIsDuplicate) return showToast(payload.contract_number
        ? `Bid Number ${payload.bid_number} with Contract Number ${payload.contract_number} already exists.`
        : `Bid Number ${payload.bid_number} already exists. Enter a Contract Number to add another contract under it.`, 'warning');
    if (!payload.contract_name) return showToast('Name of the Contract is required.', 'warning');
    if (payload.items.length === 0) return showToast('Add at least one item.', 'warning');

    const bad = invalidItemRows();
    if (bad.item.length) {
        const has = bad.item.length === 1 ? 'has' : 'have';
        return showToast(`${rowList(bad.item)} ${has} no Amount. Fill in QTY and Unit Cost, enter an Amount directly, or leave the row as a description-only line.`, 'warning');
    }
    if (bad.desc.length) {
        const phrase = bad.desc.length === 1
            ? 'is a description-only line but still carries'
            : 'are description-only lines but still carry';
        return showToast(`${rowList(bad.desc)} ${phrase} an item number, unit or cost.`, 'warning');
    }

    const button = document.getElementById('cbSaveButton');
    button.disabled = true;
    try {
        const res = await fetch(apiUrl, {
            method: id ? 'PUT' : 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload),
        });
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Unable to save.', 'error');
        showToast(data.message || 'Saved.');
        bootstrap.Modal.getInstance(document.getElementById('cbModal')).hide();
        loadBids();
    } catch (err) {
        showToast('Unable to save the bid document.', 'error');
        console.error(err);
    } finally {
        button.disabled = false;
    }
}

async function deleteBid(bidId) {
    if (!confirm(`Delete bid document ${bidLabel(cbBidsById[String(bidId)])}? This also removes its items.`)) return;
    try {
        const res  = await fetch(`${apiUrl}?id=${encodeURIComponent(bidId)}`, { method: 'DELETE' });
        const data = await res.json();
        showToast(data.message || (data.success ? 'Deleted.' : 'Unable to delete.'), data.success ? 'success' : 'error');
        if (data.success) loadBids();
    } catch (err) {
        showToast('Unable to delete the bid document.', 'error');
        console.error(err);
    }
}


// ── Signatories ─────────────────────────────────────────────────────────
// Off ref_signatory, the same master data the Purchase Order screen uses, so
// the Mayor is spelled one way across the whole application instead of being
// retyped onto every contract. Picking a name fills its title, which is what
// makes the pair impossible to mismatch.
let signatoryTitleByName = {};
// Obligation Request signatory defaults — same selection rule
// purchase_orders.php's own Obligation Request uses (matched on title, not
// pinned to a name, so replacing either officer is a Master Data edit, not
// a code change).
let cbDefaultSignatory = null;
let cbCityBudgetSignatory = null;
const CB_CITY_BUDGET_TITLE_DEFAULT = 'City Budget Officer';
const CB_CITY_BUDGET_AUTHORITY_LINE = 'Head, Budget Unit / Authorized Representative';

async function loadSignatories() {
    try {
        const res = await fetch('api/master_data.php');
        const data = await res.json();
        if (!data.success) return;
        const rows = (data.categories && data.categories.signatories) || [];
        signatoryTitleByName = {};
        rows.forEach(r => { signatoryTitleByName[r.name] = r.extra || ''; });
        document.getElementById('dlSignatories').innerHTML =
            rows.map(r => `<option value="${escapeHtml(r.name)}">`).join('');
        cbDefaultSignatory = rows[0] || null;
        cbCityBudgetSignatory = rows.find(s => /budget/i.test(s.extra || '')) || null;
    } catch (err) {
        // A missing picklist is not worth blocking the screen for — the fields
        // stay free text, which is what they were before.
        console.error(err);
    }
}

// Only fills a blank title, never overwrites one. The title on an executed
// contract is what that person held when they signed it, and master data
// changing later must not rewrite it.
function fillSignatoryTitle(nameFieldId, titleFieldId) {
    const title = signatoryTitleByName[document.getElementById(nameFieldId).value.trim()];
    const titleField = document.getElementById(titleFieldId);
    if (title && !titleField.value.trim()) titleField.value = title;
}

// ── Quick fields ────────────────────────────────────────────────────────
// Each saves itself on change. Deliberately not part of the form's Save: these
// get filled in over weeks as the paperwork moves, and a save of unrelated
// header edits must not reset one. They stay writable on a locked document —
// recording that it reached CGSO is tracking, not a change of substance.

// Saving needs the record's ID, and a document being created does not have one
// yet. Held from the last load rather than read off the form, so nothing typed
// into the form can make a quick save land on a different record.
let cbQuickBidId = null;

async function saveQuickField(input) {
    const field = input.dataset.quick;
    if (!field || !cbQuickBidId) return;
    const status = document.getElementById('cbQuickStatus');
    try {
        const res = await fetch(apiUrl, {
            method: 'PATCH',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id: cbQuickBidId, quick_field: field, value: input.value }),
        });
        const data = await res.json();
        if (!data.success) {
            status.textContent = data.message || 'Could not save that field.';
            status.className = 'po-field-hint mt-1 cb-pct-off';
            return;
        }
        status.textContent = 'Saved ' + new Date().toLocaleTimeString('en-PH');
        status.className = 'po-field-hint mt-1';
        // A tracking date or a remark can move the set to a new stage, so the
        // badge in this modal and the row behind it are both re-read. That call
        // reloads the list too, which shows the tracking number and the lock.
        refreshStageAfterQuickSave();
    } catch (err) {
        status.textContent = 'Could not save that field.';
        status.className = 'po-field-hint mt-1 cb-pct-off';
        console.error(err);
    }
}

// Common Remarks wordings, off ref_remarks. The field stays free text — a
// genuinely one-off remark must still be typeable — this only offers the
// common ones so the common ones stay spelled identically and can be counted.
async function loadRemarksList() {
    try {
        const res = await fetch('api/master_data.php');
        const data = await res.json();
        if (!data.success) return;
        const rows = (data.categories && data.categories.remarks) || [];
        document.getElementById('dlRemarks').innerHTML =
            rows.map(r => `<option value="${escapeHtml(r.name)}">`).join('');
    } catch (err) {
        console.error(err);
    }
}

// Live PO-number suggestions for the Linked Purchase Order field, searched
// on demand rather than preloaded — api/purchase_orders.php's list endpoint
// has no row cap and this app carries 26,000+ purchase orders, so eagerly
// fetching "all of them" into a datalist would be exactly the kind of
// unbounded query this app's own conventions avoid elsewhere. Debounced,
// and only fires once there is enough of a term to actually narrow the
// search — matches api/purchase_orders.php's own `?search=` filter, the
// same one the main Purchase Order list box uses.
// The field stays free text regardless: nothing here validates the PO
// actually exists before saving — api/cb_processing.php's overlay and the
// Inspection/Inventory bridge both simply show nothing if it doesn't match.
let cbPoSearchTimer = null;
function searchCbPoNumbers(term) {
    clearTimeout(cbPoSearchTimer);
    term = term.trim();
    if (term.length < 3) return;
    cbPoSearchTimer = setTimeout(async () => {
        try {
            const res = await fetch(`api/purchase_orders.php?search=${encodeURIComponent(term)}`);
            const data = await res.json();
            const rows = (data.success && Array.isArray(data.orders)) ? data.orders.slice(0, 20) : [];
            document.getElementById('dlCbPoNumbers').innerHTML =
                rows.map(r => `<option value="${escapeHtml(r.po_number)}">`).join('');
        } catch (err) {
            console.error(err);
        }
    }, 300);
}

// ── View / lock / tracking ──────────────────────────────────────────────



// View reuses the edit modal with every control switched off, rather than
// being a second rendering of the same 31 fields that could drift out of step
// with it. Reverted by openNewBid()/editBid(), which both call setFormReadOnly
// (false) on the way in.
function setFormReadOnly(readOnly) {
    document.querySelectorAll('#cbForm input, #cbForm textarea, #cbForm select')
        .forEach(el => { el.disabled = readOnly; });
    document.querySelectorAll('#cbItemsTable button, #cbForm .item-grid-action-btn')
        .forEach(el => { el.disabled = readOnly; });
    document.getElementById('cbSaveButton').classList.toggle('d-none', readOnly);
    document.querySelectorAll('.cb-edit-only').forEach(el => el.classList.toggle('d-none', readOnly));
    // The import guide is toggled open by one of the .cb-edit-only buttons and
    // stays open between modal opens, so a view following an edit would show
    // instructions for filling rows it cannot fill.
    if (readOnly) document.getElementById('cbItemGuide').classList.add('d-none');
    // Drops the Action column entirely while viewing — its three buttons are
    // already disabled, and a column of dead controls is worse than no column.
    // The width goes back to Description.
    document.getElementById('cbModal').classList.toggle('cb-viewing', readOnly);
}

async function viewBid(bidId) {
    await editBid(bidId, true);
}

async function toggleBidLock(bidId, lock) {
    const verb = lock ? 'Lock' : 'Unlock';
    if (!confirm(`${verb} bid document ${bidLabel(cbBidsById[String(bidId)])}?\n\n` + (lock
        ? 'It can no longer be edited or deleted until it is unlocked.'
        : 'It becomes editable again.'))) return;
    try {
        const res = await fetch(apiUrl, {
            method: 'PATCH',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id: bidId, lock_action: lock ? 'lock' : 'unlock' }),
        });
        const data = await res.json();
        showToast(data.message || (data.success ? 'Done.' : 'Unable to change the lock.'),
            data.success ? 'success' : 'error');
        if (data.success) loadBids();
    } catch (err) {
        showToast('Unable to change the lock.', 'error');
        console.error(err);
    }
}

// The DTS number is transcribed off a printed routing slip, so it is prompted
// for rather than being a field on the main form — it is usually written on
// after the set has been issued and locked, when the form is closed to edits.
async function trackBid(bidId) {
    try {
        const res = await fetch(`${apiUrl}?id=${encodeURIComponent(bidId)}`);
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Unable to load the bid document.', 'error');
        const current = (data.bid.barcode_number || '').trim();
        if (current) return dtsTrackBarcode(current);

        const entered = prompt(
            `Bid document ${bidLabel(data.bid)} has no Document Tracking number yet.\n\n` +
            'Enter the DTS number printed on its routing slip:', '');
        if (entered === null) return;
        const barcode = entered.trim();
        if (!barcode) return;

        const save = await fetch(apiUrl, {
            method: 'PATCH',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ id: bidId, quick_field: 'barcode_number', value: barcode }),
        });
        const saved = await save.json();
        if (!saved.success) return showToast(saved.message || 'Unable to save the tracking number.', 'error');
        loadBids();
        dtsTrackBarcode(barcode);
    } catch (err) {
        showToast('Unable to open the tracking trail.', 'error');
        console.error(err);
    }
}

// ── Contract Agreement printout ─────────────────────────────────────────

// A recreation of the BAC's printed contract, from the scanned original.

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

      <p class="cl-p">Dear ${escapeHtml(bid.representative_salutation || 'Sir/Madam')}:</p>

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

      <p class="cl-p">Dear ${escapeHtml(bid.representative_salutation || 'Sir/Madam')}:</p>

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
// ── Preview / PDF ───────────────────────────────────────────────────────
let cbPdfBlobUrl = null;
let cbPdfFileName = 'contract.pdf';

// Asks the viewer to open with its sidebar collapsed. A hint, not a guarantee.
const CB_PDF_VIEW_PARAMS = '#navpanes=0&toolbar=1&view=FitH';

// The bid currently in the preview, held so switching document does not refetch.
let cbPreviewBid = null;
let cbPreviewDoc = 'contract';

async function openBidPrint(bidId, docType) {
    cbBusy.start('Loading the document, please wait…');
    try {
        const res = await fetch(`${apiUrl}?id=${encodeURIComponent(bidId)}`);
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Unable to load the bid document.', 'error');
        cbClearPdfCache();      // the bid may have changed since the last preview
        cbPreviewBid = data.bid;
        cbPreviewDoc = CB_DOCUMENTS[docType] ? docType : 'contract';
        renderDocTabs();
        document.getElementById('cbPrintSubtitle').textContent =
            'Bid No. ' + bidLabel(data.bid) + (data.bid.contract_name ? ' — ' + data.bid.contract_name : '');
        const modalEl = document.getElementById('cbPrintModal');
        openModal('cbPrintModal');
        // Wait for the fade-in to finish before rasterising: Bootstrap animates
        // .modal-dialog with a transform while showing, and html2canvas reading
        // layout mid-transition can capture an inconsistent box.
        modalEl.addEventListener('shown.bs.modal', renderCurrentDoc, { once: true });
    } catch (err) {
        showToast('Unable to load the bid document.', 'error');
        console.error(err);
    } finally {
        cbBusy.end();
    }
}

// One switcher rather than three buttons on every list row: all three documents
// describe the same record, and whoever issues them works through the set.
function renderDocTabs() {
    document.getElementById('cbDocTabs').innerHTML = Object.keys(CB_DOCUMENTS).map(key =>
        `<button type="button" class="btn btn-sm rounded-pill ${key === cbPreviewDoc ? 'btn-primary-app' : 'btn-outline-secondary'}"
                 onclick="switchDoc('${key}')">${escapeHtml(CB_DOCUMENTS[key].label)}</button>`).join('');
}

function switchDoc(key) {
    if (!CB_DOCUMENTS[key] || key === cbPreviewDoc) return;
    // Generation moves the print source around the page while it captures it;
    // starting a second document on top of that would corrupt both.
    if (cbPdfBusy) return showToast('Still generating the document — please wait a moment.', 'warning');
    cbPreviewDoc = key;
    renderDocTabs();
    renderCurrentDoc();
}

// PDFs already produced for the bid in the preview, by document. Coming back to
// a tab is instant instead of paying for a whole regeneration. Emptied every
// time a bid is opened, because it may have been edited since.
let cbPdfCache = {};
let cbPdfBusy = false;

function cbClearPdfCache() {
    Object.values(cbPdfCache).forEach(entry => URL.revokeObjectURL(entry.url));
    cbPdfCache = {};
    cbPdfBlobUrl = null;
}

function cbShowPdf(url, fileName) {
    const buttons = ['cbPdfDownload', 'cbPdfPrint', 'cbPdfOpenTab'].map(id => document.getElementById(id));
    cbPdfBlobUrl = url;
    cbPdfFileName = fileName;
    const frame = document.getElementById('cbPdfFrame');
    frame.src = url + CB_PDF_VIEW_PARAMS;
    document.getElementById('cbPdfLoading').classList.add('d-none');
    frame.classList.remove('d-none');
    buttons.forEach(b => { if (b) b.disabled = false; });
}

function renderCurrentDoc() {
    if (!cbPreviewBid) return;
    const doc = CB_DOCUMENTS[cbPreviewDoc];
    const cached = cbPdfCache[cbPreviewDoc];
    if (cached) return cbShowPdf(cached.url, cached.name);
    document.getElementById('cbPrintSource').innerHTML = doc.build(cbPreviewBid);
    generateBidPdf(cbPreviewBid, doc, cbPreviewDoc);
}

// Space reserved at the foot of every sheet for the date/page line. The top
// band varies by document — only the contract prints a running header — so it
// lives on each entry in CB_DOCUMENTS instead.
const CB_FOOTER_BAND_PT = 20;

async function generateBidPdf(bid, doc, docKey) {
    const loading = document.getElementById('cbPdfLoading');
    const frame = document.getElementById('cbPdfFrame');
    const buttons = ['cbPdfDownload', 'cbPdfPrint', 'cbPdfOpenTab'].map(id => document.getElementById(id));

    cbPdfBusy = true;
    frame.classList.add('d-none');
    buttons.forEach(b => { if (b) b.disabled = true; });
    loading.classList.remove('d-none');
    // Sliding bar until the page count is known, then a real percentage — only
    // the paged (multi-page) renderer reports progress; one-page letters are
    // over in a moment.
    loading.innerHTML = `<div class="cb-pdf-progress indeterminate">
        <div class="cb-busy-text">Generating the document, please wait…</div>
        <div class="cb-bar text-primary"><span></span></div>
        <div class="cb-busy-pct"></div></div>`;
    const bar = cbLoader(loading.querySelector('.cb-pdf-progress'));

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
            // Awaited by the renderer, so the bar is repainted between captures
            // instead of the page freezing on its last value.
            onProgress: async (done, total) => {
                bar.progress(done / total, done + ' of ' + total + ' pages');
                await cbNextPaint();
            },
        });

        const url = URL.createObjectURL(pdf.output('blob'));
        // The contract number too, so two contracts under one bid do not
        // download over each other.
        const name = `${doc.file}_${bid.bid_number}${bid.contract_number ? '_' + bid.contract_number : ''}.pdf`;
        cbPdfCache[docKey] = { url, name };
        cbShowPdf(url, name);
    } catch (err) {
        // Surfaced directly rather than as a generic message, so a failure is
        // diagnosable instead of a dead end.
        loading.innerHTML = '<div class="text-danger small px-3"><i class="fas fa-triangle-exclamation me-1"></i>Unable to generate the PDF: '
            + escapeHtml(err.message || String(err)) + '</div>';
        console.error(err);
    } finally {
        cbPdfBusy = false;
    }
}

// The running bid/page headings and footer sit outside the frame. The four
// initialling spaces run down its inner left margin, as in the PDF guide.
//
// Drawn here rather than in the HTML because the form is one tall raster sliced
// into pages — anything inside it can only ever appear once.
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

function downloadContractPdf() {
    if (!cbPdfBlobUrl) return;
    const a = document.createElement('a');
    a.href = cbPdfBlobUrl;
    a.download = cbPdfFileName;
    document.body.appendChild(a);
    a.click();
    a.remove();
}

function printContractPdf() {
    const frame = document.getElementById('cbPdfFrame');
    try {
        frame.contentWindow.focus();
        frame.contentWindow.print();
    } catch (err) {
        // Some built-in PDF viewers don't expose a scriptable print() — fall
        // back to a new tab, where the viewer's own toolbar works normally.
        window.open(cbPdfBlobUrl, '_blank');
    }
}
window.addEventListener('DOMContentLoaded', () => {
    initTable();
    initDateFilters();
    // Competitive Insights links through as competitive_bidding.php?search=…
    // (a supplier name, a contract number). Applied to the filter box rather
    // than to the request alone, so the visitor can see what is being filtered
    // on and clear it.
    const preset = new URLSearchParams(window.location.search).get('search');
    if (preset) document.getElementById('searchText').value = preset;
    loadBids();
    document.getElementById('btnAddBid').addEventListener('click', openNewBid);
    ['contract_amount', 'cash_bond_amount', 'surety_bond_amount'].forEach(id =>
        document.getElementById(id).addEventListener('input', refreshBondHints));
    document.getElementById('contract_amount').addEventListener('input', computeBonds);
    ['pr_amount', 'contract_amount'].forEach(id =>
        document.getElementById(id).addEventListener('input', refreshSavingsBand));
    loadSignatories();
    loadRemarksList();
    document.querySelectorAll('.cb-quick').forEach(el =>
        el.addEventListener('change', () => saveQuickField(el)));
    document.getElementById('cbModal').addEventListener('shown.bs.modal', regrowModalFields);
    document.getElementById('cbPdfDownload').addEventListener('click', downloadContractPdf);
    document.getElementById('cbPdfPrint').addEventListener('click', printContractPdf);
    document.getElementById('cbPdfOpenTab').addEventListener('click', () => { if (cbPdfBlobUrl) window.open(cbPdfBlobUrl, '_blank'); });
    // Progressive Item Processing
    document.getElementById('cbpBtnNew').addEventListener('click', () => cbpOpenEditor());
    document.getElementById('cbpBtnBack').addEventListener('click', () => { cbpShowHistory(); cbpRenderHistory(); });
    document.getElementById('cbpBtnSave').addEventListener('click', cbpSave);
    // Obligation Request
    document.getElementById('obBidReimbursementButton').addEventListener('click', () => saveObligationBarcodeAndPrint('reimbursement', true));
    document.getElementById('obBidReimbursementNoFooterButton').addEventListener('click', () => saveObligationBarcodeAndPrint('reimbursement', false));
    document.getElementById('obBidPaymentButton').addEventListener('click', () => saveObligationBarcodeAndPrint('payment', true));
    document.getElementById('obBidPaymentNoFooterButton').addEventListener('click', () => saveObligationBarcodeAndPrint('payment', false));
    document.getElementById('obBidPdfDownload').addEventListener('click', downloadCbObligationPdf);
    document.getElementById('obBidPdfPrint').addEventListener('click', printCbObligationPdf);
    document.getElementById('obBidPdfOpenTab').addEventListener('click', () => { if (obBidPdfBlobUrl) window.open(obBidPdfBlobUrl, '_blank'); });
    // The description column is a percentage of a table-layout:fixed grid, so
    // resizing the window changes how its text wraps and therefore how tall it
    // needs to be. The textarea is overflow:hidden, so without re-measuring, a
    // narrower window would quietly clip the tail of a specification block with
    // no scrollbar to hint that anything was missing. Debounced, because resize
    // fires continuously while a window is being dragged.
    let regrowTimer = null;
    window.addEventListener('resize', () => {
        clearTimeout(regrowTimer);
        regrowTimer = setTimeout(regrowModalFields, 150);
    });
    document.getElementById('itemImportInput').addEventListener('change', (e) => {
        const file = e.target.files[0];
        if (file) importItemsIntoGrid(file);
        // Cleared so re-picking the same file fires 'change' again.
        e.target.value = '';
    });
    document.querySelectorAll('.cb-autogrow').forEach(el => {
        el.addEventListener('input', () => autoGrowField(el));
        autoGrowField(el);
    });
    // Notification links arrive as competitive_bidding.php?id=… — open that
    // document straight away rather than leaving the visitor to find it.
    // Older links carry ?bid_number=… instead; that opens the document when
    // the number names only one, and otherwise lists every contract under it.
    const params = new URLSearchParams(window.location.search);
    if (params.get('id')) editBid(params.get('id'));
    else if (params.get('bid_number')) openBidNumberLink(params.get('bid_number'));
});
</script>
<?php include 'assistant.php'; ?>
</body>
</html>
