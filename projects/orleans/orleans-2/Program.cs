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
            .AddRedisGrainStorage("redis", optionsBuilder => optionsBuilder.Configure(options =>
            {
                options.ConnectionString = "localhost:6379";
                options.DatabaseNumber = 1;
            }));
    });

var app = builder.Build();

app.MapGet("/", async (IGrainFactory client) =>
{
    IHelloArchive grain = client.GetGrain<IHelloArchive>(0)!;
    await grain.SayHello("Hello world");
    var res2 = await grain.GetGreetings();

    var output = $$"""
        <html>
        <head>
            <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
        </head>
        <body>
        <div class="container">
            <h1>Redis Storage</h1>
            Keep refreshing your browser.<br/>
            You will see the messages keep accumulating.<br/>
            Now stop your application and start again.<br/>
            You will see that your actor persist its data at Redis and the messages will resume at the point where you left the last time.
            <ul>
        """;
        foreach (var g in res2)
        {
            output += ($"<li>{g.Message} at {g.TimestampUtc}</li>");
        }

        output += "</ul></container></body></html>";
    
    return Results.Content(output, "text/html");
});

app.Run();


public class HelloArchiveGrain : IGrain, IHelloArchive
{
    private readonly IPersistentState<GreetingArchive> _archive;

    public IGrainContext GrainContext { get; }

    public HelloArchiveGrain(IGrainContext context, [PersistentState("archive", "redis")] IPersistentState<GreetingArchive> archive)
    {
        _archive = archive;
        GrainContext = context;
    }

    public async Task<string> SayHello(string greeting)
    {
        _archive.State.Greetings.Add(new Greeting(greeting, DateTime.UtcNow));

        await _archive.WriteStateAsync();

        return $"You said: '{greeting}', I say: Hello!";
    }

    public Task<IEnumerable<Greeting>> GetGreetings() => Task.FromResult<IEnumerable<Greeting>>(_archive.State.Greetings);
}

[GenerateSerializer]
[Alias("greeting-archive")]
public record GreetingArchive
{
    [Id(0)]
    public List<Greeting> Greetings { get; } = new List<Greeting>();
}

//Record has implicit ids by default. There is no need fro [Id] attribute here.
//You just have to make sure that you don't play around with the order of the member
[GenerateSerializer]
[Alias("greeting")]
public record Greeting(string Message, DateTime TimestampUtc); 

public interface IHelloArchive : Orleans.IGrainWithIntegerKey
{
    Task<string> SayHello(string greeting);

    Task<IEnumerable<Greeting>> GetGreetings();
}

