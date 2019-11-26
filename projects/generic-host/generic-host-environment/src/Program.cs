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
        readonly IHostingEnvironment _env;


        public HelloWorldService(IHostingEnvironment env)
        {
            _env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (_env.IsProduction())
                Console.WriteLine("Hello world production");
            else if (_env.IsStaging())
                Console.WriteLine("Hello world staging");
            else if (_env.IsDevelopment())
                Console.WriteLine("Hello world development");

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
                .UseEnvironment(EnvironmentName.Development) // change this to use other
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<HelloWorldService>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}