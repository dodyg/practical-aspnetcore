using System.Net.Mime;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.StaticFiles;

var app = WebApplication.Create();

var provider = new FileExtensionContentTypeProvider();
string GetMime(string ext)
{
    if (provider.TryGetContentType(ext, out string mime))
        return mime;
    else
        return "";
}

app.Run(async context =>
{
    context.Response.Headers.Add(HeaderNames.ContentType, MediaTypeNames.Text.Html);

    await context.Response.WriteAsync("<html><body>");
    await context.Response.WriteAsync("<h1>Geting MIME type based on a file extension</h1>");

    await context.Response.WriteAsync("<ul>");
    await context.Response.WriteAsync($"<li>.pdf = {GetMime(".pdf")}");
    await context.Response.WriteAsync($"<li>.doc = {GetMime(".doc")}");
    await context.Response.WriteAsync($"<li>.docx = {GetMime(".docx")}");
    await context.Response.WriteAsync($"<li>.json = {GetMime(".json")}");
    await context.Response.WriteAsync("</ul>");

    await context.Response.WriteAsync("</body></html>");

});

app.Run();