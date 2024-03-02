var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.MapShortCircuit(404, "robots.txt", "favicon.ico");

app.MapGet("/", () => {
    return Results.Content("""
    <html>
    <body>
        hello world
    </body>
    </html>
    """, "text/html");
}).ShortCircuit(200);

app.Run();
