var builder = WebApplication.CreateBuilder();
builder.Services.AddRequestTimeouts();

var app = builder.Build();
app.UseRequestTimeouts();

app.MapGet("/", async (HttpContext context) => {
    await Task.Delay(TimeSpan.FromSeconds(2));

    if (context.RequestAborted.IsCancellationRequested)
        return Results.Content("timeout", "text/plain");            

    return Results.Content("""
    <html>
    <body>
        hello world
    </body>
    </html>
    """, "text/html");
}).WithRequestTimeout(TimeSpan.FromSeconds(1));

app.Run();
