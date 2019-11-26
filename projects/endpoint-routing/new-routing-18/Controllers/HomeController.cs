using Microsoft.AspNetCore.Mvc;

namespace NewRouting
{
    [Route("")]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}