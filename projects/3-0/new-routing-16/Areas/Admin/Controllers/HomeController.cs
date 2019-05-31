

using Microsoft.AspNetCore.Mvc;

namespace NewRouting.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public ActionResult Index() => View();

        public ActionResult About() => Content("About");
    }
}