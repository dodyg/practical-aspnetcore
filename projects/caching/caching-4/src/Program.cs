using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using System;
using Microsoft.Extensions.FileProviders;
using System.Threading;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore;

namespace Caching.Four
{
    public class Startup
    {
        static string CACHE_KEY = "MyCache";
        static string CACHE_KEY_2 = "MyCache2";

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            var fileProvider = new PhysicalFileProvider(env.ContentRootPath);
            var cts = new CancellationTokenSource();

            int count = 0;
            int resetCount = 8;

            app.Run(async context =>
            {
                var cache = context.RequestServices.GetService<IMemoryCache>();
                var greeting = cache.Get(CACHE_KEY) as string;

                //There is no existing cache, add one
                if (string.IsNullOrWhiteSpace(greeting))
                {
                    var options = new MemoryCacheEntryOptions()
                        .AddExpirationToken(new CancellationChangeToken(cts.Token))
                        .RegisterPostEvictionCallback((object key, object value, EvictionReason reason, object state) =>
                        {
                            Console.WriteLine($"Key '{key}' with value '{value}' was removed because of '{reason}'");
                        });

                    var message = $"Hello   {DateTime.Now.Ticks}";
                    cache.Set(CACHE_KEY, message, options);
                    greeting = message;
                }

                var greeting2 =  cache.Get(CACHE_KEY_2) as string;
                if (string.IsNullOrWhiteSpace(greeting2))
                {
                    var options = new MemoryCacheEntryOptions()
                    .AddExpirationToken(new CancellationChangeToken(cts.Token))
                    .RegisterPostEvictionCallback((object key, object value, EvictionReason reason, object state) =>
                    {
                        Console.WriteLine($"Key '{key}' with value '{value}' was removed because of '{reason}'");
                    });

                    var message = $"Hello 2 {DateTime.UtcNow.Ticks}";
                    cache.Set(CACHE_KEY_2, message, options);
                    greeting2 = message;
                }

                await context.Response.WriteAsync($"Greeting {greeting}\n");
                await context.Response.WriteAsync($"Greeting {greeting2}\n");

                count++;

                if (count >= resetCount)
                {
                    await context.Response.WriteAsync("Cache expired\n");
                    cts.Cancel();
                    count = 0;
                    cts = new CancellationTokenSource();
                }
                
                await context.Response.WriteAsync($"Above greetings share the same cancellation token. They will both expire at the same time. The caches will expire if you refresh this page {resetCount} times. So far you have loaded {count} times. \n");
            });
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
                .UseEnvironment("Development");
    }
}