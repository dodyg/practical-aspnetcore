using System.Net;
using Orleans;
using Orleans.Runtime;
using Orleans.Configuration;
using Orleans.Hosting;

var builder = WebApplication.CreateBuilder();
builder.Logging.SetMinimumLevel(LogLevel.Information).AddConsole();
builder.Host.UseOrleans(builder =>
    {
        builder
            .UseLocalhostClustering()
            .UseInMemoryReminderService()
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "HelloWorldApp";
            })
            .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
            .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloReminderGrain).Assembly).WithReferences())
            .AddRedisGrainStorage("redis-reminder", optionsBuilder => optionsBuilder.Configure(options =>
            {
                options.ConnectionString = "localhost:6379"; 
                options.UseJson = true;
                options.DatabaseNumber = 1;
            }));
    });

var app = builder.Build();

app.MapGet("/", async context =>
{
    IGrainFactory client = context.RequestServices.GetService<IGrainFactory>()!;
    IHelloArchive grain = client.GetGrain<IHelloArchive>(0)!;
    await grain.SayHello("Hello world " + new Random().Next());
    var res2 = await grain.GetGreetings();

    await context.Response.WriteAsync(@"<html><head><link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/uikit@3.5.5/dist/css/uikit.min.css"" /></head>");
    await context.Response.WriteAsync("<body>");
    await context.Response.WriteAsync("Click on Set reminder to start the reminder (it will run every 1 minute). Then refresh this page to see the messages being addded.<br>");
    await context.Response.WriteAsync(@"<a href=""set-reminder"">Set reminder</a> - <a href=""remove-reminder"">Remove reminder</a><br/>");
    await context.Response.WriteAsync("<ul>");
    foreach(var g in res2)
    {
        await context.Response.WriteAsync($"<li>{g.Message} at {g.TimestampUtc}</li>");
    }
    await context.Response.WriteAsync("</ul>");
    await context.Response.WriteAsync("</body></html>");
});

// WARNING - changing state using GET is a terrible terrible practice. I use it here because this is a sample and I am lazy. Don't follow my bad example.
app.MapGet("/set-reminder", async context =>
{
    IGrainFactory client = context.RequestServices.GetService<IGrainFactory>()!;
    IHelloArchive grain = client.GetGrain<IHelloArchive>(0)!;
    
    await grain.AddReminder("repeat-hello", repeatEvery: TimeSpan.FromMinutes(1));

    context.Response.Redirect("/");
});

// WARNING - changing state using GET is a terrible terrible practice. I use it here because this is a sample and I am lazy. Don't follow my bad example.
app.MapGet("/remove-reminder", async context =>
{
    IGrainFactory client = context.RequestServices.GetService<IGrainFactory>()!;
    IHelloArchive grain = client.GetGrain<IHelloArchive>(0)!;
    
    await grain.RemoveReminder("repeat-hello");
    context.Response.Redirect("/");
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
        _log.Info($"Receive reminder {reminderName} on { DateTime.UtcNow } with status { status }");
        var g = new Greeting(_greeting, DateTime.UtcNow);
        _archive!.State.Greetings.Insert(0, g);
        await _archive!.WriteStateAsync();

        _log.Info($"`{g.Message}` added at {g.TimestampUtc}");
    }

    public async Task AddReminder(string reminder, TimeSpan repeatEvery)
    {
        if (string.IsNullOrWhiteSpace(reminder))
            throw new ArgumentNullException(nameof(reminder));

        var r = await GetReminder(reminder);

        if (r is not object)
            await RegisterOrUpdateReminder(reminder, TimeSpan.FromSeconds(1), repeatEvery);
    }

    public async Task RemoveReminder(string reminder)
    {
        if (string.IsNullOrWhiteSpace(reminder))
            throw new ArgumentNullException(nameof(reminder));

        var r = await GetReminder(reminder);

        if (r is object)
            await UnregisterReminder(r);
    }
}

public record GreetingArchive
{
    public List<Greeting> Greetings { get; } = new List<Greeting>();
}

public record Greeting(string Message, DateTime TimestampUtc);

public interface IHelloArchive : Orleans.IGrainWithIntegerKey
{
    Task AddReminder(string reminder, TimeSpan repeatEvery);

    Task RemoveReminder(string reminder);

    Task SayHello(string greeting);

    Task<IEnumerable<Greeting>> GetGreetings();
}
