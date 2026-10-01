-- Purchase Request module (Phase 1 of PR -> RFQ/Canvass -> Abstract -> PO).
--
--     mysql -u prroot -p purchase_request < database/pr_phase1.sql
--
-- PURELY ADDITIVE. Four new tables, CREATE TABLE IF NOT EXISTS, no ALTER on and
-- no reference to anything that already exists, so it cannot lock or change the
-- live purchase orders. Re-running it is harmless.
--
-- Deliberately NOT built on the tables that are already in this database:
--
--   pr / items_pr / canvas / items_canvas / abstract
--       The legacy VB.NET chain. Latin1, and its joins are already broken
--       (items_pr.PR_ID is a bigint against pr.PR_ID varchar; only 15 of 15,668
--       canvass rows point back at a PR item). The old app may still read them.
--       Left exactly as they are.
--   tbl_purchase_request / tbl_purchase_request_items_details
--       14 + 44 rows of 2024 test data with duplicate and corrupted PR numbers.
--       Left exactly as they are.
--
-- HOW THE LATER PHASES ATTACH (designed here, built later):
--   * tbl_pr_items.PR_Item_ID is the one anchor. An RFQ line, a quotation line,
--     an abstract line and a PO line will each carry an integer PR_Item_ID —
--     never the text PR number, which is what broke the legacy chain.
--   * PR_Item_ID is STABLE: editing a PR updates its item rows in place and only
--     soft-deletes rows the user removed. (The PO's own edit rewrites every item
--     row, which is fine for a PO and fatal for something others point at.)
--   * Item_Status is written only by later phases (Open here; e.g. In RFQ,
--     Awarded, Ordered later) so a PR can be part-procured without a new column.
--   * Where a PR sits in the pipeline is DERIVED from those later tables, not
--     stored on the PR. tbl_pr.Status is the approval lifecycle and nothing else.
--   * One PO can cover several PRs ("VARIOUS PR"), so the PR<->PO link will be a
--     link table, not a column on either side.
--
-- PR_Number is issued here, in the shape the PO already uses (PO-sys<Y><n><id>):
-- PR-sys<Y><n><id>, built from the row's own AUTO_INCREMENT id right after the
-- insert, so two people saving in the same second cannot get the same number.
-- Unique among live rows, enforced BY THE DATABASE (Live_PR_Number below) — the
-- legacy PR table has duplicate numbers because nothing enforced it.
--
-- Written for MySQL 8.x (this server is 8.4): generated columns and foreign keys
-- are used. utf8mb4, InnoDB, like the newer tables in this database.

