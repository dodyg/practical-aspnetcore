var app = WebApplication.Create();

app.Run(async context =>
{
    context.Response.Headers.Add("content-type", "text/html");

    await context.Response.WriteAsync("<html><body>");
    await context.Response.WriteAsync("<h1>All query string</h1>");
    await context.Response.WriteAsync(@"<a href=""?message=hello&message=world&message=again&isTrue=1&morning=good"">click this link to add query string</a><br/><br/>");
    await context.Response.WriteAsync("<ul>");
    foreach (var v in context.Request.Query)
    {
        string str = v.Value; //implicit conversion from StringValues to String
        await context.Response.WriteAsync($"<li>{v.Key} - {str}</li>");
    }
    await context.Response.WriteAsync("</ul>");
    await context.Response.WriteAsync("</body></html>");
});

app.Run();