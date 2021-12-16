using System.Net;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System.Text.Json.Serialization;
using Orleans.Concurrency;

var builder = WebApplication.CreateBuilder();
builder.Services.AddHttpClient();
builder.Logging.SetMinimumLevel(LogLevel.Information).AddConsole();
builder.Host.UseOrleans(b =>
    {
        b
            .UseLocalhostClustering()
            .UseInMemoryReminderService()
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "http-client";
            })
            .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
            .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(TimeKeeperGrain).Assembly).WithReferences());
    });

var app = builder.Build();

app.MapGet("/", async context =>
{
    IGrainFactory client = context.RequestServices.GetService<IGrainFactory>()!;
    var timezone = "Africa/Cairo";
    ITimeKeeper grain = client.GetGrain<ITimeKeeper>(timezone)!;
    var localTime = await grain.GetCurrentTime(timezone);
    await context.Response.WriteAsync(@"<html><head><link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/uikit@3.5.5/dist/css/uikit.min.css"" /></head>");
    await context.Response.WriteAsync("<body>");
    await context.Response.WriteAsync($"Local time in {localTime.timeZone} is {localTime.dateTime}");
    await context.Response.WriteAsync("</body></html>");
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

        return (worldClock.DateTime, timeZone);
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