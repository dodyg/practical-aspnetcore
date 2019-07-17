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

        private List<FieldInfo> GetConstants(Type type)
        {
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).ToList();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            app.Run(async context =>
            {
                context.Response.Headers.Add(HeaderNames.ContentType, MediaTypeNames.Text.Html);

                await context.Response.WriteAsync("<html><body>");

                await context.Response.WriteAsync("<h1>System.Net.Mime.MediaTypeNames</h1>");

                await context.Response.WriteAsync("<h2>MediaTypeNames.Application</h2>");
                await context.Response.WriteAsync("<ul>");
                foreach (var h in GetConstants(typeof(MediaTypeNames.Application)))
                {
                    await context.Response.WriteAsync($"<li>{h.Name} = {h.GetValue(h)}</li>");
                }
                await context.Response.WriteAsync("</ul>");

                await context.Response.WriteAsync("<h2>MediaTypeNames.Text</h2>");
                await context.Response.WriteAsync("<ul>");
                foreach (var h in GetConstants(typeof(MediaTypeNames.Text)))
                {
                    await context.Response.WriteAsync($"<li>{h.Name} = {h.GetValue(h)}</li>");
                }
                await context.Response.WriteAsync("</ul>");

                await context.Response.WriteAsync("<h2>MediaTypeNames.Image</h2>");
                await context.Response.WriteAsync("<ul>");
                foreach (var h in GetConstants(typeof(MediaTypeNames.Image)))
                {
                    await context.Response.WriteAsync($"<li>{h.Name} = {h.GetValue(h)}</li>");
                }
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