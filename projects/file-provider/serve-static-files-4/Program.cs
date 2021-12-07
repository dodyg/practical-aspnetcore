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
<div class=""row"">
");
        foreach (var c in contents)
        {
            if (c.Name.Contains(".png") || c.Name.Contains(".jpg"))
                await context.Response.WriteAsync($@"<div class=""col""><img src=""{c.Name}""/></div>");
            else
                await context.Response.WriteAsync($@"<div class=""col"">{c.Name}</div>");
        }

        await context.Response.WriteAsync("</div></div></body></html>");
    }
}
