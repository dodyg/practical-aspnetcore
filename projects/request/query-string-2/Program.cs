using Microsoft.Extensions.Primitives;

var app = WebApplication.Create();

//These are the three default services available at Configure
app.Run(async context =>
{
    context.Response.Headers.Add("Content-Type", "text/html");

    StringValues queryString = context.Request.Query["message"];

    await context.Response.WriteAsync("<html><body>");
    await context.Response.WriteAsync("<h1>Query String with multiple values</h1>");
    await context.Response.WriteAsync(@"<a href=""?message=hello&message=world&message=again"">click this link to add query string</a><br/><br/>");
    await context.Response.WriteAsync("<ul>");
    foreach (string v in queryString)
    {
        await context.Response.WriteAsync($"<li>{v}</li>");
    }
    await context.Response.WriteAsync("</ul>");
    await context.Response.WriteAsync("</body></html>");
});

app.Run();