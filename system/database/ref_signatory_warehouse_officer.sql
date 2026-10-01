-- Adds the Warehouse Officer to ref_signatory, so Inventory & Property can fill
-- the "Released/Issued by" line on the ITR and PTR from master data instead of
-- leaving it blank for someone to type on every transfer.
--
-- Data only: one row into an existing table, no schema change. Guarded so
-- re-running does not create a second copy — ref_signatory has no unique key
-- on Name, and two Warehouse Officers on the picklist is exactly the kind of
-- mess a fixed signatory is meant to avoid.
--
-- api/inventory.php finds this person by their TITLE (Warehouse In-Charge), not
-- by name, so when the post changes hands it is a Master Data edit, not a code
-- change.
--
--     mysql -u prroot -p purchase_request < database/ref_signatory_warehouse_officer.sql

INSERT INTO ref_signatory (Name, Title, isDeleted)
SELECT 'GIOVANNI M. TULANG', 'SUPPLY OFFICER III/WAREHOUSE INCHARGE', 0
  FROM DUAL
 WHERE NOT EXISTS (
     SELECT 1 FROM ref_signatory
      WHERE Name = 'GIOVANNI M. TULANG' AND isDeleted = 0
 );
