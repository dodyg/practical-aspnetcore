using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

const string WebRootName = "wwwroot2";
const string Page = @"<html><body><img src=""/cute-kitty.jpg"" width=""100%"" /></body></html>";

var builder = WebApplication.CreateBuilder();
builder.WebHost.UseWebRoot(WebRootName);
var app = builder.Build();
app.UseStaticFiles();
app.Run(async (context) =>
{
    await context.Response.WriteAsync(Page);
});

app.Run();

// CreateHostBuilder(args).Build().Run();
// static IHostBuilder CreateHostBuilder(string[] args) =>
//     Host.CreateDefaultBuilder(args)
//         .ConfigureWebHostDefaults(webBuilder =>
//             webBuilder
//             .Configure(app =>
//                 app
//                 .UseStaticFiles()
//                 .Run(async context =>
//                 {
//                     await context.Response.WriteAsync(Page);
//                 })
//             )
//             .UseWebRoot(WebRootName)
//         );