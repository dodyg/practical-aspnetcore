using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        private List<FieldInfo> GetConstants(Type type)
        {
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            return fieldInfos.ToList();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                context.Response.Headers.Add(HeaderNames.ContentType, "text/html");

                await context.Response.WriteAsync("<html><body>");
                await context.Response.WriteAsync("<h1>Request Headers from Microsoft.Net.Http.Headers.HeaderNames</h1>");
                await context.Response.WriteAsync("<ul>");
                foreach (var h in GetConstants(typeof(HeaderNames)))
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
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}