
using Microsoft.AspNetCore.Mvc;

namespace PracticalAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}