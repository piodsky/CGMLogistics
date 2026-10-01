<?php
// Moves a Purchase Request along its status ladder: submit, approve, return,
// reopen, cancel. One endpoint, one function (prTransition in
// lib/pr_workflow.php) doing the checking and the write, so every button on the
// page goes through the same rules.
require_once '../config.php';
require_once '../lib/pr_workflow.php';
requireAuth();
requireModule('purchase_requests');
header('Content-Type: application/json');
$db = getPoDB();

if ($_SERVER['REQUEST_METHOD'] !== 'POST') jsonResponse(false, 'Only POST is allowed.');

$data = getInput();
$id = (int)($data['id'] ?? 0);
$action = (string)($data['action'] ?? '');
$reason = trim((string)($data['reason'] ?? ''));
if (!$id) jsonResponse(false, 'Purchase request id is required.');

$me = getCurrentUser();
$actor = $me['full_name'] ?? ($me['username'] ?? 'unknown');
// Read fresh from HRMIS — the session only carries the collapsed three-tier role.
$isApprover = prActionPermission(hrmisRoleId($me['id'] ?? 0), 'can_approve');

try {
    $r = prTransition($db, $id, $action, $reason, $isApprover, (int)($me['id'] ?? 0) ?: null, $actor);
    $pr = prLoad($db, $id);
} catch (InvalidArgumentException $e) {
    jsonResponse(false, $e->getMessage());
} catch (Throwable $e) {
    error_log('purchase_request_status: ' . $e->getMessage());
    jsonResponse(false, 'Nothing was changed: the status could not be updated.');
}

$number = $pr['PR_Number'];
logAudit(strtoupper($action) . '_PR', 'purchase_requests',
    "{$number}: {$r['from']} -> {$r['to']}" . ($reason !== '' ? " — {$reason}" : ''));

// Submitted goes to the people who can act on it; the rest are records of what
// happened, so everyone on the desk hears them (notifyPoUsers has two audiences).
$titles = [
    'submit' => 'Purchase Request Submitted', 'approve' => 'Purchase Request Approved',
    'return' => 'Purchase Request Returned', 'reopen' => 'Purchase Request Reopened', 'cancel' => 'Purchase Request Cancelled',
];
notifyPoUsers($titles[$action] ?? 'Purchase Request Updated',
    "{$actor} — {$number}: {$r['from']} → {$r['to']}" . ($reason !== '' ? ' (' . mb_substr($reason, 0, 80) . ')' : ''),
    'purchase_requests.php?pr_number=' . urlencode($number), null, $action === 'submit' ? 'admins' : 'all');

jsonResponse(true, "{$number} is now {$r['to']}.", ['status' => $r['to'], 'row_version' => (int)$pr['Row_Version']]);
