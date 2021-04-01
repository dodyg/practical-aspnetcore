using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace PracticalAspNetCore
{
    public class Startup
    {
        public record Greeting(string Message);

        JsonResult About() => new JsonResult(new { about = "me" });

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
                
            app.UseEndpoints(endpoints =>
            {
                JsonResult Index() => new JsonResult(new Greeting("Hello World"));

                endpoints.Map("/", (Func<JsonResult>)Index);
                endpoints.Map("/about",(Func<JsonResult>)About);
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