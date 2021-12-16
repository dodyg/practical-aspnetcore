using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;

await new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddSingleton<ClusterClientHostedService>();
        services.AddSingleton<IHostedService>(_ => _.GetService<ClusterClientHostedService>()!);
        services.AddSingleton(_ => _.GetService<ClusterClientHostedService>()!.Client);

        services.AddHostedService<HelloWorldClientHostedService>();

        services.Configure<ConsoleLifetimeOptions>(options =>
        {
            options.SuppressStatusMessages = true;
        });
    })
    .ConfigureLogging(builder =>
    {
        builder.SetMinimumLevel(LogLevel.Information);
        builder.AddConsole();
    })
    .RunConsoleAsync();

public class HelloWorldClientHostedService : IHostedService
{
    private readonly IClusterClient _client;

    public HelloWorldClientHostedService(IClusterClient client)
    {
        _client = client;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // example of calling grains from the initialized client
        var friend = _client.GetGrain<IHello>(0);
        var response = await friend.SayHello("Good morning, my friend!");
        Console.WriteLine($"{response}");

        // example of calling IHelloArchive grqain that implements persistence
        var g = this._client.GetGrain<IHelloArchive>(0);
        response = await g.SayHello("Good day!");
        Console.WriteLine($"{response}");

        response = await g.SayHello("Good evening!");
        Console.WriteLine($"{response}");

        var greetings = await g.GetGreetings();
        Console.WriteLine($"\nArchived greetings: {Utils.EnumerableToString(greetings)}");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

public class ClusterClientHostedService : IHostedService
{
    private readonly ILogger<ClusterClientHostedService> _logger;

    public ClusterClientHostedService(ILogger<ClusterClientHostedService> logger, ILoggerProvider loggerProvider)
    {
        _logger = logger;
        Client = new ClientBuilder()
            .UseLocalhostClustering()
            .ConfigureLogging(builder => builder.AddProvider(loggerProvider))
            .Build();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var attempt = 0;
        var maxAttempts = 100;
        var delay = TimeSpan.FromSeconds(1);
        return Client.Connect(async error =>
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }

            if (++attempt < maxAttempts)
            {
                _logger.LogWarning(error,
                    "Failed to connect to Orleans cluster on attempt {@Attempt} of {@MaxAttempts}.",
                    attempt, maxAttempts);

                try
                {
                    await Task.Delay(delay, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    return false;
                }

                return true;
            }
            else
            {
                _logger.LogError(error,
                    "Failed to connect to Orleans cluster on attempt {@Attempt} of {@MaxAttempts}.",
                    attempt, maxAttempts);

                return false;
            }
        });
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            await Client.Close();
        }
        catch (OrleansException error)
        {
            _logger.LogWarning(error, "Error while gracefully disconnecting from Orleans cluster. Will ignore and continue to shutdown.");
        }
    }

    public IClusterClient Client { get; }
}