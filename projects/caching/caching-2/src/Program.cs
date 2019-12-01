using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using System;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        static string CACHE_KEY = "MyCache";
        static string FILE_TO_WATCH = "cache-file.txt";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
                        .AddExpirationToken(fileProvider.Watch(FILE_TO_WATCH));

                    var message = "Hello " + DateTimeOffset.UtcNow.ToString();
                    cache.Set(CACHE_KEY, message, options);
                    greeting = message;
                }

                await context.Response.WriteAsync($"After the first load, this greeting '{greeting}' is cached. \nCheck the time stamp of the message compared to this current one {DateTimeOffset.UtcNow}.\n");
                await context.Response.WriteAsync($"To expire the cache, modify the file dependency located at {Path.Combine(env.ContentRootPath, FILE_TO_WATCH)} and refresh your browser.");
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