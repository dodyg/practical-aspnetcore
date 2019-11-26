using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using System;
using Microsoft.AspNetCore;

namespace HelloWorldWithReload 
{
    interface ICustomFeature 
    {
        string Greetings {get;}
    }

    public class CustomFeature : ICustomFeature
    {
        
        string _greetings;

        public CustomFeature (string greetings)
        {
            _greetings = greetings;
        }

        public string  Greetings => _greetings;
    }

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next ) =>
            {
                context.Features.Set<ICustomFeature>(new CustomFeature("First greeting"));
                await next.Invoke();
            });

            app.Use(async (context, next) =>
            {
                var custom = context.Features.Get<ICustomFeature>();
                await context.Response.WriteAsync($"{custom?.Greetings}\n");
                context.Features.Set<ICustomFeature>(new CustomFeature("Second greeting"));
                await next.Invoke();
            });

            app.Run(context =>
            {
                var custom = context.Features.Get<ICustomFeature>();
                return context.Response.WriteAsync($"{custom?.Greetings}");
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