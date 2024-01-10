using System.Net;
using Orleans.Configuration;
using Orleans.Concurrency;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder();
builder.Services.AddHttpClient();
builder.Logging.SetMinimumLevel(LogLevel.Information).AddConsole();
builder.Host.UseOrleans(b =>
    {
        b
            .UseLocalhostClustering()
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "orleans-5";
            })
            .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback);
    });

var app = builder.Build();

app.MapGet("/", async (IGrainFactory client) =>
{
    var timezone = "Africa/Cairo";
    ITimeKeeper grain = client.GetGrain<ITimeKeeper>(timezone)!;
    var localTime = await grain.GetCurrentTime(timezone);

    var output = $$"""
        <html>
        <head>
            <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
        </head>
        <body>
        <div class="container">
            Local time in {{localTime.timeZone}} is {{localTime.dateTime}}
        </body></html>
        """;
    
    return Results.Content(output, "text/html");
});


app.Run();


[StatelessWorker]
public class TimeKeeperGrain : Grain, ITimeKeeper
{
    private readonly ILogger _log;

    private readonly IHttpClientFactory _httpFactory;

    public TimeKeeperGrain(ILogger<TimeKeeperGrain> log, IHttpClientFactory httpFactory)
    {
        _log = log;
        _httpFactory = httpFactory;
    }

    public async Task<(DateTimeOffset dateTime, string timeZone)> GetCurrentTime(string timeZone)
    {
        var client = _httpFactory.CreateClient();

        var result = await client.GetAsync($"http://worldtimeapi.org/api/timezone/{timeZone}");
        var worldClock = await result.Content.ReadFromJsonAsync<WorldTime>();

        return (worldClock!.DateTime, timeZone);
    }
}

public interface ITimeKeeper : IGrainWithStringKey
{
    Task<(DateTimeOffset dateTime, string timeZone)> GetCurrentTime(string timeZone);
}

public class WorldTime
{
    [JsonPropertyName("datetime")]
    public DateTimeOffset DateTime { get; set; }
}