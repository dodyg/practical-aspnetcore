using Microsoft.AspNetCore.Mvc;

namespace PracticalAspNetCore
{
    [Route("")]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}