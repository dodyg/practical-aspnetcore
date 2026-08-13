#:sdk Microsoft.NET.Sdk.Web
#:package markdig@1.3.2
using Markdig;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.MapGet("/", () =>
{
    var content = """
    This is a project-less **ASP.NET Core** web application.
    """;

    return Results.Content($"""
    <html><body>{Markdown.ToHtml(content)}</body></html>
    """, "text/html");
});

app.Run();