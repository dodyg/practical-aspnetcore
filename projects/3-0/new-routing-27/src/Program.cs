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

namespace NewRouting
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages(x =>
            {
                x.Conventions.AddAreaPageRoute("Admin", "/Index", "/Manage");
                x.Conventions.AddAreaPageRoute("Customer", "/Index", "/CustomerService");
            });
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment environment)
        {
            app.UseRouting();

            app.UseEndpoints(routes =>
            {
                routes.MapRazorPages();
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