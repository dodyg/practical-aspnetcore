using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();
builder.WebHost.UseUrls("http://localhost:5002/");

var app = builder.Build();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")),
    RequestPath = new PathString("/node_modules")
});

app.UseRouting();
app.MapDefaultControllerRoute();
app.Run();
