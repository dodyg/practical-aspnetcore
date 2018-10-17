using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

namespace MvcAcceptXml
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }

    public class HelloWorld
    {
        public string Hello { get; set; }
        public string World { get; set; }
    }

    public class Result
    {
        public string Output { get; set; }
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

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}