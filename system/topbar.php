<?php
// topbar.php — included on the right side of every page's .top-navbar
// Requires: $currentUser
// The "My Profile" modal itself lives in profile_modal.php, included separately
// at the end of <body> — .top-navbar has position:sticky + z-index, which
// creates its own stacking context, and a modal nested inside it gets trapped
// below Bootstrap's dynamically-appended .modal-backdrop (unclickable).
$tbUser    = $currentUser ?? ['full_name' => 'User', 'role' => 'user', 'avatar_path' => null];
$tbInitial = strtoupper(substr($tbUser['full_name'], 0, 1));
$tbAvatar  = avatarUrl($tbUser);
?>
<div class="topbar-actions">
    <div class="dropdown">
        <button class="topbar-icon-btn" type="button" id="topbarBell" data-bs-toggle="dropdown" aria-expanded="false" data-bs-auto-close="outside">
            <i class="fas fa-bell"></i>
            <span class="badge-dot d-none" id="topbarBellBadge">0</span>
        </button>
        <div class="dropdown-menu dropdown-menu-end notif-dropdown" aria-labelledby="topbarBell">
            <div class="notif-head">
                <span class="notif-title">Notifications</span>
                <div class="notif-more-wrap">
                    <button type="button" class="notif-more" id="topbarNotifMore" title="Options" aria-label="Notification options"><i class="fas fa-ellipsis"></i></button>
                    <div class="notif-menu d-none" id="topbarNotifMenu">
                        <button type="button" onclick="topbarMarkAllRead()"><i class="fas fa-check-double"></i> Mark all as read</button>
                    </div>
                </div>
            </div>
            <div class="notif-tabs" id="topbarNotifTabs">
                <button type="button" class="active" data-filter="all">All</button>
                <button type="button" data-filter="unread">Unread</button>
            </div>
            <div class="notif-list" id="topbarNotifList">
                <div class="notif-empty"><i class="fas fa-spinner fa-spin"></i>Loading…</div>
            </div>
            <?php /* One shared menu for every row's "…" button, filled and positioned
                     when opened. Lives here, outside the scrolling list, because the
                     list clips anything that overflows it. */ ?>
            <div class="notif-menu notif-row-menu d-none" id="topbarNotifRowMenu"></div>
        </div>
    </div>

    <div class="dropdown">
        <button class="user-pill" type="button" id="topbarUserMenu" data-bs-toggle="dropdown" aria-expanded="false">
            <span class="up-avatar">
                <?php if ($tbAvatar): ?>
                    <?php /* If the image 404s anyway, swap it for the initial rather
                             than leaving the browser's broken-image icon in a circle. */ ?>
                    <img src="<?= e($tbAvatar) ?>" alt=""
                         onerror="this.replaceWith(document.createTextNode(<?= e(json_encode($tbInitial)) ?>))">
                <?php else: ?>
                    <?= e($tbInitial) ?>
                <?php endif; ?>
            </span>
            <span class="up-name d-none d-sm-inline"><?= e($tbUser['full_name']) ?></span>
            <i class="fas fa-chevron-down up-caret"></i>
        </button>
        <div class="dropdown-menu dropdown-menu-end user-dropdown" aria-labelledby="topbarUserMenu">
            <div class="ud-role"><?= e(hrmisRoleName($tbUser['role_id'] ?? null) ?? roleLabel($tbUser['role'])) ?></div>
            <a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#profileModal"><i class="fas fa-user"></i> My Profile</a>
            <div class="dropdown-divider"></div>
            <a class="dropdown-item text-danger" href="#" onclick="confirmLogout();return false;"><i class="fas fa-sign-out-alt"></i> Logout</a>
        </div>
    </div>
</div>

<script>
function topbarEscapeHtml(value) {
    if (value === null || value === undefined) return '';
    return String(value)
        .replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;').replace(/'/g, '&#39;');
}

function topbarParseDate(iso) {
    return new Date(String(iso).replace(' ', 'T'));
}

// Facebook-style shorthand: 33m, 3h, 1d, 2w.
function topbarTimeAgo(iso) {
    const diff = (Date.now() - topbarParseDate(iso)) / 1000;
    if (diff < 60) return 'Just now';
    if (diff < 3600) return Math.floor(diff / 60) + 'm';
    if (diff < 86400) return Math.floor(diff / 3600) + 'h';
    if (diff < 7 * 86400) return Math.floor(diff / 86400) + 'd';
    if (diff < 365 * 86400) return Math.floor(diff / (7 * 86400)) + 'w';
    return Math.floor(diff / (365 * 86400)) + 'y';
}

