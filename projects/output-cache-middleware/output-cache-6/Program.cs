var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.With(c => c.HttpContext.Request.Path.StartsWithSegments("/cached")));
});

var app = builder.Build();
app.MapGet("/", () =>
{
    return Results.Content("""
    <html>
        <body>
            <ul>
                <li><a href="/now">DateTime.UtcNow()</a></li>
                <li><a href="/cached/now">DateTime.UtcNow() cached</a></li>
                <li><a href="/time/cached/now">DateTime.UtcNow() not cached</a></li>
            </ul>
        </body>
    </html>
    """, "text/html");
});

app.UseOutputCache();
app.MapGet("/now", () => DateTime.UtcNow.ToString());
app.MapGet("/cached/now", () => DateTime.UtcNow.ToString());
app.MapGet("/time/cached/now", () => DateTime.UtcNow.ToString());

app.Run();