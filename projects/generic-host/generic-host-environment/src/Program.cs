using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PracticalAspNetCore
{
    public class HelloWorldService : IHostedService
    {
        readonly IHostEnvironment _env;
        readonly ILogger _log;

        public HelloWorldService(IHostEnvironment env, ILogger<HelloWorldService> logger)
        {
            _env = env;
            _log = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_env.IsProduction())
                _log.LogDebug("Hello world production");
            else if (_env.IsStaging())
                _log.LogDebug("Hello world staging");
            else if (_env.IsDevelopment())
                _log.LogDebug("Hello world development");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .UseEnvironment(Environments.Development) // change this to use other
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