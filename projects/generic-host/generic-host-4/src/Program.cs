using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Logging;

namespace PracticalAspNetCore
{
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

            using (host)
            {
                host.Start();

                await host.StopAsync(TimeSpan.FromSeconds(5));
            }
        }
    }
}