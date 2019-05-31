

using Microsoft.AspNetCore.Mvc;

namespace NewRouting.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        public ActionResult Index() => View();
    }
}