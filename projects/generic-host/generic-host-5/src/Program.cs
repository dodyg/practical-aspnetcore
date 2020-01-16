using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace PracticalAspNetCore
{
    public class CountingService : IHostedService, IDisposable
    {
        CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        Task _executingTask;

        readonly ILogger _log;

        public CountingService(ILogger<CountingService> logger)
        {
            _log = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
        }

        async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var count = 0;
            do
            {
                _log.LogDebug(count.ToString());
                count++;
                await Task.Delay(1000, cancellationToken);
            } while (!cancellationToken.IsCancellationRequested);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
                return;

            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        public void Dispose() => _stoppingCts.Cancel();
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<CountingService>();
                })
                .UseConsoleLifetime()
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