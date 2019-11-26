using Microsoft.AspNetCore.Mvc;

namespace NewRouting.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index() => Content("Admin Page");
    }
}