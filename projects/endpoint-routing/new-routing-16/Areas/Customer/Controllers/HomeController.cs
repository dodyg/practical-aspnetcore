

using Microsoft.AspNetCore.Mvc;

namespace PracticalAspNetCore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        public ActionResult Index() => View();
    }
}