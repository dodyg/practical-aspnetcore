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
            services.AddTransient<TerminalMiddleware>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(TerminalMiddleware));
        }
    }

    public class TerminalMiddleware : IMiddleware
    {
        ILogger<TerminalMiddleware> _log;

        DateTime _date = DateTime.Now;

        public TerminalMiddleware(ILogger<TerminalMiddleware> log)
        {
            _log = log;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _log.LogDebug($"Request: {context.Request.Path}");
            context.Response.Headers.Add("Content-Type", "text/plain");
            await context.Response.WriteAsync($"This Middleware is transient. Keep refreshing your page. The date will keep changing: {_date}.");
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
