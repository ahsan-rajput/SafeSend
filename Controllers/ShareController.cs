using Microsoft.AspNetCore.Mvc;

namespace SafeSend.Controllers;

public class ShareController : Controller
{
    [HttpGet]
    public IActionResult Create() => View();

    [HttpGet("s/{id}")]
    public IActionResult ViewLink(string id)
    {
        ViewData["ShareId"] = id;
        return View("ViewLink");
    }
}
