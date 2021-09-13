using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

namespace PracticalAspNetCore 
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
                }).RequireHost("localhost:8111");

                endpoint.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("hello world from http://localhost:8112");
                }).RequireHost("localhost:8112");
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