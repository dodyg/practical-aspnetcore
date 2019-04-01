using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace NewRouting
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting(route =>
            {
                route.MapGet("hello", async context => await context.Response.WriteAsync("hello"));
                route.MapPost("new-hello", async context => await context.Response.WriteAsync("hello"));
                route.MapDelete("hello", async context => await context.Response.WriteAsync("hello"));
                route.MapPut("hello", async context => await context.Response.WriteAsync("hello"));
                route.MapControllers();
                route.MapRazorPages();

                route.Map("", async context =>
                {
                    foreach (var x in route.DataSources)
                    {
                        await context.Response.WriteAsync($"{x}\n");
                        foreach (var e in x.Endpoints)
                        {
                            await context.Response.WriteAsync($"Display Name: {e.DisplayName}\n");
                        }
                        await context.Response.WriteAsync("\n");
                    }
                });
            });
        }
    }

    [Route("MVC")]
    public class HomeController : Controller
    {
        [HttpGet("Greeting")]
        public IActionResult Index() => Content("Oi");

        [HttpPost("Greeting")]
        public IActionResult Greeting() => Content("Oi");
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().
                    UseEnvironment(Environments.Development);
                });
    }
}