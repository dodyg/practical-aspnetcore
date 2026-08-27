var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
var app = builder.Build();

app.MapOpenApi();

app.MapGet("/", () => Results.Text("""
    <html><body>
        <h1>Multiple Produces&lt;T&gt; per status code</h1>
        <ul>
            <li><a href="/openapi/v1.json">OpenAPI document</a></li>
            <li><a href="/ping?format=text">GET /ping?format=text</a> text/plain</li>
            <li><a href="/ping">GET /ping</a>  application/json</li>
        </ul>
    </body></html>
    """, "text/html")).ExcludeFromDescription();

app.MapGet("/ping", (string? format) =>
        format == "text" ? Results.Text("pong") : Results.Ok(new PingResult("pong", DateTime.UtcNow)))
   .Produces<string>(contentType: "text/plain")
   .Produces<PingResult>(contentType: "application/json");

app.Run();

public record PingResult(string Message, DateTime At);
