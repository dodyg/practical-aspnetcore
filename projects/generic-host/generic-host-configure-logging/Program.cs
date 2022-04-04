await new HostBuilder()
    .ConfigureLogging((hostContext, configLogging) =>
    {
        configLogging.ClearProviders();
        configLogging.AddConsole();
        configLogging.SetMinimumLevel(LogLevel.Trace);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<HelloWorldService>();
    })
    .Build().RunAsync();

public class HelloWorldService : IHostedService
{
    readonly ILogger _logger;
    public HelloWorldService(ILogger<HelloWorldService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Hello world");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Goodbye");
        return Task.CompletedTask;
    }
}