// The big round icon says WHICH part of the system the notification is about,
// and the small badge on its corner says WHAT happened — the same two-part
// read as Facebook's avatar-plus-reaction. There is no actor avatar to show:
// a notification row stores the message text, not a user id. Both tables are
// first-match-wins, so order matters ("unlock" must beat "lock", and "undone"
// must beat "revised").
const TOPBAR_NOTIF_MODULES = [
    ['competitive_bidding', '#7c3aed', 'fa-gavel'],
    ['purchase_orders',     '#2563eb', 'fa-file-invoice'],
    ['inspection',          '#0d9488', 'fa-clipboard-check'],
    ['inventory',           '#d97706', 'fa-boxes-stacked'],
];
const TOPBAR_NOTIF_EVENTS = [
    [/unlock/i,                                 '#d97706', 'fa-lock-open'],
    [/lock/i,                                   '#64748b', 'fa-lock'],
    [/undone|undid/i,                           '#ea580c', 'fa-rotate-left'],
    [/removed|cleared/i,                        '#dc2626', 'fa-trash'],
    [/approved/i,                               '#16a34a', 'fa-check'],
    [/created|recorded|received into/i,         '#16a34a', 'fa-plus'],
    [/transfer|returned/i,                      '#2563eb', 'fa-right-left'],
    [/updated|revised|\bset\b|status/i,         '#2563eb', 'fa-pen'],
];

function topbarNotifIcons(n) {
    const link = String(n.link || '');
    const mod = TOPBAR_NOTIF_MODULES.find(m => link.startsWith(m[0])) || [null, '#64748b', 'fa-bell'];
    const evt = TOPBAR_NOTIF_EVENTS.find(e => e[0].test(n.title || '')) || [null, '#64748b', 'fa-bell'];
    return { modColor: mod[1], modIcon: mod[2], evtColor: evt[1], evtIcon: evt[2] };
}

// The real count, not the badge's own text — the badge displays "9+" past
// nine, and parsing THAT back into a number to decrement it would floor
// anything above 9 at 9 forever. Kept in sync by topbarLoadNotifications()
// and stepped down by topbarMarkRead()/topbarMarkAllRead() without a full
// reload, which is what makes clicking a notification feel instant instead
// of waiting for the next time the bell is opened.
let topbarUnreadCount = 0;

function topbarRenderBadge() {
    const badge = document.getElementById('topbarBellBadge');
    if (topbarUnreadCount > 0) {
        badge.textContent = topbarUnreadCount > 9 ? '9+' : topbarUnreadCount;
        badge.classList.remove('d-none');
    } else {
        badge.classList.add('d-none');
    }
}

const TOPBAR_NOTIF_PAGE = 10;
const topbarNotif = { filter: 'all', items: [], hasMore: false, seq: 0 };

// append = false reloads the first page (opening the bell, switching tab);
// append = true is "See previous notifications" asking for the next page.
async function topbarLoadNotifications(append = false) {
    const st  = topbarNotif;
    const seq = ++st.seq;        // a slower, older response must not overwrite a newer one
    const list = document.getElementById('topbarNotifList');
    if (append) {
        const btn = list.querySelector('.notif-more-btn');
        if (btn) { btn.disabled = true; btn.textContent = 'Loading…'; }
    }
    try {
        // On the Unread tab an item marked read here has already left the server's
        // unread list, so it must not be counted or "See previous" skips a row.
        const offset = !append ? 0
            : (st.filter === 'unread' ? st.items.filter(n => !topbarIsRead(n)).length : st.items.length);
        const res  = await fetch(`api/notifications.php?limit=${TOPBAR_NOTIF_PAGE}&offset=${offset}`
            + (st.filter === 'unread' ? '&unread=1' : ''));
        const data = await res.json();
        if (!data.success || seq !== st.seq) return;

        topbarUnreadCount = data.unread_count || 0;
        topbarRenderBadge();
        st.items   = append ? st.items.concat(data.notifications) : data.notifications;
        st.hasMore = !!data.has_more;
        topbarRenderNotifications();
    } catch (err) {
        if (seq !== st.seq) return;
        list.innerHTML = '<div class="notif-empty">Failed to load notifications.</div>';
        console.error(err);
    }
}

// is_read arrives as 0/1 from the API; Number() keeps a stringly "0" from
// counting as truthy.
const topbarIsRead = n => Number(n.is_read) === 1;

