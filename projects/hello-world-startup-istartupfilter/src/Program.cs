using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;

namespace StartupBasic 
{
    public class GreetingStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                builder.UseMiddleware<GreetingMiddleware>();
                builder.UseMiddleware<HelloWorldMiddleware>();
                next(builder);
            };
        }
    }

    public class HelloWorldMiddleware
    {
        public HelloWorldMiddleware(RequestDelegate next)
        {

        }   

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{context.Items["Greetings"]}");
        }     
    }

    public class GreetingMiddleware 
    {
        RequestDelegate _next;
        public GreetingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Items["Greetings"] = "Hello world from GreetingMiddleware";
            await _next(context);
        }
    }

    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are two services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, GreetingStartupFilter>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //Empty by design
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