await Host.CreateDefaultBuilder(args)
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
          .RunConsoleAsync();

public class HelloWorldService : IHostedService
{
    ILogger _log;

    public HelloWorldService(ILogger<HelloWorldService> logger)
    {
        _log = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _log.LogDebug("Start Hello world");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _log.LogDebug("Goodbye");
        return Task.CompletedTask;
    }
}
