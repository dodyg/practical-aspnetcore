

using Microsoft.AspNetCore.Mvc;

namespace PracticalAspNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public ActionResult Index() => View();

        public ActionResult About() => Content("About");
    }
}