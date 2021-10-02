using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var app = WebApplication.Create();

app.Use((context, next) => 
{
    context.Items["start"] = "<html><body>";
    return next(context);
});

app.Use((context, next) => 
{
    context.Items["end"] = "</body></html>";
    return next(context);
});

app.MapGet("/", (HttpContext context) =>
{
    return Results.Text(context.Items["start"] + "Hello world" + context.Items["end"], "text/html");
});

app.Run();