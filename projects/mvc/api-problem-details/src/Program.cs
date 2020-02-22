using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public ActionResult<string> Index()
        {
            try
            {
                throw new ApplicationException("Catch this one");
            }
            catch(Exception ex)
            {
                return new ObjectResult(
                    new ProblemDetails
                    {
                        Title = "Use Microsoft.AspNetCore.Mvc.ProblemDetails to describe error in your web APIs",
                        Detail = "It is implemeting this RFC https://tools.ietf.org/html/rfc7807 and can be easily extended.",
                        Status = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError        
                    });
            }
        }
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
                    webBuilder.UseStartup<Startup>()
                );
    }
}