using System.Net;
using Orleans;
using Orleans.Runtime;
using Orleans.Configuration;
using Orleans.Hosting;

var builder = WebApplication.CreateBuilder();
builder.Logging.SetMinimumLevel(LogLevel.Information).AddConsole();

builder.Host.UseOrleans(b =>
{
    b
        .UseLocalhostClustering()
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "dev";
            options.ServiceId = "HelloWorldApp";
        })
        .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloArchiveGrain).Assembly).WithReferences())
        .AddRedisGrainStorage("redis-4", optionsBuilder => optionsBuilder.Configure(options =>
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
    await grain.SayHello("Hello world");
    var res2 = await grain.GetGreetings();

    await context.Response.WriteAsync(@"<html><head><link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/uikit@3.5.5/dist/css/uikit.min.css"" /></head>");
    await context.Response.WriteAsync("<body>");
    await context.Response.WriteAsync("Keep refreshing your browser <br>");
    await context.Response.WriteAsync("<ul>");
    foreach (var g in res2)
    {
        await context.Response.WriteAsync($"<li>{g.Message} at {g.TimestampUtc}</li>");
    }
    await context.Response.WriteAsync("</ul>");
    await context.Response.WriteAsync("</body></html>");
});

app.Run();

public class HelloArchiveGrain : Grain, IHelloArchive
{
    private readonly IPersistentState<GreetingArchive> _archive;

    public HelloArchiveGrain([PersistentState("archive", "redis-4")] IPersistentState<GreetingArchive> archive)
    {
        _archive = archive;
    }

    public async Task<string> SayHello(string greeting)
    {
        _archive.State.Greetings.Add(new Greeting(greeting, DateTime.UtcNow));

        await _archive.WriteStateAsync();

        return $"You said: '{greeting}', I say: Hello!";
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
    Task<string> SayHello(string greeting);

    Task<IEnumerable<Greeting>> GetGreetings();
}