// The row is a div holding a link, not a link itself: the "…" button sits on
// the row, and a button nested inside an <a> would navigate on every click.
function topbarNotifItemHtml(n) {
    const ic = topbarNotifIcons(n);
    const id = topbarEscapeHtml(n.id);
    return `
        <div class="notif-item ${topbarIsRead(n) ? '' : 'unread'}" data-id="${id}">
            <a class="ni-link" href="${topbarEscapeHtml(n.link) || '#'}" onclick="topbarMarkRead('${id}', this.closest('.notif-item'))">
                <span class="ni-avatar" style="background:${ic.modColor}">
                    <i class="fas ${ic.modIcon}"></i>
                    <span class="ni-badge" style="background:${ic.evtColor}"><i class="fas ${ic.evtIcon}"></i></span>
                </span>
                <span class="ni-body">
                    <div class="ni-text"><strong>${topbarEscapeHtml(n.title)}</strong>${n.message ? `<div class="ni-msg">${topbarEscapeHtml(n.message)}</div>` : ''}</div>
                    <div class="ni-time">${topbarTimeAgo(n.created_at)}</div>
                </span>
            </a>
            <span class="ni-dot"></span>
            <button type="button" class="ni-more" title="More options" aria-label="More options for this notification"><i class="fas fa-ellipsis"></i></button>
        </div>`;
}

// New = the last hour, Today = the rest of today, Earlier = anything before.
// Time-based like Facebook's, not read/unread: a read item can still be "New".
function topbarRenderNotifications() {
    const st   = topbarNotif;
    const list = document.getElementById('topbarNotifList');
    const scrollTop = list.scrollTop;   // a re-render must not throw the reader back to the top
    topbarCloseMenus();
    if (!st.items.length) {
        list.innerHTML = st.filter === 'unread'
            ? '<div class="notif-empty"><i class="fas fa-check-circle"></i>You\'re all caught up.</div>'
            : '<div class="notif-empty"><i class="fas fa-bell-slash"></i>No notifications yet.</div>';
        return;
    }
    const now = new Date();
    const startOfToday = new Date(now.getFullYear(), now.getMonth(), now.getDate());
    const groups = { 'New': [], 'Today': [], 'Earlier': [] };
    st.items.forEach(n => {
        const t = topbarParseDate(n.created_at);
        groups[(now - t) / 1000 < 3600 ? 'New' : (t >= startOfToday ? 'Today' : 'Earlier')].push(n);
    });
    list.innerHTML = Object.entries(groups)
        .filter(([, items]) => items.length)
        .map(([label, items]) => `<div class="notif-group"><span>${label}</span></div>${items.map(topbarNotifItemHtml).join('')}`)
        .join('')
        + (st.hasMore ? '<button type="button" class="notif-more-btn" onclick="topbarLoadNotifications(true)">See previous notifications</button>' : '');
    list.scrollTop = scrollTop;
}

// ── Per-notification menu ("…" on a row): mark read / unread, delete ──────
let topbarRowMenuId = null;

function topbarCloseMenus() {
    document.getElementById('topbarNotifMenu')?.classList.add('d-none');
    document.getElementById('topbarNotifRowMenu')?.classList.add('d-none');
    document.querySelectorAll('.notif-item.menu-open').forEach(el => el.classList.remove('menu-open'));
    topbarRowMenuId = null;
}

function topbarOpenRowMenu(btn) {
    const row  = btn.closest('.notif-item');
    const item = topbarNotif.items.find(n => n.id === row.dataset.id);
    if (!item) return;
    const wasOpen = topbarRowMenuId === item.id;
    topbarCloseMenus();
    if (wasOpen) return;                       // clicking the same "…" again closes it

    const read = topbarIsRead(item);
    const menu = document.getElementById('topbarNotifRowMenu');
    menu.innerHTML = `
        <button type="button" data-action="${read ? 'unread' : 'read'}"><i class="fas ${read ? 'fa-envelope' : 'fa-check'}"></i> ${read ? 'Mark as unread' : 'Mark as read'}</button>
        <button type="button" data-action="delete"><i class="far fa-rectangle-xmark"></i> Delete this notification</button>`;
    menu.dataset.id = item.id;
    menu.classList.remove('d-none');           // shown first so it can be measured

    // Placed against the panel, not the button: the panel is the menu's offset
    // parent. Right edge to the button's, then clamped inside the panel and
    // flipped above the button when there is no room below.
    const panel = document.querySelector('.notif-dropdown').getBoundingClientRect();
    const b = btn.getBoundingClientRect();
    let left = b.right - panel.left - menu.offsetWidth;
    left = Math.max(8, Math.min(left, panel.width - menu.offsetWidth - 8));
    let top = b.bottom - panel.top + 4;
    if (top + menu.offsetHeight > panel.height - 8) top = b.top - panel.top - menu.offsetHeight - 4;
    menu.style.left = left + 'px';
    menu.style.top  = Math.max(8, top) + 'px';

    row.classList.add('menu-open');
    topbarRowMenuId = item.id;
}

