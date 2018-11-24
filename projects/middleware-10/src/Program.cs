using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Logging;

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
        int _count;

        public TerminalMiddleware(RequestDelegate next)
        {
        }

        public async Task Invoke(HttpContext context, ILogger<TerminalMiddleware> log)
        {
            log.LogDebug($"Request: {context.Request.Path}");
            context.Response.Headers.Add("Content-Type", "text/plain");
            await context.Response.WriteAsync($"Middleware is singleton. Keep refreshing {_count++}. \n\nIf you are wondering why the count is incremented twice (in Chrome at least), check the log. It will show that Chrome also try to load /favicon.ico.");
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
