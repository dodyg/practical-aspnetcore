using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace ApiProblemDetailsExample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //These are three services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().
                SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.UseMvc();
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
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}