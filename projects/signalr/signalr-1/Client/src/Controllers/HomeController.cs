using Microsoft.AspNetCore.Mvc;

namespace Client
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
