using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SafeSend.Models;

namespace SafeSend.Controllers;

public class AccountController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) : Controller
{
    [HttpGet]
    public IActionResult Register() => User.Identity?.IsAuthenticated == true ? RedirectToAction("Index", "Dashboard") : View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            DisplayName = model.DisplayName
        };
        var result = await userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Dashboard");
        }
        foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
        return View(model);
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return User.Identity?.IsAuthenticated == true ? RedirectToAction("Index", "Dashboard") : View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (!ModelState.IsValid) return View(model);
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
        if (result.Succeeded)
            return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl!) : RedirectToAction("Index", "Dashboard");
        if (result.IsLockedOut) ModelState.AddModelError(string.Empty, "Your account is temporarily locked. Please try again later.");
        else ModelState.AddModelError(string.Empty, "Invalid email or password.");
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> EmailAvailable(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return Json(new { available = false, message = "Enter an email address." });
        var existing = await userManager.FindByEmailAsync(email.Trim());
        return Json(new { available = existing is null, message = existing is null ? "Email is available." : "An account with this email already exists." });
    }
}
