using Microsoft.Extensions.FileProviders;

var app = WebApplication.Create();

app.Run(async context =>
{
    context.Response.Headers.Add("Content-Type", "text/html");

    var contentRoot = app.Environment.ContentRootFileProvider;
    var contentPhysical = app.Environment.ContentRootFileProvider as PhysicalFileProvider;

    await context.Response.WriteAsync("<h1>Content Root</h1>");
    await context.Response.WriteAsync($"{contentPhysical.Root}");
    await context.Response.WriteAsync($"<ul>");
    foreach (var f in contentRoot.GetDirectoryContents(""))
    {
        if (f.IsDirectory)
            await context.Response.WriteAsync($"<li>{f.Name} - Directory</li>");
        else
            await context.Response.WriteAsync($"<li>{f.Name}</li>");
    }
    await context.Response.WriteAsync($"</ul>");

    var webRoot = app.Environment.WebRootFileProvider;
    var webPhysical = app.Environment.WebRootFileProvider as PhysicalFileProvider;

    await context.Response.WriteAsync("<h1>Web Root</h1>");
    await context.Response.WriteAsync($"{webPhysical.Root}");
    await context.Response.WriteAsync($"<ul>");
    foreach (var f in webRoot.GetDirectoryContents(""))
    {
        if (f.IsDirectory)
            await context.Response.WriteAsync($"<li>{f.Name} - Directory</li>");
        else
            await context.Response.WriteAsync($"<li>{f.Name}</li>");
    }
    await context.Response.WriteAsync($"</ul>");
});

app.Run();