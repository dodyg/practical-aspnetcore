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
                <li><a href="/cached-now?version=1">DateTime.UtcNow() cached version 1</a></li>
                <li><a href="/cached-now?version=2">DateTime.UtcNow() cached version 2</a></li>
                <li><a href="/cached-now?version=1&culture=en">DateTime.UtcNow() cached version 1 in en culture</a></li>
                <li><a href="/cached-now?version=1&culture=ar">DateTime.UtcNow() cached version 1 in ar culture</a></li>
            </ul>
        </body>
    </html>
    """, "text/html");
});

app.UseOutputCache();
app.MapGet("/now", () => DateTime.UtcNow.ToString());
app.MapGet("/cached-now", () => DateTime.UtcNow.ToString()).CacheOutput(p => p.SetVaryByQuery("version", "culture"));

app.Run();