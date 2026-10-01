<?php
require_once 'config.php';
requireAuth();
requireModule('inventory');
$currentUser = getCurrentUser();
$activePage  = 'inventory';
?>
<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Inventory &amp; Property | <?= e(APP_NAME) ?></title>
<link rel="icon" type="image/png" href="image/cgm-32.png">
<link href="vendor/css/bootstrap.min.css" rel="stylesheet">
<link rel="stylesheet" href="vendor/css/all.min.css">
<link rel="stylesheet" href="vendor/css/dataTables.bootstrap5.min.css">
<link rel="stylesheet" href="<?= asset('css/base.css') ?>">
<link rel="stylesheet" href="<?= asset('css/inventory-print.css') ?>">
<style>
/* Same card/field/chip vocabulary as Inspection & Acceptance and Purchase
   Orders, copied rather than shared, matching how those two pages already
   do it — each screen in this app is self-contained. Only what this page
   adds beyond that vocabulary is defined past the shared block below. */
.table-actions { display: flex; gap: .35rem; flex-wrap: wrap; }
.po-card { background: #fff; border: 1px solid #e2e8f0; border-radius: 14px; padding: 1.25rem 1.4rem; margin-bottom: 1.1rem; }
.po-card-compact { padding: .95rem 1.15rem; }
.po-card-title { display: flex; align-items: center; gap: .55rem; font-weight: 700; color: var(--navy); font-size: 1rem; margin-bottom: 1.1rem; }
.po-card-compact .po-card-title { margin-bottom: .65rem; font-size: .92rem; }
.po-card-title i {
    width: 26px; height: 26px; border-radius: 8px; flex: 0 0 auto;
    display: inline-flex; align-items: center; justify-content: center;
    font-size: .8rem; background: #e8f0ff; color: var(--primary);
}
.po-card.sec-order   .po-card-title i { background: #e8f0ff; color: #1d4ed8; }
.po-card.sec-receipt .po-card-title i { background: #fff2e6; color: #c2410c; }
.po-card.sec-people  .po-card-title i { background: #eef7ee; color: #15803d; }
.po-card.sec-order   { border-top: 3px solid #bfdbfe; }
.po-card.sec-receipt { border-top: 3px solid #fed7aa; }
.po-card.sec-people  { border-top: 3px solid #bbf7d0; }

/* The per-row Duplicate button is an edit action: gone while a record is only
   being viewed (applyInvMode() sets .inv-readonly). */
.inv-readonly .inv-dup-btn { display: none; }
/* Description while viewing: the formatted copy (invDescViewHtml()) replaces
   the textarea, which cannot show a bold first line over normal spec lines.
   Editing (Revise / new intake) shows the textarea again. */
.inv-desc-view { display: none; white-space: normal; line-height: 1.35; font-size: .82rem; color: #0f172a; padding: .2rem 0; }
.inv-readonly .inv-desc-view { display: block; }
/* Scoped through .insp-items-table on purpose: the grid's own
   ".insp-items-table textarea.item-desc { display: block }" further down is
   exactly as specific as ".inv-readonly textarea.item-desc" and, coming later,
   would win — which showed every description twice. */
.inv-readonly .insp-items-table textarea.item-desc { display: none; }
.inv-desc-view .dv-main.b { font-weight: 700; }
.inv-desc-view .dv-spec { color: #334155; margin-top: .1rem; }
.inv-row-actions { display: flex; gap: .25rem; }
.inv-row-actions .btn { padding: .15rem .4rem; }
/* Duplicate is green, like the Add Item Row button beside it; Unit breakdown is
   blue. Opaque tints rather than see-through ones, so they read the same on the
   white and the striped rows. */
.inv-row-actions .inv-dup-btn { color: #14764b; background: #f0f9f4; border-color: #86c9a7; }
.inv-row-actions .inv-dup-btn:hover { color: #fff; background: #14764b; border-color: #14764b; }
.inv-row-actions .inv-sub-btn { color: #1d4ed8; background: #eff6ff; border-color: #93c5fd; }
.inv-row-actions .inv-sub-btn:hover { color: #fff; background: #1d4ed8; border-color: #1d4ed8; }

/* The action buttons along the foot of a saved record. Each has its own colour so
   they can be told apart at a glance — the same light tint that fills solid on
   hover, as the row buttons above do. Keyed on the ids (which outrank Bootstrap's
   .btn-outline-* rules, hover and pressed states included). */
#inventoryModal #btnInvIssue,   #inventoryModal #btnInvTransfer, #inventoryModal #btnInvReturn,
#inventoryModal #btnInvHistory, #inventoryModal #btnInvSubDetails, #inventoryModal #btnInvPrint { font-weight: 600; }
#inventoryModal #btnInvIssue      { color: #14764b; background: #f0f9f4; border-color: #86c9a7; }   /* green  — issues the PAR/ICS numbers */
#inventoryModal #btnInvTransfer   { color: #6d28d9; background: #f5f3ff; border-color: #c4b5fd; }   /* purple — moves it to another person */
#inventoryModal #btnInvReturn     { color: #b45309; background: #fffbeb; border-color: #fcd34d; }   /* amber  — back to stock */
#inventoryModal #btnInvHistory    { color: #0e7490; background: #ecfeff; border-color: #67e8f9; }   /* cyan   — the transfer log */
#inventoryModal #btnInvSubDetails { color: #1d4ed8; background: #eff6ff; border-color: #93c5fd; }   /* blue   — unit breakdown */
#inventoryModal #btnInvPrint      { color: #4338ca; background: #eef2ff; border-color: #a5b4fc; }   /* indigo — print card / tag */
#inventoryModal #btnInvIssue:hover,      #inventoryModal #btnInvIssue:active      { color: #fff; background: #14764b; border-color: #14764b; }
#inventoryModal #btnInvTransfer:hover,   #inventoryModal #btnInvTransfer:active   { color: #fff; background: #6d28d9; border-color: #6d28d9; }
#inventoryModal #btnInvReturn:hover,     #inventoryModal #btnInvReturn:active     { color: #fff; background: #b45309; border-color: #b45309; }
#inventoryModal #btnInvHistory:hover,    #inventoryModal #btnInvHistory:active    { color: #fff; background: #0e7490; border-color: #0e7490; }
#inventoryModal #btnInvSubDetails:hover, #inventoryModal #btnInvSubDetails:active { color: #fff; background: #1d4ed8; border-color: #1d4ed8; }
#inventoryModal #btnInvPrint:hover,      #inventoryModal #btnInvPrint:active      { color: #fff; background: #4338ca; border-color: #4338ca; }

/* Shared green actions for manual entry and new item rows. */
.btn.inv-add-button {
    display: inline-flex; align-items: center; justify-content: center; gap: 9px;
    min-height: 40px; padding: 5px 14px 5px 6px; border-radius: 11px;
    border: 1px solid #86c9a7; background: #fff;
    color: #14764b; font-size: .8rem; font-weight: 600; line-height: 1.25;
    white-space: nowrap; vertical-align: middle;
    box-shadow: 0 2px 4px rgba(20,118,75,.08);
    transition: transform .16s ease, box-shadow .16s ease, border-color .16s ease;
}
.inv-add-button .inv-add-icon {
    display: inline-flex; align-items: center; justify-content: center; flex: 0 0 28px;
    width: 28px; height: 28px; border-radius: 8px;
    background: #f0f9f4; border: 1px solid #d5eddf;
}
.inv-add-button .inv-add-icon i,
.po-card .inv-add-button .inv-add-icon i {
    width: auto; height: auto; margin: 0; padding: 0;
    background: transparent; color: inherit; font-size: .78rem;
}
.btn.inv-add-button:hover {
    color: #10633e; background: #fff; border-color: #14764b;
    transform: translateY(-1px); box-shadow: 0 4px 10px rgba(20,118,75,.12);
}
.btn.inv-add-button:focus-visible {
    color: #14764b; background: #fff; outline: 3px solid #86d6b0; outline-offset: 3px;
}
.btn.inv-add-button:active {
    color: #10633e; background: #fff; border-color: #10633e;
    transform: translateY(0); box-shadow: inset 0 2px 4px rgba(20,118,75,.08);
}
.btn.inv-add-button:disabled {
    color: #14764b; background: #fff; opacity: .55; transform: none; box-shadow: none;
}
@media (prefers-reduced-motion: reduce) {
    .btn.inv-add-button { transition: none; }
    .btn.inv-add-button:hover { transform: none; }
}

.po-field-group { margin-bottom: 1rem; }
.tight-field-row .po-field-group { margin-bottom: .6rem; }
.po-field-group > label { font-size: .8rem; font-weight: 600; color: var(--muted); margin-bottom: .3rem; display: block; }
.po-input-wrap { position: relative; }
.po-input-wrap > .po-input-icon {
    position: absolute; left: 5px; top: 50%; transform: translateY(-50%);
    width: 28px; height: 28px; border-radius: 8px; background: rgba(37,99,235,.1); color: var(--primary);
    display: flex; align-items: center; justify-content: center; font-size: .78rem; pointer-events: none;
}
.po-input-wrap > .form-control, .po-input-wrap > .form-select {
    padding: .4rem .6rem .4rem 40px; min-height: 38px; font-size: .87rem;
}
.po-card-compact .po-input-wrap > .form-control,
.po-card-compact .po-input-wrap > .form-select { padding: .3rem .5rem .3rem 32px; min-height: 32px; font-size: .8rem; }
.po-card-compact .po-input-wrap > .po-input-icon { width: 22px; height: 22px; font-size: .68rem; }
.po-field-hint { font-size: .72rem; color: var(--muted); margin-top: .25rem; line-height: 1.35; }
.po-field-hint:empty { display: none; }
.po-field-hint-warn { color: #92400e; }

.po-chip {
    display: inline-flex; align-items: center; gap: .4rem;
    padding: .3rem .7rem; border-radius: 20px;
    font-size: .75rem; font-weight: 600; white-space: nowrap;
    border: 1px solid var(--border); background: #fff; color: var(--muted);
    cursor: pointer; transition: background .15s, border-color .15s, color .15s;
}
.po-chip:hover { background: #f8fafc; }
.po-chip-num { font-weight: 700; color: var(--text); font-variant-numeric: tabular-nums; }
.po-chip.active { background: #eff6ff; border-color: var(--primary); color: var(--primary); }
.po-chip.active .po-chip-num { color: var(--primary); }

/* Keep the Inventory Records controls compact while letting the status chips
   and actions wrap as separate, readable groups on narrower screens. */
.inv-inventory-header { gap: .65rem 1rem; }
.inv-toolbar {
    display: flex; flex: 1 1 900px; min-width: 0; align-items: center;
    justify-content: flex-end; flex-wrap: wrap; gap: .4rem .75rem;
}
.inv-toolbar-group { display: flex; align-items: center; flex-wrap: wrap; gap: .35rem; }
.inv-toolbar .po-chip { padding: .26rem .58rem; font-size: .72rem; }
.inv-toolbar-actions .btn { white-space: nowrap; }
.inv-toolbar .inv-add-button {
    min-height: 34px; padding: 3px 10px 3px 5px; gap: 6px; border-radius: 9px;
}
.inv-toolbar .inv-add-button .inv-add-icon { flex-basis: 24px; width: 24px; height: 24px; }
@media (max-width: 1400px) {
    .inv-inventory-header { align-items: flex-start !important; }
    .inv-toolbar { flex-basis: 100%; justify-content: flex-start; }
}
@media (max-width: 640px) {
    .inv-toolbar-status, .inv-toolbar-actions { width: 100%; }
    .inv-toolbar-actions { padding-top: .35rem; border-top: 1px solid var(--border); }
}

/* Same sizing/colors as the PO/PR pills in purchase_orders.php's own list —
   a PO number carried over into Inventory should read as the identical
   identifier, not a smaller lookalike. */
.num-pill {
    display: inline-block; padding: .18rem .55rem; border-radius: 6px;
    font-size: .8rem; font-weight: 700; letter-spacing: .01em;
    border: 1px solid transparent; white-space: nowrap;
}
.po-num-pill { color: #c2410c; background: #fff2e6; border-color: #fed7aa; }
.pr-num-pill { color: #1d4ed8; background: #e8f0ff; border-color: #bfdbfe; }
/* A PO can cite several PRs — one pill each, same as purchase_orders.php. */
.pr-num-group { display: inline-flex; flex-wrap: wrap; gap: .25rem; }

.status-badge { display: inline-flex; align-items: center; gap: .3rem; font-size: .72rem; font-weight: 700; padding: .18rem .55rem; border-radius: 20px; }
.status-issued   { background: #dcfce7; color: #15803d; }
.status-awaiting  { background: #fef3c7; color: #b45309; }
/* Legacy property entered directly, with no purchase order behind it — see
   btnAddManually in the script below. Purple rather than the amber/green
   Awaiting/Issued pair: this is provenance, not a workflow state, and must
   not read as a third status option in that same traffic-light language. */
.status-manual   { background: #ede9fe; color: #6d28d9; }
.status-partial   { background: #e0e7ff; color: #4338ca; }

.readonly-banner {
    display: flex; align-items: center; gap: .6rem; padding: .6rem .9rem; border-radius: 10px;
    background: #fffbeb; border: 1px solid #fde68a; color: #92400e; font-size: .82rem; margin-bottom: 1rem;
}
.po-summary-strip { display: grid; grid-template-columns: repeat(auto-fit, minmax(11rem, 1fr)); gap: .8rem; }
.po-summary-strip .cell {
    background: linear-gradient(180deg, #f8fbff, #f1f5f9);
    border: 1px solid #dbe3ee; border-left: 3px solid #93b4f5;
    border-radius: 10px; padding: .55rem .75rem;
}
.po-summary-strip .cell .v .num-pill { font-size: .8rem; }
.po-summary-strip .cell .k { font-size: .68rem; text-transform: uppercase; letter-spacing: .05em; color: var(--muted); font-weight: 700; }
.po-summary-strip .cell .v { font-size: .87rem; font-weight: 600; color: var(--text); word-break: break-word; }

.insp-modal-header {
    background: linear-gradient(135deg, var(--navy), #1e40af);
    color: #fff; border-bottom: none; padding: 1.15rem 1.4rem;
    display: flex; align-items: center; gap: 1rem;
}
.insp-header-icon {
    width: 46px; height: 46px; border-radius: 14px; flex: 0 0 auto;
    background: rgba(255,255,255,.18);
    display: flex; align-items: center; justify-content: center; font-size: 1.25rem; color: #fff;
}
.insp-header-text { flex: 1 1 auto; min-width: 0; }
.insp-header-text .modal-title { color: #fff; font-weight: 700; font-size: 1.15rem; margin: 0; }
.insp-header-text small { display: block; color: rgba(255,255,255,.72); font-size: .8rem; margin-top: .1rem; }
.insp-header-close {
    width: 38px; height: 38px; flex: 0 0 auto; border-radius: 12px;
    border: 1px solid rgba(255,255,255,.3); background: rgba(255,255,255,.12); color: #fff;
    display: flex; align-items: center; justify-content: center; transition: background .15s;
}
.insp-header-close:hover { background: rgba(255,255,255,.25); color: #fff; }
.modal-insp-wide { max-width: min(1600px, 96vw); }

/* ── Item grid ──────────────────────────────────────────────────────────── */
.insp-items-table { width: 100%; border-collapse: collapse; font-size: .82rem; }
.insp-items-table th {
    background: linear-gradient(180deg, #eaf1fd, #dfe9fa);
    font-size: .68rem; text-transform: uppercase; letter-spacing: .03em;
    color: var(--navy); font-weight: 700;
    padding: .45rem .5rem; text-align: left; white-space: nowrap;
    border-bottom: 1px solid #c7d8f5; position: sticky; top: 0; z-index: 1;
}
.insp-items-table tbody tr:nth-child(even) { background: #fafcff; }
.insp-items-table tbody tr:hover { background: #eef4fe; }
.insp-items-table td { padding: .22rem .32rem; border-bottom: 1px solid #eef2f7; vertical-align: top; }
.insp-items-table input, .insp-items-table select {
    width: 100%; border: 1px solid transparent; background: transparent;
    padding: .28rem .35rem; border-radius: 6px; font-size: .8rem;
}
/* Every free-text column in this grid grows the same way Description does —
   .item-desc keeps its own name (the title-row bold/navy override below
   still targets it specifically), .inv-grow is the shared class for the rest
   (Unit, Tag No., Serial No., Accountable Officer, Position, Location,
   Classification, Condition, Remarks). */
.insp-items-table textarea.item-desc,
.insp-items-table textarea.inv-grow {
    width: 100%; border: 1px solid transparent; background: transparent;
    padding: .28rem .35rem; border-radius: 6px; font-size: .8rem;
    resize: none; overflow: hidden; line-height: 1.35; display: block; min-height: 26px; font-family: inherit;
}
.insp-items-table input:focus, .insp-items-table select:focus,
.insp-items-table textarea.item-desc:focus,
.insp-items-table textarea.inv-grow:focus { border-color: var(--primary); background: #fff; outline: none; }
.insp-items-table input[readonly] { color: var(--muted); }
.insp-items-table td.num input { text-align: right; font-variant-numeric: tabular-nums; }
/* Unit / Qty / Unit Cost / Total each get a floor they cannot be squeezed under.
   The grid has ~20 columns in a 100%-wide auto-layout table, and a width on a
   <th> is only a hint there: the browser shrank these four to make room, until a
   Unit of "ew qe ew q" wrapped one word per line and a Total came out as "74,33".
   A min-width holds, and the scroll box around the table takes the overflow. */
.insp-items-table .inv-unit-col  { width: 110px; min-width: 110px; }
.insp-items-table .inv-qty-col   { width: 116px; min-width: 116px; }
.insp-items-table .inv-cost-col  { width: 124px; min-width: 124px; }
.insp-items-table .inv-total-col { width: 136px; min-width: 136px; }
.insp-items-table input.inv-qty-input {
    min-width: 104px; min-height: 36px; padding: .45rem .65rem;
    border: 1px solid #cbd5e1; border-radius: 8px; background: #fff;
    font-size: .87rem; -moz-appearance: textfield; appearance: textfield;
}
.insp-items-table input.inv-qty-input:focus {
    border-color: var(--primary); box-shadow: 0 0 0 3px rgba(37,99,235,.1);
}
.insp-items-table input.inv-qty-input::-webkit-inner-spin-button,
.insp-items-table input.inv-qty-input::-webkit-outer-spin-button { -webkit-appearance: none; margin: 0; }
.insp-items-scroll { overflow: auto; min-height: 0; border: 1px solid #cddcf7; border-radius: 10px; }

/* Shared floating suggestion box for the grid's textarea columns that used to
   carry a native <datalist> (Accountable Officer, Position, Location,
   Classification, Condition) — a <textarea> can't drive a datalist the way an
   <input> could, so switching those to textareas for auto-grow meant rebuilding
   the picklist as a popup instead, same idea as inspRemarks's own suggestion
   box in inspection.php. One shared instance repositioned per field (via
   position:fixed + getBoundingClientRect(), so it isn't thrown off by
   .insp-items-scroll's own internal scrolling) rather than one per row — this
   grid can run to dozens of rows and each would otherwise need its own copy
   of every suggestable column's list. */
.inv-suggest-box {
    position: fixed; z-index: 2000; max-height: 220px; overflow-y: auto;
    background: #fff; border: 1px solid var(--border); border-radius: 8px;
    box-shadow: 0 6px 18px rgba(0,0,0,.12);
}
.inv-suggest-box .inv-suggest-item { padding: .45rem .7rem; cursor: pointer; font-size: .82rem; }
.inv-suggest-box .inv-suggest-item:hover { background: var(--primary); color: #fff; }
.insp-left-col-scroll { overflow-y: auto; min-height: 0; padding-right: .3rem; }

/* Classification pill inside a grid cell — read-only, computed server-side,
   coloured the same way the worklist badge is so the two never look like
   they disagree. */
.inv-cat-pill {
    display: inline-block; padding: .15rem .5rem; border-radius: 20px;
    font-size: .68rem; font-weight: 700; white-space: nowrap;
}
.inv-cat-pill.par  { background: #ede9fe; color: #6d28d9; }
.inv-cat-pill.high { background: #dbeafe; color: #1d4ed8; }
.inv-cat-pill.low   { background: #f1f5f9; color: #475569; }
.inv-number-pill {
    display: inline-block; padding: .15rem .5rem; border-radius: 6px;
    font-size: .72rem; font-weight: 700; white-space: nowrap;
    background: #f0fdf4; color: #15803d; border: 1px solid #bbf7d0;
}
.inv-number-pill.empty { background: #fff7ed; color: #b45309; border-color: #fed7aa; font-weight: 600; }
/* Issued or last renewed three or more years ago: due to be renewed and
   reprinted. Amber, with a small renew mark; the pill's TEXT stays exactly the
   number, since the Issue dialog reads it back. */
.inv-number-pill.due { background: #fff7ed; color: #c2410c; border-color: #fdba74; }
.inv-number-pill.due i { margin-left: .3rem; font-size: .68rem; }
.status-due { background: #ffedd5; color: #c2410c; }
.inv-due-note { font-size: .8rem; border-radius: 10px; padding: .55rem .75rem; margin-bottom: .75rem;
                background: #fff7ed; border: 1px solid #fdba74; color: #9a3412; }

/* A heading row carried over from the purchase order — "SET 1", "LOT 1" — is
   a label, not a stocked item: it never had a unit, quantity or cost against
   it there either, so it cannot be classified, issued a PAR/ICS number, or
   selected for issuance here. Same "blank" test the PO item grid uses (see
   its own .po-item-title note) and the same presentation-only treatment —
   the empty inputs stay in the DOM so saving keeps reading the row exactly
   as before. */
.inv-item-title > td { background: #f1f5f9; }
.inv-item-title input, .inv-item-title textarea.inv-grow { display: none; }
.inv-item-title .item-desc {
    font-weight: 700; color: var(--navy); background: #f1f5f9;
    border-color: transparent; box-shadow: none;
}
.inv-item-title .inv-cat-pill, .inv-item-title .inv-number-pill { display: none; }
.inv-item-title td.no-print .btn { display: none; }
.inv-title-dash { display: none; color: #cbd5e1; text-align: center; font-size: .8rem; }
.inv-item-title .inv-title-dash { display: block; }

.insp-total-row {
    display: flex; justify-content: flex-end; gap: 2rem; font-size: .88rem;
    margin-top: .6rem; padding: .55rem .9rem;
    background: linear-gradient(135deg, #eef4fe, #e6edfb);
    border: 1px solid #cddcf7; border-radius: 10px;
}
.insp-total-row .lbl { color: var(--muted); font-weight: 600; }
.insp-total-row .val { font-weight: 700; color: var(--navy); font-variant-numeric: tabular-nums; }

#inventoryModal .modal-body   { background: #f5f8fd; }
#inventoryModal .modal-footer { background: #f5f8fd; border-top: 1px solid #dbe3ee; }
#inventoryModal .po-card      { box-shadow: 0 1px 3px rgba(30,58,138,.06); }

/* Purpose is free text and often a full sentence — same treatment as every
   other list in this app: shown in full, wrapping onto as many lines as it
   needs, capped in width only. */
.inv-purpose-cell { display: block; max-width: 300px; min-width: 160px; white-space: normal; word-break: break-word; }
#inventoryTable tbody td { vertical-align: top; }

/* ── The PO picker (create mode only) ───────────────────────────────────── */
.inv-po-pick-results { max-height: 260px; overflow-y: auto; border: 1px solid var(--border); border-radius: 10px; margin-top: .4rem; }
.inv-po-pick-row { padding: .5rem .7rem; border-bottom: 1px solid #f1f5f9; cursor: pointer; font-size: .82rem; }
.inv-po-pick-row:last-child { border-bottom: none; }
.inv-po-pick-row:hover { background: #eff6ff; }
.inv-po-pick-row .meta { color: var(--muted); font-size: .74rem; }

/* ── Issuance / Transfer dialogs ─────────────────────────────────────────── */
.inv-doc-choice { display: flex; gap: .6rem; flex-wrap: wrap; }
.inv-doc-choice .form-check { border: 1px solid var(--border); border-radius: 10px; padding: .5rem .8rem .5rem 2.2rem; flex: 1 1 140px; }
.inv-doc-choice .form-check-input:checked ~ .form-check-label { color: var(--primary); font-weight: 700; }
.inv-issued-result {
    display: flex; align-items: center; gap: .6rem; padding: .7rem .9rem; border-radius: 10px;
    background: #f0fdf4; border: 1px solid #bbf7d0; color: #15803d; font-weight: 600; margin-top: .8rem;
}
.inv-issued-result .num { font-size: 1.05rem; font-family: monospace; }
/* A Competitive Bidding contract in the Receive search / intake summary. */
.inv-cb-badge { display: inline-flex; align-items: center; font-size: .68rem; font-weight: 700; color: #6d28d9;
                background: #f5f3ff; border: 1px solid #c4b5fd; border-radius: 999px; padding: .05rem .5rem;
                white-space: nowrap; vertical-align: middle; }
/* The number a New issue will get, previewed under Number Year / Month. Blue,
   not green: it is a preview, not yet issued. */
.inv-number-preview { margin-top: .4rem; font-size: .84rem; color: #1e3a8a; }
.inv-number-preview .num { font-family: monospace; font-size: 1rem; font-weight: 700; color: #1d4ed8;
                           background: #eff6ff; border: 1px solid #bfdbfe; border-radius: 6px; padding: .05rem .45rem; }
.inv-number-preview.bad { color: #b45309; font-weight: 600; }

/* ── Transfer history timeline ────────────────────────────────────────────── */
.inv-hist-item { border-left: 3px solid #93b4f5; padding: .5rem 0 .5rem .9rem; margin-bottom: .5rem; position: relative; }
.inv-hist-item::before { content: ''; position: absolute; left: -6px; top: .65rem; width: 10px; height: 10px; border-radius: 50%; background: var(--primary); }
.inv-hist-num { font-weight: 700; color: var(--navy); }
.inv-hist-meta { font-size: .78rem; color: var(--muted); margin-top: .15rem; }
.inv-hist-flow { font-size: .84rem; margin-top: .3rem; }
.inv-hist-flow .from { color: #b45309; } .inv-hist-flow .to { color: #15803d; font-weight: 600; }
.inv-empty { text-align: center; color: var(--muted); padding: 1.4rem; border: 1px dashed var(--border); border-radius: 10px; }

@media print { .sidebar, .top-navbar, .no-print { display: none !important; } }

/* Fix Names dialog */
.fn-card { border: 1px solid #dbe4f3; border-radius: 10px; padding: .6rem .75rem; margin-bottom: .75rem; background: #fff; }
.fn-head { display: flex; align-items: center; gap: .75rem; flex-wrap: wrap; margin-bottom: .35rem; }
.fn-keep-name { font-weight: 700; color: var(--navy); }
.fn-row { display: grid; grid-template-columns: 26px 26px 54px 1fr; align-items: center; gap: .35rem;
          padding: .2rem .3rem; border-radius: 6px; margin: 0; cursor: pointer; }
.fn-row:hover { background: #f3f7fe; }
.fn-row.is-keep { background: #ecfdf5; }
.fn-row.is-out { opacity: .5; }
.fn-cols { display: grid; grid-template-columns: 26px 26px 54px 1fr; gap: .35rem; padding: 0 .3rem;
           font-size: .68rem; text-transform: uppercase; letter-spacing: .03em; color: var(--muted); font-weight: 700; }
.fn-n { text-align: right; font-variant-numeric: tabular-nums; color: var(--muted); }
.fn-name { overflow-wrap: anywhere; }
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
                <div class="page-title">Inventory &amp; Property</div>
                <div class="page-subtitle">Receive purchase orders into custody, classify PAR/ICS, issue and transfer property.</div>
            </div>
        </div>
        <div class="d-flex align-items-center gap-2">
            <?php include 'topbar.php'; ?>
        </div>
    </div>

    <div class="content-area">
        <div class="row g-3 mb-3 no-print">
            <div class="col-sm-6 col-lg-3">
                <div class="stat-card">
                    <div class="stat-icon blue"><i class="fas fa-dolly"></i></div>
                    <div><div class="stat-value" id="tileToday">—</div><div class="stat-label">Received Today</div></div>
                </div>
            </div>
            <div class="col-sm-6 col-lg-3">
                <div class="stat-card">
                    <div class="stat-icon teal"><i class="fas fa-calendar-check"></i></div>
                    <div><div class="stat-value" id="tileMonth">—</div><div class="stat-label">Received This Month</div></div>
                </div>
            </div>
            <div class="col-sm-6 col-lg-3">
                <div class="stat-card">
                    <div class="stat-icon green"><i class="fas fa-peso-sign"></i></div>
                    <div><div class="stat-value" id="tileValue" style="font-size:1.15rem;">—</div><div class="stat-label">Value This Month</div></div>
                </div>
            </div>
            <div class="col-sm-6 col-lg-3">
                <div class="stat-card">
                    <div class="stat-icon green"><i class="fas fa-file-circle-check"></i></div>
                    <div><div class="stat-value" id="tileIssued">—</div><div class="stat-label">PAR/ICS Issued</div></div>
                </div>
            </div>
        </div>

        <div class="po-card no-print">
            <div class="row g-2 align-items-end">
                <div class="col-sm-6 col-md-5 po-field-group mb-0">
                    <label>Search</label>
                    <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-magnifying-glass"></i></span>
                    <input type="text" id="filterSearch" class="form-control" placeholder="PO/PR number, supplier, department"></div>
                </div>
                <div class="col-sm-6 col-md-3 po-field-group mb-0">
                    <label>From</label>
                    <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-calendar"></i></span>
                    <input type="date" id="filterFrom" class="form-control"></div>
                </div>
                <div class="col-sm-6 col-md-3 po-field-group mb-0">
                    <label>To</label>
                    <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-calendar"></i></span>
                    <input type="date" id="filterTo" class="form-control"></div>
                </div>
                <div class="col-sm-6 col-md-1 po-field-group mb-0">
                    <button class="btn btn-primary-app w-100" id="btnFilter" title="Filter"><i class="fas fa-filter"></i></button>
                </div>
            </div>
        </div>

        <div class="panel">
            <div class="panel-header inv-inventory-header d-flex align-items-center justify-content-between flex-wrap gap-2">
                <h6 class="panel-title"><i class="fas fa-warehouse me-2"></i>Inventory Records</h6>
                <div class="inv-toolbar">
                  <div class="inv-toolbar-group inv-toolbar-status">
                    <span class="po-chip active" id="chipAll" onclick="setStatusFilter('')">Total <span class="po-chip-num" id="chipAllNum">0</span></span>
                    <span class="po-chip" id="chipAwaiting" onclick="setStatusFilter('awaiting')" style="border-color:#fcd34d;color:#b45309;">Awaiting Issuance <span class="po-chip-num" id="chipAwaitingNum">0</span></span>
                    <span class="po-chip" id="chipIssued" onclick="setStatusFilter('issued')" style="border-color:#86efac;color:#15803d;">Fully Issued <span class="po-chip-num" id="chipIssuedNum">0</span></span>
                    <span class="po-chip" id="chipManual" onclick="setStatusFilter('manual')" style="border-color:#c4b5fd;color:#6d28d9;">Manual <span class="po-chip-num" id="chipManualNum">0</span></span>
                    <span class="po-chip" id="chipDue" onclick="setStatusFilter('due')" style="border-color:#fdba74;color:#c2410c;" title="Records with a PAR/ICS number issued or last renewed 3 or more years ago">Due to Renew <span class="po-chip-num" id="chipDueNum">0</span></span>
                  </div>
                  <div class="inv-toolbar-group inv-toolbar-actions">
                    <button class="btn btn-outline-primary btn-sm rounded-pill" id="btnPersonnelMr"><i class="fas fa-id-card me-1"></i>Personnel MR</button>
                    <?php /* Rewrites saved names, so it is offered only to accounts allowed to use
                             system tools — the API enforces the same rule; this just hides the button.
                             btn-outline-warning rounded-pill matches master_data.php's "Clean up
                             spellings" button — same kind of name-merge tool, different table. */ ?>
                    <?php if (canManageSystemTools($currentUser)): ?>
                    <button class="btn btn-outline-warning btn-sm rounded-pill" id="btnFixNames"><i class="fas fa-user-pen me-1"></i>Fix Names</button>
                    <?php endif; ?>
                    <button class="btn btn-outline-info btn-sm rounded-pill" id="btnPrsList"><i class="fas fa-rotate-left me-1"></i>Property Returns</button>
                    <button type="button" class="btn inv-add-button" id="btnAddManually"><span class="inv-add-icon" aria-hidden="true"><i class="fas fa-pen"></i></span><span>Add Manually</span></button>
                    <button class="btn btn-primary-app btn-sm" id="btnReceive"><i class="fas fa-plus me-1"></i>Receive PO</button>
                  </div>
                </div>
            </div>
            <div class="panel-body">
                <table id="inventoryTable" class="app-table table table-hover table-bordered mb-0" style="width:100%">
                    <thead>
                        <tr>
                            <th>PO Number</th><th>PR Number</th><th>Supplier</th>
                            <th>Department</th><th>Purpose</th><th>Received</th>
                            <th>Items</th><th>Value</th><th>Status</th><th class="no-print">Actions</th>
                        </tr>
                    </thead>
                    <tbody id="inventoryBody"></tbody>
                </table>
            </div>
        </div>
    </div>
</div>
</div>

<!-- ═══════════════════════════════════════════════════════════════════════
     Receive / View / Revise
     ═══════════════════════════════════════════════════════════════════════ -->
<div class="modal fade" id="inventoryModal" tabindex="-1" data-bs-backdrop="static">
  <div class="modal-dialog modal-insp-wide modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header insp-modal-header">
        <div class="insp-header-icon"><i class="fas fa-warehouse"></i></div>
        <div class="insp-header-text">
            <h5 class="modal-title" id="invModalTitle">Receive into Inventory</h5>
            <small id="invModalSubtitle">Bring a purchase order's items into property custody.</small>
        </div>
        <button type="button" class="insp-header-close" data-bs-dismiss="modal"><i class="fas fa-xmark"></i></button>
      </div>
      <div class="modal-body">
        <div class="readonly-banner d-none" id="invReadonlyBanner">
            <i class="fas fa-lock"></i>
            <div>This record is already on file. Choose <strong>Revise</strong> to change it — the current version is kept as history.</div>
        </div>

        <div id="invPoPicker" class="po-card sec-order">
            <div class="po-card-title"><i class="fas fa-magnifying-glass"></i> Select Purchase Order</div>
            <div class="po-field-group mb-0">
                <input type="text" id="invPoSearch" class="form-control" placeholder="Search by PO number, PR number, supplier, department — or a bid / contract number…" autocomplete="off">
                <div class="po-field-hint">Press F3 or start typing. Only purchase orders not already in Inventory are useful here — an existing one opens for Revise instead from the list.</div>
                <div class="inv-po-pick-results d-none" id="invPoResults"></div>
            </div>
        </div>

        <!-- Manual entry — legacy property with no purchase order behind it
             (acquired years before this system existed). Stands in for the
             "Select Purchase Order" card above; see btnAddManually below and
             the `manual` branch of api/inventory.php's save handler. -->
        <div id="invManualHeader" class="po-card sec-order" style="display:none">
            <div class="po-card-title"><i class="fas fa-pen"></i> Manual Entry Details</div>
            <div class="po-field-hint mb-2">Manual inventory record. Type the actual PO Number and PR Number from the source document.</div>
            <div class="row g-2 tight-field-row">
                <div class="col-md-6 po-field-group">
                    <label>PO Number <span class="text-danger">*</span></label>
                    <input type="text" id="manualPo" class="form-control" required>
                </div>
                <div class="col-md-6 po-field-group">
                    <label>PR Number <span class="text-danger">*</span></label>
                    <input type="text" id="manualPr" class="form-control" required>
                </div>
                <div class="col-md-6 po-field-group">
                    <label>Supplier Name</label>
                    <input type="text" id="manualSupplier" class="form-control" autocomplete="off"
                           oninput="invFieldSuggest(this, 'suppliers')" onfocus="invFieldSuggest(this, 'suppliers')" onblur="invSuggestHide()">
                </div>
                <div class="col-md-6 po-field-group">
                    <label>Department</label>
                    <input type="text" id="manualDept" class="form-control" autocomplete="off"
                           oninput="invFieldSuggest(this, 'departments')" onfocus="invFieldSuggest(this, 'departments')" onblur="invSuggestHide()">
                </div>
                <div class="col-md-6 po-field-group">
                    <label>Category</label>
                    <input type="text" id="manualCategory" class="form-control" autocomplete="off"
                           oninput="invFieldSuggest(this, 'categories')" onfocus="invFieldSuggest(this, 'categories')" onblur="invSuggestHide()">
                </div>
            </div>
        </div>

        <div class="row g-3" id="invEntryBody" style="display:none">
        <div class="col-12">

        <div class="po-card po-card-compact sec-order" id="invPoSummaryCard">
          <div class="po-card-title"><i class="fas fa-file-invoice"></i> Purchase Order</div>
          <div class="po-summary-strip">
            <div class="cell"><div class="k">PO Number</div><div class="v" id="sumPo">—</div></div>
            <div class="cell"><div class="k">PR Number</div><div class="v" id="sumPr">—</div></div>
            <div class="cell"><div class="k">Supplier</div><div class="v" id="sumSupplier">—</div></div>
            <div class="cell"><div class="k">Department</div><div class="v" id="sumDept">—</div></div>
            <div class="cell"><div class="k">Category</div><div class="v" id="sumCategory">—</div></div>
          </div>
        </div>

        <div class="row g-3">
        <div class="col-lg-7">
        <div class="po-card po-card-compact sec-receipt h-100">
          <div class="po-card-title"><i class="fas fa-clipboard-list"></i> Intake Details</div>
          <div class="row g-2 tight-field-row">
            <div class="col-md-6 po-field-group">
              <label>Date Entry</label>
              <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-calendar-day"></i></span>
              <input type="datetime-local" id="invDateEntry" class="form-control"></div>
            </div>
            <div class="col-md-6 po-field-group">
              <label>Initial</label>
              <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-signature"></i></span>
              <input type="text" id="invInitial" class="form-control" maxlength="20"></div>
            </div>
            <div class="col-md-6 po-field-group">
              <label>Mode of Procurement</label>
              <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-file-contract"></i></span>
              <input type="text" id="invMode" class="form-control"></div>
            </div>
            <div class="col-md-6 po-field-group">
              <label>Source of Fund</label>
              <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-sack-dollar"></i></span>
              <input type="text" id="invSource" class="form-control" placeholder="e.g. General Fund"></div>
            </div>
            <div class="col-12 po-field-group">
              <label>Purpose</label>
              <div class="po-input-wrap"><span class="po-input-icon" style="top:19px;transform:none;"><i class="fas fa-bullseye"></i></span>
              <textarea id="invPurpose" class="form-control" rows="2"></textarea></div>
            </div>
          </div>
        </div>
        </div>

        <div class="col-lg-5">
        <div class="po-card po-card-compact sec-people h-100">
          <div class="po-card-title"><i class="fas fa-user-shield"></i> Signing as CGSO Officer</div>
          <div class="row g-2 tight-field-row">
            <div class="col-md-6 po-field-group">
              <label>CGSO Officer</label>
              <input type="text" id="invCgsoOfficer" class="form-control" list="invDlCgso" autocomplete="off">
            </div>
            <div class="col-md-6 po-field-group">
              <label>Position</label>
              <input type="text" id="invCgsoPosition" class="form-control">
            </div>
          </div>
          <datalist id="invDlCgso"></datalist>
        </div>
        </div>
        </div>

        <div class="po-card po-card-compact sec-people">
          <div class="po-card-title d-flex justify-content-between align-items-center flex-wrap gap-2">
            <span><i class="fas fa-boxes-stacked"></i> Items</span>
            <div class="d-flex align-items-center gap-3">
                <button type="button" class="btn inv-add-button po-edit-only" id="btnInvAddRow"><span class="inv-add-icon" aria-hidden="true"><i class="fas fa-plus"></i></span><span>Add Item Row</span></button>
                <div class="form-check form-check-inline mb-0">
                    <input class="form-check-input" type="checkbox" id="invCheckAll">
                    <label class="form-check-label small" for="invCheckAll">Select all</label>
                </div>
            </div>
          </div>
          <div class="po-field-hint po-edit-only mb-2">Press Alt+Enter in Description to start a new line.</div>
          <div class="insp-items-scroll" style="max-height:min(55vh, 640px); min-height:320px">
            <table class="insp-items-table">
                <thead>
                    <tr>
                        <th style="width:26px"></th><th style="width:26px">#</th>
                        <th style="min-width:220px">Description</th>
                        <th class="inv-unit-col">Unit</th><th class="inv-qty-col">Qty</th>
                        <th class="inv-cost-col">Unit Cost</th><th class="inv-total-col">Total</th>
                        <th style="width:96px">Tag No.</th><th style="width:96px">Serial No.</th>
                        <th style="width:96px">Category</th><th style="width:120px">PAR/ICS No.</th>
                        <th style="min-width:150px">Accountable Officer</th><th style="min-width:130px">Position</th>
                        <th style="min-width:150px">Location</th><th style="min-width:150px">Classification</th>
                        <th style="width:110px">Condition</th><th style="min-width:120px">Remarks</th>
                        <th style="width:40px" class="no-print">Lot</th><th style="width:70px" class="no-print">Est. Life</th>
                        <th style="width:76px" class="no-print"></th>
                    </tr>
                </thead>
                <tbody id="invItemsBody"></tbody>
            </table>
          </div>
          <div class="insp-total-row">
            <div><span class="lbl">Items:</span> <span class="val" id="invTotalItems">0</span></div>
            <div><span class="lbl">Total Value:</span> <span class="val" id="invTotalValue">₱0.00</span></div>
          </div>
        </div>

        </div>
      </div>
      <!-- Missing until 2026-09-15: the closing tag for .modal-body above
           meant #inventoryModal's own outer closing tag never arrived
           either — every modal defined later in this file (invSubModal,
           invHistoryModal, invPrintModal, invPreviewModal, invMrModal) was a
           literal DOM descendant of this one. That never mattered for the
           others because they're only ever opened from JS while
           #inventoryModal is already open (backdrop already up, so the
           nesting was invisible in practice) — Personnel MR was the first
           entry point that opens straight from the page, and an ancestor
           that's display:none (this modal, when closed) hides everything
           nested inside it too, so invMrModal could never actually show. -->
      </div>
      <div class="modal-footer flex-wrap gap-2">
        <div class="me-auto d-flex gap-2">
            <button type="button" class="btn btn-outline-primary btn-sm d-none" id="btnInvIssue"><i class="fas fa-stamp me-1"></i>Issue PAR/ICS</button>
            <button type="button" class="btn btn-outline-secondary btn-sm d-none" id="btnInvTransfer"><i class="fas fa-right-left me-1"></i>Transfer</button>
            <button type="button" class="btn btn-outline-secondary btn-sm d-none" id="btnInvReturn"><i class="fas fa-rotate-left me-1"></i>Return to Stock</button>
            <button type="button" class="btn btn-outline-secondary btn-sm d-none" id="btnInvHistory"><i class="fas fa-clock-rotate-left me-1"></i>Transfer History</button>
            <button type="button" class="btn btn-outline-secondary btn-sm d-none" id="btnInvSubDetails"><i class="fas fa-list-ul me-1"></i>Unit Breakdown</button>
            <button type="button" class="btn btn-outline-secondary btn-sm d-none" id="btnInvPrint"><i class="fas fa-print me-1"></i>Print Card / Tag</button>
        </div>
        <!-- Everything logged against this PO in Inventory — received, revised,
             undone, PAR/ICS issued or renewed, transfers, returns, unit
             breakdown — like the Purchase Order's own History button.
             Transfer History (left) is the custody trail; this is the full log. -->
        <button type="button" class="btn btn-outline-secondary d-none" id="btnInvRecordHistory"><i class="fas fa-clock-rotate-left me-1"></i> History</button>
        <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-outline-danger d-none" id="btnInvUndo"><i class="fas fa-rotate-left me-1"></i>Undo</button>
        <button type="button" class="btn btn-outline-primary d-none" id="btnInvRevise">Revise</button>
        <button type="button" class="btn btn-primary-app d-none" id="btnInvSave"><i class="fas fa-save me-1"></i><span id="invSaveLabel">Save</span></button>
      </div>
    </div>
  </div>
</div>

<!-- ═══════════════════════════════════════════════════════════════════════
     Unit breakdown (sub-details)
     ═══════════════════════════════════════════════════════════════════════ -->
<div class="modal fade" id="invSubModal" tabindex="-1">
  <div class="modal-dialog modal-lg modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title"><i class="fas fa-list-ul me-2"></i>Unit Breakdown</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
      </div>
      <div class="modal-body">
        <div class="po-field-hint mb-2">One inventory line can cover several physically distinct units — five identical desktop computers under one line, say — each with its own serial number, tag, and (if it moves separately later) its own custodian.</div>
        <div class="table-responsive">
            <table class="insp-items-table" id="invSubTable">
                <thead><tr>
                    <th>Description</th><th style="width:90px">Unit Cost</th>
                    <th style="width:110px">Serial No.</th><th style="width:110px">Tag No.</th>
                    <th style="min-width:140px">Accountable Officer</th><th style="min-width:140px">Location</th>
                    <th style="min-width:110px">Condition</th><th style="min-width:120px">Remarks</th><th style="width:32px"></th>
                </tr></thead>
                <tbody id="invSubBody"></tbody>
            </table>
        </div>
        <button type="button" class="btn btn-sm btn-outline-secondary" id="btnInvSubAdd"><i class="fas fa-plus me-1"></i>Add Row</button>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary-app" id="btnInvSubSave">Save Breakdown</button>
      </div>
    </div>
  </div>
</div>

<!-- ═══════════════════════════════════════════════════════════════════════
     Issue PAR / ICS
     ═══════════════════════════════════════════════════════════════════════ -->
<div class="modal fade" id="invIssueModal" tabindex="-1">
  <div class="modal-dialog modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title"><i class="fas fa-stamp me-2"></i>Issue Property Document</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
      </div>
      <div class="modal-body">
        <div class="po-field-hint mb-2" id="invIssueScope"></div>

        <label class="form-label small fw-semibold">Document Type</label>
        <div class="inv-doc-choice mb-3">
            <div class="form-check"><input class="form-check-input" type="radio" name="invDocType" id="invDocPar" value="par">
                <label class="form-check-label" for="invDocPar">PAR</label></div>
            <div class="form-check"><input class="form-check-input" type="radio" name="invDocType" id="invDocIcs" value="ics">
                <label class="form-check-label" for="invDocIcs">ICS</label></div>
        </div>

        <label class="form-label small fw-semibold">Layout</label>
        <div class="inv-doc-choice mb-3">
            <div class="form-check"><input class="form-check-input" type="radio" name="invLayout" id="invLayoutStd" value="standard" checked>
                <label class="form-check-label" for="invLayoutStd">Standard</label></div>
            <div class="form-check"><input class="form-check-input" type="radio" name="invLayout" id="invLayoutLot" value="lot">
                <label class="form-check-label" for="invLayoutLot">By LOT</label></div>
        </div>

        <div class="inv-due-note d-none" id="invIssueDueNote"></div>
        <label class="form-label small fw-semibold">Mode</label>
        <div class="inv-doc-choice mb-3">
            <div class="form-check"><input class="form-check-input" type="radio" name="invMode2" id="invModeNew" value="new" checked>
                <label class="form-check-label" for="invModeNew">New</label></div>
            <div class="form-check"><input class="form-check-input" type="radio" name="invMode2" id="invModeReprint" value="reprint">
                <label class="form-check-label" for="invModeReprint">Reprint</label></div>
            <div class="form-check"><input class="form-check-input" type="radio" name="invMode2" id="invModeRenew" value="renew">
                <label class="form-check-label" for="invModeRenew">Renew</label></div>
        </div>

        <!-- New numbers only. The year/month a number is filed under
             (ICS<year><month><serial>) — starts from the record's Date Entry;
             change it for an old record being encoded now, so it numbers in
             its own year's series. See invNumberMonth() in
             api/inventory_issuance.php. -->
        <div class="row g-2 mb-3" id="invIssueNumberGroup">
            <div class="col-sm-5 po-field-group mb-0">
                <label>Number Year / Month</label>
                <input type="month" id="invIssueNumberMonth" class="form-control">
            </div>
            <div class="col-sm-7 po-field-group mb-0">
                <label id="invIssueNumberLabel">ICS Number</label>
                <input type="text" id="invIssuePaperNumber" class="form-control" autocomplete="off"
                       placeholder="e.g. ICS2021502904" style="font-family: monospace; font-weight: 700;">
            </div>
            <div class="col-12">
                <div class="inv-number-preview" id="invIssueNumberPreview"></div>
                <div class="po-field-hint">Suggested from the year and month — type over it to use the number printed on the original paper.</div>
            </div>
        </div>

        <div class="row g-2">
            <!-- Not saved anywhere — typed fresh before each print, same
                 pattern as the PO printout's Signatory/Initial fields. See
                 js/inventory-print.js's buildIssuance() doc comment. -->
            <div class="col-12 po-field-group">
                <label>Property Number</label>
                <input type="text" id="invIssuePropertyNumber" class="form-control" placeholder="Optional">
            </div>
            <div class="col-md-6 po-field-group">
                <label>CGSO Officer</label>
                <input type="text" id="invIssueCgso" class="form-control" list="invDlCgso">
            </div>
            <div class="col-md-6 po-field-group">
                <label>CGSO Position</label>
                <input type="text" id="invIssueCgsoPosition" class="form-control">
            </div>
        </div>

        <div class="inv-issued-result d-none" id="invIssuedResult">
            <i class="fas fa-circle-check"></i>
            <div>Issued <span class="num" id="invIssuedNumber"></span></div>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Cancel</button>
        <button type="button" class="btn btn-primary-app" id="btnInvIssueGo"><i class="fas fa-file-pdf me-1"></i>Generate &amp; Preview</button>
      </div>
    </div>
  </div>
</div>

<!-- ═══════════════════════════════════════════════════════════════════════
     Transfer
     ═══════════════════════════════════════════════════════════════════════ -->
<div class="modal fade" id="invTransferModal" tabindex="-1">
  <div class="modal-dialog modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title"><i class="fas fa-right-left me-2"></i>Transfer Property</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
      </div>
      <div class="modal-body">
        <div class="po-field-hint mb-2" id="invTransferScope"></div>

        <label class="form-label small fw-semibold">Document Type</label>
        <div class="inv-doc-choice mb-3">
            <div class="form-check"><input class="form-check-input" type="radio" name="invTransferType" id="invTypeItr" value="itr" checked>
                <label class="form-check-label" for="invTypeItr">ITR — Inventory Transfer</label></div>
            <div class="form-check"><input class="form-check-input" type="radio" name="invTransferType" id="invTypePtr" value="ptr">
                <label class="form-check-label" for="invTypePtr">PTR — Property Transfer</label></div>
        </div>

        <label class="form-label small fw-semibold">Transfer Type</label>
        <div class="inv-doc-choice mb-3">
            <div class="form-check"><input class="form-check-input" type="radio" name="invTransferKind" id="invKindReassign" value="Reassignment" checked>
                <label class="form-check-label" for="invKindReassign">Reassignment</label></div>
            <div class="form-check"><input class="form-check-input" type="radio" name="invTransferKind" id="invKindRelocation" value="Relocation">
                <label class="form-check-label" for="invKindRelocation">Relocation</label></div>
            <div class="form-check"><input class="form-check-input" type="radio" name="invTransferKind" id="invKindDonation" value="Donation">
                <label class="form-check-label" for="invKindDonation">Donation</label></div>
            <div class="form-check"><input class="form-check-input" type="radio" name="invTransferKind" id="invKindOther" value="Other">
                <label class="form-check-label" for="invKindOther">Other</label></div>
        </div>

        <div class="row g-2">
            <div class="col-12 po-field-group">
                <label>From (current custodian)</label>
                <input type="text" id="invTransferFrom" class="form-control" readonly>
            </div>
            <div class="col-md-6 po-field-group">
                <label>To — Accountable Officer <span class="text-danger">*</span></label>
                <input type="text" id="invTransferToOfficer" class="form-control" list="invDlOfficers" autocomplete="off">
            </div>
            <div class="col-md-6 po-field-group">
                <label>To — Position</label>
                <input type="text" id="invTransferToPosition" class="form-control" list="invDlPositions" autocomplete="off">
            </div>
            <div class="col-md-6 po-field-group">
                <label>To — Location</label>
                <input type="text" id="invTransferToLocation" class="form-control" list="invDlLocations" autocomplete="off">
            </div>
            <div class="col-md-6 po-field-group">
                <label>Fund Cluster</label>
                <input type="text" id="invTransferFundCluster" class="form-control">
            </div>
            <div class="col-12 po-field-group">
                <label>Reason for Transfer</label>
                <textarea id="invTransferReason" class="form-control" rows="2"></textarea>
            </div>
            <div class="col-md-6 po-field-group">
                <label>Approved By (CGSO)</label>
                <input type="text" id="invTransferApprovedBy" class="form-control" list="invDlCgso">
            </div>
            <div class="col-md-6 po-field-group">
                <label>Issued By (Warehouse Officer)</label>
                <input type="text" id="invTransferIssuedBy" class="form-control" list="invDlCgso">
            </div>
            <div class="col-md-6 po-field-group">
                <label>Issued By — Position</label>
                <input type="text" id="invTransferIssuedPosition" class="form-control" list="invDlPositions" autocomplete="off">
            </div>
        </div>
        <datalist id="invDlOfficers"></datalist>
        <datalist id="invDlPositions"></datalist>
        <datalist id="invDlLocations"></datalist>

        <div class="inv-issued-result d-none" id="invTransferResult">
            <i class="fas fa-circle-check"></i>
            <div>Recorded as <span class="num" id="invTransferNumber"></span></div>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Cancel</button>
        <button type="button" class="btn btn-primary-app" id="btnInvTransferGo"><i class="fas fa-file-pdf me-1"></i>Record &amp; Preview</button>
      </div>
    </div>
  </div>
</div>

<!-- ═══════════════════════════════════════════════════════════════════════
     Return to Stock (Property Return Slip) — structurally a transfer whose
     destination is GSO itself; see api/inventory_issuance.php's 'transfer'
     action note. No Transfer Type / Transfer Kind radios here — Donation/
     Reassignment/Relocation/Other describe a move between two employees,
     which a return isn't.
     ═══════════════════════════════════════════════════════════════════════ -->
<div class="modal fade" id="invReturnModal" tabindex="-1">
  <div class="modal-dialog modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title"><i class="fas fa-rotate-left me-2"></i>Return to Stock (PRS)</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
      </div>
      <div class="modal-body">
        <div class="po-field-hint mb-2" id="invReturnScope"></div>

        <div class="row g-2">
            <div class="col-12 po-field-group">
                <label>From (returning officer)</label>
                <input type="text" id="invReturnFrom" class="form-control" readonly>
            </div>
            <div class="col-md-6 po-field-group">
                <label>Condition</label>
                <input type="text" id="invReturnCondition" class="form-control" list="invDlCondition" autocomplete="off">
            </div>
            <div class="col-md-6 po-field-group">
                <label>Returned To</label>
                <input type="text" id="invReturnToLocation" class="form-control" value="GSO Storage">
            </div>
            <div class="col-12 po-field-group">
                <label>Reason for Return</label>
                <textarea id="invReturnReason" class="form-control" rows="2"></textarea>
            </div>
            <div class="col-md-6 po-field-group">
                <label>Received By (GSO)</label>
                <input type="text" id="invReturnReceivedBy" class="form-control" list="invDlCgso">
            </div>
            <div class="col-md-6 po-field-group">
                <label>Received By — Position</label>
                <input type="text" id="invReturnReceivedPosition" class="form-control" list="invDlPositions" autocomplete="off">
            </div>
            <div class="col-md-6 po-field-group">
                <label>Approved By (CGSO)</label>
                <input type="text" id="invReturnApprovedBy" class="form-control" list="invDlCgso">
            </div>
        </div>

        <div class="inv-issued-result d-none" id="invReturnResult">
            <i class="fas fa-circle-check"></i>
            <div>Recorded as <span class="num" id="invReturnNumber"></span></div>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Cancel</button>
        <button type="button" class="btn btn-primary-app" id="btnInvReturnGo"><i class="fas fa-file-pdf me-1"></i>Record &amp; Preview</button>
      </div>
    </div>
  </div>
</div>

<!-- ═══════════════════════════════════════════════════════════════════════
     Transfer history
     ═══════════════════════════════════════════════════════════════════════ -->
<div class="modal fade" id="invHistoryModal" tabindex="-1">
  <div class="modal-dialog modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title"><i class="fas fa-clock-rotate-left me-2"></i>Transfer History</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
      </div>
      <div class="modal-body" id="invHistoryBody"></div>
      <div class="modal-footer">
        <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>

<!-- ═══════════════════════════════════════════════════════════════════════
     Print: Property Card / Tag (no number issued, straight to preview)
     ═══════════════════════════════════════════════════════════════════════ -->
<div class="modal fade" id="invPrintModal" tabindex="-1">
  <div class="modal-dialog modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title"><i class="fas fa-print me-2"></i>Print</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
      </div>
      <div class="modal-body">
        <div class="po-field-hint mb-2" id="invPrintScope"></div>
        <div class="inv-doc-choice">
            <div class="form-check"><input class="form-check-input" type="radio" name="invPrintType" id="invPrintCard" value="card" checked>
                <label class="form-check-label" for="invPrintCard">Property Card</label></div>
            <div class="form-check"><input class="form-check-input" type="radio" name="invPrintType" id="invPrintCardSemi" value="card_semi">
                <label class="form-check-label" for="invPrintCardSemi">Property Card (Semi-Expendable)</label></div>
            <div class="form-check"><input class="form-check-input" type="radio" name="invPrintType" id="invPrintTagBig" value="tag_big">
                <label class="form-check-label" for="invPrintTagBig">Property Tag (Big)</label></div>
            <div class="form-check"><input class="form-check-input" type="radio" name="invPrintType" id="invPrintTagSmall" value="tag_small">
                <label class="form-check-label" for="invPrintTagSmall">Property Tag (Small)</label></div>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Cancel</button>
        <button type="button" class="btn btn-primary-app" id="btnInvPrintGo"><i class="fas fa-file-pdf me-1"></i>Preview</button>
      </div>
    </div>
  </div>
</div>

<!-- ═══════════════════════════════════════════════════════════════════════
     Shared PDF preview — same pipeline as every other printout in this app.
     ═══════════════════════════════════════════════════════════════════════ -->
<div class="modal fade" id="invPreviewModal" tabindex="-1">
  <div class="modal-dialog modal-xl modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="invPreviewTitle"><i class="fas fa-file-pdf me-2"></i>Preview</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
      </div>
      <div class="modal-body">
        <div class="pp-toolbar">
            <div class="pp-tool">
                <label for="invPaperSize">Paper</label>
                <select id="invPaperSize" class="form-select form-select-sm">
                    <option value="auto">Auto — fewest pages</option>
                    <option value="a4">A4</option>
                    <option value="long">Long / Folio</option>
                    <option value="legal">Legal</option>
                    <option value="letter">Letter</option>
                </select>
            </div>
            <div class="pp-layout-status" id="invLayoutStatus"></div>
        </div>
        <div id="invPreviewLoading" class="text-center py-5"></div>
        <iframe id="invPreviewFrame" class="pdf-preview-frame d-none" title="Report preview"></iframe>
        <div class="inv-offscreen"><div id="invSheetArea"></div></div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-outline-secondary" id="invOpenTab" disabled><i class="fas fa-up-right-from-square me-1"></i> Open in Tab</button>
        <button type="button" class="btn btn-outline-primary" id="invDownload" disabled><i class="fas fa-download me-1"></i> Download</button>
        <button type="button" class="btn btn-primary-app" id="invPrint" disabled><i class="fas fa-print me-1"></i> Print</button>
      </div>
    </div>
  </div>
</div>

<!-- ═══════════════════════════════════════════════════════════════════════
     Personnel Memorandum Receipt — everything currently in ONE person's
     custody, across every PO (unlike every other document here, which is
     scoped to a single PO). Renders through the same invPreviewModal/
     doRenderInv('mr', ...) pipeline as PAR/ICS/Card/Tag once a name is
     picked — see js/inventory-print.js's buildMR().
     ═══════════════════════════════════════════════════════════════════════ -->
<div class="modal fade" id="invMrModal" tabindex="-1">
  <div class="modal-dialog modal-xl modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title"><i class="fas fa-id-card me-2"></i>Personnel Memorandum Receipt</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
      </div>
      <div class="modal-body">
        <div class="po-field-group mb-3">
          <label>Accountable Officer</label>
          <div class="po-input-wrap"><span class="po-input-icon"><i class="fas fa-user"></i></span>
            <input type="text" id="mrOfficerSearch" class="form-control" placeholder="Search by name…" autocomplete="off"
                   oninput="invFieldSuggest(this, 'mr_officers', mrSelectOfficer)"
                   onfocus="invFieldSuggest(this, 'mr_officers', mrSelectOfficer)"
                   onblur="invSuggestHide()">
          </div>
        </div>
        <div id="mrEmpty" class="text-center text-muted py-5">
          <i class="fas fa-id-card fa-2x mb-2 d-block"></i>Search for a person to see everything currently in their custody.
        </div>
        <div id="mrResult" class="d-none">
          <div class="po-summary-strip mb-3">
            <div class="cell"><div class="k">Officer</div><div class="v" id="mrOfficerName">—</div></div>
            <div class="cell"><div class="k">Items</div><div class="v" id="mrItemCount">0</div></div>
            <div class="cell"><div class="k">Total Value</div><div class="v" id="mrTotalValue">₱0.00</div></div>
          </div>
          <div id="mrAlso" class="po-field-hint mb-2 d-none"></div>
          <div class="table-responsive">
            <table class="app-table table table-bordered mb-0">
              <thead><tr>
                <th>Description</th><th>Unit</th><th>Qty</th><th>Unit Cost</th><th>Total</th>
                <th>Property/Tag No.</th><th>PO Number</th><th>Location</th>
              </tr></thead>
              <tbody id="mrItemsBody"></tbody>
            </table>
          </div>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary-app" id="btnMrPrint" disabled><i class="fas fa-print me-1"></i>Preview / Print</button>
      </div>
    </div>
  </div>
</div>

<!-- ═══════════════════════════════════════════════════════════════════════
     Fix Names — merge the several spellings a custodian is saved under.
     Nothing merges on its own: the suggestions are only suggestions. See
     api/officer_cleanup.php.
     ═══════════════════════════════════════════════════════════════════════ -->
<div class="modal fade" id="invFixNamesModal" tabindex="-1" data-bs-backdrop="static">
  <div class="modal-dialog modal-xl modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title"><i class="fas fa-user-pen me-2"></i>Fix Names</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
      </div>
      <div class="modal-body">
        <div class="po-field-hint mb-3">
          The Accountable Officer on an item is typed by hand, so the same person ends up saved under several spellings and
          their items are split between them. Tick the spellings that are the same person, choose the one to
          <strong>keep</strong>, and press <strong>Merge</strong>. Nothing is merged unless you press it, and every merge can be
          undone from History. Older revisions and the transfer log are left as they were recorded.
        </div>
        <ul class="nav nav-pills mb-3" id="fnTabs">
          <li class="nav-item"><button type="button" class="nav-link active" data-tab="suggested">Suggested <span class="badge bg-secondary ms-1" id="fnSuggestedCount">0</span></button></li>
          <li class="nav-item"><button type="button" class="nav-link" data-tab="find">Find a name</button></li>
          <li class="nav-item"><button type="button" class="nav-link" data-tab="history">History</button></li>
        </ul>

        <div id="fnPaneSuggested">
          <input type="text" class="form-control form-control-sm mb-3" id="fnFilter" placeholder="Filter suggestions…" autocomplete="off">
          <div id="fnSuggestedList" class="text-muted small">Loading…</div>
        </div>

        <div id="fnPaneFind" class="d-none">
          <div class="po-field-hint mb-2">For a person the suggestions did not group — for example two spellings that differ in a middle initial. Search, tick the ones that are the same person, choose the one to keep.</div>
          <input type="text" class="form-control form-control-sm mb-3" id="fnSearch" placeholder="Type part of a name (at least 2 letters)…" autocomplete="off">
          <div id="fnFindResult" class="text-muted small"></div>
        </div>

        <div id="fnPaneHistory" class="d-none">
          <div id="fnHistoryList" class="text-muted small">Loading…</div>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>

<!-- ═══════════════════════════════════════════════════════════════════════
     Property Returns — every PRS ever issued, across every PO (unlike the
     per-PO Transfer History modal above). Read-only list with a Reprint
     action per row; see api/inventory_issuance.php's ?prs_list=1.
     ═══════════════════════════════════════════════════════════════════════ -->
<div class="modal fade" id="invPrsListModal" tabindex="-1">
  <div class="modal-dialog modal-lg modal-dialog-scrollable">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title"><i class="fas fa-rotate-left me-2"></i>Property Returns</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
      </div>
      <div class="modal-body" id="invPrsListBody"></div>
      <div class="modal-footer">
        <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>

<script src="vendor/js/jquery.min.js"></script>
<script src="vendor/js/bootstrap.bundle.min.js"></script>
<script src="vendor/js/jquery.dataTables.min.js"></script>
<script src="vendor/js/dataTables.bootstrap5.min.js"></script>
<script src="vendor/js/html2canvas.min.js"></script>
<script src="vendor/js/jspdf.umd.min.js"></script>
<script src="<?= asset('js/print-pdf.js') ?>"></script>
<script src="<?= asset('js/inventory-print.js') ?>"></script>
<script src="<?= asset('js/record-history.js') ?>"></script>
<script>
const apiUrl = 'api/inventory.php';
const issuanceUrl = 'api/inventory_issuance.php';

function escapeHtml(value) {
    if (value === null || value === undefined) return '';
    return String(value).replace(/&/g,'&amp;').replace(/</g,'&lt;').replace(/>/g,'&gt;').replace(/"/g,'&quot;').replace(/'/g,'&#39;');
}
function money(v) { const n = parseFloat(v); return isFinite(n) ? n.toLocaleString('en-US',{minimumFractionDigits:2,maximumFractionDigits:2}) : '0.00'; }
// Same test purchase_orders.php's item grid uses to tell "really zero" from
// "nothing entered" — a bare falsy check would treat a genuine 0 the same as
// blank, which is exactly the distinction a title-row test needs.
function itemHasValue(v) {
    return v !== null && v !== undefined && String(v).trim() !== '' && parseFloat(v) !== 0;
}
function qty(v) { const n = parseFloat(v); if (!isFinite(n)) return ''; return Number.isInteger(n) ? String(n) : String(parseFloat(n.toFixed(3))); }
function fmtDate(v) {
    if (!v) return '';
    const t = Date.parse(String(v).replace(' ', 'T'));
    if (isNaN(t)) return '';
    const d = new Date(t);
    return String(d.getMonth()+1).padStart(2,'0') + '/' + String(d.getDate()).padStart(2,'0') + '/' + d.getFullYear();
}
function toLocalInput(v) {
    if (!v) return '';
    const t = Date.parse(String(v).replace(' ', 'T'));
    if (isNaN(t)) return '';
    const d = new Date(t);
    return d.getFullYear() + '-' + String(d.getMonth()+1).padStart(2,'0') + '-' + String(d.getDate()).padStart(2,'0')
         + 'T' + String(d.getHours()).padStart(2,'0') + ':' + String(d.getMinutes()).padStart(2,'0');
}
function openModal(id) { bootstrap.Modal.getOrCreateInstance(document.getElementById(id)).show(); }
function showToast(msg, type) {
    // Reuses the same lightweight toast markup every other page in this app
    // already defines locally.
    const id = 't' + Date.now();
    const bg = type === 'error' || type === 'danger' ? 'bg-danger' : type === 'warning' ? 'bg-warning text-dark' : 'bg-success';
    let box = document.getElementById('toastContainer');
    if (!box) {
        box = document.createElement('div');
        box.id = 'toastContainer';
        box.className = 'toast-container position-fixed top-0 end-0 p-3';
        document.body.appendChild(box);
    }
    box.insertAdjacentHTML('beforeend', `<div id="${id}" class="toast align-items-center text-white ${bg} border-0"><div class="d-flex"><div class="toast-body">${escapeHtml(msg)}</div><button class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button></div></div>`);
    const t = new bootstrap.Toast(document.getElementById(id), { delay: 4500 });
    t.show();
    document.getElementById(id).addEventListener('hidden.bs.toast', () => document.getElementById(id)?.remove());
}

let officeList = [];       // {name, position} candidates for signatory pickers
let cgsoOfficer = null;
let warehouseOfficer = null;   // signs "Released/Issued by" on ITR / PTR — from ref_signatory
let classificationList = [];
let conditionList = [];
let locationList = [];      // ref_department — also what Manual Entry's Department suggests from
let supplierList = [];
let categoryList = [];
let positionList = [];
let officerList = [];          // active on payroll — Accountable Officer / Transfer To
let mrOfficerList = [];        // names saved as Accountable Officer on inventory — Personnel MR search only
let officerPositionMap = {};   // name -> position on their latest payroll line

async function loadSignatories() {
    try {
        const res = await fetch(`${apiUrl}?signatories=1`);
        const data = await res.json();
        if (!data.success) return;
        officerList = data.officers || [];
        mrOfficerList = data.mr_officers || [];
        officerPositionMap = data.officer_positions || {};
        positionList = data.positions || [];
        locationList = data.locations || [];
        supplierList = data.suppliers || [];
        categoryList = data.categories || [];
        classificationList = data.classifications || [];
        conditionList = data.conditions || [];
        cgsoOfficer = data.cgso_officer || null;
        warehouseOfficer = data.warehouse_officer || null;

        document.getElementById('invDlCgso').innerHTML =
            (data.cgso_staff || []).map(p => `<option value="${escapeHtml(p.name)}">${escapeHtml(p.position||'')}</option>`).join('');
        document.getElementById('invDlOfficers').innerHTML = officerList.map(n => `<option value="${escapeHtml(n)}">`).join('');
        document.getElementById('invDlPositions').innerHTML = positionList.map(n => `<option value="${escapeHtml(n)}">`).join('');
        document.getElementById('invDlLocations').innerHTML = locationList.map(n => `<option value="${escapeHtml(n)}">`).join('');
    } catch (err) { console.error(err); }
}

async function loadStats() {
    try {
        const res = await fetch(`${apiUrl}?stats=1`);
        const data = await res.json();
        if (!data.success) return;
        const s = data.stats;
        document.getElementById('tileToday').textContent    = s.received_today || 0;
        document.getElementById('tileMonth').textContent    = s.received_month || 0;
        document.getElementById('tileValue').textContent    = '₱' + money(s.value_month);
        document.getElementById('tileIssued').textContent  = Number(s.par_ics_issued || 0).toLocaleString('en-US');
    } catch (err) { console.error(err); }
}

// ── List ───────────────────────────────────────────────────────────────────
let invTable = null;
let invStatusFilter = '';

function setStatusFilter(status) {
    invStatusFilter = status;
    document.querySelectorAll('#chipAll,#chipAwaiting,#chipIssued,#chipManual,#chipDue').forEach(c => c.classList.remove('active'));
    (status === 'awaiting' ? document.getElementById('chipAwaiting')
        : status === 'issued' ? document.getElementById('chipIssued')
        : status === 'manual' ? document.getElementById('chipManual')
        : status === 'due' ? document.getElementById('chipDue')
        : document.getElementById('chipAll')).classList.add('active');
    loadList();
}

async function loadList() {
    const params = new URLSearchParams({
        search: document.getElementById('filterSearch').value.trim(),
        from:   document.getElementById('filterFrom').value,
        to:     document.getElementById('filterTo').value,
        status: invStatusFilter,
    });
    try {
        const res = await fetch(`${apiUrl}?${params}`);
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Unable to load Inventory.', 'error');
        renderRows(data.records || []);
        const c = data.counts || {};
        document.getElementById('chipAllNum').textContent = c.total || 0;
        document.getElementById('chipAwaitingNum').textContent = c.awaiting || 0;
        document.getElementById('chipIssuedNum').textContent = c.issued || 0;
        document.getElementById('chipManualNum').textContent = c.manual || 0;
        document.getElementById('chipDueNum').textContent = c.due || 0;
    } catch (err) {
        showToast('Unable to load Inventory.', 'error');
        console.error(err);
    }
}

// Same split as purchase_orders.php's own PR Number column — a PO can cite
// several PRs ("20251936, 20251919, …"), and one pill each reads far better
// than a single long comma-separated blob. Splits on comma/newline only:
// the value passed in is already HTML-escaped, so splitting on ';' too would
// tear an entity like &amp; in half.
function renderPrPills(prNumber) {
    if (!prNumber) return '<span class="text-muted">—</span>';
    const parts = escapeHtml(prNumber).split(/[,\n\r]+/).map(s => s.trim()).filter(Boolean);
    if (!parts.length) return '<span class="text-muted">—</span>';
    return `<span class="pr-num-group">${parts.map(p => `<span class="num-pill pr-num-pill">${p}</span>`).join('')}</span>`;
}

function renderRows(rows) {
    const body = document.getElementById('inventoryBody');
    if (invTable) { invTable.destroy(); invTable = null; }
    body.innerHTML = rows.map(r => {
        const badge = r.awaiting_issuance
            ? `<span class="status-badge status-awaiting"><i class="fas fa-hourglass-half"></i> ${r.issued_count}/${r.item_count} issued</span>`
            : `<span class="status-badge status-issued"><i class="fas fa-check"></i> Fully issued</span>`;
        const manualBadge = r.is_manual ? `<span class="status-badge status-manual ms-1"><i class="fas fa-pen"></i> Manual Entry</span>` : '';
        // PAR/ICS numbers here that were issued or last renewed 3+ years ago.
        const dueBadge = r.due_count > 0
            ? `<span class="status-badge status-due ms-1" title="${r.due_count} line${r.due_count === 1 ? '' : 's'} issued or last renewed 3 or more years ago — due to be renewed and reprinted"><i class="fas fa-rotate"></i> ${r.due_count} to renew</span>` : '';
        return `<tr>
            <td><span class="num-pill po-num-pill">${escapeHtml(r.po_number)}</span>${manualBadge}${dueBadge}</td>
            <td>${renderPrPills(r.pr_number)}</td>
            <td>${escapeHtml(r.supplier_name) || '<span class="text-muted">—</span>'}</td>
            <td>${escapeHtml(r.department) || '<span class="text-muted">—</span>'}</td>
            <td>${r.purpose ? `<span class="inv-purpose-cell">${escapeHtml(r.purpose)}</span>` : '<span class="text-muted">—</span>'}</td>
            <td>${fmtDate(r.date_created)}</td>
            <td>${r.item_count}</td>
            <td>₱${money(r.total_value)}</td>
            <td>${badge}</td>
            <td class="no-print"><div class="table-actions">
                <button class="btn btn-sm btn-outline-primary" onclick="openInventory('${escapeHtml(r.record_key)}','view')" title="View"><i class="fas fa-eye"></i></button>
                ${r.is_manual ? `<button class="btn btn-sm btn-outline-danger" onclick="deleteInventoryRecord('${escapeHtml(r.record_key)}', event)" title="Delete manual entry"><i class="fas fa-trash"></i></button>` : ''}
            </div></td>
        </tr>`;
    }).join('');

    invTable = $('#inventoryTable').DataTable({
        pageLength: 10, lengthMenu: [10, 25, 50, 100], order: [],
        language: { search: '', searchPlaceholder: 'Search table…' },
    });
}

async function deleteInventoryRecord(recordKey, ev) {
    ev?.stopPropagation();
    const warning = `Warning: remove PO ${recordKey} from Inventory?` +
        '\n\nThe current inventory version and its item rows will be removed from the active list.' +
        '\nIf an earlier revision exists, it will be restored. This cannot be undone from this dialog.';
    if (!window.confirm(warning)) return;
    try {
        const res = await fetch(apiUrl, {
            method: 'PATCH',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ action: 'undo', record_key: recordKey }),
        });
        const out = await res.json();
        if (!out.success) return showToast(out.message || 'Unable to delete inventory record.', 'error');
        showToast(out.message || 'Inventory record removed.', 'success');
        loadList();
        loadStats();
    } catch (err) {
        showToast('Unable to delete inventory record.', 'error');
        console.error(err);
    }
}

// ── Record / View / Revise ──────────────────────────────────────────────
let invMode = 'create';       // create | view | revise
// True for a record typed in by hand (no linked PO) — usually old paperwork
// being encoded now. Only these get the Issue dialog's Number Year / Month box.
let invIsManual = false;
let invPoNumber = '';
let invRecordKey = '';
let invHeaderId = null;
let invPrevious = null;

// mode: 'picker' (search for a real PO — "Receive PO", items grid hidden
// until one is picked), 'manual' (no PO — "Add Manually": the Manual Entry
// Details card AND the items grid show together, there is nothing to search
// for first), or 'entry' (a PO has been picked, or this is view/revise —
// just the summary strip / items grid, same as 'manual' minus its own card).
function invSetPoPickerVisible(mode) {
    document.getElementById('invPoPicker').style.display = mode === 'picker' ? '' : 'none';
    document.getElementById('invManualHeader').style.display = mode === 'manual' ? '' : 'none';
    document.getElementById('invEntryBody').style.display = (mode === 'entry' || mode === 'manual') ? '' : 'none';
    // The read-only "Purchase Order" summary strip only has anything to show
    // once a real PO has been picked (or for view/revise) — for manual
    // entry, the Manual Entry Details card above already covers that ground.
    document.getElementById('invPoSummaryCard').style.display = mode === 'manual' ? 'none' : '';
}

function invResetForNewEntry() {
    invHeaderId = null; invPrevious = null;
    document.getElementById('invReadonlyBanner').classList.add('d-none');
    ['btnInvIssue','btnInvTransfer','btnInvReturn','btnInvHistory','btnInvSubDetails','btnInvPrint','btnInvUndo','btnInvRevise'].forEach(id =>
        document.getElementById(id).classList.add('d-none'));
    document.getElementById('btnInvAddRow').classList.remove('d-none');
    document.getElementById('btnInvSave').classList.remove('d-none');
    document.getElementById('invSaveLabel').textContent = 'Save';
    document.getElementById('invItemsBody').innerHTML = '';
    invItems = [];
    document.getElementById('invCheckAll').checked = false;
    ['invInitial', 'invMode', 'invSource', 'invPurpose'].forEach(id => {
        document.getElementById(id).value = '';
    });
    document.getElementById('invDateEntry').value = toLocalInput(new Date().toISOString());
    updateInvTotals();
    document.getElementById('invCgsoOfficer').value = cgsoOfficer ? cgsoOfficer.name : '';
    document.getElementById('invCgsoPosition').value = cgsoOfficer ? (cgsoOfficer.position || '') : '';
    // Reopen for typing even when the previous record was viewed read-only.
    applyInvMode();
    openModal('inventoryModal');
}

document.getElementById('btnReceive').addEventListener('click', () => {
    invMode = 'create'; invPoNumber = ''; invRecordKey = ''; invIsManual = false;
    document.getElementById('invModalTitle').textContent = 'Receive into Inventory';
    document.getElementById('invModalSubtitle').textContent = "Bring a purchase order's items into property custody.";
    document.getElementById('invPoSearch').value = '';
    document.getElementById('invPoResults').innerHTML = '';
    document.getElementById('invPoResults').classList.add('d-none');
    invSetPoPickerVisible('picker');
    invResetForNewEntry();
    setTimeout(() => document.getElementById('invPoSearch').focus(), 300);
});

document.getElementById('btnAddManually').addEventListener('click', () => {
    invMode = 'manual'; invPoNumber = ''; invRecordKey = ''; invIsManual = true;
    document.getElementById('invModalTitle').textContent = 'Add Property Manually';
    document.getElementById('invModalSubtitle').textContent = 'Manually entered — no linked purchase order.';
    document.getElementById('manualPo').value = '';
    document.getElementById('manualPr').value = '';
    document.getElementById('manualSupplier').value = '';
    document.getElementById('manualDept').value = '';
    document.getElementById('manualCategory').value = '';
    invSetPoPickerVisible('manual');
    invResetForNewEntry();
    setTimeout(() => document.getElementById('manualPo').focus(), 300);
});

document.getElementById('btnInvAddRow').addEventListener('click', () => {
    if (invMode === 'view') return;
    const appendFrom = invItems.length;
    invItems.push({
        is_title: false,
        inv_id: null, items_description: '', unit: '', quantity: '', unit_cost: '', total_cost: '',
        tag_number: '', serial_number: '', inventory_category: '', par_ics_number: '',
        accountable_officer: '', position: '', location: '', classification: '', condition: '',
        remarks: '', number_series: '', estimated_life: '', sub_details: [],
    });
    // Append without replacing the fields that hold the user's current edits.
    renderInvItems(invItems, appendFrom);
    document.querySelector('#invItemsBody tr:last-child .item-desc').focus();
});

// Duplicates one item row, directly beneath it — for a single purchase-order
// line that has to be split between several custodians: 20 chairs for 5
// barangays becomes five rows of 4.
//
// What is copied is what describes the ITEM (description, unit, quantity, unit
// cost, classification, condition, remarks, number series, estimated life, and
// the purchase-order item it came from) — read from what is on screen NOW, so a
// change just typed into the source row comes with it. What is NOT copied is
// what belongs to one particular unit or one particular person: Tag No., Serial
// No., PAR/ICS No., Accountable Officer, Position and Location. Copying a
// custodian would silently put the same person's name on every copy, and a copy
// quietly sharing a serial number is worse than a blank the person has to fill.
//
// The copy is a new row (no Inv_ID), so it is saved with the rest, in this order.
// The cursor lands in its Qty, because that is the number that has to change.
function invDuplicateRow(i) {
    if (invMode === 'view') return;
    const src = document.querySelector(`#invItemsBody tr[data-idx="${i}"]`);
    if (!src || src.classList.contains('inv-item-title')) return;
    const v = f => (src.querySelector(`[data-f="${f}"]`)?.value ?? '').trim();
    const qty = parseFloat(v('quantity')) || 0, cost = parseFloat(v('unit_cost')) || 0;
    const appendFrom = invItems.length;
    invItems.push({
        is_title: false, inv_id: null, item_id: (invItems[i] || {}).item_id,
        items_description: v('items_description'), unit: v('unit'),
        quantity: v('quantity'), unit_cost: v('unit_cost'), total_cost: qty * cost,
        inventory_category: src.querySelector('[data-f="inventory_category"]')?.textContent.trim() || '',
        classification: v('classification'), condition: v('condition'), remarks: v('remarks'),
        number_series: v('number_series'), estimated_life: v('estimated_life'),
        tag_number: '', serial_number: '', par_ics_number: '',
        accountable_officer: '', position: '', location: '', sub_details: [],
    });
    // Rendered at the end like Add Item Row, then moved under its source. Rows
    // are matched to their data by data-idx, not by position, so moving one is safe.
    renderInvItems(invItems, appendFrom);
    const fresh = document.querySelector('#invItemsBody tr:last-child');
    src.after(fresh);
    invRenumberRows();
    fresh.scrollIntoView({ block: 'nearest' });
    const q = fresh.querySelector('[data-f="quantity"]');
    q.focus(); q.select();
    showToast('Row duplicated. Set its Qty, Accountable Officer and Location.');
}

// The # column counts stocked items (title rows take no number), in the order
// the rows now sit on screen.
function invRenumberRows() {
    let n = 0;
    document.querySelectorAll('#invItemsBody tr').forEach(tr => {
        if (tr.classList.contains('inv-item-title')) return;
        if (tr.children[1]) tr.children[1].textContent = ++n;
    });
}

// ── Personnel Memorandum Receipt ────────────────────────────────────────
let mrOfficer = null; // { name, rows, total_value } for whoever is currently loaded

document.getElementById('btnPersonnelMr').addEventListener('click', () => {
    mrOfficer = null;
    document.getElementById('mrOfficerSearch').value = '';
    document.getElementById('mrEmpty').classList.remove('d-none');
    document.getElementById('mrResult').classList.add('d-none');
    document.getElementById('btnMrPrint').disabled = true;
    openModal('invMrModal');
    setTimeout(() => document.getElementById('mrOfficerSearch').focus(), 300);
});

// Passed to invFieldSuggest() as its onSelect callback — runs once a name is
// actually picked from the popup, not on every keystroke.
async function mrSelectOfficer(name) {
    document.getElementById('mrOfficerSearch').value = name;
    try {
        const res = await fetch(`${apiUrl}?mr=1&officer=${encodeURIComponent(name)}`);
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Unable to load records.', 'error');
        mrOfficer = { name, position: data.position || '', department: data.department || '',
                      rows: data.rows || [], total_value: data.total_value || 0 };
        document.getElementById('mrEmpty').classList.add('d-none');
        document.getElementById('mrResult').classList.remove('d-none');
        document.getElementById('mrOfficerName').textContent = name;
        // The same person is sometimes saved under several spellings; the API
        // folds them together, and this says so instead of showing more than
        // the name that was picked without explanation.
        const also = data.also_saved_as || [];
        const alsoEl = document.getElementById('mrAlso');
        alsoEl.textContent = also.length ? 'Also includes items saved under: ' + also.join('; ') : '';
        alsoEl.classList.toggle('d-none', !also.length);
        document.getElementById('mrItemCount').textContent = mrOfficer.rows.length;
        document.getElementById('mrTotalValue').textContent = '₱' + money(mrOfficer.total_value);
        document.getElementById('mrItemsBody').innerHTML = mrOfficer.rows.length ? mrOfficer.rows.map(r => `
            <tr>
                <td>${escapeHtml(r.items_description || '')}</td>
                <td>${escapeHtml(r.unit || '')}</td>
                <td>${r.quantity ?? ''}</td>
                <td>${money(r.unit_cost)}</td>
                <td>${money(r.total_cost)}</td>
                <td>${escapeHtml(r.tag_number || r.serial_number || '') || '<span class="text-muted">—</span>'}</td>
                <td>${escapeHtml(r.po_number || '')}</td>
                <td>${escapeHtml(r.location || '') || '<span class="text-muted">—</span>'}</td>
            </tr>`).join('')
            : '<tr><td colspan="8" class="text-center text-muted py-3">No items currently on file for this person.</td></tr>';
        document.getElementById('btnMrPrint').disabled = mrOfficer.rows.length === 0;
    } catch (err) {
        showToast('Unable to load records.', 'error');
        console.error(err);
    }
}

document.getElementById('btnMrPrint').addEventListener('click', async () => {
    if (!mrOfficer || !mrOfficer.rows.length) return;
    bootstrap.Modal.getInstance(document.getElementById('invMrModal'))?.hide();
    await renderInvDocument('mr', {
        officer: mrOfficer.name, position: mrOfficer.position, department: mrOfficer.department,
        rows: mrOfficer.rows, cgso: cgsoOfficer,
    });
});

let invPoSearchTimer = null;
document.getElementById('invPoSearch').addEventListener('input', (e) => {
    clearTimeout(invPoSearchTimer);
    const term = e.target.value.trim();
    if (term.length < 2) { document.getElementById('invPoResults').classList.add('d-none'); return; }
    invPoSearchTimer = setTimeout(() => invSearchPo(term), 300);
});

async function invSearchPo(term) {
    try {
        const res = await fetch(`${apiUrl}?search_po=${encodeURIComponent(term)}`);
        const data = await res.json();
        const results = document.getElementById('invPoResults');
        const orders = (data.orders || []).slice(0, 20);
        if (!orders.length) {
            results.innerHTML = '<div class="inv-po-pick-row text-muted">No matching purchase orders.</div>';
            results.classList.remove('d-none');
            return;
        }
        // A Competitive Bidding contract is received like a PO, under its
        // CB-<Bid>-<Contract> key (api/inventory.php invBidKey()); the badge
        // says where it comes from.
        results.innerHTML = orders.map(o => `
            <div class="inv-po-pick-row" onclick="invPickPo('${escapeHtml(o.po_number)}')">
                <div><span class="num-pill po-num-pill">${escapeHtml(o.po_number)}</span>
                     ${o.source === 'bid' ? invBidBadge(o) : ''} ${escapeHtml(o.supplier_name || '')}</div>
                <div class="meta">${escapeHtml(o.department || '')} · ${escapeHtml(o.purpose || '').slice(0,80)}</div>
            </div>`).join('');
        results.classList.remove('d-none');
    } catch (err) { console.error(err); }
}

function invBidBadge(o) {
    const detail = 'Bid ' + (o.bid_number || '') + (o.contract_number ? ' · Contract ' + o.contract_number : '');
    return `<span class="inv-cb-badge" title="${escapeHtml(detail)}"><i class="fas fa-gavel me-1"></i>Competitive Bidding</span>`;
}

async function invPickPo(poNumber) {
    try {
        const res = await fetch(`${apiUrl}?preview_po=${encodeURIComponent(poNumber)}`);
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Unable to load this purchase order.', 'error');
        invPoNumber = poNumber;
        invRecordKey = poNumber;
        const p = data.preview;
        document.getElementById('sumPo').innerHTML = `<span class="num-pill po-num-pill">${escapeHtml(p.po_number)}</span>`
            + (p.source === 'bid' ? ' ' + invBidBadge(p) : '');
        document.getElementById('sumPr').innerHTML = p.pr_number ? `<span class="num-pill pr-num-pill">${escapeHtml(p.pr_number)}</span>` : '—';
        document.getElementById('sumSupplier').textContent = p.supplier_name || '—';
        document.getElementById('sumDept').textContent = p.department || '—';
        document.getElementById('sumCategory').textContent = p.category || '—';
        document.getElementById('invMode').value = p.mode_of_procurement || '';
        document.getElementById('invSource').value = '';
        document.getElementById('invPurpose').value = p.purpose || '';
        document.getElementById('invDateEntry').value = toLocalInput(new Date().toISOString());

        renderInvItems((p.items || []).map(it => ({
            item_id: it.Item_ID, items_description: it.Items_Description, unit: it.Unit,
            quantity: it.Quantity, unit_cost: it.Unit_Cost, total_cost: it.Total_Cost,
            inventory_category: it.inventory_category, number_series: it.NumberSeries,
            tag_number: '', serial_number: '', par_ics_number: '',
            accountable_officer: '', position: '', location: '', classification: '',
            condition: 'IN GOOD CONDITION', remarks: '', estimated_life: '',
            // is_amended/amendment_no/original_items_description come from
            // api/inventory.php overlaying the latest PO amendment (see
            // lib/po_amendment_items.php) — present only on a fresh preview,
            // never on an already-saved inventory record (that's a snapshot,
            // taken as of intake, same as everything else this module keeps).
            is_amended: !!it.is_amended, amendment_no: it.amendment_no || null,
            original_items_description: it.original_items_description || '',
            // cb_bid_number/cb_processed_quantity/etc. come from
            // api/inventory.php overlaying Competitive Bidding processing
            // steps (see lib/cb_processing_items.php) — same "fresh preview
            // only" scoping as the amendment fields above.
            cb_bid_number: it.cb_bid_number || null,
            cb_ordered_quantity: it.cb_ordered_quantity, cb_processed_quantity: it.cb_processed_quantity,
            cb_remaining_quantity: it.cb_remaining_quantity,
        })));
        invSetPoPickerVisible('entry');
        applyInvMode();
    } catch (err) {
        showToast('Unable to load this purchase order.', 'error');
        console.error(err);
    }
}

async function openInventory(recordKey, mode) {
    invMode = mode; invRecordKey = recordKey;
    try {
        const res = await fetch(`${apiUrl}?record_key=${encodeURIComponent(recordKey)}`);
        const data = await res.json();
        if (!data.success) return showToast(data.message || 'Unable to load.', 'error');

        const h = data.header;
        invHeaderId = h.header_id;
        invRecordKey = h.record_key || recordKey;
        invPoNumber = h.po_number;
        invIsManual = !!h.is_manual;
        invPrevious = data.previous || null;

        document.getElementById('invModalTitle').textContent = mode === 'view' ? 'Inventory Record' : 'Revise Inventory Record';
        document.getElementById('invModalSubtitle').textContent = h.is_manual
            ? 'Manually entered — no linked purchase order.'
            : (mode === 'view'
                ? 'What is currently on file for this purchase order.'
                : 'Saving keeps the current version on file and files this as a new one.');
        document.getElementById('invReadonlyBanner').classList.toggle('d-none', mode !== 'view');
        invSetPoPickerVisible('entry');

        document.getElementById('sumPo').innerHTML = `<span class="num-pill po-num-pill">${escapeHtml(h.po_number)}</span>`;
        document.getElementById('sumPr').innerHTML = h.pr_number ? `<span class="num-pill pr-num-pill">${escapeHtml(h.pr_number)}</span>` : '—';
        document.getElementById('sumSupplier').textContent = h.supplier_name || '—';
        document.getElementById('sumDept').textContent = h.department || '—';
        document.getElementById('sumCategory').textContent = h.category || '—';
        document.getElementById('invMode').value = h.mode_of_procurement || '';
        document.getElementById('invSource').value = h.source_of_fund || '';
        document.getElementById('invPurpose').value = h.purpose || '';
        document.getElementById('invDateEntry').value = toLocalInput(h.date_entry);
        document.getElementById('invInitial').value = h.initial || '';
        document.getElementById('invCgsoOfficer').value = cgsoOfficer ? cgsoOfficer.name : '';
        document.getElementById('invCgsoPosition').value = cgsoOfficer ? (cgsoOfficer.position || '') : '';

        renderInvItems((data.items || []).map(it => ({
            inv_id: it.inv_id, item_id: it.item_id, items_description: it.items_description, unit: it.unit,
            quantity: it.quantity, unit_cost: it.unit_cost, total_cost: it.total_cost,
            tag_number: it.tag_number, serial_number: it.serial_number,
            inventory_category: it.inventory_category, par_ics_number: it.par_ics_number,
            issued_year: it.issued_year, renew_due: it.renew_due,
            accountable_officer: it.accountable_officer, position: it.position, location: it.location,
            classification: it.classification, condition: it.condition, remarks: it.remarks,
            number_series: it.number_series, estimated_life: it.estimated_life,
            sub_details: it.sub_details || [],
        })));

        applyInvMode();
        openModal('inventoryModal');
    } catch (err) {
        showToast('Unable to load.', 'error');
        console.error(err);
    }
}

function applyInvMode() {
    const readOnly = invMode === 'view';
    // 'manual' is an unsaved state exactly like 'create' — the record has no
    // Inv_ID yet, so nothing that acts on an already-saved record (Issue,
    // Transfer, Return to Stock, History, Unit Breakdown, Print) applies.
    const unsaved = invMode === 'create' || invMode === 'manual';
    document.querySelectorAll('#inventoryModal input, #inventoryModal textarea, #inventoryModal select').forEach(el => {
        if (el.id === 'invPoSearch') return;
        el.readOnly = readOnly || el.dataset.f === 'total_cost';
        if (el.tagName === 'SELECT') el.disabled = readOnly;
    });
    document.getElementById('btnInvSave').classList.toggle('d-none', readOnly);
    document.getElementById('btnInvRevise').classList.toggle('d-none', !readOnly);
    document.getElementById('btnInvUndo').classList.toggle('d-none', !readOnly);
    document.getElementById('btnInvIssue').classList.toggle('d-none', unsaved);
    document.getElementById('btnInvTransfer').classList.toggle('d-none', unsaved);
    document.getElementById('btnInvReturn').classList.toggle('d-none', unsaved);
    document.getElementById('btnInvHistory').classList.toggle('d-none', unsaved);
    document.getElementById('btnInvSubDetails').classList.toggle('d-none', unsaved);
    document.getElementById('btnInvPrint').classList.toggle('d-none', unsaved);
    document.getElementById('btnInvRecordHistory').classList.toggle('d-none', unsaved);
    document.getElementById('btnInvAddRow').classList.toggle('d-none', readOnly);
    document.getElementById('inventoryModal').classList.toggle('inv-readonly', readOnly);
    // While viewing, the description textareas are hidden (the formatted
    // .inv-desc-view shows instead) and so measure 0px tall. Size them again
    // once they are visible, or Revise would open them collapsed.
    if (!readOnly) requestAnimationFrame(() => autoGrowInvBatch(document.querySelectorAll('#invItemsBody textarea.item-desc')));
    document.getElementById('invSaveLabel').textContent = invMode === 'revise' ? 'Save Revision' : 'Save';
}

document.getElementById('btnInvRevise').addEventListener('click', () => {
    invMode = 'revise';
    document.getElementById('invModalTitle').textContent = 'Revise Inventory Record';
    document.getElementById('invModalSubtitle').textContent = 'Saving keeps the current version on file and files this as a new one.';
    document.getElementById('invReadonlyBanner').classList.add('d-none');
    applyInvMode();
});

// ── Items grid ─────────────────────────────────────────────────────────
let invItems = [];

function invCatPillClass(cat) {
    if (cat === 'PAR') return 'par';
    if (cat === 'ICS HIGH VALUE') return 'high';
    return 'low';
}

// A heading row carried over from the purchase order — no unit, quantity or
// cost was ever entered against it there either. Same test invIsTitleRow()
// applies server-side, kept identical so the grid and the API never disagree
// about which rows are labels.
function invRowIsTitle(it) {
    // An empty row added by the user must keep its item fields editable.
    if (it.is_title === false) return false;
    return !String(it.unit || '').trim()
        && !itemHasValue(it.quantity) && !itemHasValue(it.unit_cost) && !itemHasValue(it.total_cost);
}

// One auto-growing textarea cell, optionally wired to the shared suggestion
// popup (invFieldSuggest()) for the columns that used to be an <input> with a
// native <datalist> — a <textarea> has no datalist support, so those five
// (Accountable Officer, Position, Location, Classification, Condition) need
// the popup to keep suggesting at all; the rest (Unit, Tag No., Serial No.,
// Remarks) never had suggestions and just grow.
function invGrowCell(field, value, suggestKey) {
    // Picking an officer also fills the row's Position from their payroll line.
    const onPick = suggestKey === 'officers' ? ', invFillPositionFromOfficer' : '';
    const events = suggestKey
        ? ` oninput="autoGrowInv(this); invFieldSuggest(this, '${suggestKey}'${onPick})" onfocus="invFieldSuggest(this, '${suggestKey}'${onPick})" onblur="invSuggestHide()"`
        : ` oninput="autoGrowInv(this)"`;
    return `<textarea rows="1" class="inv-grow" data-f="${field}"${events}>${escapeHtml(value || '')}</textarea>`;
}

function renderInvItems(items, appendFrom = 0) {
    invItems = items;
    const body = document.getElementById('invItemsBody');
    // The # column counts stocked items, not label rows — a "SET 1" divider
    // does not take up a number, the same way it does not count toward
    // item_count on the server (see invIsTitleRow()'s note in api/inventory.php).
    let itemNo = 0;
    const rowsHtml = items.map((it, i) => {
        const cat = it.inventory_category || '';
        // A number issued or last renewed 3+ years ago is drawn amber, with the year
        // in its tooltip. Its text is still exactly the number — the Issue dialog
        // reads it back — and the mark is an <i>, which has no text.
        const numberPill = it.par_ics_number
            ? (it.renew_due
                ? `<span class="inv-number-pill due" data-renew-due="1" data-issued-year="${escapeHtml(it.issued_year || '')}" title="Issued or last renewed in ${escapeHtml(it.issued_year || 'an earlier year')} — due to be renewed and reprinted every 3 years. Use Issue PAR/ICS → Renew.">${escapeHtml(it.par_ics_number)}<i class="fas fa-rotate"></i></span>`
                : `<span class="inv-number-pill">${escapeHtml(it.par_ics_number)}</span>`)
            : `<span class="inv-number-pill empty">Not issued</span>`;
        const isTitle = invRowIsTitle(it);
        const dash = '<span class="inv-title-dash">&mdash;</span>';
        const rowNumber = isTitle ? dash : ++itemNo;
        if (i < appendFrom) return '';
        // it.is_amended/amendment_no/original_items_description are only ever
        // set on a fresh pre-intake preview (see openPreviewPo() above) — a
        // record already on file shows exactly what was snapshotted at
        // intake, same as every other field here.
        const amendBadge = it.is_amended
            ? ` <span title="Amended (${escapeHtml(it.amendment_no)}) — originally: ${escapeHtml(it.original_items_description || '')}"
                    style="display:inline-flex;align-items:center;gap:.25rem;font-size:.68rem;font-weight:700;color:#1d4ed8;background:#eff6ff;border:1px solid #bfdbfe;border-radius:999px;padding:.1rem .5rem;margin-left:.35rem;vertical-align:middle;cursor:help;">
                    <i class="fas fa-pen-to-square"></i> ${escapeHtml(it.amendment_no)}</span>`
            : '';
        // it.cb_bid_number/etc. — same "fresh preview only" scoping as
        // amendBadge above. See lib/cb_processing_items.php.
        const cbBadge = it.cb_bid_number
            ? ` <span title="Bid ${escapeHtml(it.cb_bid_number)} — ${escapeHtml(it.cb_processed_quantity)} of ${escapeHtml(it.cb_ordered_quantity)} processed, ${escapeHtml(it.cb_remaining_quantity)} remaining"
                    style="display:inline-flex;align-items:center;gap:.25rem;font-size:.68rem;font-weight:700;color:#1d4ed8;background:#eff6ff;border:1px solid #bfdbfe;border-radius:999px;padding:.1rem .5rem;margin-left:.35rem;vertical-align:middle;cursor:help;">
                    <i class="fas fa-layer-group"></i> CB: ${escapeHtml(it.cb_processed_quantity)}/${escapeHtml(it.cb_ordered_quantity)}</span>`
            : '';
        return `<tr data-idx="${i}" data-inv-id="${it.inv_id || ''}" class="${isTitle ? 'inv-item-title' : ''}">
            <td><input type="checkbox" class="inv-row-check"></td>
            <td class="text-muted">${rowNumber}</td>
            <td><textarea rows="1" class="item-desc" data-f="items_description" oninput="autoGrowInv(this)">${escapeHtml(it.items_description||'')}</textarea><div class="inv-desc-view">${invDescViewHtml(it.items_description)}</div>${amendBadge}${cbBadge}</td>
            <td>${invGrowCell('unit', it.unit)}${dash}</td>
            <td class="num inv-qty-col"><input type="number" class="inv-qty-input" inputmode="decimal" aria-label="Quantity" step="0.001" value="${it.quantity ?? ''}" data-f="quantity" oninput="recalcInvRow(this)">${dash}</td>
            <td class="num"><input type="number" step="0.01" value="${it.unit_cost ?? ''}" data-f="unit_cost" oninput="recalcInvRow(this)">${dash}</td>
            <td class="num"><input type="text" value="${money(it.total_cost)}" data-f="total_cost" readonly>${dash}</td>
            <td>${invGrowCell('tag_number', it.tag_number)}${dash}</td>
            <td>${invGrowCell('serial_number', it.serial_number)}${dash}</td>
            <td>${isTitle ? dash : `<span class="inv-cat-pill ${invCatPillClass(cat)}" data-f="inventory_category">${escapeHtml(cat)}</span>`}</td>
            <td>${isTitle ? dash : numberPill}</td>
            <td>${invGrowCell('accountable_officer', it.accountable_officer, 'officers')}${dash}</td>
            <td>${invGrowCell('position', it.position, 'positions')}${dash}</td>
            <td>${invGrowCell('location', it.location, 'locations')}${dash}</td>
            <td>${invGrowCell('classification', it.classification, 'classification')}${dash}</td>
            <td>${invGrowCell('condition', it.condition, 'condition')}${dash}</td>
            <td>${invGrowCell('remarks', it.remarks)}${dash}</td>
            <td class="no-print"><input type="text" value="${escapeHtml(it.number_series||'')}" data-f="number_series">${dash}</td>
            <td class="no-print"><input type="text" value="${escapeHtml(it.estimated_life||'')}" data-f="estimated_life">${dash}</td>
            <td class="no-print">${isTitle ? '' : `<div class="inv-row-actions">
                <button type="button" class="btn btn-sm btn-outline-secondary inv-dup-btn" onclick="invDuplicateRow(${i})" title="Duplicate this row — e.g. to split the quantity between several barangays"><i class="fas fa-clone"></i></button>
                <button type="button" class="btn btn-sm btn-outline-secondary inv-sub-btn" onclick="invOpenSubDetails(${i})" title="Unit breakdown"><i class="fas fa-list-ul"></i></button></div>`}</td>
        </tr>`;
    }).join('');
    if (appendFrom > 0) body.insertAdjacentHTML('beforeend', rowsHtml);
    else body.innerHTML = rowsHtml;
    autoGrowInvBatch(document.querySelectorAll('#invItemsBody .item-desc, #invItemsBody .inv-grow'));
    updateInvTotals();
    invRefreshSuggestLists();
}

function invRefreshSuggestLists() {
    const dl = (id, list) => { const el = document.getElementById(id); if (el) el.innerHTML = list.map(v => `<option value="${escapeHtml(v)}">`).join(''); };
    if (!document.getElementById('invDlClassification')) {
        const d1 = document.createElement('datalist'); d1.id = 'invDlClassification'; document.body.appendChild(d1);
        const d2 = document.createElement('datalist'); d2.id = 'invDlCondition'; document.body.appendChild(d2);
    }
    dl('invDlClassification', classificationList);
    dl('invDlCondition', conditionList);
}

function autoGrowInv(el) { el.style.height = 'auto'; el.style.height = el.scrollHeight + 'px'; }

// The description as shown while a record is only being VIEWED: first line
// (the item) bold when spec lines sit under it, the specs in normal text —
// same rule as the printouts (descHtml() in js/inventory-print.js). A textarea
// cannot mix bold and normal text, so this is a separate element; the
// textarea stays the source of truth and is what shows while editing.
function invDescViewHtml(text) {
    const lines = String(text || '').replace(/[ \t]+$/gm, '').replace(/\s+$/, '').split('\n');
    const specs = lines.slice(1);
    const bold = specs.some(l => l.trim() !== '');
    return `<div class="dv-main${bold ? ' b' : ''}">${escapeHtml(lines[0])}</div>`
        + (specs.length ? `<div class="dv-spec">${specs.map(l => escapeHtml(l) || '&nbsp;').join('<br>')}</div>` : '');
}

// A person picked as Accountable Officer brings their current position with
// them, from their latest payroll line. It replaces whatever the row's
// Position held: that belonged to the previous officer, and leaving it would
// pair the new name with someone else's title. Nobody on payroll under that
// name (a typed-in name, say) leaves the Position alone.
function invFillPositionFromOfficer(name, el) {
    const position = officerPositionMap[name];
    const cell = position && el.closest('tr')?.querySelector('[data-f="position"]');
    if (!cell) return;
    cell.value = position;
    autoGrowInv(cell);
}

// Same for the Transfer dialog's receiving officer. That box uses a native
// <datalist>, which reports a pick as a 'change' event rather than a callback.
document.getElementById('invTransferToOfficer').addEventListener('change', e => {
    const position = officerPositionMap[e.target.value.trim()];
    if (position) document.getElementById('invTransferToPosition').value = position;
});

document.getElementById('invItemsBody').addEventListener('keydown', (event) => {
    const el = event.target;
    if (event.key !== 'Enter' || !event.altKey || event.ctrlKey || event.metaKey
        || !el.matches('textarea.item-desc') || el.readOnly || el.disabled || invMode === 'view') return;
    event.preventDefault();
    el.setRangeText('\n', el.selectionStart, el.selectionEnd, 'end');
    el.dispatchEvent(new Event('input', { bubbles: true }));
});

// Calling autoGrowInv() in a tight loop — once per every growable column
// (Description, Unit, Tag No., Serial No., Accountable Officer, Position,
// Location, Classification, Condition, Remarks) on every row — is what was
// actually lagging the grid on a PO with a lot of items: each call both
// WRITES a height and immediately READS scrollHeight, and interleaving those
// forces the browser to run a full synchronous layout on every single
// iteration (a 50-item PO is 500 growable fields, so 500 forced reflows back
// to back). Splitting into two passes — collapse everything first, THEN
// measure and set every final height — still does the same amount of work
// but only forces the browser to actually lay things out twice total,
// regardless of how many fields there are.
function autoGrowInvBatch(elements) {
    const els = Array.from(elements);
    els.forEach(el => { el.style.height = 'auto'; });
    els.forEach(el => { el.style.height = el.scrollHeight + 'px'; });
}

// One shared popup for every suggestable grid textarea (Accountable Officer,
// Position, Location, Classification, Condition) — see the CSS comment on
// .inv-suggest-box for why one shared instance rather than one per row.
// position:fixed + a fresh getBoundingClientRect() on every open means it
// tracks the actual field on screen even though .insp-items-scroll can be
// scrolled independently of the page.
function invSuggestBox() {
    let box = document.getElementById('invGridSuggest');
    if (!box) {
        box = document.createElement('div');
        box.id = 'invGridSuggest';
        box.className = 'inv-suggest-box d-none';
        document.body.appendChild(box);
    }
    return box;
}
function invSuggestHide() { invSuggestBox().classList.add('d-none'); }
// Hides on any scroll (the grid's own internal one included) rather than
// tracking to follow it — a suggestion list drifting away from the field
// mid-scroll would be worse than it just closing.
document.addEventListener('scroll', invSuggestHide, true);

// onSelect (optional): called after el.value is set, for a caller that needs
// to react to the pick itself — the Personnel MR search box, for one, needs
// to actually run the search, not just fill its own field like a grid cell
// does. autoGrowInv only makes sense for the grid's own textareas, hence the
// tagName guard — a plain <input> (the MR search box) has no such thing to
// grow, and forcing a height style onto one would just look wrong.
function invFieldSuggest(el, listKey, onSelect) {
    const lists = {
        officers: officerList, mr_officers: mrOfficerList, positions: positionList, locations: locationList,
        classification: classificationList, condition: conditionList,
        suppliers: supplierList, departments: locationList, categories: categoryList,
    };
    const list = lists[listKey] || [];
    const box = invSuggestBox();
    const q = el.value.trim().toLowerCase();
    // Capped at 50 for the same reason purchase_orders.php's own field
    // suggestions are — Accountable Officer alone is the full HRMIS personnel
    // list (9,000+ names), and dumping all of it into the DOM on focus before
    // anything is typed is the slow part, not the filtering.
    const matches = (q ? list.filter(v => v.toLowerCase().includes(q)) : list).slice(0, 50);
    if (!matches.length) { box.classList.add('d-none'); return; }
    box.innerHTML = matches.map(v => `<div class="inv-suggest-item"></div>`).join('');
    box.querySelectorAll('.inv-suggest-item').forEach((item, i) => {
        item.textContent = matches[i];
        // mousedown, not click — it fires before the textarea's own blur, so
        // the value is set while the field still technically has focus rather
        // than after invSuggestHide() has already run on blur.
        item.addEventListener('mousedown', (e) => {
            e.preventDefault();
            el.value = matches[i];
            if (el.tagName === 'TEXTAREA') autoGrowInv(el);
            box.classList.add('d-none');
            if (onSelect) onSelect(matches[i], el);
        });
    });
    const r = el.getBoundingClientRect();
    box.style.left  = Math.round(r.left) + 'px';
    box.style.top   = Math.round(r.bottom + 2) + 'px';
    box.style.width = Math.round(Math.max(r.width, 220)) + 'px';
    box.classList.remove('d-none');
}

// renderInvItems() runs its own autoGrowInv() pass before openModal() ever
// shows #inventoryModal — the textareas are still display:none at that point
// (Bootstrap hasn't started the fade-in yet), so scrollHeight reads back 0
// and every row locks in at one line regardless of how long its Description
// actually is. Re-measuring once the modal has actually finished showing
// fixes the height for real, without touching the render-time pass that
// still matters for a description edited after the modal is already open.
document.getElementById('inventoryModal').addEventListener('shown.bs.modal', () => {
    autoGrowInvBatch(document.querySelectorAll('#invItemsBody .item-desc, #invItemsBody .inv-grow'));
});

function recalcInvRow(el) {
    const tr = el.closest('tr');
    const qtyEl = tr.querySelector('[data-f="quantity"]');
    const costEl = tr.querySelector('[data-f="unit_cost"]');
    const totalEl = tr.querySelector('[data-f="total_cost"]');
    const q = parseFloat(qtyEl.value) || 0;
    const c = parseFloat(costEl.value) || 0;
    const total = q * c;
    totalEl.value = money(total);

    // Classification recomputes live, in front of the operator, using the
    // SAME continuous cutoffs the server applies on save — never left
    // unclassified the way the legacy grid left every row but the last one.
    const pill = tr.querySelector('[data-f="inventory_category"]');
    let cat;
    if (c < 5000) cat = 'ICS LOW VALUE';
    else if (c < 49001) cat = 'ICS HIGH VALUE';
    else cat = 'PAR';
    pill.textContent = cat;
    pill.className = 'inv-cat-pill ' + invCatPillClass(cat);

    updateInvTotals();
}

function updateInvTotals() {
    // Title rows ('SET 1') don't count as items — same reasoning as the
    // worklist's item_count on the server.
    const rows = document.querySelectorAll('#invItemsBody tr:not(.inv-item-title)');
    let total = 0;
    rows.forEach(tr => { total += parseFloat((tr.querySelector('[data-f="total_cost"]').value || '0').replace(/,/g,'')) || 0; });
    document.getElementById('invTotalItems').textContent = rows.length;
    document.getElementById('invTotalValue').textContent = '₱' + money(total);
}

document.getElementById('invCheckAll').addEventListener('change', (e) => {
    // Title rows are excluded — their checkbox is hidden (.inv-item-title
    // input{display:none}) and there is nothing on one that Issue/Transfer
    // could ever act on.
    document.querySelectorAll('#invItemsBody tr:not(.inv-item-title) .inv-row-check')
        .forEach(c => c.checked = e.target.checked);
});

function invCollectItems() {
    return Array.from(document.querySelectorAll('#invItemsBody tr')).map(tr => ({
        inv_id: tr.dataset.invId ? parseInt(tr.dataset.invId, 10) : null,
        item_id: tr.dataset.invId ? invItems[parseInt(tr.dataset.idx,10)].item_id : (invItems[parseInt(tr.dataset.idx,10)] || {}).item_id,
        items_description: tr.querySelector('[data-f="items_description"]').value.trim(),
        unit: tr.querySelector('[data-f="unit"]').value.trim(),
        quantity: tr.querySelector('[data-f="quantity"]').value,
        unit_cost: tr.querySelector('[data-f="unit_cost"]').value,
        tag_number: tr.querySelector('[data-f="tag_number"]').value.trim(),
        serial_number: tr.querySelector('[data-f="serial_number"]').value.trim(),
        accountable_officer: tr.querySelector('[data-f="accountable_officer"]').value.trim(),
        position: tr.querySelector('[data-f="position"]').value.trim(),
        location: tr.querySelector('[data-f="location"]').value.trim(),
        classification: tr.querySelector('[data-f="classification"]').value.trim(),
        condition: tr.querySelector('[data-f="condition"]').value.trim(),
        remarks: tr.querySelector('[data-f="remarks"]').value.trim(),
        number_series: tr.querySelector('[data-f="number_series"]').value.trim(),
        estimated_life: tr.querySelector('[data-f="estimated_life"]').value.trim(),
    }));
}

function invCheckedInvIds() {
    return Array.from(document.querySelectorAll('#invItemsBody tr'))
        .filter(tr => tr.querySelector('.inv-row-check').checked && tr.dataset.invId)
        .map(tr => parseInt(tr.dataset.invId, 10));
}

// ── Save ───────────────────────────────────────────────────────────────
document.getElementById('btnInvSave').addEventListener('click', async () => {
    if (invMode === 'create' && !invRecordKey) return showToast('Select a purchase order first.', 'warning');
    const items = invCollectItems();
    if (!items.length) return showToast('There are no items to save.', 'warning');

    const isManual = invMode === 'manual';
    const manualPo = isManual ? document.getElementById('manualPo').value.trim() : '';
    const manualPr = isManual ? document.getElementById('manualPr').value.trim() : '';
    if (isManual && manualPo === '') return showToast('PO Number is required for manual entry.', 'warning');
    if (isManual && manualPr === '') return showToast('PR Number is required for manual entry.', 'warning');
    const payload = {
        po_number: isManual ? manualPo : invPoNumber,
        record_key: invRecordKey,
        manual: isManual,
        pr_number: isManual ? manualPr : document.getElementById('sumPr').textContent.trim(),
        department: isManual ? document.getElementById('manualDept').value.trim() : document.getElementById('sumDept').textContent.trim(),
        category: isManual ? document.getElementById('manualCategory').value.trim() : document.getElementById('sumCategory').textContent.trim(),
        mode_of_procurement: document.getElementById('invMode').value.trim(),
        supplier_name: isManual ? document.getElementById('manualSupplier').value.trim() : document.getElementById('sumSupplier').textContent.trim(),
        supplier_address: '',
        purpose: document.getElementById('invPurpose').value.trim(),
        source_of_fund: document.getElementById('invSource').value.trim(),
        date_entry: document.getElementById('invDateEntry').value,
        initial: document.getElementById('invInitial').value.trim(),
        items,
    };

    try {
        const res = await fetch(apiUrl, {
            method: invMode === 'revise' ? 'PUT' : 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload),
        });
        const out = await res.json();
        if (!out.success) return showToast(out.message || 'Save failed.', 'error');
        showToast(out.message || 'Saved.', 'success');
        bootstrap.Modal.getInstance(document.getElementById('inventoryModal'))?.hide();
        loadList(); loadStats();
    } catch (err) {
        showToast('Save failed.', 'error');
        console.error(err);
    }
});

// ── Undo ──────────────────────────────────────────────────────────────
document.getElementById('btnInvUndo').addEventListener('click', async () => {
    const msg = invPrevious
        ? `Undo the last revision on PO ${invPoNumber}? This restores the version saved on ${fmtDate(invPrevious.date_created)}.`
        : `Undo — this removes PO ${invPoNumber} from Inventory entirely, and it will show as not yet received. Continue?`;
    if (!confirm(msg)) return;
    try {
        const res = await fetch(apiUrl, {
            method: 'PATCH', headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ action: 'undo', record_key: invRecordKey }),
        });
        const out = await res.json();
        if (!out.success) return showToast(out.message || 'Undo failed.', 'error');
        showToast(out.message, 'success');
        bootstrap.Modal.getInstance(document.getElementById('inventoryModal'))?.hide();
        loadList(); loadStats();
    } catch (err) {
        showToast('Undo failed.', 'error');
        console.error(err);
    }
});

// ── Sub-details (unit breakdown) ────────────────────────────────────────
let invSubItemIdx = null;

function invOpenSubDetails(idx) {
    invSubItemIdx = idx;
    const it = invItems[idx];
    renderInvSubRows(it.sub_details && it.sub_details.length ? it.sub_details.map(s => ({
        items_description: s.items_description, unit_cost: s.unit_cost, serial_number: s.serial_number,
        tag_number: s.tag_number, accountable_officer: s.accountable_officer, location: s.location,
        condition: s.condition, remarks: s.remarks,
    })) : []);
    openModal('invSubModal');
}

document.getElementById('btnInvSubDetails').addEventListener('click', () => {
    const checked = Array.from(document.querySelectorAll('#invItemsBody tr')).findIndex(tr => tr.querySelector('.inv-row-check').checked);
    if (checked < 0) return showToast('Check one item row first.', 'warning');
    invOpenSubDetails(checked);
});

function renderInvSubRows(rows) {
    document.getElementById('invSubBody').innerHTML = rows.map(r => invSubRowHtml(r)).join('');
}
function invSubRowHtml(r) {
    r = r || {};
    return `<tr>
        <td><input type="text" value="${escapeHtml(r.items_description||'')}" data-sf="items_description"></td>
        <td><input type="number" step="0.01" value="${r.unit_cost ?? ''}" data-sf="unit_cost"></td>
        <td><input type="text" value="${escapeHtml(r.serial_number||'')}" data-sf="serial_number"></td>
        <td><input type="text" value="${escapeHtml(r.tag_number||'')}" data-sf="tag_number"></td>
        <td><input type="text" value="${escapeHtml(r.accountable_officer||'')}" data-sf="accountable_officer" list="invDlOfficers"></td>
        <td><input type="text" value="${escapeHtml(r.location||'')}" data-sf="location" list="invDlLocations"></td>
        <td><input type="text" value="${escapeHtml(r.condition||'')}" data-sf="condition" list="invDlCondition"></td>
        <td><input type="text" value="${escapeHtml(r.remarks||'')}" data-sf="remarks"></td>
        <td><button type="button" class="btn btn-sm btn-outline-danger" onclick="this.closest('tr').remove()"><i class="fas fa-trash"></i></button></td>
    </tr>`;
}
document.getElementById('btnInvSubAdd').addEventListener('click', () => {
    document.getElementById('invSubBody').insertAdjacentHTML('beforeend', invSubRowHtml({}));
});

document.getElementById('btnInvSubSave').addEventListener('click', async () => {
    const it = invItems[invSubItemIdx];
    if (!it || !it.inv_id) return showToast('Save the record first, then add a unit breakdown.', 'warning');
    const rows = Array.from(document.querySelectorAll('#invSubBody tr')).map(tr => ({
        items_description: tr.querySelector('[data-sf="items_description"]').value.trim(),
        unit_cost: tr.querySelector('[data-sf="unit_cost"]').value,
        serial_number: tr.querySelector('[data-sf="serial_number"]').value.trim(),
        tag_number: tr.querySelector('[data-sf="tag_number"]').value.trim(),
        accountable_officer: tr.querySelector('[data-sf="accountable_officer"]').value.trim(),
        location: tr.querySelector('[data-sf="location"]').value.trim(),
        condition: tr.querySelector('[data-sf="condition"]').value.trim(),
        remarks: tr.querySelector('[data-sf="remarks"]').value.trim(),
    }));
    try {
        const res = await fetch(apiUrl, {
            method: 'PATCH', headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ action: 'sub_details', inv_id: it.inv_id, po_number: invRecordKey, rows }),
        });
        const out = await res.json();
        if (!out.success) return showToast(out.message || 'Save failed.', 'error');
        showToast('Unit breakdown saved.', 'success');
        it.sub_details = rows;
        bootstrap.Modal.getInstance(document.getElementById('invSubModal'))?.hide();
    } catch (err) {
        showToast('Save failed.', 'error');
        console.error(err);
    }
});

// ── Issue PAR / ICS ──────────────────────────────────────────────────────
// What Renew will turn a number into, for the notice only — the server builds the
// real one (lib/par_numbers.php: strip every trailing -R<year>, add this year's).
function renewedPreview(number) {
    return String(number).trim().replace(/(?:-R[0-9]{4})+$/, '') + '-R' + new Date().getFullYear();
}

document.getElementById('btnInvIssue').addEventListener('click', () => {
    const invIds = invCheckedInvIds();
    if (!invIds.length) return showToast('Check at least one item row first.', 'warning');

    const rows = Array.from(document.querySelectorAll('#invItemsBody tr')).filter(tr => invIds.includes(parseInt(tr.dataset.invId,10)));
    const cats = new Set(rows.map(tr => tr.querySelector('[data-f="inventory_category"]').textContent.trim()));
    document.getElementById('invIssueScope').textContent = `${invIds.length} item(s) selected on PO ${invPoNumber}.`;

    if (cats.size === 1) {
        const cat = [...cats][0];
        document.getElementById(cat === 'PAR' ? 'invDocPar' : 'invDocIcs').checked = true;
    }
    const numbers = new Set(rows.map(tr => {
        const p = tr.querySelector('.inv-number-pill'); return p && !p.classList.contains('empty') ? p.textContent.trim() : '';
    }).filter(Boolean));
    document.getElementById('invModeNew').checked = numbers.size === 0;
    document.getElementById('invModeReprint').disabled = numbers.size !== 1;
    document.getElementById('invModeRenew').disabled = numbers.size !== 1;
    if (numbers.size === 1) document.getElementById('invModeReprint').checked = true;

    // Every three years the number is due to be renewed and reprinted. When the
    // selected lines are, say so and pre-select Renew (a suggestion — Reprint
    // and New are still one click away). Judged by the server (renew_due), not here.
    const dueNote = document.getElementById('invIssueDueNote');
    const duePills = rows.map(tr => tr.querySelector('.inv-number-pill.due')).filter(Boolean);
    if (numbers.size === 1 && duePills.length) {
        const year = duePills[0].dataset.issuedYear;
        dueNote.innerHTML = `<i class="fas fa-rotate me-1"></i><strong>Due for renewal.</strong> This number was ${year ? `issued or last renewed in <strong>${escapeHtml(year)}</strong>` : 'issued 3 or more years ago'} — it is renewed and reprinted every 3 years. <strong>Renew</strong> is selected; the number becomes <strong>${escapeHtml(renewedPreview([...numbers][0]))}</strong>.`;
        dueNote.classList.remove('d-none');
        document.getElementById('invModeRenew').checked = true;
    } else {
        dueNote.classList.add('d-none');
    }

    document.getElementById('invIssueCgso').value = cgsoOfficer ? cgsoOfficer.name : '';
    document.getElementById('invIssueCgsoPosition').value = cgsoOfficer ? (cgsoOfficer.position || '') : '';
    // Cleared on every open, not remembered across POs — a stale property
    // number carrying over to the next document is worse than a blank box.
    document.getElementById('invIssuePropertyNumber').value = '';
    document.getElementById('invIssuedResult').classList.add('d-none');
    // Starts as the record's Date Entry — the same year/month the server used
    // on its own before this box existed, so an untouched box changes nothing.
    const monthBox = document.getElementById('invIssueNumberMonth');
    const now = new Date();
    monthBox.max = `${now.getFullYear()}-${String(now.getMonth() + 1).padStart(2, '0')}`;
    monthBox.value = (document.getElementById('invDateEntry').value || '').slice(0, 7) || monthBox.max;
    // Emptied on every open so it refills with a fresh suggestion — a number
    // typed for one document must never carry over onto the next.
    document.getElementById('invIssuePaperNumber').value = '';
    invSuggestedNumber = '';
    document.getElementById('invIssueModal').dataset.invIds = JSON.stringify(invIds);
    refreshIssueNumberPreview();
    openModal('invIssueModal');
});

// The Number box shows only on a MANUAL entry (old paperwork encoded now; a
// record received from a PO numbers by its Date Entry as it always has), and
// only for a New number (Reprint keeps the one on file; Renew adds -R<year>).
//
// One editable box: it is filled with the next number for the picked year and
// month, and can be typed over with the number printed on the original paper.
// Left as suggested, the server allocates it at issue time (the serial can move
// on if someone else issues first); typed over, it is checked live against the
// same rules the issue applies (format, PAR/ICS match, not on another record).
let invNumberPreviewTimer = null, invNumberPreviewSeq = 0;
let invSuggestedNumber = '';

function invNumberIsSuggested() {
    const typed = document.getElementById('invIssuePaperNumber').value.trim().toUpperCase();
    return typed === '' || typed === invSuggestedNumber;
}

// Year/month or PAR/ICS changed: fetch a fresh suggestion, and put it in the
// box unless someone has already typed their own number there.
function refreshIssueNumberPreview() {
    const show = invIsManual && document.getElementById('invModeNew').checked;
    document.getElementById('invIssueNumberGroup').classList.toggle('d-none', !show);
    if (!show) return;
    const docType = (document.querySelector('input[name="invDocType"]:checked') || {}).value;
    document.getElementById('invIssueNumberLabel').textContent = (docType ? docType.toUpperCase() : 'PAR/ICS') + ' Number';
    const month = document.getElementById('invIssueNumberMonth').value;
    const out = document.getElementById('invIssueNumberPreview');
    out.classList.remove('bad');
    clearTimeout(invNumberPreviewTimer);
    const seq = ++invNumberPreviewSeq;
    if (!docType) { out.textContent = 'Choose PAR or ICS to see the number.'; return; }
    if (!month) { out.textContent = 'Pick a year and month for the suggestion.'; out.classList.add('bad'); return; }
    out.textContent = 'Checking…';
    invNumberPreviewTimer = setTimeout(async () => {
        try {
            const res = await fetch(`${issuanceUrl}?next_number=1&doc_type=${docType}&month=${encodeURIComponent(month)}`);
            const data = await res.json();
            if (seq !== invNumberPreviewSeq) return;   // a newer pick is already on its way
            if (!data.success) { out.textContent = data.message || 'Unable to suggest a number.'; out.classList.add('bad'); return; }
            const box = document.getElementById('invIssuePaperNumber');
            const keepTyped = !invNumberIsSuggested();
            invSuggestedNumber = data.number;
            if (!keepTyped) box.value = data.number;
            keepTyped ? checkTypedIssueNumber() : showSuggestedNote();
        } catch (err) {
            if (seq === invNumberPreviewSeq) out.textContent = '';
            console.error(err);
        }
    }, 250);
}

function showSuggestedNote() {
    const out = document.getElementById('invIssueNumberPreview');
    out.classList.remove('bad');
    out.innerHTML = `<i class="fas fa-wand-magic-sparkles me-1"></i>Suggested — the next number for that month.`;
}

// The box was typed in: check what is there now.
function checkTypedIssueNumber() {
    const out = document.getElementById('invIssueNumberPreview');
    const docType = (document.querySelector('input[name="invDocType"]:checked') || {}).value;
    const typed = document.getElementById('invIssuePaperNumber').value.trim();
    clearTimeout(invNumberPreviewTimer);
    const seq = ++invNumberPreviewSeq;
    out.classList.remove('bad');
    if (invNumberIsSuggested()) {
        if (!typed && invSuggestedNumber) {
            out.textContent = 'Empty — the suggested number will be used.';
        } else {
            showSuggestedNote();
        }
        return;
    }
    if (!docType) { out.textContent = 'Choose PAR or ICS first.'; return; }
    out.textContent = 'Checking…';
    const invIds = JSON.parse(document.getElementById('invIssueModal').dataset.invIds || '[]');
    invNumberPreviewTimer = setTimeout(async () => {
        try {
            const res = await fetch(`${issuanceUrl}?check_paper_number=${encodeURIComponent(typed)}&doc_type=${docType}`
                + `&po_number=${encodeURIComponent(invRecordKey)}&inv_ids=${invIds.join(',')}`);
            const data = await res.json();
            if (seq !== invNumberPreviewSeq) return;
            if (!data.success) { out.textContent = data.message || 'Unable to check the number.'; out.classList.add('bad'); return; }
            out.innerHTML = `<i class="fas fa-pen me-1"></i>Typed from the paper — will be issued as <span class="num">${escapeHtml(data.number)}</span>`;
        } catch (err) {
            if (seq === invNumberPreviewSeq) out.textContent = '';
            console.error(err);
        }
    }, 300);
}
document.querySelectorAll('input[name="invMode2"], input[name="invDocType"]').forEach(r =>
    r.addEventListener('change', refreshIssueNumberPreview));
document.getElementById('invIssueNumberMonth').addEventListener('input', refreshIssueNumberPreview);
document.getElementById('invIssuePaperNumber').addEventListener('input', checkTypedIssueNumber);

document.getElementById('btnInvIssueGo').addEventListener('click', async () => {
    const invIds = JSON.parse(document.getElementById('invIssueModal').dataset.invIds || '[]');
    const docType = document.querySelector('input[name="invDocType"]:checked').value;
    const layout = document.querySelector('input[name="invLayout"]:checked').value;
    const mode = document.querySelector('input[name="invMode2"]:checked').value;
    // Manual entry, New: the Number box as left by the user. Still the
    // suggestion (or emptied) → the server allocates the next number for the
    // picked year/month. Typed over → that exact number, checked again there.
    const backEncode = mode === 'new' && invIsManual;
    const fromPaper = backEncode && !invNumberIsSuggested();
    const paperNumber = fromPaper ? document.getElementById('invIssuePaperNumber').value.trim() : '';

    try {
        const res = await fetch(issuanceUrl, {
            method: 'POST', headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ action: 'issue', doc_type: docType, mode, po_number: invRecordKey, inv_ids: invIds,
                number_month: backEncode && !fromPaper ? document.getElementById('invIssueNumberMonth').value : '',
                paper_number: paperNumber }),
        });
        const out = await res.json();
        if (!out.success) return showToast(out.message || 'Could not issue.', 'error');

        document.getElementById('invIssuedNumber').textContent = out.number;
        document.getElementById('invIssuedResult').classList.remove('d-none');
        showToast(out.message, 'success');

        // Reflect the new/renewed number straight into the grid without a
        // full reload, then render the printable document from the same data.
        invIds.forEach(id => {
            const tr = document.querySelector(`#invItemsBody tr[data-inv-id="${id}"]`);
            if (!tr) return;
            const cell = tr.querySelector('td:nth-child(11)');
            cell.innerHTML = `<span class="inv-number-pill">${escapeHtml(out.number)}</span>`;
        });

        const rows = invIds.map(id => {
            const tr = document.querySelector(`#invItemsBody tr[data-inv-id="${id}"]`);
            return invRowToPrintData(tr);
        });
        const cgso = { name: document.getElementById('invIssueCgso').value.trim(), position: document.getElementById('invIssueCgsoPosition').value.trim() };
        // ICS carries a HIGH VALUE / LOW VALUE subtitle on the real form —
        // read straight off the rows being issued rather than asking the
        // operator to say it twice.
        const bucket = (rows[0] || {}).inventory_category === 'ICS LOW VALUE' ? 'LOW VALUE' : 'HIGH VALUE';
        await renderInvDocument(docType === 'par' ? 'par' : 'ics', {
            rows, number: out.number, layout, cgso, bucket,
            poNumber: invPoNumber,
            department: document.getElementById('sumDept').textContent.trim(),   // prints as Entity Name
            supplier: document.getElementById('sumSupplier').textContent.trim(),
            purpose: document.getElementById('invPurpose').value.trim(),
            sourceOfFund: document.getElementById('invSource').value.trim(),
            dateAcquired: document.getElementById('invDateEntry').value,
            initial: document.getElementById('invInitial').value.trim(),
            propertyNumber: document.getElementById('invIssuePropertyNumber').value.trim(),
        });
        loadList(); loadStats();
    } catch (err) {
        showToast('Could not issue.', 'error');
        console.error(err);
    }
});

// ── Fix Names ─────────────────────────────────────────────────────────
// Merging the several spellings a custodian is saved under. Every merge is the
// user's own choice — tick the spellings, pick the one to keep, press Merge —
// and api/officer_cleanup.php previews it, writes an undo record, and re-checks
// everything server-side. See that file for what is and is not rewritten.
const fnApiUrl = 'api/officer_cleanup.php';
let fnSeq = 0;          // gives every card its own radio group

// Read as text first: a PHP fatal comes back as an HTML page, and res.json() on
// that says nothing about what went wrong.
async function fnRequest(url, options) {
    const res = await fetch(url, options);
    const raw = await res.text();
    try { return JSON.parse(raw); }
    catch { return { success: false, message: `The server returned an error (HTTP ${res.status}).` }; }
}
const fnGet  = query => fnRequest(`${fnApiUrl}?${query}`);
const fnPost = body  => fnRequest(fnApiUrl, { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(body) });

function fnTab(name) {
    document.querySelectorAll('#fnTabs .nav-link').forEach(b => b.classList.toggle('active', b.dataset.tab === name));
    document.getElementById('fnPaneSuggested').classList.toggle('d-none', name !== 'suggested');
    document.getElementById('fnPaneFind').classList.toggle('d-none', name !== 'find');
    document.getElementById('fnPaneHistory').classList.toggle('d-none', name !== 'history');
    if (name === 'history') fnLoadHistory();
}

// One card = one person's spellings. Include (tick) says which are the same
// person; Keep (radio) says which spelling survives. Suggested cards start with
// everything ticked and the most-used spelling kept; a hand-searched card
// starts with nothing ticked, because nothing has been decided yet.
function fnCardHtml(variants, keepName, manual) {
    const group = 'fnkeep' + (++fnSeq);
    const rows = variants.map(v => `
        <label class="fn-row">
            <input type="checkbox" class="fn-inc" value="${escapeHtml(v.name)}"${manual ? '' : ' checked'}>
            <input type="radio" class="fn-keep" name="${group}" value="${escapeHtml(v.name)}"${v.name === keepName ? ' checked' : ''}>
            <span class="fn-n">${v.n}</span>
            <span class="fn-name">${escapeHtml(v.name)}</span>
        </label>`).join('');
    return `<div class="fn-card">
        <div class="fn-head">
            <span class="small fn-summary"></span>
            <button type="button" class="btn btn-sm btn-warning ms-auto fn-merge">Merge</button>
        </div>
        <div class="fn-cols"><span>Incl.</span><span>Keep</span><span class="text-end">Items</span><span>Saved spelling</span></div>
        ${rows}
    </div>`;
}

function fnRefreshCard(card) {
    const keepRadio = card.querySelector('.fn-keep:checked');
    // The spelling being kept is always part of the merge.
    if (keepRadio) keepRadio.closest('.fn-row').querySelector('.fn-inc').checked = true;
    let included = 0, moving = 0;
    card.querySelectorAll('.fn-row').forEach(row => {
        const inc = row.querySelector('.fn-inc').checked;
        const keep = row.querySelector('.fn-keep').checked;
        row.classList.toggle('is-keep', keep);
        row.classList.toggle('is-out', !inc);
        if (inc) { included++; if (!keep) moving += parseInt(row.querySelector('.fn-n').textContent, 10) || 0; }
    });
    const ok = included >= 2 && !!keepRadio;
    card.querySelector('.fn-merge').disabled = !ok;
    const others = included - 1;
    card.querySelector('.fn-summary').innerHTML = !keepRadio
        ? '<span class="text-muted">Tick the spellings that are the same person, then choose the one to keep.</span>'
        : ok ? `Keep <span class="fn-keep-name">${escapeHtml(keepRadio.value)}</span> · ${others} other spelling${others === 1 ? '' : 's'} · about ${moving} item${moving === 1 ? '' : 's'} to update`
             : '<span class="text-muted">Tick at least two spellings.</span>';
}

async function fnLoadGroups() {
    const box = document.getElementById('fnSuggestedList');
    const data = await fnGet('');
    if (!data.success) { box.textContent = data.message || 'Unable to load.'; return; }
    const groups = data.groups || [];
    document.getElementById('fnSuggestedCount').textContent = groups.length;
    box.innerHTML = groups.length
        ? groups.map(g => fnCardHtml(g.variants, g.target, false)).join('')
        : 'Nothing to suggest — no one is saved under spellings close enough to match automatically. Use “Find a name” for anything else.';
    box.querySelectorAll('.fn-card').forEach(fnRefreshCard);
    fnApplyFilter();
}

function fnApplyFilter() {
    const q = document.getElementById('fnFilter').value.trim().toLowerCase();
    document.querySelectorAll('#fnSuggestedList .fn-card').forEach(c => {
        c.style.display = c.textContent.toLowerCase().includes(q) ? '' : 'none';
    });
}

let fnSearchTimer = null;
async function fnRunSearch() {
    const q = document.getElementById('fnSearch').value.trim();
    const box = document.getElementById('fnFindResult');
    if (q.length < 2) { box.textContent = ''; return; }
    const data = await fnGet('search=' + encodeURIComponent(q));
    if (!data.success) { box.textContent = data.message || 'Unable to search.'; return; }
    const names = data.names || [];
    box.innerHTML = names.length >= 2 ? fnCardHtml(names, '', true)
        : names.length ? 'Only one saved spelling matches — there is nothing to merge.' : 'No saved name matches.';
    box.querySelectorAll('.fn-card').forEach(fnRefreshCard);
}

async function fnMergeCard(btn) {
    const card = btn.closest('.fn-card');
    const names = [...card.querySelectorAll('.fn-inc:checked')].map(i => i.value);
    const keep = card.querySelector('.fn-keep:checked')?.value;
    if (names.length < 2 || !keep) return showToast('Tick at least two spellings and choose the one to keep.', 'warning');
    btn.disabled = true;
    try {
        // Asked of the server first, so the confirmation quotes the real number
        // of rows rather than the screen's estimate.
        const pre = await fnPost({ action: 'preview', names, target: keep });
        if (!pre.success) return showToast(pre.message || 'Could not check that merge.', 'error');
        if (!pre.rows) return showToast('Nothing to change — those are already spelled the same.', 'warning');
        if (!confirm(`Change ${pre.rows} item row${pre.rows === 1 ? '' : 's'} to “${keep}”?\n\nSpellings being merged:\n  • ${names.join('\n  • ')}\n\nYou can undo this from History.`)) return;
        const res = await fnPost({ action: 'merge', names, target: keep });
        if (!res.success) return showToast(res.message || 'Nothing was changed.', 'error');
        showToast(res.message);
        fnAfterChange();
    } catch (err) {
        showToast('The merge did not go through: ' + (err.message || err), 'error');
        console.error(err);
    } finally {
        btn.disabled = false;
    }
}

// Refreshes everything that shows names: this dialog, and the suggestion lists
// behind the rest of the screen (the Personnel MR search in particular).
function fnAfterChange() {
    fnLoadGroups();
    fnRunSearch();
    loadSignatories();
    if (!document.getElementById('fnPaneHistory').classList.contains('d-none')) fnLoadHistory();
}

async function fnLoadHistory() {
    const box = document.getElementById('fnHistoryList');
    const data = await fnGet('undo_list=1');
    if (!data.success) { box.textContent = data.message || 'Unable to load.'; return; }
    const merges = data.merges || [];
    box.innerHTML = merges.length ? merges.map(m => {
        const others = (m.names || []).filter(n => n !== m.target).join('; ');
        const when = m.at ? new Date(m.at).toLocaleString() : '';
        return `<div class="fn-card"><div class="fn-head">
            <span><strong>${escapeHtml(m.target)}</strong> <span class="text-muted">←</span> ${escapeHtml(others)}</span>
            <span class="text-muted small">${escapeHtml(when)} · ${escapeHtml(m.by)} · ${m.rows} row${m.rows === 1 ? '' : 's'}</span>
            ${m.undone_at ? '<span class="badge bg-secondary ms-auto">Undone</span>'
                          : `<button type="button" class="btn btn-sm btn-outline-danger ms-auto" onclick="fnUndo('${escapeHtml(m.file)}')">Undo</button>`}
        </div></div>`;
    }).join('') : 'No merges yet.';
}

async function fnUndo(file) {
    if (!confirm('Put the names back the way they were before this merge?\n\nAny row edited since is left as it is.')) return;
    const res = await fnPost({ action: 'undo', file });
    showToast(res.message || 'Done.', res.success ? 'success' : 'error');
    if (res.success) fnAfterChange();
    fnLoadHistory();
}

document.getElementById('btnFixNames')?.addEventListener('click', () => {
    openModal('invFixNamesModal');
    fnTab('suggested');
    fnLoadGroups();
});
document.getElementById('fnTabs').addEventListener('click', e => { const b = e.target.closest('[data-tab]'); if (b) fnTab(b.dataset.tab); });
document.getElementById('fnFilter').addEventListener('input', fnApplyFilter);
document.getElementById('fnSearch').addEventListener('input', () => { clearTimeout(fnSearchTimer); fnSearchTimer = setTimeout(fnRunSearch, 300); });
// Delegated: the cards are rebuilt on every load and search.
document.getElementById('invFixNamesModal').addEventListener('change', e => { const c = e.target.closest('.fn-card'); if (c) fnRefreshCard(c); });
document.getElementById('invFixNamesModal').addEventListener('click', e => { const b = e.target.closest('.fn-merge'); if (b) fnMergeCard(b); });

// ── Transfer ──────────────────────────────────────────────────────────
// What an ITR is almost always for here: property moving to a barangay, whose
// accounting books it against specific account titles. Filled in for an ITR so
// nobody retypes it, but it is only a starting value — the box stays editable.
const INV_ITR_DEFAULT_REASON = 'FOR BOOKING TO BARANGAY ACCOUNTING TO SPECIFIC ACCOUNT TITLES';

// Switching ITR <-> PTR swaps the default in or out, but only while the box
// still holds the default (or nothing): a reason someone has typed is theirs.
document.querySelectorAll('input[name="invTransferType"]').forEach(radio => radio.addEventListener('change', () => {
    const box = document.getElementById('invTransferReason');
    const isItr = document.querySelector('input[name="invTransferType"]:checked').value === 'itr';
    if (isItr && box.value.trim() === '') box.value = INV_ITR_DEFAULT_REASON;
    else if (!isItr && box.value.trim() === INV_ITR_DEFAULT_REASON) box.value = '';
}));

document.getElementById('btnInvTransfer').addEventListener('click', () => {
    const invIds = invCheckedInvIds();
    if (!invIds.length) return showToast('Check at least one item row first.', 'warning');
    const rows = Array.from(document.querySelectorAll('#invItemsBody tr')).filter(tr => invIds.includes(parseInt(tr.dataset.invId,10)));
    document.getElementById('invTransferScope').textContent = `${invIds.length} item(s) selected on PO ${invPoNumber}.`;
    document.getElementById('invTransferFrom').value = rows[0].querySelector('[data-f="accountable_officer"]').value + ' · ' + rows[0].querySelector('[data-f="location"]').value;
    document.getElementById('invTransferToOfficer').value = '';
    document.getElementById('invTransferToPosition').value = '';
    document.getElementById('invTransferToLocation').value = '';
    document.getElementById('invTransferFundCluster').value = '';
    document.getElementById('invTransferReason').value =
        document.querySelector('input[name="invTransferType"]:checked').value === 'itr' ? INV_ITR_DEFAULT_REASON : '';
    document.getElementById('invKindReassign').checked = true;
    document.getElementById('invTransferApprovedBy').value = cgsoOfficer ? cgsoOfficer.name : '';
    // Released/Issued by is the Warehouse Officer on file, not left for the
    // operator to type on every transfer. Editable, like every signatory field.
    document.getElementById('invTransferIssuedBy').value = warehouseOfficer ? warehouseOfficer.name : '';
    document.getElementById('invTransferIssuedPosition').value = warehouseOfficer ? (warehouseOfficer.position || '') : '';
    document.getElementById('invTransferResult').classList.add('d-none');
    document.getElementById('invTransferModal').dataset.invIds = JSON.stringify(invIds);
    openModal('invTransferModal');
});

document.getElementById('btnInvTransferGo').addEventListener('click', async () => {
    const invIds = JSON.parse(document.getElementById('invTransferModal').dataset.invIds || '[]');
    const docType = document.querySelector('input[name="invTransferType"]:checked').value;
    const transferKind = document.querySelector('input[name="invTransferKind"]:checked').value;
    const toOfficer = document.getElementById('invTransferToOfficer').value.trim();
    if (!toOfficer) return showToast('The receiving officer is required.', 'warning');

    // The real ITR/PTR forms check one box — Donation / Reassignment /
    // Relocation / Other — separately from the free-text reason. Rather than
    // a new column, the checked box rides along as a bracketed tag on the
    // same Reason_for_Transfer text the transfer table already has.
    const reasonText = document.getElementById('invTransferReason').value.trim();
    const reason = '[' + transferKind + ']' + (reasonText ? ' ' + reasonText : '');

    const payload = {
        action: 'transfer', doc_type: docType, po_number: invRecordKey, inv_ids: invIds,
        to_officer: toOfficer,
        to_position: document.getElementById('invTransferToPosition').value.trim(),
        to_location: document.getElementById('invTransferToLocation').value.trim(),
        fund_cluster: document.getElementById('invTransferFundCluster').value.trim(),
        reason,
        approved_by: document.getElementById('invTransferApprovedBy').value.trim(),
        approved_position: cgsoOfficer ? (cgsoOfficer.position || '') : '',
        issued_by: document.getElementById('invTransferIssuedBy').value.trim(),
        issued_position: document.getElementById('invTransferIssuedPosition').value.trim(),
    };

    try {
        const res = await fetch(issuanceUrl, {
            method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(payload),
        });
        const out = await res.json();
        if (!out.success) return showToast(out.message || 'Transfer failed.', 'error');
        document.getElementById('invTransferNumber').textContent = out.number;
        document.getElementById('invTransferResult').classList.remove('d-none');
        showToast(out.message, 'success');

        const rows = invIds.map(id => {
            const tr = document.querySelector(`#invItemsBody tr[data-inv-id="${id}"]`);
            const d = invRowToPrintData(tr);
            return d;
        });
        await renderInvDocument(docType, {
            rows, number: out.number,
            from: document.getElementById('invTransferFrom').value,
            to: `${toOfficer} · ${document.getElementById('invTransferToLocation').value.trim()}`,
            toOfficer, toPosition: payload.to_position, toLocation: payload.to_location,
            fundCluster: payload.fund_cluster, reason: reasonText, transferKind,
            department: document.getElementById('sumDept').textContent.trim(),
            supplier: document.getElementById('sumSupplier').textContent.trim(),
            purpose: document.getElementById('invPurpose').value.trim(),
            approvedBy: payload.approved_by, approvedPosition: payload.approved_position,
            issuedBy: payload.issued_by, issuedPosition: payload.issued_position,
            toOfficerPosition: payload.to_position,
            cgso: cgsoOfficer,
        });
        loadList(); loadStats();
    } catch (err) {
        showToast('Transfer failed.', 'error');
        console.error(err);
    }
});

// ── Return to Stock (PRS) ────────────────────────────────────────────────
document.getElementById('btnInvReturn').addEventListener('click', () => {
    const invIds = invCheckedInvIds();
    if (!invIds.length) return showToast('Check at least one item row first.', 'warning');
    const rows = Array.from(document.querySelectorAll('#invItemsBody tr')).filter(tr => invIds.includes(parseInt(tr.dataset.invId,10)));
    document.getElementById('invReturnScope').textContent = `${invIds.length} item(s) selected on PO ${invPoNumber}.`;
    document.getElementById('invReturnFrom').value = rows[0].querySelector('[data-f="accountable_officer"]').value + ' · ' + rows[0].querySelector('[data-f="location"]').value;
    document.getElementById('invReturnCondition').value = rows[0].querySelector('[data-f="condition"]').value || '';
    document.getElementById('invReturnToLocation').value = 'GSO Storage';
    document.getElementById('invReturnReason').value = '';
    document.getElementById('invReturnReceivedBy').value = cgsoOfficer ? cgsoOfficer.name : '';
    document.getElementById('invReturnReceivedPosition').value = cgsoOfficer ? (cgsoOfficer.position || '') : '';
    document.getElementById('invReturnApprovedBy').value = cgsoOfficer ? cgsoOfficer.name : '';
    document.getElementById('invReturnResult').classList.add('d-none');
    document.getElementById('invReturnModal').dataset.invIds = JSON.stringify(invIds);
    openModal('invReturnModal');
});

document.getElementById('btnInvReturnGo').addEventListener('click', async () => {
    const invIds = JSON.parse(document.getElementById('invReturnModal').dataset.invIds || '[]');
    const returnedTo = document.getElementById('invReturnToLocation').value.trim();
    if (!returnedTo) return showToast('Where the property is returned to is required.', 'warning');
    const fromValue = document.getElementById('invReturnFrom').value;
    const reasonText = document.getElementById('invReturnReason').value.trim();
    const condition = document.getElementById('invReturnCondition').value.trim();
    const receivedBy = document.getElementById('invReturnReceivedBy').value.trim();
    const receivedPosition = document.getElementById('invReturnReceivedPosition').value.trim();

    const payload = {
        action: 'prs', po_number: invRecordKey, inv_ids: invIds,
        returned_to: returnedTo, condition, reason: reasonText,
        received_by: receivedBy, received_position: receivedPosition,
        approved_by: document.getElementById('invReturnApprovedBy').value.trim(),
        approved_position: cgsoOfficer ? (cgsoOfficer.position || '') : '',
    };

    try {
        const res = await fetch(issuanceUrl, {
            method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(payload),
        });
        const out = await res.json();
        if (!out.success) return showToast(out.message || 'Return failed.', 'error');
        document.getElementById('invReturnNumber').textContent = out.number;
        document.getElementById('invReturnResult').classList.remove('d-none');
        showToast(out.message, 'success');

        const rows = invIds.map(id => {
            const tr = document.querySelector(`#invItemsBody tr[data-inv-id="${id}"]`);
            return invRowToPrintData(tr);
        });
        await renderInvDocument('prs', {
            rows, number: out.number, from: fromValue, reason: reasonText, condition,
            returnedTo, receivedBy, receivedPosition,
            approvedBy: payload.approved_by, approvedPosition: payload.approved_position,
            department: document.getElementById('sumDept').textContent.trim(),
            supplier: document.getElementById('sumSupplier').textContent.trim(),
            poNumber: invPoNumber,
            cgso: cgsoOfficer,
        });
        loadList(); loadStats();
    } catch (err) {
        showToast('Return failed.', 'error');
        console.error(err);
    }
});

// ── Transfer history ─────────────────────────────────────────────────────
document.getElementById('btnInvHistory').addEventListener('click', async () => {
    const body = document.getElementById('invHistoryBody');
    body.innerHTML = '<div class="text-center py-4"><div class="spinner-border text-primary"></div></div>';
    openModal('invHistoryModal');
    try {
        const res = await fetch(`${issuanceUrl}?transfers=1&po_number=${encodeURIComponent(invRecordKey)}`);
        const data = await res.json();
        if (!data.success) { body.innerHTML = `<div class="inv-empty">${escapeHtml(data.message||'Unable to load history.')}</div>`; return; }
        const transfers = data.transfers || [];
        if (!transfers.length) { body.innerHTML = '<div class="inv-empty">No transfers recorded yet for this purchase order.</div>'; return; }
        body.innerHTML = transfers.map(t => `
            <div class="inv-hist-item">
                <div class="inv-hist-num">${escapeHtml(t.transfer_type)} ${escapeHtml(t.transfer_number)}</div>
                <div class="inv-hist-meta">${fmtDate(t.date_transferred)} · printed by ${escapeHtml(t.printed_by||'—')}</div>
                <div class="inv-hist-flow"><span class="from">${escapeHtml(t.from_officer||'—')} (${escapeHtml(t.from_location||'—')})</span>
                    <i class="fas fa-arrow-right mx-1"></i>
                    <span class="to">${escapeHtml(t.to_officer||'—')} (${escapeHtml(t.to_location||'—')})</span></div>
                ${t.reason ? `<div class="inv-hist-meta">Reason: ${escapeHtml(t.reason)}</div>` : ''}
                <div class="inv-hist-meta">${(t.items||[]).length} item(s): ${(t.items||[]).map(i => escapeHtml(i.items_description)).join(', ')}</div>
            </div>`).join('');
    } catch (err) {
        body.innerHTML = '<div class="inv-empty">Unable to load history.</div>';
        console.error(err);
    }
});

// ── Property Returns list (cross-PO) ─────────────────────────────────────
let prsListData = [];
document.getElementById('btnPrsList').addEventListener('click', async () => {
    const body = document.getElementById('invPrsListBody');
    body.innerHTML = '<div class="text-center py-4"><div class="spinner-border text-primary"></div></div>';
    openModal('invPrsListModal');
    try {
        const res = await fetch(`${issuanceUrl}?prs_list=1`);
        const data = await res.json();
        if (!data.success) { body.innerHTML = `<div class="inv-empty">${escapeHtml(data.message||'Unable to load returns.')}</div>`; return; }
        prsListData = data.prs || [];
        if (!prsListData.length) { body.innerHTML = '<div class="inv-empty">No property has been returned to stock yet.</div>'; return; }
        body.innerHTML = prsListData.map((p, i) => `
            <div class="inv-hist-item">
                <div class="inv-hist-num">${escapeHtml(p.prs_number)}
                    <button type="button" class="btn btn-outline-secondary btn-sm float-end" onclick="prsReprint(${i})"><i class="fas fa-print me-1"></i>Reprint</button>
                </div>
                <div class="inv-hist-meta">${fmtDate(p.date_returned)} · PO ${escapeHtml(p.po_number||'—')} · printed by ${escapeHtml(p.printed_by||'—')}</div>
                <div class="inv-hist-flow"><span class="from">${escapeHtml(p.from_officer||'—')} (${escapeHtml(p.from_location||'—')})</span>
                    <i class="fas fa-arrow-right mx-1"></i>
                    <span class="to">${escapeHtml(p.returned_to||'—')}</span></div>
                ${p.reason ? `<div class="inv-hist-meta">${escapeHtml(p.reason)}</div>` : ''}
                <div class="inv-hist-meta">${(p.items||[]).length} item(s): ${(p.items||[]).map(x => escapeHtml(x.items_description)).join(', ')}</div>
            </div>`).join('');
    } catch (err) {
        body.innerHTML = '<div class="inv-empty">Unable to load returns.</div>';
        console.error(err);
    }
});

// Rebuilds a PRS's print payload entirely from the stored list data — this
// list is cross-PO, so unlike every other reprint path in this file there is
// no guarantee the item grid behind it (if any is even open) belongs to the
// same purchase order.
function prsReprint(idx) {
    const p = prsListData[idx];
    if (!p) return;
    const rows = (p.items || []).map(it => ({
        items_description: it.items_description, unit: '', quantity: it.quantity,
        unit_cost: it.unit_cost, tag_number: it.tag_number, serial_number: it.serial_number,
        po_number: p.po_number,
    }));
    renderInvDocument('prs', {
        rows, number: p.prs_number,
        from: `${p.from_officer || ''} · ${p.from_location || ''}`,
        reason: p.reason || '', condition: p.condition_on_return || '',
        returnedTo: p.returned_to || '', receivedBy: p.received_by || '', receivedPosition: p.received_position || '',
        approvedBy: p.approved_by || '', approvedPosition: p.approved_position || '',
        department: p.department || '', supplier: '', poNumber: p.po_number || '',
        cgso: cgsoOfficer,
    });
}

// ── Record History ──────────────────────────────────────────────────────
document.getElementById('btnInvRecordHistory').addEventListener('click', () => {
    if (!invPoNumber) return;
    RecordHistory.open({ title: 'Inventory History', label: invPoNumber,
        url: `${apiUrl}?record_key=${encodeURIComponent(invRecordKey)}&history=1` });
});

// ── Print: Property Card / Tag ──────────────────────────────────────────
document.getElementById('btnInvPrint').addEventListener('click', () => {
    const invIds = invCheckedInvIds();
    if (!invIds.length) return showToast('Check at least one item row first.', 'warning');
    document.getElementById('invPrintScope').textContent = `${invIds.length} item(s) selected on PO ${invPoNumber}.`;
    document.getElementById('invPrintModal').dataset.invIds = JSON.stringify(invIds);
    openModal('invPrintModal');
});

document.getElementById('btnInvPrintGo').addEventListener('click', async () => {
    const invIds = JSON.parse(document.getElementById('invPrintModal').dataset.invIds || '[]');
    const type = document.querySelector('input[name="invPrintType"]:checked').value;
    const rows = invIds.map(id => invRowToPrintData(document.querySelector(`#invItemsBody tr[data-inv-id="${id}"]`)));
    bootstrap.Modal.getInstance(document.getElementById('invPrintModal'))?.hide();
    await renderInvDocument(type, { rows, cgso: cgsoOfficer,
        department: document.getElementById('sumDept').textContent.trim() });   // Entity Name on the cards
});

// ── Row -> print-data bridge ──────────────────────────────────────────────
function invRowToPrintData(tr) {
    if (!tr) return {};
    // Most cells are inputs (.value); the category cell is a plain pill span
    // (.textContent) — falling back covers both without two helpers.
    const f = (name) => {
        const el = tr.querySelector(`[data-f="${name}"]`);
        if (!el) return '';
        return ('value' in el) ? el.value : el.textContent.trim();
    };
    const pill = tr.querySelector('.inv-number-pill');
    return {
        inv_id: tr.dataset.invId ? parseInt(tr.dataset.invId, 10) : null,
        po_number: invPoNumber,
        par_ics_number: pill && !pill.classList.contains('empty') ? pill.textContent.trim() : '',
        inventory_category: f('inventory_category'),
        items_description: f('items_description'), unit: f('unit'), quantity: f('quantity'),
        unit_cost: f('unit_cost'), total_cost: (tr.querySelector('[data-f="total_cost"]')||{}).value,
        tag_number: f('tag_number'), serial_number: f('serial_number'),
        accountable_officer: f('accountable_officer'), position: f('position'), location: f('location'),
        classification: f('classification'), condition: f('condition'), remarks: f('remarks'),
        estimated_life: f('estimated_life'),
        date_acquired: document.getElementById('invDateEntry').value,
        // The item's saved Unit Breakdown, printed as detail lines under it on
        // the PAR/ICS — see buildIssuance() in js/inventory-print.js.
        sub_details: ((invItems[parseInt(tr.dataset.idx, 10)] || {}).sub_details) || [],
    };
}

// ── PDF rendering (shared) ──────────────────────────────────────────────
let invBlobUrl = null, invFileName = 'inventory.pdf';

async function renderInvDocument(docType, payload) {
    const titles = { par: 'Property Acknowledgement Receipt', ics: 'Inventory Custodian Slip',
        itr: 'Inventory Transfer Report', ptr: 'Property Transfer Report',
        card: 'Property Card', card_semi: 'Property Card (Semi-Expendable)',
        tag_big: 'Property Tag', tag_small: 'Property Tag (Small)',
        mr: 'Memorandum Receipt', prs: 'Property Return Slip' };
    document.getElementById('invPreviewTitle').innerHTML = '<i class="fas fa-file-pdf me-2"></i>' + escapeHtml(titles[docType] || 'Preview');
    document.getElementById('invPaperSize').value = 'auto';
    openModal('invPreviewModal');
    document.getElementById('invPreviewModal').addEventListener('shown.bs.modal', () => doRenderInv(docType, payload), { once: true });
}

async function doRenderInv(docType, payload) {
    const loading = document.getElementById('invPreviewLoading');
    const frame = document.getElementById('invPreviewFrame');
    const buttons = ['invDownload', 'invPrint', 'invOpenTab'].map(id => document.getElementById(id));
    frame.classList.add('d-none');
    buttons.forEach(b => b.disabled = true);
    loading.classList.remove('d-none');
    loading.innerHTML = '<div class="spinner-border text-primary" role="status"></div><div class="small mt-2">Generating PDF preview…</div>';
    document.getElementById('invLayoutStatus').innerHTML = '';

    try {
        let html;
        if (docType === 'par' || docType === 'ics') {
            html = InventoryPrint.buildIssuance(docType, payload);
        } else if (docType === 'itr' || docType === 'ptr') {
            html = InventoryPrint.buildTransfer(docType, payload);
        } else if (docType === 'card' || docType === 'card_semi') {
            // The card's ledger is the item's own transfer history — the same
            // record kept for the Transfer History dialog, not a second copy.
            const invId = (payload.rows[0] || {}).inv_id;
            let movements = [];
            if (invId && invRecordKey) {
                try {
                    const histRes = await fetch(`${issuanceUrl}?transfers=1&po_number=${encodeURIComponent(invRecordKey)}`);
                    const hist = await histRes.json();
                    // Loose equality: PDO can hand this back as a numeric
                    // string, and invId here is always a real number. Each
                    // transfer can cover several items — fold in just the
                    // quantity/cost this ONE item carried on that transfer,
                    // so the card's running balance is this item's, not the
                    // whole transfer's.
                    movements = (hist.transfers || [])
                        .map(t => ({ t, item: (t.items || []).find(i => i.inv_id == invId) }))
                        .filter(x => x.item)
                        .sort((a, b) => new Date(a.t.date_transferred) - new Date(b.t.date_transferred))
                        .map(x => Object.assign({}, x.t, { quantity: x.item.quantity, unit_cost: x.item.unit_cost }));
                } catch (e) { /* the card still prints without a ledger */ }
            }
            html = InventoryPrint.buildPropertyCard(Object.assign({}, payload, { movements }), docType === 'card_semi');
        } else if (docType === 'mr') {
            html = InventoryPrint.buildMR(payload);
        } else if (docType === 'prs') {
            html = InventoryPrint.buildPRS(payload);
        } else {
            // The tag's barcode encodes the PAR/ICS number — same rule the
            // legacy tag button used (fontencoder.Code128 on that same cell).
            const num = (payload.rows[0] || {}).par_ics_number;
            let barcodePng = '', barcodeWidth = 0;
            if (num) {
                try {
                    const bRes = await fetch(`${apiUrl}?barcode=${encodeURIComponent(num)}`);
                    const b = await bRes.json();
                    if (b.success) { barcodePng = b.png; barcodeWidth = b.width; }
                } catch (e) { /* the tag still prints without a scannable barcode */ }
            }
            html = InventoryPrint.buildTag(Object.assign({}, payload, { barcodePng, barcodeWidth }), docType === 'tag_small');
        }

        document.getElementById('invSheetArea').innerHTML = html;
        if (!window.PrintPdf || typeof PrintPdf.renderToPdf !== 'function') {
            throw new Error('the print engine did not load, or your browser is holding an old copy of it. Reload with Ctrl+F5 and try again.');
        }
        const source = document.querySelector('#invSheetArea .inv-sheet');
        // Where a new page may start. PAR/ICS (user's request, 2026-09-29):
        // the details, acknowledgement and signature box continue straight
        // after the last item, like the PO report, instead of jumping as one
        // block to a page of their own — so they get break points too. The
        // "***nothing follows***" row stays whole, while description line
        // blocks are also safe break points, so a very long
        // item or unit-detail description can continue onto the next page
        // without being clipped.
        const isIssuance = docType === 'par' || docType === 'ics';
        const isCard = docType === 'card' || docType === 'card_semi';
        const breakSelector = isIssuance
            ? '.inv-items tr:not(.inv-sub):not(.inv-nothing-row), .inv-info-list tr, .inv-ack, .inv-sign-grid, .inv-desc-line'
            : isCard
                ? '.inv-card-head tr, .inv-ledger tr, .inv-desc-line'
                : '.inv-items tr, .inv-desc-line';
        const gridSelector = isIssuance
            ? '.inv-items, .inv-info-list'
            : isCard
                ? '.inv-card-head, .inv-ledger'
                : '.inv-items';
        const result = await PrintPdf.renderToPdf(source, {
            paper: document.getElementById('invPaperSize').value,
            breakSelector,
            gridSelector,
        });
        const info = PrintPdf.describeLayout(result.layout);
        document.getElementById('invLayoutStatus').innerHTML =
            '<span>' + escapeHtml(info.paper) + '</span><span class="pp-sheets">' + escapeHtml(info.sheetsLabel) + '</span>'
            + (info.note ? '<span class="pp-layout-note">' + escapeHtml(info.note) + '</span>' : '');

        if (invBlobUrl) URL.revokeObjectURL(invBlobUrl);
        invBlobUrl = URL.createObjectURL(result.blob);
        // MR isn't scoped to a PO the way every other doc type here is —
        // invPoNumber would be whatever (if anything) was last open in the
        // Receive/View modal, not this document's own subject.
        invFileName = docType === 'mr'
            ? 'MR_' + (payload.officer || 'personnel').replace(/[^A-Za-z0-9]+/g, '_') + '.pdf'
            : docType.toUpperCase() + '_' + (invPoNumber || 'inventory') + '.pdf';
        frame.src = invBlobUrl + '#navpanes=0&toolbar=1&view=FitH';
        loading.classList.add('d-none');
        frame.classList.remove('d-none');
        buttons.forEach(b => b.disabled = false);
    } catch (err) {
        loading.innerHTML = '<div class="text-danger small px-3"><i class="fas fa-triangle-exclamation me-1"></i>Unable to generate the PDF: ' + escapeHtml(err.message||String(err)) + '</div>';
        console.error(err);
    }
}
document.getElementById('invPaperSize').addEventListener('change', () => {
    // Re-render is cheap and the data is already in the sheet area; simplest
    // to just leave the current render — paper choice mainly matters at
    // export/print time via the same recompute the other pages use.
});
document.getElementById('invDownload').addEventListener('click', () => {
    if (!invBlobUrl) return;
    const a = document.createElement('a'); a.href = invBlobUrl; a.download = invFileName;
    document.body.appendChild(a); a.click(); a.remove();
});
document.getElementById('invPrint').addEventListener('click', () => {
    const frame = document.getElementById('invPreviewFrame');
    try { frame.contentWindow.focus(); frame.contentWindow.print(); }
    catch (err) { if (invBlobUrl) window.open(invBlobUrl, '_blank'); }
});
document.getElementById('invOpenTab').addEventListener('click', () => { if (invBlobUrl) window.open(invBlobUrl, '_blank'); });

// ── Boot ──────────────────────────────────────────────────────────────
document.addEventListener('DOMContentLoaded', () => {
    document.getElementById('btnFilter').addEventListener('click', loadList);
    document.getElementById('filterSearch').addEventListener('keydown', (e) => { if (e.key === 'Enter') loadList(); });

    const q = new URLSearchParams(location.search);
    if (q.get('record_key') || q.get('po_number')) {
        openInventory(q.get('record_key') || q.get('po_number'), 'view');
    }

    loadSignatories();
    loadStats();
    loadList();
});
</script>
<?php include 'assistant.php'; ?>
</body>
</html>
