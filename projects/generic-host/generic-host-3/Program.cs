await new HostBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<HelloWorldService>();
        services.AddHostedService<HelloWorldService2>();
        services.AddHostedService<HelloWorldService3>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
        logging.AddFilter((provider, category, logLevel) =>
        {
            return !category.Contains("Microsoft");
        });
    }).RunConsoleAsync();


public class HelloWorldService : IHostedService
{
    readonly ILogger _log;

    public HelloWorldService(ILogger<HelloWorldService> logger)
    {
        _log = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _log.LogDebug("Hello world 1");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _log.LogDebug("Goodbye 1");
        return Task.CompletedTask;
    }
}

public class HelloWorldService2 : IHostedService
{
    readonly ILogger _log;

    public HelloWorldService2(ILogger<HelloWorldService2> logger)
    {
        _log = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _log.LogDebug("Hello world 2");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _log.LogDebug("Goodbye 2");
        return Task.CompletedTask;
    }
}

public class HelloWorldService3 : IHostedService
{
    readonly ILogger _log;

    public HelloWorldService3(ILogger<HelloWorldService3> logger)
    {
        _log = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _log.LogDebug("Hello world 3");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _log.LogDebug("Goodbye 3");
        return Task.CompletedTask;
    }
}
