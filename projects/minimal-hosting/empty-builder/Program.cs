var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions());
builder.WebHost.UseKestrelCore();
builder.Services.AddRouting();

var app = builder.Build();

app.MapGet("/", () => {
    return Results.Content("""
    <html>
    <body>
        hello world with WebApplication.CreateEmptyBuilder()
    </body>
    </html>
    """, "text/html");
});

app.Run();
