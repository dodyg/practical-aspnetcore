using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketModule.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => Content("From Ticket");

        public ActionResult About() => Content("About Ticket");
    }
}
