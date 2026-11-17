using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSend.Repositories;

namespace SafeSend.Controllers;

[Authorize]
public class DashboardController(IShareLinkRepository repository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var links = await repository.GetByOwnerAsync(userId);
        return View(links);
    }
}
