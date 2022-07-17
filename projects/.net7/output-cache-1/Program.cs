var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOutputCache();

var app = builder.Build();
app.MapGet("/", () =>
{
    return Results.Content("""
    <html>
        <body>
            <ul>
                <li><a href="/now">DateTime.UtcNow()</a></li>
                <li><a href="/cached-now">DateTime.UtcNow() cached</a></li>
            </ul>
        </body>
    </html>
    """, "text/html");
});

app.UseOutputCache();
app.MapGet("/now", () => DateTime.UtcNow.ToString());
app.MapGet("/cached-now", () => DateTime.UtcNow.ToString()).CacheOutput();

app.Run();