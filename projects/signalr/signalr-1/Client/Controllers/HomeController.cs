using Microsoft.AspNetCore.Mvc;

namespace PracticalAspNetCore
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index() => View();
    }
}
