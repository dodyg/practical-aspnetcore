using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;

var options = new WebApplicationOptions
{
    ContentRootPath = Path.Combine(Assembly.GetExecutingAssembly().Location, "root")
};

var app = WebApplication.Create(options);
app.UseStaticFiles();

app.Run(async (context) =>
{
    await context.Response.WriteAsync(@"<html><body><img src=""/cute-kitty.jpg"" /></body></html>");
});

await app.RunAsync();