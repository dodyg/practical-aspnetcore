using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore;

namespace Features.Session
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            app.UseSession();

            app.Use(async (context, next)=>
            {
                var session = context.Features.Get<ISessionFeature>();
                try
                {
                    session.Session.SetString("Message", "Hello world");
                    session.Session.SetInt32("Year", DateTime.Now.Year);
                }
                catch(Exception ex)
                {
                    await context.Response.WriteAsync($"{ex.Message}");
                }
                await next.Invoke();
            });
            
            app.Run(async context =>
            {
                var session = context.Features.Get<ISessionFeature>();
                
                try
                {
                    string msg = session.Session.GetString("Message");
                    int? year = session.Session.GetInt32("Year");
                    await context.Response.WriteAsync($"{msg} {year}");
                }
                catch(Exception ex)
                {
                    await context.Response.WriteAsync($"{ex.Message}");
                }
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