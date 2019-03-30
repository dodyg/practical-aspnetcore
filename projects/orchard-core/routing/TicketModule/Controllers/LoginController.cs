using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketModule.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index() => Content("From Ticket Login");
    }
}