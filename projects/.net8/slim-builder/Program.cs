var builder = WebApplication.CreateSlimBuilder();
var app = builder.Build();

app.MapGet("/", () => {
    return Results.Content("""
    <html>
    <body>
        hello world with WebApplication.CreateSlimBuilder()
    </body>
    </html>
    """, "text/html");
});

app.Run();
