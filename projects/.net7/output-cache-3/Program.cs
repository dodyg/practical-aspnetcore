using Microsoft.AspNetCore.OutputCaching;

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
                <li><a href="/cached-now">Cached DateTime.UtcNow()</a></li>
                <li><a href="/cached-now-tagged">Cached DateTime.UtcNow() tagged</a></li>
                <li><a href="/clear">Clear tagged cache</a></li>
            </ul>
        </body>
    </html>
    """, "text/html");
});

app.UseOutputCache();
app.MapGet("/now", () => DateTime.UtcNow.ToString());
app.MapGet("/cached-now", () => DateTime.UtcNow.ToString()).CacheOutput();
app.MapGet("/cached-now-tagged", () => DateTime.UtcNow.ToString()).CacheOutput(p => p.Tag("datetime"));
app.MapGet("/clear", async (IOutputCacheStore cache) =>
{
    await cache.EvictByTagAsync("datetime", CancellationToken.None);
    return Results.Ok(new { message = "Go back and click on Cached DateTime.UtcNow() tagged to see that the previous cached date has been purged." });
});

app.Run();