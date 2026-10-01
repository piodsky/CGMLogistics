<?php
// Read-only PO amendment overlay — shared by api/inspection.php,
// api/inspection_progress.php and api/inventory.php, so all three agree on
// what "the latest valid amendment" for a given PO item line is.
//
// This file only SELECTs from tbl_po_amendments / tbl_po_amendment_items
// (database/po_amendments.sql). It never writes anywhere, and it never
// touches tbl_purchase_order or tbl_purchase_order_items_details — the only
// file that writes the amendment tables is api/po_amendments.php, and
// nothing ever writes the original PO tables on an amendment's behalf.

// One row per PO_Item_ID: whichever amendment touched that line most
// recently (a line can be amended more than once across separate amendment
// events; the latest wins). Keyed so callers can overlay by the id already
// on their own item rows.
function getLatestPoItemAmendments(PDO $poDb, string $poNumber): array {
    try {
        $stmt = $poDb->prepare(
            "SELECT ai.PO_Item_ID, ai.Amended_Items_Description, ai.Amended_Unit,
                    ai.Amended_Quantity, ai.Amended_Unit_Cost, ai.Amended_Total_Cost,
                    ai.Changed_Fields,
                    ai.Original_Items_Description, ai.Original_Unit,
                    ai.Original_Quantity, ai.Original_Unit_Cost, ai.Original_Total_Cost,
                    a.Amendment_No, a.Date_Created AS amended_at
             FROM tbl_po_amendment_items ai
             INNER JOIN tbl_po_amendments a ON a.Amendment_ID = ai.Amendment_ID
             WHERE a.PO_Number = ? AND a.isDeleted = 0
             ORDER BY a.Date_Created ASC, a.Amendment_ID ASC, ai.Amendment_Item_ID ASC"
        );
        $stmt->execute([$poNumber]);
        $latest = [];
        // Ascending order + overwrite-on-repeat: whichever row lands last in
        // $latest for a given PO_Item_ID is necessarily the most recent
        // amendment — no MAX()/GROUP BY needed.
        foreach ($stmt->fetchAll(PDO::FETCH_ASSOC) as $row) {
            $latest[(int)$row['PO_Item_ID']] = $row;
        }
        return $latest;
    } catch (Throwable $e) {
        // The amendment tables may not exist yet on a deploy that hasn't run
        // database/po_amendments.sql — every consumer of this file must keep
        // behaving exactly as it did before that migration lands.
        error_log('getLatestPoItemAmendments failed: ' . $e->getMessage());
        return [];
    }
}

// Overlays the latest amendment (if any) onto an already-fetched array of PO
// item rows. Each row must carry the PO item's own tbl_purchase_order_items_
// details.ID under $idKey. $fieldMap maps the caller's own field-name casing
// (it differs per consumer: inspection.php uses items_description/unit/...,
// inventory.php uses Items_Description/Unit/...) to the Amended_* column
// that should be written over it.
//
// Non-destructive: a row with no amendment only gains 'is_amended' (false)
// and 'amendment_no' (null); nothing existing about the row is altered.
function applyPoItemAmendments(PDO $poDb, string $poNumber, array $items, string $idKey, array $fieldMap): array {
    if (!$items) return $items;
    $latest = getLatestPoItemAmendments($poDb, $poNumber);
    foreach ($items as &$row) {
        $rowId = isset($row[$idKey]) ? (int)$row[$idKey] : 0;
        $amend = ($rowId && isset($latest[$rowId])) ? $latest[$rowId] : null;
        if (!$amend) {
            $row['is_amended']   = false;
            $row['amendment_no'] = null;
            continue;
        }
        foreach ($fieldMap as $itemField => $amendColumn) {
            if ($amend[$amendColumn] !== null) $row[$itemField] = $amend[$amendColumn];
        }
        $row['is_amended']                 = true;
        $row['amendment_no']               = $amend['Amendment_No'];
        $row['amended_at']                 = $amend['amended_at'];
        $row['changed_fields']             = $amend['Changed_Fields'];
        $row['original_items_description'] = $amend['Original_Items_Description'];
        $row['original_unit']              = $amend['Original_Unit'];
        $row['original_quantity']          = $amend['Original_Quantity'];
        $row['original_unit_cost']         = $amend['Original_Unit_Cost'];
        $row['original_total_cost']        = $amend['Original_Total_Cost'];
    }
    unset($row);
    return $items;
}
