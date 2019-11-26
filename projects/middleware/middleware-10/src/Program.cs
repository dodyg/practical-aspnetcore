using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Logging;
using System;

namespace HelloWorldWithMiddleware
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(TerminalMiddleware));
        }
    }

    public class TerminalMiddleware
    {
        DateTime _date = DateTime.Now;

        public TerminalMiddleware(RequestDelegate next)
        {
        }

        public async Task Invoke(HttpContext context, ILogger<TerminalMiddleware> log)
        {
            log.LogDebug($"Request: {context.Request.Path}");
            context.Response.Headers.Add("Content-Type", "text/plain");
            await context.Response.WriteAsync($"Middleware is singleton. Keep refreshing the page. You will see that the date does not change {_date}.");
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
                .ConfigureLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Debug);
                })
                .UseEnvironment("Development");
    }
}
