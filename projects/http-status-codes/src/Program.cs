using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
using System.Net;

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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync(@"<html>
                <head>
                    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/bulma/0.7.5/css/bulma.css"" />
                </head>
                <body class=""content"">
                <h1>Battle of the Http Status Codes</h1>
                ");

                await context.Response.WriteAsync(@"<table class=""table""><tbody><tr><td><h2>Microsoft.AspNetCore.Http.StatusCodes</h2><ul>");
                foreach (var code in GetConstants(typeof(StatusCodes)))
                {
                    await context.Response.WriteAsync($"<li>{code.Name} = {code.GetValue(code)}</li> \n");
                }

                await context.Response.WriteAsync("</ul></td><td><h2>System.Net.HttpStatusCode</h2><ul>");

                foreach (var code in Enum.GetNames(typeof(HttpStatusCode)))
                {
                    var status = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), code);
                    await context.Response.WriteAsync($"<li>{code} = {(int)status}</li> \n");
                }

                await context.Response.WriteAsync("</ul></td></tr></tbody></table>");
                await context.Response.WriteAsync(@"</body></html>");
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