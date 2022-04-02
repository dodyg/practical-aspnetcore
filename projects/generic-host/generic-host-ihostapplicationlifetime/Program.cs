using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PracticalAspNetCore
{
    public class HelloWorldService : IHostedService
    {
        readonly IHostApplicationLifetime _lifetime;
        readonly ILogger _log;

        public HelloWorldService(IHostApplicationLifetime lifetime, ILogger<HelloWorldService> logger)
        {
            _log = logger;
            _lifetime = lifetime;
            _lifetime.ApplicationStarted.Register(OnStarted);
            _lifetime.ApplicationStopping.Register(OnStopping);
            _lifetime.ApplicationStopped.Register(OnStopped);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _log.LogDebug("StartAsync");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _log.LogDebug("StopAsync");
            return Task.CompletedTask;
        }

        void OnStarted()
        {
            _log.LogDebug("OnStarted");
        }

        void OnStopping()
        {
            _log.LogDebug("OnStopping");
        }

        void OnStopped()
        {
            _log.LogDebug("OnStopped");
        }

    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
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
                .Build();

            await host.RunAsync();
        }
    }
}