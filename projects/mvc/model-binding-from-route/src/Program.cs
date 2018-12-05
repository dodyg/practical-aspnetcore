using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace ModelBinding
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //These are three services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().
                SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.UseMvcWithDefaultRoute();
        }
    }

    public class GreetingParams
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public bool IsAmazing { get; set; }

        public short? Age { get; set; }

        public override string ToString() => $"User Id: {UserId}, Name: {Name}, Is Amazing: {IsAmazing}, Age: {Age}";
    }

    public class HomeController : Controller
    {
        string _nav = @"
           <ul>
                <li><a href=""/"">/</a></li>
                <li><a href=""/annie"">/annie</a></li>
                <li><a href=""/true"">/true</a></li>
                <li><a href=""/user/1"">/user/33</a></li>
                <li><a href=""/33"">/33</a></li>
                <li><a href=""/annie/true/33/1"">/annie/true/33/1</a></li>
            </ul>";

        [HttpGet("")]
        public ActionResult Index([FromRoute] GreetingParams greet)
        {
            return new ContentResult
            {
                Content = $@"<html><body>
                <h1>Class binding with [FromRoute]</h1>
                {greet}
                {_nav}
                </body></html>",
                ContentType = "text/html"
            };
        }

        [HttpGet("{name}")]
        public ActionResult Name([FromRoute] GreetingParams greet)
        {
            return new ContentResult
            {
                Content = $@"<html><body>
                <h1>Class binding with [FromRoute]</h1>
                {greet}
                {_nav}
                </body></html>",
                ContentType = "text/html"
            };
        }

        [HttpGet("{isamazing:bool}")]
        public ActionResult IsAmazing([FromRoute] GreetingParams greet)
        {
            return new ContentResult
            {
                Content = $@"<html><body>
                <h1>Class binding with [FromRoute]</h1>
                {greet}
                {_nav}
                </body></html>",
                ContentType = "text/html"
            };
        }

        [HttpGet("/user/{userid:int}")]
        public ActionResult UserId([FromRoute] GreetingParams greet)
        {
            return new ContentResult
            {
                Content = $@"<html><body>
                <h1>Class binding with [FromRoute]</h1>
                {greet}
                {_nav}
                </body></html>",
                ContentType = "text/html"
            };
        }

        [HttpGet("{age:int}")]
        public ActionResult Age([FromRoute] GreetingParams greet)
        {
            return new ContentResult
            {
                Content = $@"<html><body>
                <h1>Class binding with [FromRoute]</h1>
                {greet}
                {_nav}
                </body></html>",
                ContentType = "text/html"
            };
        }

        [HttpGet("/{name}/{isamazing:bool}/{age:int}/{userid:int}")]
        public ActionResult All([FromRoute] GreetingParams greet)
        {
            return new ContentResult
            {
                Content = $@"<html><body>
                <h1>Class binding with [FromRoute]</h1>
                {greet}
                {_nav}
                </body></html>",
                ContentType = "text/html"
            };
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