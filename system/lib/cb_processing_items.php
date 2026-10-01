<?php
// Read-only Competitive Bidding processing overlay — shared by
// api/inspection.php and api/inventory.php, so a PO's item rows show the
// same "how much of this has been processed through Competitive Bidding"
// figure everywhere it's surfaced.
//
// This file only SELECTs from tbl_competitive_bidding,
// tbl_competitive_bidding_items_details, tbl_cb_processing and
// tbl_cb_processing_items (database/cb_po_link.sql, database/cb_processing.sql).
// It never writes anywhere, and it never touches tbl_purchase_order or
// tbl_purchase_order_items_details — api/cb_processing.php is the only file
// that writes the processing tables, and nothing ever writes PO_Number back
// onto a PO from here (the link is set on the BID, via
// api/competitive_bidding.php's existing quick-field mechanism).
//
// The match between a PO item row and a bid item row is NOT a real key —
// the two tables were filled in independently by hand, with nothing joining
// them at the item level (only Bid_Number <-> PO_Number at the header).
// Matched by normalized (trimmed, lowercased) description, the same
// fallback strategy Progress Billing already uses for a billing line whose
// Item_ID doesn't resolve to an ordered PO line. A PO item with no matching
// bid item simply gets no CB data attached — never a guessed match
// presented as fact.

// The bid currently linked to this PO, or null. Most-recent wins if more
// than one bid somehow claims the same PO_Number (should not happen in
// practice — PO_Number is meant to be set once, from the bid, after award).
function getCbLinkForPo(PDO $poDb, string $poNumber): ?array {
    try {
        $stmt = $poDb->prepare(
            "SELECT ID, Bid_Number, Contract_Amount
             FROM tbl_competitive_bidding
             WHERE PO_Number = ? AND isDeleted = 0
             ORDER BY Date_Created DESC LIMIT 1"
        );
        $stmt->execute([$poNumber]);
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        return $row ?: null;
    } catch (Throwable $e) {
        // PO_Number column may not exist yet (cb_po_link.sql not run) — a PO
        // must keep loading exactly as it did before this feature existed.
        error_log('getCbLinkForPo failed: ' . $e->getMessage());
        return null;
    }
}

// Every live item on the bid, with its own total processed quantity summed
// across all live processing steps — the per-item baseline
// applyCbProcessingOverlay() matches PO rows against. Keyed by normalized
// description so the caller can look a PO row up in one array access.
//
// Keyed on the bid's record ID (CB_ID), not its Bid Number: several contracts
// can share one Bid Number, and only the linked one's items belong here.
function getCbItemProgressByDescription(PDO $poDb, int $cbId): array {
    try {
        $itemStmt = $poDb->prepare(
            "SELECT ID, Items_Description, Quantity
             FROM tbl_competitive_bidding_items_details
             WHERE CB_ID = ? AND isDeleted = 0"
        );
        $itemStmt->execute([$cbId]);
        $bidItems = $itemStmt->fetchAll(PDO::FETCH_ASSOC);

        $procStmt = $poDb->prepare(
            "SELECT pi.CB_Item_ID, SUM(pi.Quantity) AS processed
             FROM tbl_cb_processing_items pi
             INNER JOIN tbl_cb_processing p ON p.Step_ID = pi.Step_ID AND p.isDeleted = 0
             WHERE p.CB_ID = ? AND pi.isDeleted = 0
             GROUP BY pi.CB_Item_ID"
        );
        $procStmt->execute([$cbId]);
        $processedByItemId = [];
        foreach ($procStmt->fetchAll(PDO::FETCH_ASSOC) as $row) {
            $processedByItemId[(int)$row['CB_Item_ID']] = (float)$row['processed'];
        }

        $byDescription = [];
        foreach ($bidItems as $bi) {
            $desc = trim((string)$bi['Items_Description']);
            if ($desc === '') continue; // LOT/description-only rows are not processable, never matched against
            $key = mb_strtolower($desc);
            $ordered   = (float)$bi['Quantity'];
            $processed = $processedByItemId[(int)$bi['ID']] ?? 0.0;
            $byDescription[$key] = [
                'cb_item_id'            => (int)$bi['ID'],
                'ordered_quantity'      => $ordered,
                'processed_quantity'    => $processed,
                'remaining_quantity'    => $ordered - $processed,
            ];
        }
        return $byDescription;
    } catch (Throwable $e) {
        error_log('getCbItemProgressByDescription failed: ' . $e->getMessage());
        return [];
    }
}

// Overlays CB processing data onto an already-fetched array of PO item rows.
// Each row must carry its own description under $descKey (the field name
// differs per caller: inspection.php uses items_description, inventory.php
// uses Items_Description). Non-destructive: a row with no linked bid or no
// matching bid item gains only 'cb_bid_number' = null.
function applyCbProcessingOverlay(PDO $poDb, string $poNumber, array $items, string $descKey): array {
    if (!$items) return $items;
    $link = getCbLinkForPo($poDb, $poNumber);
    if (!$link) {
        foreach ($items as &$row) { $row['cb_bid_number'] = null; }
        unset($row);
        return $items;
    }
    $bidNumber = $link['Bid_Number'];
    $progress = getCbItemProgressByDescription($poDb, (int)$link['ID']);
    foreach ($items as &$row) {
        $desc = trim((string)($row[$descKey] ?? ''));
        $key = $desc !== '' ? mb_strtolower($desc) : null;
        $match = ($key !== null && isset($progress[$key])) ? $progress[$key] : null;
        if (!$match) {
            $row['cb_bid_number'] = null;
            continue;
        }
        $row['cb_bid_number']         = $bidNumber;
        $row['cb_ordered_quantity']   = $match['ordered_quantity'];
        $row['cb_processed_quantity'] = $match['processed_quantity'];
        $row['cb_remaining_quantity'] = $match['remaining_quantity'];
    }
    unset($row);
    return $items;
}
