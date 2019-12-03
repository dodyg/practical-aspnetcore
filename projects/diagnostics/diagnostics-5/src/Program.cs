using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseStatusCodePagesWithRedirects("/error?status={0}");

            app.Map("/error", errorApp =>
            {
                errorApp.Run(async context =>
                {
                    await context.Response.WriteAsync($"This is a redirected error message status {context.Request.Query["status"]}");
                });
            });

            app.Run(context =>
            {
                context.Response.StatusCode = 500;//change this as necessary
                return Task.CompletedTask;
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}