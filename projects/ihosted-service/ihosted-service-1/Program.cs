var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<Greeter>();
builder.Services.AddHostedService<GreeterUpdaterService>();

var app = builder.Build();
app.Run(context =>
{
    var greet = context.RequestServices.GetService<Greeter>();

    return context.Response.WriteAsync($"Please reload page (greeting updated every 1 second in the background) {greet}");
});

app.Run();

/// <summary>
/// Background service that updates a Greeter counter
/// </summary>
public class GreeterUpdaterService : BackgroundService
{
    private readonly Greeter _greeter;

    public GreeterUpdaterService(Greeter greeter)
    {
        _greeter = greeter;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _greeter.Counter++;
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}

public class Greeter
{
    public int Counter { get; set; }
    public override string ToString() => $"Hello world {Counter}";
}
