var app = WebApplication.Create();

app.UseStatusCodePagesWithRedirects("/error?status={0}");

app.Map("/error", errorApp =>
{
    errorApp.Run(async context =>
    {
        await context.Response.WriteAsync($"This is a redirected error message status {context.Request.Query["status"]}");
    });
});

app.Run(context =>
{
    context.Response.StatusCode = 500;//change this as necessary
        return Task.CompletedTask;
});

app.Run();