var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<Greeter>();
builder.Services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, GreeterUpdaterService>();

var app = builder.Build();
app.Run(context =>
{
    var greet = context.RequestServices.GetService<Greeter>();

    return context.Response.WriteAsync($"Please reload page (greeting updated every 1 second in the background) {greet}");
});

app.Run();

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

    public virtual void Dispose() => _stoppingCts.Cancel();
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

