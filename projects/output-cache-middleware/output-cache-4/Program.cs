using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.With(c => true));
});

var app = builder.Build();
app.MapGet("/", () =>
{
    return Results.Content("""
    <html>
        <body>
            <ul>
                <li><a href="/now">DateTime.UtcNow()</a></li>
                <li><a href="/random">RandomNumberGenerator.GetInt32(int.MaxValue)</a></li>
            </ul>
        </body>
    </html>
    """, "text/html");
});

app.UseOutputCache();
app.MapGet("/now", () => DateTime.UtcNow.ToString());
app.MapGet("/random", () => RandomNumberGenerator.GetInt32(int.MaxValue));

app.Run();