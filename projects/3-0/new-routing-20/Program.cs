using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Connections;
using System.Buffers;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System;
using Microsoft.AspNetCore.Http.Features;

namespace NewRouting
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment environment)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("Admin", "Admin", new
                {
                    Controller = "Admin",
                    Action = "Index"
                });

                endpoints.MapControllerRoute("About", "About", new
                {
                    Controller = "Home",
                    Action = "About"
                });

                endpoints.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });
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
                {
                    webBuilder.UseStartup<Startup>().
                    UseEnvironment(Environments.Development);
                });
    }
}