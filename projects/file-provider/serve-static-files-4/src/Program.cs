using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore;

namespace ServeStaticFiles 
{

    public class DirectoryFormatter : IDirectoryFormatter
    {
        public async Task GenerateContentAsync(HttpContext context, IEnumerable<Microsoft.Extensions.FileProviders.IFileInfo> contents)
        {
            context.Response.ContentType = "text/html";

            await context.Response.WriteAsync(@"
<html>
<head>
    <link href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/css/bootstrap.min.css"" rel=""stylesheet"" integrity=""sha384-rwoIResjU2yc3z8GV/NPeZWAv56rSmLldC3R/AZzGRnGxQQKnKkoFVhFQhNUwEyJ"" crossorigin=""anonymous"">
</head>
<body>
<div class=""container"">
<div class=""row"">
");
            foreach(var c in contents)
            {
                if (c.Name.Contains(".png") || c.Name.Contains(".jpg"))
                    await context.Response.WriteAsync($@"<div class=""col""><img src=""{c.Name}""/></div>");
                else
                    await context.Response.WriteAsync($@"<div class=""col"">{c.Name}</div>");
            }

            await context.Response.WriteAsync("</div></div></body></html>");
        }
    }

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                Formatter = new DirectoryFormatter()
            });
        }
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}