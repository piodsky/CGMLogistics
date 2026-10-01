-- Purchase Order AMENDMENT history.
--
-- A PO's item specs (description/unit/quantity/unit cost) sometimes need a
-- formal, tracked correction after the PO has already been raised (and
-- possibly printed/locked) — e.g. "Intel Core i5, 16GB RAM, 512GB SSD"
-- amended to "Intel Core i7, 32GB RAM, 1TB SSD". The existing Edit flow
-- (api/purchase_orders.php PUT) is the wrong tool for this: it soft-deletes
-- every item row and reinserts a fresh set with no record of what the
-- previous text said, no reason captured, and no amendment numbering — it's
-- meant for correcting mistakes on the working record, not for a traceable
-- amendment.
--
-- This is purely additive and, deliberately, never written to by anything
-- outside api/po_amendments.php:
--   * tbl_purchase_order and tbl_purchase_order_items_details are NEVER
--     updated by the amendment feature — not on save, not ever. The
--     "current" PO Number stays exactly as-is; nothing here creates or
--     duplicates a PO. The amended spec is shown by overlaying the latest
--     amendment on top of the original values at READ time (see
--     lib/po_amendment_items.php), the same pattern the Supplier Change
--     Request feature already uses (Supplier_Change_* columns that never
--     touch Supplier_Name itself).
--   * Two tables, matching the tbl_inventory_transfers /
--     tbl_inventory_transfer_items shape already used elsewhere in this
--     schema: one row per amendment EVENT (which can change several item
--     lines at once), and a child row per line actually changed in that
--     event, carrying the FULL before/after snapshot so history survives
--     any later amendment of the same line.
--
-- Server is MySQL 8.4 on 192.168.11.70 (purchase_request), same server
-- tbl_inventory_transfers lives on. Run once:
--   mysql -h 192.168.11.70 -u <user> -p purchase_request < po_amendments.sql

CREATE TABLE `tbl_po_amendments` (
    `Amendment_ID`   int             NOT NULL AUTO_INCREMENT,
    `PO_Number`      varchar(30)     NOT NULL,
    -- "AM-001", "AM-002", ... — sequential per PO_Number, computed at insert
    -- time from COUNT(*)+1 scoped to that PO_Number inside the same
    -- transaction as the insert (see api/po_amendments.php).
    `Amendment_No`   varchar(20)     NOT NULL,
    `Reason`         text            NOT NULL,
    `Created_By`     varchar(255)    NULL DEFAULT NULL,
    `Created_By_ID`  int             NULL DEFAULT NULL,
    `Date_Created`   datetime        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    -- Reserved for a future void/undo of a whole amendment event. Not
    -- exercised by this build — every row is 0 for now, but every read
    -- filters on it so undo can ship later without a second migration.
    `isDeleted`      int             NOT NULL DEFAULT '0',
    PRIMARY KEY (`Amendment_ID`),
    KEY `idx_po_amendments_po` (`PO_Number`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `tbl_po_amendment_items` (
    `Amendment_Item_ID` int           NOT NULL AUTO_INCREMENT,
    `Amendment_ID`       int          NOT NULL,
    -- tbl_purchase_order_items_details.ID this line corresponds to. That
    -- column already exists (it's the table's own primary key / ORDER BY
    -- column) but is never selected out to the client today — the new API
    -- reads it directly. NULL is never written; every amendment line must
    -- tie back to a real, still-live PO item row.
    `PO_Item_ID`         int          NOT NULL,
    `Original_Items_Description` text        NULL DEFAULT NULL,
    `Amended_Items_Description`  text        NULL DEFAULT NULL,
    `Original_Unit`       varchar(255)       NULL DEFAULT NULL,
    `Amended_Unit`        varchar(255)       NULL DEFAULT NULL,
    `Original_Quantity`   decimal(15,2)      NULL DEFAULT NULL,
    `Amended_Quantity`    decimal(15,2)      NULL DEFAULT NULL,
    `Original_Unit_Cost`  decimal(15,2)      NULL DEFAULT NULL,
    `Amended_Unit_Cost`   decimal(15,2)      NULL DEFAULT NULL,
    `Original_Total_Cost` decimal(15,2)      NULL DEFAULT NULL,
    `Amended_Total_Cost`  decimal(15,2)      NULL DEFAULT NULL,
    -- Comma list of which fields on THIS line actually changed, e.g.
    -- "items_description,unit_cost" — computed server-side at save time so
    -- the UI never has to re-diff. A line with no real change is not
    -- inserted at all.
    `Changed_Fields`      varchar(255)       NOT NULL DEFAULT '',
    `Date_Created`        datetime           NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`Amendment_Item_ID`),
    KEY `idx_po_amendment_items_amendment` (`Amendment_ID`),
    KEY `idx_po_amendment_items_poitem` (`PO_Item_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Verify:
--   SHOW TABLES LIKE 'tbl_po_amendment%';
--   SELECT COUNT(*) FROM tbl_po_amendments;       -- expect 0
--   SELECT COUNT(*) FROM tbl_po_amendment_items;  -- expect 0
--
-- To undo (nothing depends on these tables until the UI ships):
--   DROP TABLE tbl_po_amendment_items;
--   DROP TABLE tbl_po_amendments;