CREATE TABLE IF NOT EXISTS `tbl_pr` (
  `PR_ID`                      INT UNSIGNED  NOT NULL AUTO_INCREMENT,
  `PR_Number`                  VARCHAR(40)   NOT NULL,
  `PR_Date`                    DATE          NOT NULL,
  `Fund_Cluster`               VARCHAR(100)  NULL,
  `Category`                   VARCHAR(255)  NULL,
  `Department`                 VARCHAR(255)  NOT NULL,
  `Division`                   VARCHAR(255)  NULL,
  `Section`                    VARCHAR(255)  NULL,
  `Responsibility_Center_Code` VARCHAR(50)   NULL,
  `Purpose`                    TEXT          NOT NULL,
  -- What the requester proposes. Confirmed (or changed) in the RFQ phase.
  `Mode_of_Procurement`        VARCHAR(100)  NULL,
  `Requested_By`               VARCHAR(255)  NULL,
  `Requested_By_Position`      VARCHAR(255)  NULL,
  `Funds_Certified_By`         VARCHAR(255)  NULL,
  `Funds_Certified_Position`   VARCHAR(255)  NULL,
  `Approved_By`                VARCHAR(255)  NULL,
  `Approved_By_Position`       VARCHAR(255)  NULL,
  -- Always recomputed by the server from the item rows on every save. It exists
  -- so the list can sort and total without joining 100k item rows.
  `Total_Estimated_Cost`       DECIMAL(15,2) NOT NULL DEFAULT 0.00,
  -- Draft | Submitted | Returned | Approved | Cancelled. Approval lifecycle only.
  `Status`                     VARCHAR(20)   NOT NULL DEFAULT 'Draft',
  `Remarks`                    TEXT          NULL,
  -- Bumped on every save; a save carrying an older number is refused, so two
  -- people editing one PR cannot silently overwrite each other.
  `Row_Version`                INT UNSIGNED  NOT NULL DEFAULT 1,
  `Created_By`                 VARCHAR(100)  NULL,
  `Date_Created`               DATETIME      NOT NULL,
  `Updated_By`                 VARCHAR(100)  NULL,
  `Date_Updated`               DATETIME      NULL,
  `isDeleted`                  TINYINT(1)    NOT NULL DEFAULT 0,
  `Deleted_By`                 VARCHAR(100)  NULL,
  `Date_Deleted`               DATETIME      NULL,
  -- NULL for a deleted row, so a deleted draft frees its number while two live
  -- rows can never share one. NULLs do not collide in a UNIQUE index.
  `Live_PR_Number`             VARCHAR(40)   GENERATED ALWAYS AS (IF(`isDeleted` = 0, `PR_Number`, NULL)) STORED,
  PRIMARY KEY (`PR_ID`),
  UNIQUE KEY `uq_pr_live_number` (`Live_PR_Number`),
  KEY `idx_pr_status` (`isDeleted`, `Status`),
  KEY `idx_pr_date` (`PR_Date`),
  KEY `idx_pr_number` (`PR_Number`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE IF NOT EXISTS `tbl_pr_items` (
  `PR_Item_ID`          INT UNSIGNED  NOT NULL AUTO_INCREMENT,
  `PR_ID`               INT UNSIGNED  NOT NULL,
  `Line_No`             SMALLINT UNSIGNED NOT NULL,
  -- tbl_ppmp_bac_items, the item catalogue the PO also feeds. No foreign key:
  -- that table belongs to the legacy schema.
  `Item_ID`             INT           NULL,
  `Stock_No`            VARCHAR(50)   NULL,
  -- A LOT / heading line: description only, every number blank.
  `Is_Title`            TINYINT(1)    NOT NULL DEFAULT 0,
  `Unit`                VARCHAR(50)   NULL,
  `Items_Description`   TEXT          NOT NULL,
  `Quantity`            DECIMAL(12,3) NULL,
  `Estimated_Unit_Cost` DECIMAL(15,2) NULL,
  `Estimated_Total_Cost` DECIMAL(15,2) NULL,
  `Remarks`             VARCHAR(255)  NULL,
  -- Open here. Written by the RFQ / Abstract / PO phases, never by this one.
  `Item_Status`         VARCHAR(20)   NOT NULL DEFAULT 'Open',
  `isDeleted`           TINYINT(1)    NOT NULL DEFAULT 0,
  `Date_Created`        DATETIME      NOT NULL,
  `Date_Updated`        DATETIME      NULL,
  PRIMARY KEY (`PR_Item_ID`),
  KEY `idx_pri_pr` (`PR_ID`, `isDeleted`, `Line_No`),
  CONSTRAINT `fk_pri_pr` FOREIGN KEY (`PR_ID`) REFERENCES `tbl_pr` (`PR_ID`)
    ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Append-only: one row per status change, never updated or deleted. Drives the
-- timeline in the View dialog and answers "who approved this, and when".
CREATE TABLE IF NOT EXISTS `tbl_pr_status_log` (
  `Log_ID`       INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `PR_ID`        INT UNSIGNED NOT NULL,
  `Action`       VARCHAR(20)  NOT NULL,
  `From_Status`  VARCHAR(20)  NULL,
  `To_Status`    VARCHAR(20)  NOT NULL,
  `Reason`       VARCHAR(500) NULL,
  `Actor_ID`     INT          NULL,
  `Actor_Name`   VARCHAR(150) NULL,
  `Date_Created` DATETIME     NOT NULL,
  PRIMARY KEY (`Log_ID`),
  KEY `idx_prl_pr` (`PR_ID`, `Log_ID`),
  CONSTRAINT `fk_prl_pr` FOREIGN KEY (`PR_ID`) REFERENCES `tbl_pr` (`PR_ID`)
    ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Who may approve / delete a PR, per HRMIS role — same idea and same shape as
-- tbl_po_permissions. Starts EMPTY: a role with no row falls back to the
-- Super Administrator / Administrator / Procurement Administrator default in
-- prActionPermission() (lib/pr_workflow.php), so nothing changes until a row is
-- written. Own table, not a column on HRMIS's shared system_access_level.
CREATE TABLE IF NOT EXISTS `tbl_pr_permissions` (
  `role_id`      INT          NOT NULL,
  `can_approve`  TINYINT(1)   NOT NULL DEFAULT 0,
  `can_delete`   TINYINT(1)   NOT NULL DEFAULT 0,
  `Date_Updated` DATETIME     NULL,
  `Updated_By`   VARCHAR(100) NULL,
  PRIMARY KEY (`role_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- To undo (only if nothing has been entered): drop in this order.
--   DROP TABLE tbl_pr_status_log, tbl_pr_items, tbl_pr, tbl_pr_permissions;
