using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;

string CACHE_KEY = "MyCache";

var builder = WebApplication.CreateBuilder();
builder.Services.AddMemoryCache();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFilter((provider, category, logLevel) => !category.Contains("Microsoft.AspNetCore"));

var app = builder.Build();
var fileProvider = new PhysicalFileProvider(app.Environment.ContentRootPath);
app.Run(async context =>
{
    var log = context.RequestServices.GetService<ILoggerFactory>().CreateLogger("app");

    var cache = context.RequestServices.GetService<IMemoryCache>();
    var greeting = cache.Get(CACHE_KEY) as string;

    //There is no existing cache, add one
    if (string.IsNullOrWhiteSpace(greeting))
    {
        var options = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(5))
            .RegisterPostEvictionCallback((object key, object value, EvictionReason reason, object state) =>
            {
                if (state == null)
                    log.LogInformation("State is null");

                log.LogInformation($"Key '{key}' with value '{value}' was removed because of '{reason}'");
            });
        //You can also use .SetSlidingExpiration

        var message = "Hello " + DateTimeOffset.UtcNow.ToString();
        cache.Set(CACHE_KEY, message, options);
        greeting = message;
    }

    context.Response.Headers.Add("Content-Type", "text/html");
    await context.Response.WriteAsync(@"
        <html>
            <body>
                <p>
                    The cache is set to expire in 5 seconds. Wait until 5 seconds then refresh this page. Check your console log to see the post eviction message event.  
                </p>
                <p>
                    If you don't refresh this page, the eviction message won't be posted at the console. Here's <a href=""https://github.com/aspnet/Caching/issues/353#issuecomment-333956801"">an explanation</a> for this behavior.
                </p>
            </body>
        </html>");
});

app.Run();