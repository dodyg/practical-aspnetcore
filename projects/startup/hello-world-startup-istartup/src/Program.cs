using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;

namespace PracticalAspNetCore
{
    public class Startup : IStartup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app)
        {
            //With normal startup class these two variables are available from the parameters of Configure method
            var env = (IHostingEnvironment)app.ApplicationServices.GetService(typeof(IHostingEnvironment));
            var logger = (ILoggerFactory)app.ApplicationServices.GetService(typeof(ILoggerFactory));
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"{env.EnvironmentName}");
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
                    webBuilder.UseStartup<Startup>()
                );
    }
}