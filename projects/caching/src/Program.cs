using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using System;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore;

namespace Caching
{
    public class Startup
    {
        static string CACHE_KEY = "MyCache";

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            var fileProvider = new PhysicalFileProvider(env.ContentRootPath);

            app.Run(async context =>
            {
                var cache = context.RequestServices.GetService<IMemoryCache>();
                var greeting = cache.Get(CACHE_KEY) as string;

                //There is no existing cache, add one
                if (string.IsNullOrWhiteSpace(greeting)){
                    var options = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(100));
                        //You can also use .SetSlidingExpiration

                    var message = "Hello " + DateTimeOffset.UtcNow.ToString();
                    cache.Set(CACHE_KEY, message, options);
                    greeting = message;
                }

                await context.Response.WriteAsync($"After the first load, this greeting '{greeting}' is cached. \nCheck the time stamp of the message compared to this current one {DateTimeOffset.UtcNow}.\n");
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