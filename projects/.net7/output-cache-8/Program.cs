var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.With(c => c.HttpContext.Request.Path.StartsWithSegments("/cached")));
    options.AddPolicy("10Minutes", o => o.Expire(TimeSpan.FromMinutes(10)));
});

var app = builder.Build();
app.MapGet("/", () =>
{
    return Results.Content("""
    <html>
        <body>
            <ul>
                <li><a href="/now">DateTime.UtcNow()</a></li>
                <li><a href="/cached/now">DateTime.UtcNow() cached for default 60 seconds</a></li>
                <li><a href="/cached/now-nope">DateTime.UtcNow() cached for 10 minutes</a></li>
            </ul>
        </body>
    </html>
    """, "text/html");
});

app.UseOutputCache();
app.MapGet("/now", () => DateTime.UtcNow.ToString());
app.MapGet("/cached/now", () => DateTime.UtcNow.ToString());
app.MapGet("/cached/now-nope", () => DateTime.UtcNow.ToString()).CacheOutput("10Minutes");

app.Run();