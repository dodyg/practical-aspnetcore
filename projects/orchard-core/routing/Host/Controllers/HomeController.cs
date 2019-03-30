using Microsoft.AspNetCore.Mvc;

namespace Host
{
    [Route("")]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}