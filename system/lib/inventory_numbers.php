<?php
// Inventory transfer numbers (ITR / PTR): one NEW number for every transfer.
//
// Why this exists. The transfer action used to work the next number out as "the
// highest id in the numbering log (auto_itr / auto_ptr) plus one" and then never
// wrote the number into that log. So the highest id never moved, and every
// transfer recorded that way got the same number: 17 ITRs, across many purchase
// orders, all printed as ITR202691204. A purchase order with five transfers is
// five different documents and needs five different numbers.
//
// Here the number is allocated by the log's OWN AUTO_INCREMENT id, the way a PO
// number is built from its row id: insert the log row first, read the id the
// database gave it, build the number from that. Two people recording transfers at
// the same moment cannot be handed the same id, so they cannot get the same number
// — which reading "max + 1" and inserting later could not promise.
//
// The number keeps the shape the log has always had (unpadded month, padded id:
// ITR202691203 = 2026, month 9, id 1203), so it reads as part of the same series.
//
// A number that is ALREADY on a recorded transfer is never issued again. That
// matters once, right now: ITR202691204 is the number all 17 existing transfers
// share, and it is also exactly what the next id would produce. It is skipped and
// the row that would have carried it is left in the log as skipped.

// $t: the ISSUANCE_TABLES entry for the document ('seq', 'idCol', 'numCol',
// 'prefix', 'pad'). $transfersTable and $seqTable can be overridden so this can be
// rehearsed against temporary tables. Call it INSIDE the transfer's transaction, so
// a transfer that fails to save does not leave a number logged against nothing.
function invIssueTransferNumber(PDO $db, array $t, string $transferType, array $invIds, string $category, string $printedBy,
                                string $transfersTable = 'tbl_inventory_transfers'): string {
    $ins   = $db->prepare("INSERT INTO {$t['seq']} ({$t['numCol']}, Printed_by, Inv_ID, Inventory_Category) VALUES (?, ?, ?, ?)");
    $used  = $db->prepare("SELECT COUNT(*) FROM {$transfersTable} WHERE Transfer_Type = ? AND Transfer_Number = ?");
    $set   = $db->prepare("UPDATE {$t['seq']} SET {$t['numCol']} = ? WHERE {$t['idCol']} = ?");
    $skip  = $db->prepare("UPDATE {$t['seq']} SET {$t['numCol']} = ?, Printed_by = ?, Inv_ID = '' WHERE {$t['idCol']} = ?");
    for ($attempt = 0; $attempt < 100; $attempt++) {
        $ins->execute(['', $printedBy, implode(',', $invIds), $category]);
        $id = (int)$db->lastInsertId();
        // Same construction as invNextNumber(): unpadded month, id padded to $t['pad'].
        $number = $t['prefix'] . date('Y') . (int)date('n') . str_pad((string)$id, $t['pad'], '0', STR_PAD_LEFT);
        $used->execute([strtoupper($transferType), $number]);
        if ((int)$used->fetchColumn() === 0) {
            $set->execute([$number, $id]);
            return $number;
        }
        $skip->execute([$number, '(skipped: number already used)', $id]);
    }
    throw new RuntimeException('Could not find an unused number.');
}

