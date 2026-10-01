<?php
// Notification bell.
//
// One source now: the Purchase Order module's notifications on the HRMIS
// server (tbl_po_notifications, written by notifyPoUsers() in config.php).
// The Pyo Base System's own local notifications table went away with
// pyobasesystem_db — nothing ever wrote to it in this app, so merging it in
// only added a second set of ids to disambiguate and a second database that
// had to be reachable for the bell to render.
//
// Ids are still prefixed "po:" so the mark-read routing and the frontend that
// parses that prefix keep working unchanged, and so a second source can be
// reintroduced later without another id collision.
require_once '../config.php';
requireAuth();
header('Content-Type: application/json');

$user   = getCurrentUser();
$method = $_SERVER['REQUEST_METHOD'];

if ($method === 'GET') {
    // Paged: the bell shows a page at a time and "See previous notifications"
    // asks for the next. One extra row is fetched to learn whether there is a
    // next page without a second COUNT query.
    $limit      = min(30, max(1, (int)($_GET['limit'] ?? 10)));
    $offset     = max(0, (int)($_GET['offset'] ?? 0));
    $unreadOnly = ($_GET['unread'] ?? '') === '1';

    $rows    = getRecentPoNotifications($user['id'], $limit + 1, $offset, $unreadOnly);
    $hasMore = count($rows) > $limit;
    $rows    = array_slice($rows, 0, $limit);

    $notifications = array_map(
        fn($n) => ['id' => 'po:' . $n['id'], 'source' => 'po'] + $n,
        $rows
    );

    jsonResponse(true, '', [
        'notifications' => $notifications,
        'unread_count'  => getUnreadPoNotificationCount($user['id']),
        'has_more'      => $hasMore,
    ]);
}

if ($method === 'POST') {
    $data   = getInput();
    $action = $data['action'] ?? '';

    // Accepts both "po:7" and a bare "7" so an older cached page doesn't break.
    $notificationId = function () use ($data) {
        [$source, $id] = array_pad(explode(':', (string)($data['id'] ?? ''), 2), 2, null);
        $id = (int)($id ?? $source);
        if (!$id) jsonResponse(false, 'Invalid notification ID.');
        return $id;
    };

    if ($action === 'mark_read') {
        markPoNotificationRead($notificationId(), $user['id']);
        jsonResponse(true, 'Marked as read.');
    }

    if ($action === 'mark_unread') {
        markPoNotificationUnread($notificationId(), $user['id']);
        jsonResponse(true, 'Marked as unread.');
    }

    if ($action === 'delete') {
        if (!deletePoNotification($notificationId(), $user['id'])) {
            jsonResponse(false, 'Notification not found.');
        }
        jsonResponse(true, 'Notification deleted.');
    }

    if ($action === 'mark_all_read') {
        markAllPoNotificationsRead($user['id']);
        jsonResponse(true, 'All notifications marked as read.');
    }

    jsonResponse(false, 'Invalid action.');
}

jsonResponse(false, 'Method not allowed.');
