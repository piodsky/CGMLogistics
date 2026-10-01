/* PO report preview for an individual Competitive Bidding processing step. */
(function (global) {
    'use strict';
    let printData = null;
    let pdfUrl = null;
    let fileName = 'Processing_Step_PO.pdf';
    let requestId = 0;
    let busy = false;
    const el = id => document.getElementById(id);

    function enableOutput(enabled) {
        ['cbpPoOpenTab', 'cbpPoDownload', 'cbpPoPrint'].forEach(id => { el(id).disabled = !enabled; });
    }

    function clearPreview() {
        enableOutput(false);
        el('cbpPoPdfFrame').classList.add('d-none');
        el('cbpPoPdfFrame').removeAttribute('src');
        if (pdfUrl) URL.revokeObjectURL(pdfUrl);
        pdfUrl = null;
    }

    async function open(stepNo) {
        if (busy) return;
        // The record ID is what the API needs (several contracts can share a
        // Bid Number); the label is only for the subtitle and file name.
        const bidId = el('cbpBidId').value;
        const bidNumber = el('cbpBidNumber').value;
        if (!bidId || !Number.isInteger(Number(stepNo)) || Number(stepNo) < 1) return;
        const currentRequest = ++requestId;
        busy = true;
        printData = null;
        clearPreview();
        el('cbpPoPrintOptions').disabled = true;
        el('cbpPoPrintSubtitle').textContent = `${bidNumber} · Step ${stepNo}`;
        el('cbpPoPrintContext').textContent = '';
        el('cbpPoPrintStatus').textContent = 'Loading this processing step…';
        openModal('cbpPoPrintModal');
        try {
            const query = new URLSearchParams({ id: bidId, print_step: stepNo });
            const response = await fetch(`api/cb_processing.php?${query}`);
            const data = await response.json();
            if (!response.ok || !data.success) throw new Error(data.message || 'Unable to load this processing step.');
            if (currentRequest !== requestId) return;
            printData = data;
            el('cbpPoSignatory').value = data.signatory_name || '';
            el('cbpPoSignatoryTitle').value = data.signatory_title || '';
            el('cbpPoDate').value = String(data.processing_date || '').slice(0, 10);
            el('cbpPoPrintContext').textContent = data.order.po_number
                ? `PO ${data.order.po_number} · Only Step ${data.step_no}'s items and quantities will be printed.`
                : `Only Step ${data.step_no}'s items and quantities will be printed. Link a PO in Additional Details to fill its PO number and other PO fields.`;
            el('cbpPoPrintStatus').textContent = 'Review the print details, then generate the preview.';
            el('cbpPoPrintOptions').disabled = false;
            fileName = `PO_${bidNumber}_Step_${data.step_no}.pdf`.replace(/[<>:"/\\|?*\x00-\x1f]/g, '_');
        } catch (error) {
            if (currentRequest === requestId) el('cbpPoPrintStatus').textContent = error.message || 'Unable to load this processing step.';
        } finally {
            if (currentRequest === requestId) busy = false;
        }
    }

    async function generate(event) {
        event.preventDefault();
        if (busy || !printData) return;
        const initial = el('cbpPoInitial');
        initial.value = initial.value.trim();
        if (!el('cbpPoPrintForm').reportValidity()) return;
        const currentRequest = ++requestId;
        busy = true;
        clearPreview();
        el('cbpPoPrintOptions').disabled = true;
        el('cbpPoPrintStatus').textContent = 'Generating PDF preview…';
        try {
            const dateValue = el('cbpPoDate').value;
            const dateText = dateValue ? new Date(dateValue + 'T00:00:00').toLocaleDateString('en-PH', {
                year: 'numeric', month: 'long', day: 'numeric',
            }) : '';
            el('cbpPoPrintSource').innerHTML = PoPrint.build(printData.order, 'standard',
                el('cbpPoSignatory').value.trim(), el('cbpPoSignatoryTitle').value.trim(), initial.value, dateText);
            const source = el('cbpPoPrintSource').querySelector('.print-page');
            const result = await PrintPdf.renderToPdf(source, {
                paper: el('cbpPoPaper').value,
                breakSelector: 'tr, .pp-dl, .pp-summary-box, .pp-terms, .pp-conforme-band, .pp-sign-row',
                gridSelector: '.pp-items',
            });
            if (currentRequest !== requestId) return;
            pdfUrl = URL.createObjectURL(result.blob);
            el('cbpPoPdfFrame').src = pdfUrl + '#toolbar=0&navpanes=0&view=FitH';
            el('cbpPoPdfFrame').classList.remove('d-none');
            const layout = PrintPdf.describeLayout(result.layout);
            el('cbpPoPrintStatus').textContent = [layout.paper, layout.sheetsLabel, layout.note].filter(Boolean).join(' · ');
            enableOutput(true);
        } catch (error) {
            if (currentRequest === requestId) el('cbpPoPrintStatus').textContent = 'Unable to generate the PDF: ' + (error.message || String(error));
        } finally {
            if (currentRequest === requestId) {
                busy = false;
                el('cbpPoPrintOptions').disabled = false;
            }
        }
    }

    global.cbpOpenPoPrint = open;
    document.addEventListener('DOMContentLoaded', () => {
        el('cbpPoPrintForm').addEventListener('submit', generate);
        const invalidate = () => {
            if (!printData || busy) return;
            clearPreview();
            el('cbpPoPrintStatus').textContent = 'Print details changed. Generate the preview again.';
        };
        el('cbpPoPrintOptions').addEventListener('input', invalidate);
        el('cbpPoPrintOptions').addEventListener('change', invalidate);
        el('cbpPoSignatory').addEventListener('change', () => {
            const title = signatoryTitleByName[el('cbpPoSignatory').value.trim()];
            if (title) el('cbpPoSignatoryTitle').value = title;
        });
        el('cbpPoOpenTab').addEventListener('click', () => { if (pdfUrl) window.open(pdfUrl, '_blank', 'noopener'); });
        el('cbpPoDownload').addEventListener('click', () => {
            if (!pdfUrl) return;
            const link = document.createElement('a');
            link.href = pdfUrl;
            link.download = fileName;
            link.click();
        });
        el('cbpPoPrint').addEventListener('click', () => {
            if (pdfUrl) el('cbpPoPdfFrame').contentWindow?.print();
        });
        el('cbpPoPrintModal').addEventListener('hidden.bs.modal', () => {
            ++requestId;
            busy = false;
            printData = null;
            clearPreview();
            // Processing history remains open behind the preview.
            if (document.querySelector('.modal.show')) document.body.classList.add('modal-open');
        });
    });
})(window);
