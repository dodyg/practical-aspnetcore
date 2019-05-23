using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Connections;
using System.Buffers;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http.Endpoints;

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

            if (endPoint.DisplayName == "/about")
            {
                httpContext.Items.Add("GreetingFromMiddleWare", "Hello world from GreetingMiddleware");
            }

            await _next.Invoke(httpContext);
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
                {
                    webBuilder.UseStartup<Startup>().
                    UseEnvironment(Environments.Development);
                });
    }
}