using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Runtime;
using Orleans.Configuration;
using Orleans.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using Orleans.Concurrency;

await Host.CreateDefaultBuilder(args)
    .ConfigureLogging(builder =>
    {
        builder.SetMinimumLevel(LogLevel.Information);
        builder.AddConsole();
    })
    .UseOrleans(builder =>
    {
        builder
            .UseLocalhostClustering()
            .UseInMemoryReminderService()
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "http-client";
            })
            .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
            .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(TimeKeeperGrain).Assembly).WithReferences());
    })
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
    .RunConsoleAsync();

class Startup
{
    IHostEnvironment _env;

    public Startup(IHostEnvironment env)
    {
        _env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (_env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
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
        });
    }
}

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

public interface ITimeKeeper: IGrainWithStringKey
{
    Task<(DateTimeOffset dateTime, string timeZone)> GetCurrentTime(string timeZone);
}

public class WorldTime
{
    [JsonPropertyName("datetime")]
    public DateTimeOffset DateTime { get; set; }
}