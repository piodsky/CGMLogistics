<?php
// PAR / ICS number renewal — the rules, in one place.
//
// A PAR or ICS number is issued once (PAR2026900516) and then RENEWED: every few
// years the same property is re-acknowledged and its form is reprinted with the
// year of the renewal on the end (PAR2026900516-R2029). The renewal is the same
// property under the same number, so the suffix REPLACES the previous one — it is
// never stacked. An earlier version appended "-R<year>" to whatever was there,
// which is how PAR2026900516 became PAR2026900516-R2026-R2026-R2026.
//
//   PAR2026900516            issued 2026
//   PAR2026900516-R2026      renewed 2026
//   PAR2026900516-R2029      renewed again in 2029 (replaces -R2026, not appended)
//
// Renewal is suggested every PAR_ICS_RENEW_YEARS years. The number is the only
// record of when it was issued: the issuance log (auto_parnew / auto_icsnew) has
// no date column. So "when was this last issued or renewed" is read off the
// number itself — the year of the last -R suffix if there is one, otherwise the
// year the number was first issued (the four digits after the PAR / ICS prefix).
// Whole years, on purpose: it matches how the suffix is written.
//
// Used by api/inventory_issuance.php (to renew) and api/inventory.php (to flag
// what is due), so the two cannot disagree.

const PAR_ICS_RENEW_YEARS = 3;

// The number with every trailing -R<year> taken off. Strips ALL of them, so a
// number that was already stacked by the old behaviour comes back clean.
function parIcsBaseNumber(string $number): string {
    return preg_replace('/(?:-R[0-9]{4})+$/', '', trim($number));
}

// The number as it reads after a renewal in $year (this year by default). Renewing
// again in the same year gives the same number back — the suffix is never repeated.
function parIcsRenewedNumber(string $number, ?int $year = null): string {
    return parIcsBaseNumber($number) . '-R' . ($year ?? (int)date('Y'));
}

// The year this number was last issued or renewed, or null when it cannot be told
// (an old free-form number, a blank). Numbers from before the PAR / ICS prefixes
// are digits only ("20210003123"), so the prefix is optional. A first four digits
// that are not a plausible year are not guessed at.
function parIcsIssuedYear(string $number): ?int {
    $number = trim($number);
    if (preg_match('/-R([0-9]{4})$/', $number, $m)) {
        $year = (int)$m[1];
    } elseif (preg_match('/^[A-Za-z]*([0-9]{4})/', $number, $m)) {
        $year = (int)$m[1];
    } else {
        return null;
    }
    return ($year >= 2000 && $year <= 2100) ? $year : null;
}

// True once PAR_ICS_RENEW_YEARS full years have passed since it was issued or last
// renewed: 2023 -> due in 2026. A number whose year cannot be read is never
// reported as due — flagging on a guess would only train people to ignore it.
function parIcsRenewalDue(string $number, ?int $nowYear = null): bool {
    $issued = parIcsIssuedYear($number);
    return $issued !== null && (($nowYear ?? (int)date('Y')) - $issued) >= PAR_ICS_RENEW_YEARS;
}
