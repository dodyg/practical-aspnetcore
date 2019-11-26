using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace GenericHostBasic
{
    public class HelloWorldService : IHostedService
    {
        readonly IConfiguration _config;
        public HelloWorldService(IConfiguration config)
        {
            _config = config;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{_config["Greet"]}");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{_config["Goodbye"]}");
            return Task.CompletedTask;
        }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
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
                .Build();

            await host.RunAsync();
        }
    }
}