using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace PracticalAspNetCore
{
    public class MyData 
    {
        public string Name => "Anne";
    }

    public class Startup
    {
        JsonResult TryContext([FromServices] MyData data) => new JsonResult(new { greetings = $"Hello {data.Name}" });

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MyData>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
                
            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/", (Func<MyData, JsonResult>)TryContext);
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