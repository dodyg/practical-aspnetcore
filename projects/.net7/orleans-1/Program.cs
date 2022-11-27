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
                options.ServiceId = "orleans-1";
            })
            .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
            .AddMemoryGrainStorage(name: "ArchiveStorage");
    });

var app = builder.Build();

app.MapGet("/", async (HttpContext context) =>
{
    IGrainFactory client = context.RequestServices.GetService<IGrainFactory>()!;
    IHelloArchive grain = client.GetGrain<IHelloArchive>(0)!;
    await grain.SayHello("Hello world");
    await context.Response.WriteAsync("Keep refreshing your browser \n");
    var res2 = await grain.GetGreetings();
    await context.Response.WriteAsync(string.Join("\n", res2));
});

app.Run();

public class HelloArchiveGrain : IGrain, IHelloArchive
{
    private readonly IPersistentState<GreetingArchive> _archive;

    public IGrainContext GrainContext { get; }

    public HelloArchiveGrain(IGrainContext context, [PersistentState("archive", "ArchiveStorage")] IPersistentState<GreetingArchive> archive)
    {
        _archive = archive;
        GrainContext = context;
    }

    public async Task<string> SayHello(string greeting)
    {
        _archive.State.Greetings.Add(greeting);

        await _archive.WriteStateAsync();

        return $"You said: '{greeting}', I say: Hello!";
    }

    public Task<IEnumerable<string>> GetGreetings() => Task.FromResult<IEnumerable<string>>(_archive.State.Greetings);
}

[GenerateSerializer]
[Alias("greeting-archive")]
public class GreetingArchive
{
    [Id(0)]
    public List<string> Greetings { get; } = new();
}

public interface IHelloArchive : Orleans.IGrainWithIntegerKey
{
    Task<string> SayHello(string greeting);

    Task<IEnumerable<string>> GetGreetings();
}