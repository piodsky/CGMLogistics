-- Competitive Bidding: link child rows to their bid by record ID.
--
-- Why: BAC can award one bid as several contracts, each under the SAME Bid
-- Number but its own Contract Number. Until now every child table found its
-- bid by Bid_Number alone, so two bids sharing a number would have shared
-- (and wiped) each other's item rows and processing steps. CB_ID is the
-- parent's own tbl_competitive_bidding.ID, which never collides and never
-- changes when the Bid Number is corrected.
--
-- Only these two tables need it. tbl_cb_processing_items already carries
-- Step_ID (its exact header row), so it reaches the bid through that.
--
-- Additive only: two nullable columns + indexes, then a backfill. No existing
-- column is changed and nothing is deleted. Bid_Number stays on both tables
-- (still written, for readability) — it just stops being the join key.
--
-- Server is MySQL 8.4 on 192.168.11.70 (purchase_request). Run once:
--     mysql -h 192.168.11.70 -u <user> -p purchase_request < database/cb_parent_id.sql
-- Re-running errors with "Duplicate column name" on the first ALTER — that
-- means it has already been applied. The two backfill UPDATEs alone are
-- safe to re-run at any time (they only touch rows still NULL).

ALTER TABLE tbl_competitive_bidding_items_details
    ADD COLUMN CB_ID INT UNSIGNED NULL AFTER ID,
    ADD INDEX idx_cbi_cbid (CB_ID, isDeleted);

ALTER TABLE tbl_cb_processing
    ADD COLUMN CB_ID INT UNSIGNED NULL AFTER Step_ID,
    ADD INDEX idx_cb_processing_cbid (CB_ID, isDeleted);

-- Backfill. Before this change a Bid_Number was unique among LIVE bids, so
-- the live parent is the unambiguous owner. A number whose only parents are
-- deleted (their rows are all deleted too, never read) goes to the newest.
UPDATE tbl_competitive_bidding_items_details c
  JOIN (SELECT Bid_Number, COALESCE(MAX(CASE WHEN isDeleted = 0 THEN ID END), MAX(ID)) AS ID
          FROM tbl_competitive_bidding GROUP BY Bid_Number) b
    ON b.Bid_Number = c.Bid_Number
   SET c.CB_ID = b.ID
 WHERE c.CB_ID IS NULL;

UPDATE tbl_cb_processing c
  JOIN (SELECT Bid_Number, COALESCE(MAX(CASE WHEN isDeleted = 0 THEN ID END), MAX(ID)) AS ID
          FROM tbl_competitive_bidding GROUP BY Bid_Number) b
    ON b.Bid_Number = c.Bid_Number
   SET c.CB_ID = b.ID
 WHERE c.CB_ID IS NULL;

-- Verify (both expect 0):
--   SELECT COUNT(*) FROM tbl_competitive_bidding_items_details WHERE CB_ID IS NULL;
--   SELECT COUNT(*) FROM tbl_cb_processing WHERE CB_ID IS NULL;
--
-- To undo (only before the matching code ships — the code needs CB_ID):
--   ALTER TABLE tbl_competitive_bidding_items_details DROP INDEX idx_cbi_cbid, DROP COLUMN CB_ID;
--   ALTER TABLE tbl_cb_processing DROP INDEX idx_cb_processing_cbid, DROP COLUMN CB_ID;
