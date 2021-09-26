using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;

var options = new WebApplicationOptions
{
    // Here we are trying to get the path to the root of the project. This is just useful for this sample;it's not really applicable for general purpose use.
    ContentRootPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin\\"))), "root")
};

var builder = WebApplication.CreateBuilder(options);
var app = builder.Build();
app.UseStaticFiles();

app.Run(async (context) =>
{
    await context.Response.WriteAsync(@"<html><body><img src=""/cute-kitty.jpg"" width=""100%"" /></body></html>");
});

await app.RunAsync();