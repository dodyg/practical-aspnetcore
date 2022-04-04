
await new HostBuilder()
    .ConfigureAppConfiguration(configHost =>
    {
        var dict = new Dictionary<string, string>
        {
                    {"Greet", "Hello World"},
                    {"Goodbye", "Goodbye Luv"}
        };
        configHost.AddInMemoryCollection(dict);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<HelloWorldService>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
        logging.AddFilter((provider, category, logLevel) =>
        {
            return !category.Contains("Microsoft");
        });
    })
    .Build().RunAsync();

public class HelloWorldService : IHostedService
{
    readonly IConfiguration _config;
    readonly ILogger _log;

    public HelloWorldService(IConfiguration config, ILogger<HelloWorldService> logger)
    {
        _config = config;
        _log = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _log.LogDebug($"{_config["Greet"]}");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _log.LogDebug($"{_config["Goodbye"]}");
        return Task.CompletedTask;
    }
}
