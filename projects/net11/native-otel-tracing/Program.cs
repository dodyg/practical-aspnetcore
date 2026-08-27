using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddSource("Microsoft.AspNetCore")
        .AddConsoleExporter());

var app = builder.Build();

app.MapGet("/", () => TypedResults.Text("""
<html>
<body>
<h1>Hello, native OpenTelemetry!</h1>

Click <a href="/slow">here too</a>
</body>
</html>
""", "text/html"));

app.MapGet("/slow", async () =>
{
    await Task.Delay(500);
    return "slow response";
});

app.Run();

