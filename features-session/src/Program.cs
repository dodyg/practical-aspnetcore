using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;
using Microsoft.AspNetCore.Http.Features;
using System.Text;

namespace Features.Connection 
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
              var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}