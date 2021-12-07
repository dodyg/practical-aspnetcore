using Microsoft.AspNetCore.StaticFiles;

var app = WebApplication.Create();
app.UseStaticFiles();
app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    Formatter = new DirectoryFormatter()
});

app.Run();

public class DirectoryFormatter : IDirectoryFormatter
{
    public async Task GenerateContentAsync(HttpContext context, IEnumerable<Microsoft.Extensions.FileProviders.IFileInfo> contents)
    {
        context.Response.ContentType = "text/html";

        await context.Response.WriteAsync(@"
<html>
<head>
    <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css"" rel=""stylesheet"" integrity=""sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3"" crossorigin=""anonymous"">
</head>
<body>
<div class=""container"">
");
        await context.Response.WriteAsync("\n");

        foreach (var c in contents)
        {
            await context.Response.WriteAsync($"<div class=\"row justify-content-center\">\n");

            if (c.IsDirectory)
            {
                await context.Response.WriteAsync($"<div class=\"col\"><strong>Directory <a href=\"{c.Name}\">{c.Name}</a></strong></div>\n");
            }
            else
            {
                if (c.Name.Contains(".png") || c.Name.Contains(".jpg"))
                    await context.Response.WriteAsync($"<div class=\"col\"><img src=\"{c.Name}\" class=\"img-thumbnail\"/></div>\n");
                else
                    await context.Response.WriteAsync($"<div class=\"col\"><a href=\"{c.Name}\">{c.Name}</a></div>\n");
            }

            await context.Response.WriteAsync("</div>\n");
        }

        await context.Response.WriteAsync("\n</div></body></html>");
    }
}