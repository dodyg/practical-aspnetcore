using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Connections;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                     options.LoginPath = new PathString("/login");
                });

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment environment)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(@"<html><body>
                    This is home page<br/>
                    <a href=""protected"">Click here</a> trying to access protected area
                    </body></html>");
                })
                .AllowAnonymous();

                endpoints.MapGet("/login", async context =>
                {
                    await context.Response.WriteAsync("You must login");
                });

                endpoints.MapGet("/protected", async context =>
                {
                    await context.Response.WriteAsync("Valuable area");
                }).RequireAuthorization();
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