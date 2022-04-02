using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PracticalAspNetCore
{
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

    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
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
                });
    }
}