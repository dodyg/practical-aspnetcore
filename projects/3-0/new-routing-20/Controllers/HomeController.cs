
using Microsoft.AspNetCore.Mvc;

namespace NewRouting.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult About() => Content("About Page");
    }
}