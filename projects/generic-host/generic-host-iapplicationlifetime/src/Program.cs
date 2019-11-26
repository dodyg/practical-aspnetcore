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
        readonly IApplicationLifetime _lifetime;

        public HelloWorldService(IApplicationLifetime lifetime)
        {
            _lifetime = lifetime;
            _lifetime.ApplicationStarted.Register(OnStarted);
            _lifetime.ApplicationStopping.Register(OnStopping);
            _lifetime.ApplicationStopped.Register(OnStopped);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("StartAsync");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("StopAsync");
            return Task.CompletedTask;
        }

        void OnStarted()
        {
            Console.WriteLine("OnStarted");
        }

        void OnStopping()
        {
            Console.WriteLine("OnStopping");
        }

        void OnStopped()
        {
            Console.WriteLine("OnStopped");
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
                .Build();

            await host.RunAsync();
        }
    }
}