using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;

namespace DI.One
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are two services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(x => new SingletonDate());
            services.AddTransient(x => new TransientDate());
            services.AddScoped(x => new ScopedDate());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure

            app.Use(async (context, next) =>
            {
                var single = context.RequestServices.GetService<SingletonDate>();
                var scoped = context.RequestServices.GetService<ScopedDate>();
                var transient = context.RequestServices.GetService<TransientDate>();

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

                var single = context.RequestServices.GetService<SingletonDate>();
                var scoped = context.RequestServices.GetService<ScopedDate>();
                var transient = context.RequestServices.GetService<TransientDate>();
                
                await context.Response.WriteAsync("----------------------------------\n");
                await context.Response.WriteAsync($"Singleton : {single.Date.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}\n");
                await context.Response.WriteAsync($"Scoped: {scoped.Date.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}\n");
                await context.Response.WriteAsync($"Transient: {transient.Date.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}\n");
            });
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
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}