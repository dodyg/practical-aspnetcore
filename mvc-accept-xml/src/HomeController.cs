using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class HelloWorld
    {
        public string Hello { get; set;}
        public string World { get; set;}
    }

    public class Result
    {
        public string Output { get; set;}
    }

    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return this.Content("Make a post with { Hello : \"hello\", World : \"World\" } payload to localhost:5000");
        }

        [HttpPost("")]
        public IActionResult Index([FromBody] HelloWorld payload)
        {
            return Json(new Result { Output = payload.Hello + " = " + payload.World });
        }
    }
}