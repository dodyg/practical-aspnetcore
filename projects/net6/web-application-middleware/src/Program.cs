using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var app = WebApplication.Create();

app.Use((context, next) => 
{
    context.Items["start"] = context.Request.Path.ToString() switch
    {
        "/about-us" =>  @"<html><body style=""color:red;"">",
        _ => "<html><body>"
    };

    return next(context);
});


app.Use((context, next) => 
{
    context.Items["end"] = "</body></html>";
    return next(context);
});

app.MapGet("/", (HttpContext context) =>
{
    return Results.Text(context.Items["start"] + @"Hello world<br/> Go to <a href=""/about-us"">about us</a>" + context.Items["end"], "text/html");
});

app.MapGet("/about-us", (HttpContext context) =>
{
    return Results.Text(context.Items["start"] + "About Us" + context.Items["end"], "text/html");
});


app.Run();