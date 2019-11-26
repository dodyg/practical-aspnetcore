using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Connections;
using System.Threading.Tasks;
using System;

namespace NewRouting
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment environment)
        {
            app.UseRouting();

            app.UseMiddleware<GreeterMiddleware>();
            app.UseEndpoints(routes =>
            {
                routes.MapRazorPages();
            });
        }
    }

    public class GreeterMiddleware
    {
        RequestDelegate _next;

        readonly LinkGenerator _linkGenerator;

        public GreeterMiddleware(RequestDelegate next, LinkGenerator linkGenerator)
        {
            _next = next;
            _linkGenerator = linkGenerator;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            Endpoint endPoint = httpContext.GetEndpoint();

            var message = endPoint.Metadata.GetMetadata<MessageAttribute>();

            if (message != null)
            {
                httpContext.Items.Add("GreetingFromMiddleWare", message.Content);
            }

            await _next.Invoke(httpContext);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MessageAttribute : System.Attribute
    {
        public string Content { get; set; }
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
                {
                    webBuilder.UseStartup<Startup>().
                    UseEnvironment(Environments.Development);
                });
    }
}