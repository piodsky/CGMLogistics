-- Accounting User: a VIEW-ONLY role, and the existing "accounting" login moved onto it.
--
--     mysql -u prroot -p purchase_request < database/accounting_user_role.sql
--
-- What the role is allowed (the rules are in config.php — VIEW_ONLY_ROLE_IDS,
-- isViewOnlyUser(), and requireAuth(), which refuses every save / change / delete
-- for the role on the server, whatever the page or API):
--   * see Purchase Orders, Inspection & Acceptance, Inventory and Reports;
--   * preview, print and export;
--   * add or change nothing.
--
-- Three writes, each guarded so re-running is harmless:
--
-- 1. A new row in the shared HRMIS role table. table_role is read by the legacy
--    VB.NET apps too; a new row only adds a choice to their role list. The
--    columns are the role's TEMPLATE — System Users copies them onto a person's
--    system_access_level row when this role is assigned — so they say: Purchase
--    Orders, Inspection, Inventory, Reports, and nothing else (no Purchase
--    Request, Bidding, Abstract, ICS/ARE, Warehousing or any System screen).
--    role_id is left to AUTO_INCREMENT, which is 15 today; config.php's
--    VIEW_ONLY_ROLE_IDS names 15, so check the id this gives (bottom of file).
--
-- 2. Belt and braces for the Purchase Order screen: no edit / delete / lock,
--    through tbl_po_permissions, the same table Access Levels writes and the
--    same thing roles 5, 6 and 10 already have set. It makes the PO screen hide
--    those buttons itself; the server rule above is the real lock.
--
-- 3. Moves the existing "accounting" login (user_id 112, piid 5542, City
--    Accountants Office) onto the new role, and gives its access row the four
--    flags. Only touches that one row and only when it is still as found —
--    role_id 14 on the login, role_id 7 on the access row.
--
-- ORIGINAL values, for undoing step 3 (nothing else on that access row is changed):
--     users.role_id 14 (Warehousing Administrator)
--     system_access_level AL_ID 126: role_id 7, purchase_order 0,
--       inspection_and_acceptance 0, inventory 0, reports 1  (system 1, unchanged)

-- ── 1. The role ─────────────────────────────────────────────────────────
INSERT INTO table_role
    (role, description, `system`, system_user, system_access_level, maintainance_table, personel_administration,
     database_management, ppmp_items, ppmp, app, procurement, purchase_request, canvas_quotation, abstract,
     purchase_order, inspection, inventory, segregation, ics, are, archive, warehousing, reports)
SELECT 'Accounting User', 'View only: Purchase Orders, Inspection, Inventory and Reports. Cannot add or change anything.',
       0, 0, 0, 0, 0,
       0, 0, 0, 0, 1, 0, 0, 0,
       1, 1, 1, 0, 0, 0, 0, 0, 1
FROM DUAL
WHERE NOT EXISTS (SELECT 1 FROM table_role WHERE role = 'Accounting User');

-- ── 2. No edit / delete / lock on Purchase Orders ───────────────────────
INSERT INTO tbl_po_permissions (role_id, can_lock, can_delete, can_edit, Date_Updated, Updated_By)
SELECT r.role_id, 0, 0, 0, NOW(), 'accounting_user_role.sql'
FROM table_role r
WHERE r.role = 'Accounting User'
  AND NOT EXISTS (SELECT 1 FROM tbl_po_permissions p WHERE p.role_id = r.role_id);

-- ── 3. The existing "accounting" login ──────────────────────────────────
UPDATE users
SET role_id = (SELECT role_id FROM table_role WHERE role = 'Accounting User')
WHERE user_id = 112 AND user_name = 'accounting' AND role_id = 14;

UPDATE system_access_level
SET role_id = (SELECT role_id FROM table_role WHERE role = 'Accounting User'),
    purchase_order = 1, inspection_and_acceptance = 1, inventory = 1, reports = 1
WHERE AL_ID = 126 AND piid = 5542 AND role_id = 7;

-- Check: the role's id (config.php must say the same), and the account.
--   SELECT role_id, role FROM table_role WHERE role = 'Accounting User';
--   SELECT user_id, user_name, role_id FROM users WHERE user_id = 112;
