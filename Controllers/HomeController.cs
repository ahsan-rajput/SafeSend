using Microsoft.AspNetCore.Mvc;

namespace SafeSend.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
    public IActionResult Error() => View();
}
