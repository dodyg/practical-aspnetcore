using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing.Patterns;

namespace HelloWorldWithReload 
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("hello world from http://localhost:8111");
                }).WithMetadata(new HostAttribute ("localhost:8111"));

                endpoint.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("hello world from http://localhost:8112");
                }).WithMetadata(new HostAttribute ("localhost:8112"));
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
                    webBuilder.ConfigureKestrel(k =>
                    {
                       k.ListenLocalhost(8111);
                       k.ListenLocalhost(8112); 
                    });
                    webBuilder.UseStartup<Startup>().
                    UseEnvironment(Environments.Development);
                });
    }
}