// Optimistic: the list and the badge change at once and the request follows;
// if it fails, the list is reloaded from the server so it cannot stay wrong.
async function topbarRowAction(action, id) {
    const st  = topbarNotif;
    const idx = st.items.findIndex(n => n.id === id);
    if (idx < 0) return;
    const item = st.items[idx];
    const wasRead = topbarIsRead(item);

    if (action === 'read' && !wasRead) {
        item.is_read = 1;
        topbarUnreadCount = Math.max(0, topbarUnreadCount - 1);
    } else if (action === 'unread' && wasRead) {
        item.is_read = 0;
        topbarUnreadCount++;
    } else if (action === 'delete') {
        st.items.splice(idx, 1);
        if (!wasRead) topbarUnreadCount = Math.max(0, topbarUnreadCount - 1);
    }
    topbarRenderBadge();
    topbarRenderNotifications();
    // Deleting the last loaded row while older ones exist would otherwise strand
    // the panel on an empty state with a "See previous" button that is not there.
    if (!st.items.length && st.hasMore) topbarLoadNotifications(false);

    try {
        const res  = await fetch('api/notifications.php', {
            method: 'POST', headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ action: { read: 'mark_read', unread: 'mark_unread', delete: 'delete' }[action], id }),
        });
        const data = await res.json();
        if (!data.success) throw new Error(data.message || 'Request failed');
    } catch (err) {
        console.error(err);
        topbarLoadNotifications(false);
    }
}

// Marks read the moment it's clicked — the unread highlight clears and the
// badge count steps down right away (Facebook-style), instead of only
// catching up the next time the bell dropdown is reopened. el is the clicked
// <a> itself (passed as `this` from the inline handler) rather than looked
// up again by data-id, since we already have it for free.
function topbarMarkRead(id, el) {
    if (el && el.classList.contains('unread')) {
        el.classList.remove('unread');
        topbarUnreadCount = Math.max(0, topbarUnreadCount - 1);
        topbarRenderBadge();
        const item = topbarNotif.items.find(n => n.id === id);
        if (item) item.is_read = 1;
    }
    fetch('api/notifications.php', {
        method: 'POST', headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ action: 'mark_read', id }),
    }).catch(() => {});
}

async function topbarMarkAllRead() {
    topbarCloseMenus();
    try {
        await fetch('api/notifications.php', {
            method: 'POST', headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ action: 'mark_all_read' }),
        });
        topbarLoadNotifications();
    } catch (err) { console.error(err); }
}

// The listener wraps the call: passing topbarLoadNotifications straight in would
// hand it Bootstrap's event object as `append`, which is truthy.
document.getElementById('topbarBell')?.addEventListener('show.bs.dropdown', () => {
    document.getElementById('topbarNotifList').scrollTop = 0;
    topbarLoadNotifications(false);
});
document.getElementById('topbarBell')?.addEventListener('hide.bs.dropdown', topbarCloseMenus);

// All / Unread pills.
document.getElementById('topbarNotifTabs')?.addEventListener('click', e => {
    const btn = e.target.closest('button[data-filter]');
    if (!btn || btn.dataset.filter === topbarNotif.filter) return;
    topbarNotif.filter = btn.dataset.filter;
    topbarNotif.items = [];
    document.querySelectorAll('#topbarNotifTabs button').forEach(b => b.classList.toggle('active', b === btn));
    document.getElementById('topbarNotifList').innerHTML = '<div class="notif-empty"><i class="fas fa-spinner fa-spin"></i>Loading…</div>';
    topbarLoadNotifications(false);
});

// The header "…" menu. Any click elsewhere inside the panel closes it again.
document.getElementById('topbarNotifMore')?.addEventListener('click', () => {
    const menu = document.getElementById('topbarNotifMenu');
    const opening = menu.classList.contains('d-none');
    topbarCloseMenus();
    if (opening) menu.classList.remove('d-none');
});
document.querySelector('.notif-dropdown')?.addEventListener('click', e => {
    if (!e.target.closest('.notif-more-wrap, .ni-more, .notif-row-menu')) topbarCloseMenus();
});

// Each row's "…" button, and the shared menu it opens.
document.getElementById('topbarNotifList')?.addEventListener('click', e => {
    const btn = e.target.closest('.ni-more');
    if (btn) topbarOpenRowMenu(btn);
});
document.getElementById('topbarNotifList')?.addEventListener('scroll', () => {
    if (topbarRowMenuId) topbarCloseMenus();   // it is positioned to the row; scrolling would strand it
});
document.getElementById('topbarNotifRowMenu')?.addEventListener('click', e => {
    const btn = e.target.closest('button[data-action]');
    if (!btn) return;
    const id = e.currentTarget.dataset.id;
    topbarCloseMenus();
    topbarRowAction(btn.dataset.action, id);
});

topbarLoadNotifications(false);
</script>
