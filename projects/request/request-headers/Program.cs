var app = WebApplication.Create();

app.Run(async context =>
{
    context.Response.Headers.Add("content-type", "text/html");
    await context.Response.WriteAsync("<h1>Request Headers</h1>");
    await context.Response.WriteAsync("<ul>");
    foreach (var h in context.Request.Headers)
    {
        await context.Response.WriteAsync($"<li>{h.Key} : {h.Value}</li>");
    }
    await context.Response.WriteAsync("</ul>");
});

app.Run();