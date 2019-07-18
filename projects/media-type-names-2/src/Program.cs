using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore;
using System.Collections.Generic;
using System.Reflection;
using System.Net.Mime;
using Microsoft.Net.Http.Headers;
using System;
using Microsoft.AspNetCore.StaticFiles;

namespace StartupBasic
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
            var provider = new FileExtensionContentTypeProvider();

            string GetMime(string ext)
            {
                if (provider.TryGetContentType(ext, out string mime))
                    return mime;
                else
                    return "";
            }

            app.Run(async context =>
            {
                context.Response.Headers.Add(HeaderNames.ContentType, MediaTypeNames.Text.Html);

                await context.Response.WriteAsync("<html><body>");
                await context.Response.WriteAsync("<h1>Geting MIME type based on a file extension</h1>");

                await context.Response.WriteAsync("<ul>");
                await context.Response.WriteAsync($"<li>.pdf = {GetMime(".pdf")}");
                await context.Response.WriteAsync($"<li>.doc = {GetMime(".doc")}");
                await context.Response.WriteAsync($"<li>.docx = {GetMime(".docx")}");
                await context.Response.WriteAsync($"<li>.json = {GetMime(".json")}");
                await context.Response.WriteAsync("</ul>");

                await context.Response.WriteAsync("</body></html>");

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