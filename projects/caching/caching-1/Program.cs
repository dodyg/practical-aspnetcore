using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;

string CACHE_KEY = "MyCache";

var builder = WebApplication.CreateBuilder();
builder.Services.AddMemoryCache();
var app = builder.Build();

var fileProvider = new PhysicalFileProvider(app.Environment.ContentRootPath);

app.Run(async context =>
{
    var cache = context.RequestServices.GetService<IMemoryCache>();
    var greeting = cache.Get(CACHE_KEY) as string;

    //There is no existing cache, add one
    if (string.IsNullOrWhiteSpace(greeting))
    {
        var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(100));
        //You can also use .SetSlidingExpiration

        var message = "Hello " + DateTimeOffset.UtcNow.ToString();
        cache.Set(CACHE_KEY, message, options);
        greeting = message;
    }

    await context.Response.WriteAsync($"After the first load, this greeting '{greeting}' is cached. \nCheck the time stamp of the message compared to this current one {DateTimeOffset.UtcNow}.\n");
});

app.Run();