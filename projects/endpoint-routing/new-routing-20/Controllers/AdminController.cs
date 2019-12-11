using Microsoft.AspNetCore.Mvc;

namespace PracticalAspNetCore.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index() => Content("Admin Page");
    }
}