using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;

var location = Assembly.GetEntryAssembly().Location;
// Find the "bin" segment of the build output path. This is a cross-platform
// alternative to the Windows-only "bin\\" separator.
var binIndex = location.LastIndexOf($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}", StringComparison.Ordinal);
var projectDirectory = location.Substring(0, binIndex);

var options = new WebApplicationOptions
{
    // Here we are trying to get the path to the root of the project. This is just useful for this sample;it's not really applicable for general purpose use.
    ContentRootPath = Path.Combine(projectDirectory, "root")
};

var builder = WebApplication.CreateBuilder(options);
var app = builder.Build();
app.UseStaticFiles();

app.Run(async (context) =>
{
    await context.Response.WriteAsync(@"<html><body><img src=""/cute-kitty.jpg"" width=""100%"" /></body></html>");
});

await app.RunAsync();