// ── Repairing transfers that already share a number ─────────────────────────
//
// For the transfers recorded before the numbering above was fixed: every ITR shared
// one number. In each group of transfers sharing a number, the EARLIEST (lowest
// Transfer_ID — the order they were recorded in) keeps it, and each later one is
// given the next new number from the numbering log, in that same order.
//
// Three places carry a transfer's number and are kept in step:
//   * tbl_inventory_transfers.Transfer_Number — the transfer itself;
//   * the numbering log (auto_itr / auto_ptr), which gets a proper row for the number
//     that is kept and for every new one — who recorded it and which items it moved;
//   * tbl_inventory_items_details.PTR_ITR_Number on each item's CURRENT row, which
//     holds the LATEST transfer the item was in. An item moved several times ends up
//     showing the number of its last transfer, which is what that column always meant.
// Soft-deleted (superseded) item rows are left as they were, and so is the audit log,
// which is a record of what was said at the time.
//
// $commit = false changes nothing and returns the groups and the transfers that would
// be renumbered (without numbers: allocating one is itself a write to the log).
// $T names the tables, so it can be rehearsed on temporary copies. Returns
// ['groups' => [...], 'changes' => [['id','old','new','po','items'=>[...]]], 'items' => [...], 'undo' => [sql...]].
function invRenumberDuplicateTransfers(PDO $db, array $t, string $type, bool $commit, array $T = []): array {
    $T += ['transfers' => 'tbl_inventory_transfers', 'tItems' => 'tbl_inventory_transfer_items', 'details' => 'tbl_inventory_items_details'];
    $type = strtoupper($type);
    $out = ['groups' => [], 'changes' => [], 'items' => [], 'undo' => []];

    $g = $db->prepare("SELECT Transfer_Number FROM {$T['transfers']} WHERE Transfer_Type = ? AND isDeleted = 0
                       GROUP BY Transfer_Number HAVING COUNT(*) > 1 ORDER BY MIN(Transfer_ID)");
    $g->execute([$type]);
    $numbers = $g->fetchAll(PDO::FETCH_COLUMN);
    if (!$numbers) return $out;

    $rowsQ  = $db->prepare("SELECT Transfer_ID, PO_Number, Printed_By FROM {$T['transfers']}
                            WHERE Transfer_Type = ? AND Transfer_Number = ? AND isDeleted = 0 ORDER BY Transfer_ID");
    $itemsQ = $db->prepare("SELECT ti.Inv_ID, d.Inventory_Category FROM {$T['tItems']} ti
                            LEFT JOIN {$T['details']} d ON d.Inv_ID = ti.Inv_ID WHERE ti.Transfer_ID = ? ORDER BY ti.Inv_ID");
    $plan = [];
    foreach ($numbers as $number) {
        $rowsQ->execute([$type, $number]);
        $rows = $rowsQ->fetchAll(PDO::FETCH_ASSOC);
        $out['groups'][] = ['number' => $number, 'transfers' => count($rows), 'keeps' => (int)$rows[0]['Transfer_ID']];
        foreach ($rows as $i => $r) {
            $itemsQ->execute([$r['Transfer_ID']]);
            $items = $itemsQ->fetchAll(PDO::FETCH_ASSOC);
            $entry = ['id' => (int)$r['Transfer_ID'], 'old' => $number, 'new' => null, 'po' => $r['PO_Number'], 'printed_by' => (string)$r['Printed_By'],
                      'items' => array_map('intval', array_column($items, 'Inv_ID')),
                      'category' => (string)(($items[0]['Inventory_Category'] ?? '') ?: ($type === 'PTR' ? 'PAR' : 'ICS')), 'keep' => $i === 0];
            $plan[] = $entry;
            if ($i > 0) $out['changes'][] = $entry;
        }
    }
    if (!$commit) return $out;

    $logHas  = $db->prepare("SELECT COUNT(*) FROM {$t['seq']} WHERE {$t['numCol']} = ?");
    $logAdd  = $db->prepare("INSERT INTO {$t['seq']} ({$t['numCol']}, Printed_by, Inv_ID, Inventory_Category) VALUES (?, ?, ?, ?)");
    $logFix  = $db->prepare("UPDATE {$t['seq']} SET Printed_by = ?, Inv_ID = ?, Inventory_Category = ? WHERE {$t['numCol']} = ? AND Printed_by LIKE '(skipped%'");
    $setNum  = $db->prepare("UPDATE {$T['transfers']} SET Transfer_Number = ? WHERE Transfer_ID = ? AND Transfer_Number = ?");
    $newOut  = [];
    try {
        $db->beginTransaction();
        foreach ($plan as $e) {
            $ids = implode(',', $e['items']);
            if ($e['keep']) {
                // The kept number gets its proper row in the log (or, if the allocator has
                // already marked that id as skipped, the row is filled in).
                $logHas->execute([$e['old']]);
                if ((int)$logHas->fetchColumn() === 0) $logAdd->execute([$e['old'], $e['printed_by'], $ids, $e['category']]);
                else $logFix->execute([$e['printed_by'], $ids, $e['category'], $e['old']]);
                continue;
            }
            $new = invIssueTransferNumber($db, $t, $type, $e['items'], $e['category'], $e['printed_by'], $T['transfers']);
            $setNum->execute([$new, $e['id'], $e['old']]);
            $newOut[$e['id']] = $new;
            $out['undo'][] = "UPDATE {$T['transfers']} SET Transfer_Number = " . $db->quote($e['old']) . " WHERE Transfer_ID = {$e['id']} AND Transfer_Number = " . $db->quote($new) . ';';
        }
        foreach ($out['changes'] as &$c) $c['new'] = $newOut[$c['id']];
        unset($c);

        // Each affected item's current row shows the number of the LATEST transfer it was in.
        $latest = $db->prepare("SELECT t.Transfer_Number FROM {$T['tItems']} ti JOIN {$T['transfers']} t ON t.Transfer_ID = ti.Transfer_ID
                                WHERE ti.Inv_ID = ? AND t.Transfer_Type = ? AND t.isDeleted = 0 ORDER BY t.Transfer_ID DESC LIMIT 1");
        $upd = $db->prepare("UPDATE {$T['details']} SET PTR_ITR_Number = ? WHERE Inv_ID = ? AND isDeleted = '0' AND PTR_ITR_Number = ?");
        $seen = [];
        foreach ($out['changes'] as $c) foreach ($c['items'] as $invId) {
            if (isset($seen[$invId . '|' . $c['old']])) continue;
            $seen[$invId . '|' . $c['old']] = true;
            $latest->execute([$invId, $type]);
            $to = $latest->fetchColumn();
            if ($to === false || $to === $c['old']) continue;
            $upd->execute([$to, $invId, $c['old']]);
            if ($upd->rowCount()) {
                $out['items'][] = ['inv_id' => $invId, 'old' => $c['old'], 'new' => $to];
                $out['undo'][] = "UPDATE {$T['details']} SET PTR_ITR_Number = " . $db->quote($c['old']) . " WHERE Inv_ID = {$invId} AND isDeleted = '0' AND PTR_ITR_Number = " . $db->quote($to) . ';';
            }
        }
        $db->commit();
    } catch (Throwable $e) {
        if ($db->inTransaction()) $db->rollBack();
        throw $e;
    }
    return $out;
}
