using System;
using Microsoft.AspNetCore.Mvc;

namespace NewRouting
{
    public class Data
    {
        public string Message { get; set; }
    }

    [Route("/")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("about")]
        [Message("Hello World from the GreetingFromMiddleWare[GET]")]
        public IActionResult About()
        {
            return View(new Data { Message = HttpContext.Items["GreetingFromMiddleWare"] as string });
        }

        [HttpPost("about")]
        [Message("Hello World from the GreetingFromMiddleWare[POST]")]
        public IActionResult AboutPost()
        {
            return View("About", new Data { Message = HttpContext.Items["GreetingFromMiddleWare"] as string });
        }
    }
}