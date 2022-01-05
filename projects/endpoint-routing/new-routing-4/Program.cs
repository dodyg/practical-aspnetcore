var app = WebApplication.Create();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hello world");
});

app.Run();
