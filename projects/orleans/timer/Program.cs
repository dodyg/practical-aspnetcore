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
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "dev";
            options.ServiceId = "HelloWorldApp";
        })
        .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloTimerGrain).Assembly).WithReferences())
        .AddRedisGrainStorage("redis-timer", optionsBuilder => optionsBuilder.Configure(options =>
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
    await context.Response.WriteAsync("Refresh your browser. There's a timer that keeps adding messages every 5 seconds.<br>");
    await context.Response.WriteAsync("<ul>");
    foreach (var g in res2)
    {
        await context.Response.WriteAsync($"<li>{g.Message} at {g.TimestampUtc}</li>");
    }
    await context.Response.WriteAsync("</ul>");
    await context.Response.WriteAsync("</body></html>");
});

app.Run();


public class HelloTimerGrain : Grain, IHelloArchive
{
    private readonly IPersistentState<GreetingArchive> _archive;
    private readonly ILogger _log;

    private string _greeting = "hello world";

    private IDisposable? _timerDisposable;

    public HelloTimerGrain([PersistentState("archive", "redis-timer")] IPersistentState<GreetingArchive> archive, ILogger<HelloTimerGrain> log)
    {
        _archive = archive;
        _log = log;
    }

    public override Task OnActivateAsync()
    {
        _timerDisposable = this.RegisterTimer(async (object data) =>
        {
            var archive = data as IPersistentState<GreetingArchive>;
            var g = new Greeting(_greeting, DateTime.UtcNow);
            archive!.State.Greetings.Insert(0, g);
            await archive!.WriteStateAsync();

            _log.Info($"`{g.Message}` added at {g.TimestampUtc}");
        }, _archive, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    public override Task OnDeactivateAsync()
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

public record GreetingArchive
{
    public List<Greeting> Greetings { get; } = new List<Greeting>();
}

public record Greeting(string Message, DateTime TimestampUtc);

public interface IHelloArchive : Orleans.IGrainWithIntegerKey
{
    Task SayHello(string greeting);

    Task<IEnumerable<Greeting>> GetGreetings();
}