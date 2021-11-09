using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

const string Page = @"<html><body><img src=""/cute-kitty.jpg"" width=""100%"" /></body></html>";

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = "wwwroot2"
});

var app = builder.Build();
app.UseStaticFiles();
app.MapGet("/", () => Results.Text(Page, "text/html"));
app.Run();