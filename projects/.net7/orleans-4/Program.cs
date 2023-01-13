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
            .AddRedisGrainStorage("redis-reminder", optionsBuilder => optionsBuilder.Configure(options =>
            {
                options.ConnectionString = "localhost:6379";
                options.DatabaseNumber = 1;
            }))
            .UseInMemoryReminderService();
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
            Click on Set reminder to start the reminder (it will run every 1 minute). Then refresh this page to see the messages being addded.<br>
            <a href="set-reminder">Set reminder</a> - <a href="remove-reminder">Remove reminder</a><br/>
        """;
        foreach (var g in res2)
        {
            output += ($"<li>{g.Message} at {g.TimestampUtc}</li>");
        }

        output += "</ul></container></body></html>";
    
    return Results.Content(output, "text/html");
});

// WARNING - changing state using GET is a terrible terrible practice. I use it here because this is a sample and I am lazy. Don't follow my bad example.
app.MapGet("/set-reminder", async (IGrainFactory client) =>
{
    IHelloArchive grain = client.GetGrain<IHelloArchive>(0)!;
    
    await grain.AddReminder("repeat-hello", repeatEvery: TimeSpan.FromMinutes(1));
    return Results.Redirect("/");
});

// WARNING - changing state using GET is a terrible terrible practice. I use it here because this is a sample and I am lazy. Don't follow my bad example.
app.MapGet("/remove-reminder", async (IGrainFactory client) =>
{
    IHelloArchive grain = client.GetGrain<IHelloArchive>(0)!;
    
    await grain.RemoveReminder("repeat-hello");
    return Results.Redirect("/");
});



app.Run();


public class HelloReminderGrain : Grain, IHelloArchive, IRemindable
{
    private readonly IPersistentState<GreetingArchive> _archive;
    private readonly ILogger _log;

    private string _greeting = "hello world";

    public HelloReminderGrain([PersistentState("archive", "redis-reminder")] IPersistentState<GreetingArchive> archive, ILogger<HelloReminderGrain> log)
    {
        _archive = archive;
        _log = log;
    }

    public Task SayHello(string greeting)
    {
        _greeting = greeting;
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Greeting>> GetGreetings() => Task.FromResult<IEnumerable<Greeting>>(_archive.State.Greetings);

    public async Task ReceiveReminder(string reminderName, TickStatus status)
    {
        _log.LogInformation($"Receive reminder {reminderName} on { DateTime.UtcNow } with status { status }");
        var g = new Greeting(_greeting, DateTime.UtcNow);
        _archive!.State.Greetings.Insert(0, g);
        await _archive!.WriteStateAsync();

        _log.LogInformation($"`{g.Message}` added at {g.TimestampUtc}");
    }

    public async Task AddReminder(string reminder, TimeSpan repeatEvery)
    {
        if (string.IsNullOrWhiteSpace(reminder))
            throw new ArgumentNullException(nameof(reminder));

        var r = await this.GetReminder(reminder);

        if (r is null)
        {
            _log.LogInformation($"RegisterOrUpdateReminder {reminder}");
            await this.RegisterOrUpdateReminder(reminder, TimeSpan.FromSeconds(1), repeatEvery);
        }
    }

    public async Task RemoveReminder(string reminder)
    {
        if (string.IsNullOrWhiteSpace(reminder))
            throw new ArgumentNullException(nameof(reminder));

        var r = await this.GetReminder(reminder);

        if (r is object)
        {
            _log.LogInformation($"UnregisterReminder {reminder}");
            await this.UnregisterReminder(r);
        }
    }
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
    Task AddReminder(string reminder, TimeSpan repeatEvery);

    Task RemoveReminder(string reminder);

    Task SayHello(string greeting);

    Task<IEnumerable<Greeting>> GetGreetings();
}