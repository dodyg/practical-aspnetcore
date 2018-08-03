using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore;

namespace FileProviderPhysical 
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are two services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure
            
            app.Run(async context =>
            {
                context.Response.Headers.Add("content-type", "text/html");

                var contentRoot = env.ContentRootFileProvider;
                var contentPhysical = env.ContentRootFileProvider as PhysicalFileProvider;

                await context.Response.WriteAsync("<h1>Content Root</h1>");                
                await context.Response.WriteAsync($"{contentPhysical.Root}");
                await context.Response.WriteAsync($"<ul>");
                foreach(var f in contentRoot.GetDirectoryContents(""))
                {
                    if (f.IsDirectory)
                        await context.Response.WriteAsync($"<li>{f.Name} - Directory</li>");
                    else
                        await context.Response.WriteAsync($"<li>{f.Name}</li>");
                }
                await context.Response.WriteAsync($"</ul>");

                var webRoot = env.WebRootFileProvider;
                var webPhysical = env.WebRootFileProvider as PhysicalFileProvider;

                await context.Response.WriteAsync("<h1>Web Root</h1>");                
                await context.Response.WriteAsync($"{webPhysical.Root}");
                await context.Response.WriteAsync($"<ul>");
                foreach(var f in webRoot.GetDirectoryContents(""))
                {
                    if (f.IsDirectory)
                        await context.Response.WriteAsync($"<li>{f.Name} - Directory</li>");
                    else
                        await context.Response.WriteAsync($"<li>{f.Name}</li>");
                }
                await context.Response.WriteAsync($"</ul>");
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