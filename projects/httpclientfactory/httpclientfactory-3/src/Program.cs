var builder = WebApplication.CreateBuilder();
builder.Services.AddTransient<RssReader>();
builder.Services.AddHttpClient<RssReader>();
       
var app = builder.Build();

app.Run(async context =>
    {
        var rss = context.RequestServices.GetService<RssReader>();
        var result = await rss.Get("http://scripting.com/rss.xml");

        context.Response.Headers.Add("Content-Type", "application/rss+xml");
        await context.Response.WriteAsync(result);
    });

app.Run();


public class RssReader
{
    readonly HttpClient _client;

    public RssReader(HttpClient client)
    {
        _client = client;
    }

    public Task<string> Get(string url) => _client.GetStringAsync(url);
}