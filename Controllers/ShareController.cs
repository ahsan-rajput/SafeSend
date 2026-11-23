using Microsoft.AspNetCore.Mvc;
using SafeSend.Models;
using SafeSend.Repositories;

namespace SafeSend.Controllers;

public class ShareController(IShareLinkRepository repository) : Controller
{
    [HttpGet]
    public IActionResult Create() => View();

    // AJAX POST: stores link metadata only in this delivery; it does not create a usable encrypted share URL.
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAjax(CreateShareLinkViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).Where(e => !string.IsNullOrWhiteSpace(e)).ToArray();
            return BadRequest(new { success = false, message = errors.FirstOrDefault() ?? "Please check the form." });
        }

        var expiryUtc = DateTime.SpecifyKind(model.ExpiresAtLocal, DateTimeKind.Local).ToUniversalTime();
        if (expiryUtc <= DateTime.UtcNow)
            return BadRequest(new { success = false, message = "Choose an expiry time in the future." });

        var ownerId = User.Identity?.IsAuthenticated == true
            ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "anonymous"
            : "anonymous";

        var link = new ShareLink
        {
            OwnerId = ownerId,
            Label = model.Label.Trim(),
            CreatedAtUtc = DateTime.UtcNow,
            ExpiresAtUtc = expiryUtc,
            IsRevoked = false
        };

        var id = await repository.CreateAsync(link);
        return Ok(new
        {
            success = true,
            id,
            message = "Link settings saved successfully. A secure shareable URL is not generated in this delivery."
        });
    }

    [HttpGet("s/{id}")]
    public IActionResult ViewLink(string id)
    {
        ViewData["ShareId"] = id;
        return View("ViewLink");
    }
}
