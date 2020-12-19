using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace PracticalAspNetCore
{
    public class TellTime
    {
        public DateTime Time => DateTime.Now;
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<TellTime>();
            services.AddTransient<Lazy<TellTime>>(x => new Lazy<TellTime>(x.GetRequiredService<TellTime>()));
        }

        public void Configure(IApplicationBuilder app)
        {
            //These are the three default services available at Configure

            app.Run(context =>
            {
                var tell = app.ApplicationServices.GetService<Lazy<TellTime>>();

                return context.Response.WriteAsync($"{tell.Value.Time}");
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