using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorPages();

var app = builder.Build();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".properties"] = "application/octet-stream";
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

app.MapRazorPages();

app.Run();
