using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;

namespace DI.One
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<SingletonDate>();
            services.AddTransient<TransientDate>();
            services.AddScoped<ScopedDate>();
            services.AddTransient<DateProvider>();
        }

        public void Configure(IApplicationBuilder app)
        {
            //These are the three default services available at Configure

            app.Use(async (context, next) =>
            {
                var dateProvider = context.RequestServices.GetService<DateProvider>();
                var single = dateProvider.Singleton;
                var scoped = dateProvider.Scoped;
                var transient = dateProvider.Transient;;

                await context.Response.WriteAsync("Open this page in two tabs \n");
                await context.Response.WriteAsync("Keep refreshing and you will see the three different DI behaviors\n");
                await context.Response.WriteAsync("----------------------------------\n");
                await context.Response.WriteAsync($"Singleton : {single.Date.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}\n");
                await context.Response.WriteAsync($"Scoped: {scoped.Date.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}\n");
                await context.Response.WriteAsync($"Transient: {transient.Date.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}\n");
                await next.Invoke();
            });

            app.Run(async (context) =>
            {
               await Task.Delay(100);//delay for 100 ms

                var dateProvider = context.RequestServices.GetService<DateProvider>();
                var single = dateProvider.Singleton;
                var scoped = dateProvider.Scoped;
                var transient = dateProvider.Transient;;

                await context.Response.WriteAsync("----------------------------------\n");
                await context.Response.WriteAsync($"Singleton : {single.Date.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}\n");
                await context.Response.WriteAsync($"Scoped: {scoped.Date.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}\n");
                await context.Response.WriteAsync($"Transient: {transient.Date.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}\n");
            });
        }
    }

    public class DateProvider
    {
        readonly IServiceProvider _provider;

        public SingletonDate Singleton => _provider.GetService<SingletonDate>();

        public ScopedDate Scoped => _provider.GetService<ScopedDate>();

        public TransientDate Transient => _provider.GetService<TransientDate>(); 

        public DateProvider(IServiceProvider provider)
        {
            _provider = provider;
        }
    }

    public class SingletonDate
    {
        public DateTime Date { get; set; } = DateTime.Now;
    }

    public class TransientDate
    {
        public DateTime Date { get; set; } = DateTime.Now;
    }

    public class ScopedDate
    {
        public DateTime Date { get; set; } = DateTime.Now;
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