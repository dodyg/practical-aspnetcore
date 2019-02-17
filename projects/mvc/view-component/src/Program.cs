using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace StartupBasic
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) =>
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration) =>
            app.UseMvcWithDefaultRoute();
    }

    public class HomeController : Controller
    {
        public ActionResult Index() => 
            ViewComponent("HelloWorld", new { message = "Hello World", times = 10 }); 
    }

    public class Program
    {
        public static void Main(string[] args) =>
            CreateWebHostBuilder(args).Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}