-- Competitive Bidding — Progressive Item Processing.
--
-- A bid's items are commonly awarded/delivered/processed in multiple
-- batches rather than all at once. This adds an append-only batch/step
-- history, same shape as Progress Billing's own two tables
-- (tbl_inspection_progress_billing / tbl_inspection_items_details_progress_
-- billing — see database/inspection_progress_billing.sql), adapted for
-- Competitive Bidding:
--
--   * tbl_competitive_bidding / tbl_competitive_bidding_items_details are
--     NEVER written to by this feature. The original bid document (and the
--     three documents printed from it — Notice of Award, Contract
--     Agreement, Notice to Proceed) stays exactly as typed; nothing here
--     recreates or renumbers it. Additive only.
--   * One row per processing STEP/EVENT (can cover several item lines at
--     once), and a child row per item line actually processed in that step,
--     carrying its own snapshot of description/unit/cost so history survives
--     a later edit to the original bid item row.
--   * isDeleted is 3-state, exactly like Progress Billing's:
--       0  live
--       1  superseded by a later save (revising the same step)
--       2  retracted by an undo
--     so an undo can never hand back a version an earlier undo discarded.
--
-- Server is MySQL 8.4 on 192.168.11.70 (purchase_request), same server
-- tbl_competitive_bidding and tbl_inspection_progress_billing live on.
-- Run once:
--   mysql -h 192.168.11.70 -u <user> -p purchase_request < cb_processing.sql

CREATE TABLE `tbl_cb_processing` (
    `Step_ID`         int             NOT NULL AUTO_INCREMENT,
    `Bid_Number`      varchar(50)     NOT NULL,
    -- The batch number, mirrors tbl_inspection_progress_billing.Billing_No.
    `Step_No`         int             NOT NULL,
    `Processing_Date` datetime        NULL DEFAULT NULL,
    `Remarks`         varchar(500)    NULL DEFAULT NULL,
    `Created_By`      varchar(255)    NULL DEFAULT NULL,
    `Created_By_ID`   int             NULL DEFAULT NULL,
    `Date_Created`    datetime        NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `isDeleted`       int             NOT NULL DEFAULT '0',
    PRIMARY KEY (`Step_ID`),
    KEY `idx_cb_processing_bid` (`Bid_Number`, `isDeleted`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `tbl_cb_processing_items` (
    `ID`                  int           NOT NULL AUTO_INCREMENT,
    -- Precise value-reference to the exact tbl_cb_processing row (header
    -- VERSION) these lines belong to — NOT just (Bid_Number, Step_No),
    -- because Step_No is reused across every revision of the same step
    -- (the old header row is soft-deleted, a new one inserted). Without
    -- this, undo could not tell "the version a revision just replaced"
    -- apart from any earlier retracted version sharing the same Step_No,
    -- once there are more than two versions on file. Progress Billing's own
    -- older tables work around the same problem with a shared timestamp
    -- column (Date_Inspected) because they predate this convention; this
    -- table gets the precise column from the start instead.
    `Step_ID`             int           NOT NULL,
    -- Kept alongside Step_ID for the same "join by business key" reasons
    -- every other table here does — lets a query find all of a bid's
    -- processed lines, or one step's current lines, without a join.
    `Bid_Number`          varchar(50)   NOT NULL,
    `Step_No`             int           NOT NULL,
    -- tbl_competitive_bidding_items_details.ID this line was processed
    -- from — the matching strategy, mirrors Progress Billing's Item_ID.
    `CB_Item_ID`          int           NOT NULL,
    -- Snapshotted at processing time (same reasoning as every other
    -- history table in this app): history must survive a later edit to the
    -- bid's own item row.
    `Items_Description`   text          NULL DEFAULT NULL,
    `Unit`                varchar(255)  NULL DEFAULT NULL,
    -- Quantity PROCESSED in this step (not the bid's ordered quantity).
    `Quantity`            decimal(11,3) NULL DEFAULT NULL,
    `Unit_Cost`           decimal(15,3) NULL DEFAULT NULL,
    `Total_Cost`          decimal(15,3) NULL DEFAULT NULL,
    `Date_Created`        datetime      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `isDeleted`           int           NOT NULL DEFAULT '0',
    PRIMARY KEY (`ID`),
    KEY `idx_cb_processing_items_stepid` (`Step_ID`, `isDeleted`),
    KEY `idx_cb_processing_items_step` (`Bid_Number`, `Step_No`, `isDeleted`),
    KEY `idx_cb_processing_items_item` (`CB_Item_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Verify:
--   SHOW TABLES LIKE 'tbl_cb_processing%';
--   SELECT COUNT(*) FROM tbl_cb_processing;       -- expect 0
--   SELECT COUNT(*) FROM tbl_cb_processing_items; -- expect 0
--
-- To undo (nothing depends on these tables until the UI ships):
--   DROP TABLE tbl_cb_processing_items;
--   DROP TABLE tbl_cb_processing;
