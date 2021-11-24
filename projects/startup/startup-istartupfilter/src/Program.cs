using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, GreetingStartupFilter>();
        }

        public void Configure()
        {
            //Empty by design
        }
    }


}