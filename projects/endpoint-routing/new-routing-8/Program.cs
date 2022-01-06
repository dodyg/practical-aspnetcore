using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapGet("/contact/us", async context =>
{
    await context.Response.WriteAsync("Contact Us");
});

app.MapFallbackToFile("index.html", new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "static")),
});

app.Run();