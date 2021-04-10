using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class GreeterUpdaterService :  IHostedService, IDisposable
    {
        Greeter _greeter;
        readonly ILogger<GreeterUpdaterService> _logger;
        
        Timer _timer;

        public GreeterUpdaterService(ILogger<GreeterUpdaterService> logger, Greeter greeter)
        {
            _logger = logger;
            _greeter = greeter;
        }

        private void DoWork(object state)
        {
            _greeter.Counter++;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(GreeterUpdaterService)} running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(GreeterUpdaterService)} is stopping.");
            
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

    public class Greeter 
    {
        public int Counter { get; set; }

        public override string ToString() => $"Hello world {Counter}";
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Greeter>();
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, GreeterUpdaterService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            //These are the four default services available at Configure
            app.Run(context =>
            {
                var greet = context.RequestServices.GetService<Greeter>();

                return context.Response.WriteAsync($"Please reload page (greeting updated every 1 second in the background) {greet}");
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}