
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [HttpGet("/index-2")]
    public IActionResult Index()
    {
        return View();
    }
}