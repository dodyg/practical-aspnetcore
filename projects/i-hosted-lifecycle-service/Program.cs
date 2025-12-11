using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.DependencyInjection;
 
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ExampleHostedService>();
builder.Services.AddLogging(opt =>
{
    opt.AddSimpleConsole(c =>
    {
        c.SingleLine = true;
        c.TimestampFormat = "[HH:mm:ss] ";
    });
});

using IHost host = builder.Build();

//Host will issue a cancel token 10 seconds after StartedAsync()
await host.RunAsync((new CancellationTokenSource(10000)).Token);

public class ExampleHostedService : IHostedLifecycleService
{
    private readonly ILogger _logger;

    public ExampleHostedService(ILogger<ExampleHostedService> logger){
        _logger = logger;
    }

    public async Task StartingAsync(CancellationToken cancellationToken){
        
        _logger.LogInformation("Step #1: StartingAsync, will take 5 seconds");

        //simulate the delay of starting the service up.
        await Task.Delay(5000);

        _logger.LogInformation("Step #2: End StartingAsync");
    }

    public async Task StartAsync(CancellationToken cancellationToken){
        _logger.LogInformation("Step #3: StartAsync");
        await Task.Yield();
    }
    
    public async Task StartedAsync(CancellationToken cancellationToken){
        _logger.LogInformation("Step #4: StartedAsync");
        await Task.Yield();
    }

    public async Task StoppingAsync(CancellationToken cancellationToken){

        _logger.LogInformation("Step #5: StoppingAsync, will take 2 seconds");

        //simulate delay when gracefully stopping the service.
        cancellationToken.WaitHandle.WaitOne(2000);

        _logger.LogInformation("Step #6: End StoppingAsync");
        await Task.Yield();
    }

    public async Task StopAsync(CancellationToken cancellationToken){
        _logger.LogInformation("Step #7: StopAsync");
        await Task.Yield();
    }

    public async Task StoppedAsync(CancellationToken cancellationToken){
        _logger.LogInformation("Step #8: StoppedAsync");
        await Task.Yield();
    }
}