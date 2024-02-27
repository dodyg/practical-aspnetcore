var builder = WebApplication.CreateBuilder();

builder.Services.AddKeyedSingleton<IGreeting, MorningGreeting>("morning");
builder.Services.AddKeyedSingleton<IGreeting, DayGreeting>("day");
builder.Services.AddKeyedSingleton<IGreeting, EveningGreeting>("evening");


var app = builder.Build();

app.MapGet("/", (IServiceProvider provider) => 
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
    var greeting = provider.GetRequiredKeyedService<IGreeting>(key);

    return Results.Content($$"""
    <html>
    <body>
        {{ greeting.Message }}
    </body>
    </html>
    """, "text/html");
});

app.Run();


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

