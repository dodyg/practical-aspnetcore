using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

string CACHE_KEY = "MyCache";
string CACHE_KEY_2 = "MyCache2";

var builder = WebApplication.CreateBuilder();
builder.Services.AddMemoryCache();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Filter out the noise 
builder.Logging.AddFilter((provider, category, logLevel) => !category.Contains("Microsoft.AspNetCore"));

var app = builder.Build();

var fileProvider = new PhysicalFileProvider(app.Environment.ContentRootPath);
var cts = new CancellationTokenSource();

int count = 0;
int resetCount = 8;

app.Run(async context =>
{
    if (context.Request.Path == "/favicon.ico") return;//skip favicon request

    var log = context.RequestServices.GetService<ILoggerFactory>().CreateLogger("app");
    var cache = context.RequestServices.GetService<IMemoryCache>();
    var greeting = cache.Get(CACHE_KEY) as string;

    //There is no existing cache, add one
    if (string.IsNullOrWhiteSpace(greeting))
    {
        var options = new MemoryCacheEntryOptions()
            .AddExpirationToken(new CancellationChangeToken(cts.Token))
            .RegisterPostEvictionCallback((object key, object value, EvictionReason reason, object state) =>
            {
                log.LogInformation($"Key '{key}' with value '{value}' was removed because of '{reason}'");
            });

        var message = $"Hello   {DateTime.Now.Ticks}";
        cache.Set(CACHE_KEY, message, options);
        greeting = message;
    }

    var greeting2 = cache.Get(CACHE_KEY_2) as string;
    if (string.IsNullOrWhiteSpace(greeting2))
    {
        var options = new MemoryCacheEntryOptions()
        .AddExpirationToken(new CancellationChangeToken(cts.Token))
        .RegisterPostEvictionCallback((object key, object value, EvictionReason reason, object state) =>
        {
            log.LogInformation($"Key '{key}' with value '{value}' was removed because of '{reason}'");
        });

        var message = $"Hello 2 {DateTime.UtcNow.Ticks}";
        cache.Set(CACHE_KEY_2, message, options);
        greeting2 = message;
    }

    await context.Response.WriteAsync($"Greeting {greeting}\n");
    await context.Response.WriteAsync($"Greeting {greeting2}\n");

    count++;

    if (count >= resetCount)
    {
        await context.Response.WriteAsync("Cache expired\n");
        cts.Cancel();
        count = 0;
        cts = new CancellationTokenSource();
    }

    await context.Response.WriteAsync($"Above greetings share the same cancellation token. They will both expire at the same time. The caches will expire if you refresh this page {resetCount} times. So far you have loaded {count} times. \n");
});

app.Run();