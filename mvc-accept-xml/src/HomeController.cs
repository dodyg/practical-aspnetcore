using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.Content("Hello world");
        }
    }
}