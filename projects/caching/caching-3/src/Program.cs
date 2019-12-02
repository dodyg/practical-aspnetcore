using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using System;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PracticalAspNetCore
{
    public class Startup
    {
        static string CACHE_KEY = "MyCache";

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env, ILogger<Startup> log)
        {
            var fileProvider = new PhysicalFileProvider(env.ContentRootPath);

            app.Run(async context =>
            {
                var cache = context.RequestServices.GetService<IMemoryCache>();
                var greeting = cache.Get(CACHE_KEY) as string;

                //There is no existing cache, add one
                if (string.IsNullOrWhiteSpace(greeting))
                {
                    var options = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(5))
                        .RegisterPostEvictionCallback((object key, object value, EvictionReason reason, object state) =>
                        {
                            if (state == null)
                                log.LogInformation("State is null");

                            log.LogInformation($"Key '{key}' with value '{value}' was removed because of '{reason}'");
                        });
                    //You can also use .SetSlidingExpiration

                    var message = "Hello " + DateTimeOffset.UtcNow.ToString();
                    cache.Set(CACHE_KEY, message, options);
                    greeting = message;
                }

                context.Response.Headers.Add("Content-Type", "text/html");
                await context.Response.WriteAsync(@"
                <html>
                    <body>
                        <p>
                            The cache is set to expire in 5 seconds. Wait until 5 seconds then refresh this page. Check your console log to see the post eviction message event.  
                        </p>
                        <p>
                            If you don't refresh this page, the eviction message won't be posted at the console. Here's <a href=""https://github.com/aspnet/Caching/issues/353#issuecomment-333956801"">an explanation</a> for this behavior.
                        </p>
                    </body>
                </html>");
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
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                // Filter out the noise 
                logging.AddFilter((provider, category, logLevel) =>
                {
                    return !category.Contains("Microsoft.AspNetCore");
                });
            })
            .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.UseStartup<Startup>()
            );
    }
}