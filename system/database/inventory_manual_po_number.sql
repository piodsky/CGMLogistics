-- Safe rollback for temporary manual duplicate-PO support.
-- This is idempotent: after the column is removed, rerunning it does nothing.
-- If mappings remain, it refuses to drop the column so internal M... keys are
-- not stranded.

DELIMITER $$
DROP PROCEDURE IF EXISTS drop_manual_po_number_when_unused$$
CREATE PROCEDURE drop_manual_po_number_when_unused()
BEGIN
    IF EXISTS (
        SELECT 1
          FROM information_schema.COLUMNS
         WHERE TABLE_SCHEMA = DATABASE()
           AND TABLE_NAME = 'tbl_inventory'
           AND COLUMN_NAME = 'Manual_PO_Number'
    ) THEN
        SET @manual_po_mapping_count = 0;
        SET @manual_po_mapping_sql =
            'SELECT COUNT(*) INTO @manual_po_mapping_count FROM tbl_inventory WHERE Manual_PO_Number IS NOT NULL AND Manual_PO_Number <> ''''';
        PREPARE manual_po_mapping_check FROM @manual_po_mapping_sql;
        EXECUTE manual_po_mapping_check;
        DEALLOCATE PREPARE manual_po_mapping_check;

        IF @manual_po_mapping_count > 0 THEN
            SIGNAL SQLSTATE '45000'
                SET MESSAGE_TEXT = 'Manual PO mappings still exist. Consolidate those records before dropping Manual_PO_Number.';
        END IF;

        SET @manual_po_drop_sql = 'ALTER TABLE tbl_inventory DROP COLUMN Manual_PO_Number';
        PREPARE manual_po_drop FROM @manual_po_drop_sql;
        EXECUTE manual_po_drop;
        DEALLOCATE PREPARE manual_po_drop;
    END IF;
END$$
DELIMITER ;

CALL drop_manual_po_number_when_unused();
DROP PROCEDURE drop_manual_po_number_when_unused;
