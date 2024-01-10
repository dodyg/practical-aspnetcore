using System.Net;
using Orleans.Runtime;
using Orleans.Configuration;

var builder = WebApplication.CreateBuilder();
builder.Logging.SetMinimumLevel(LogLevel.Information).AddConsole();
builder.Host.UseOrleans(b =>
    {
        b
            .UseLocalhostClustering()
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "orleans-2";
            })
            .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
            .AddRedisGrainStorage("redis-timer", optionsBuilder => optionsBuilder.Configure(options =>
            {
                options.ConnectionString = "localhost:6379";
                options.DatabaseNumber = 1;
            }));
    });

var app = builder.Build();

app.MapGet("/", async (IGrainFactory client) =>
{
    IHelloArchive grain = client.GetGrain<IHelloArchive>(0)!;
    await grain.SayHello("Bom dia!");
    var res2 = await grain.GetGreetings();

    var output = $$"""
        <html>
        <head>
            <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
        </head>
        <body>
        <div class="container">
            Refresh your browser. There's a timer that keeps adding messages every 5 seconds.<br>
        """;
        foreach (var g in res2)
        {
            output += ($"<li>{g.Message} at {g.TimestampUtc}</li>");
        }

        output += "</ul></container></body></html>";
    
    return Results.Content(output, "text/html");
});

app.Run();

public class HelloTimerGrain : Grain, IHelloArchive
{
    private readonly IPersistentState<GreetingArchive> _archive;
    private readonly ILogger _log;

    private string _greeting = string.Empty;

    private IDisposable? _timerDisposable;

    public HelloTimerGrain([PersistentState("archive", "redis-timer")] IPersistentState<GreetingArchive> archive, ILogger<HelloTimerGrain> log)
    {
        _archive = archive;
        _log = log;
    }

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        _timerDisposable = this.RegisterTimer(async (object data) =>
        {
            var archive = data as IPersistentState<GreetingArchive>;
            var g = new Greeting(_greeting, DateTime.UtcNow);
            archive!.State.Greetings.Insert(0, g);
            await archive!.WriteStateAsync();

            _log.LogInformation($"`{g.Message}` added at {g.TimestampUtc}");
        }, _archive, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken token)
    {
        _timerDisposable?.Dispose();
        return Task.CompletedTask;
    }

    public Task SayHello(string greeting)
    {
        _greeting = greeting;
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Greeting>> GetGreetings() => Task.FromResult<IEnumerable<Greeting>>(_archive.State.Greetings);
}

[GenerateSerializer]
public record GreetingArchive
{
    public List<Greeting> Greetings { get; } = new List<Greeting>();
}

[GenerateSerializer]
public record Greeting(string Message, DateTime TimestampUtc);

public interface IHelloArchive : Orleans.IGrainWithIntegerKey
{
    Task SayHello(string greeting);

    Task<IEnumerable<Greeting>> GetGreetings();
}