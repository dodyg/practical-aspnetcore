using Microsoft.AspNetCore.Mvc;

namespace PracticalAspNetCore
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
