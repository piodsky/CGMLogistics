-- Links a Competitive Bidding record to the Purchase Order raised against it.
--
-- Confirmed by a full code/schema search before writing this: Competitive
-- Bidding has NO existing connection to Purchase Orders anywhere in this
-- app — no Bid_Number column on tbl_purchase_order, no PO_Number column on
-- tbl_competitive_bidding, no join, nothing. That means Inspection &
-- Acceptance and Inventory (both keyed purely on PO_Number) have no way to
-- know a PO originated from an awarded bid, or how much of it has been
-- processed through Competitive Bidding's own processing steps
-- (database/cb_processing.sql).
--
-- Nullable and quick-field-writable (see CB_QUICK_FIELDS in
-- api/competitive_bidding.php) rather than part of the main form: a PO is
-- normally raised well after the bid document itself is typed, printed and
-- possibly locked, matching how the existing tracking dates
-- (Date_Received_NOA, Contract_Date, etc.) are handled.
--
-- Server is MySQL 8.4 on 192.168.11.70 (purchase_request), same server
-- every other Competitive Bidding table lives on. Run once.

ALTER TABLE `tbl_competitive_bidding`
    ADD COLUMN `PO_Number` VARCHAR(30) NULL DEFAULT NULL AFTER `Bid_Number`;

ALTER TABLE `tbl_competitive_bidding`
    ADD INDEX `idx_cb_po_number` (`PO_Number`);

-- Verify:
--   DESCRIBE tbl_competitive_bidding;
--   SELECT COUNT(*) FROM tbl_competitive_bidding WHERE PO_Number IS NOT NULL; -- expect 0
--
-- To undo (nothing depends on this column until the UI ships):
--   ALTER TABLE tbl_competitive_bidding DROP INDEX idx_cb_po_number;
--   ALTER TABLE tbl_competitive_bidding DROP COLUMN PO_Number;
