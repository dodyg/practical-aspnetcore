using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace StartupBasic
{
    public abstract class HostedService : Microsoft.Extensions.Hosting.IHostedService, IDisposable
    {
        //from https://blogs.msdn.microsoft.com/cesardelatorre/2017/11/18/implementing-background-tasks-in-microservices-with-ihostedservice-and-the-backgroundservice-class-net-core-2-x/
        
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                // Signal cancellation to the executing method
                _stoppingCts.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);

        public virtual void Dispose() =>  _stoppingCts.Cancel();
    }

    public class GreeterUpdaterService : HostedService
    {
        Greeter _greeter;
        public GreeterUpdaterService(Greeter greeter)
        {
            _greeter = greeter;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
             while (!cancellationToken.IsCancellationRequested)
            {
                _greeter.Counter++;
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }

    public class Greeter 
    {
        public int Counter { get; set; }

        public override string ToString() => $"Hello world {Counter}";
    }

    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //These are three services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Greeter>();
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, GreeterUpdaterService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
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
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}