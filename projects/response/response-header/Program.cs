var app = WebApplication.Create();
app.Run(async context =>
{
    context.Response.Headers.Append("content-type", "text/html");
    await context.Response.WriteAsync("<b>Hello world</b>");
    try
    {
        context.Response.Headers.Append("my-header", "awesome");
    }
    catch (Exception ex)
    {
        await context.Response.WriteAsync($"<br/><br/>You cannot modify header collections after the body is sent already. Exception: {ex.Message}");
    }
});

app.Run();