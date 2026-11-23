using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSend.Repositories;

namespace SafeSend.Controllers;

[Authorize]
public class DashboardController(
    IShareLinkRepository repository,
    IShareLinkSummaryRepository summaryRepository) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var links = await repository.GetByOwnerAsync(userId);
        return View(links);
    }

    // AJAX GET: refreshes the dashboard data without a full page reload.
    [HttpGet]
    public async Task<IActionResult> Refresh()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var links = await repository.GetByOwnerAsync(userId);
        var summary = await summaryRepository.GetSummaryAsync(userId);
        return Json(new
        {
            summary,
            links = links.Select(x => new
            {
                x.Id,
                x.Label,
                created = x.CreatedAtUtc.ToLocalTime().ToString("dd MMM yyyy"),
                expires = x.ExpiresAtUtc.ToLocalTime().ToString("dd MMM yyyy"),
                status = x.IsRevoked ? "Revoked" : x.ExpiresAtUtc <= DateTime.UtcNow ? "Expired" : "Active"
            })
        });
    }

    // AJAX POST: a user can revoke only their own link.
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Revoke(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var revoked = await repository.RevokeAsync(id, userId);
        if (!revoked) return NotFound(new { success = false, message = "The link could not be found or was already revoked." });
        return Ok(new { success = true, message = "Link revoked." });
    }
}
