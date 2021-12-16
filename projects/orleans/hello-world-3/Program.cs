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

public class HelloArchiveGrain : Grain, IHelloArchive
{
    private readonly IPersistentState<GreetingArchive> _archive;

    public HelloArchiveGrain([PersistentState("archive", "ArchiveStorage")] IPersistentState<GreetingArchive> archive)
    {
        _archive = archive;
    }

    public async Task<string> SayHello(string greeting)
    {
        _archive.State.Greetings.Add(greeting);

        await _archive.WriteStateAsync();

        return $"You said: '{greeting}', I say: Hello!";
    }

    public Task<IEnumerable<string>> GetGreetings() => Task.FromResult<IEnumerable<string>>(_archive.State.Greetings);
}

public class GreetingArchive
{
    public List<string> Greetings { get; } = new List<string>();
}

public interface IHelloArchive : Orleans.IGrainWithIntegerKey
{
    Task<string> SayHello(string greeting);

    Task<IEnumerable<string>> GetGreetings();
}