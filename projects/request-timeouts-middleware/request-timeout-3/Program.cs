var builder = WebApplication.CreateBuilder();
builder.Services.AddRequestTimeouts(options =>
{
    options.DefaultPolicy = new Microsoft.AspNetCore.Http.Timeouts.RequestTimeoutPolicy
    {
        Timeout = TimeSpan.FromSeconds(1),
        TimeoutStatusCode = 200,
        WriteTimeoutResponse = async (HttpContext context) => 
        {
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("timeout is triggered");
        }
    };
});

var app = builder.Build();
app.UseRequestTimeouts();

app.MapGet("/", async (HttpContext context) => {
    await Task.Delay(TimeSpan.FromSeconds(2));

    context.RequestAborted.ThrowIfCancellationRequested();

    return Results.Content("""
    <html>
    <body>
        hello world
    </body>
    </html>
    """, "text/html");
});

app.Run();
