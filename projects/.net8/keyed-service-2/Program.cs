using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();

builder.Services.AddKeyedSingleton<IGreeting, MorningGreeting>("morning");
builder.Services.AddKeyedSingleton<IGreeting, DayGreeting>("day");
builder.Services.AddKeyedSingleton<IGreeting, EveningGreeting>("evening");
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

app.Run();

public class HomeController: ControllerBase
{

    readonly IServiceProvider _keyProvider;

    public HomeController(IServiceProvider keyProvider)
    {
        _keyProvider = keyProvider;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        string GetServiceKey()
        {
            var currentTime = DateTime.Now;
            if (currentTime.Hour < 12)
                return "morning";
            else if (currentTime.Hour < 18)
                return "day";
            else
                return "evening";
        }

        var key = GetServiceKey();
        var greeting = _keyProvider.GetRequiredKeyedService<IGreeting>(key);

        return Content($$"""
        <html>
        <body>
            {{ greeting.Message }}
        </body>
        </html>
        """, "text/html");
    }
}


interface IGreeting
{
    string Message { get; }
}

public class MorningGreeting : IGreeting
{
    public string Message => "Good morning";
}

public class DayGreeting : IGreeting
{
    public string Message => "Good day";
}

public class EveningGreeting : IGreeting
{
    public string Message => "Good evening";
}

