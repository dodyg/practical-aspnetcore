using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace PracticalAspNetCore
{
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

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
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
                .Build();

            await host.RunAsync();
        }
    }
}