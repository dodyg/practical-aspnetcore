using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore;

namespace StartupBasic 
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {            
             var options = new RewriteOptions()
                .AddRedirect("/$", "/"); //redirect when path ends with /

            app.UseRewriter(options);

            var routes = new RouteBuilder(app);
            routes.MapGet("", (context) => {
                context.Response.Headers.Add("content-type", "text/html");
                return context.Response.WriteAsync($"Always display this page when path ends with / e.g. <a href=\"/hello-world/\">/hello-world/</a> or <a href=\"/welcome/everybody/inthis/train/\">/welcome/everybody/inthis/train/</a>.");
            });

            app.UseRouter(routes.Build());
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