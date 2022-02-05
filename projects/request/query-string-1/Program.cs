using Microsoft.Extensions.Primitives;
var app = WebApplication.Create();
//These are the three default services available at Configure
app.Run(async context =>
{
    context.Response.Headers.Add("content-type", "text/html");

    StringValues queryString = context.Request.Query["message"];

    await context.Response.WriteAsync("<html><body>");
    await context.Response.WriteAsync("<h1>Query String with a single value</h1>");
    await context.Response.WriteAsync(@"<a href=""?message=hello world"">click this link to add query string</a><br/><br/>");
    await context.Response.WriteAsync($"'Message' query string: {queryString}");
    await context.Response.WriteAsync("</body></html>");
});
app.Run();