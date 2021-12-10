var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<Greeting>();
builder.Services.AddSingleton<Goodbye>();

var app = builder.Build();
app.UseMiddleware(typeof(TerminalMiddleware));
app.Run();

public class Greeting
{
    public string Greet() => "Good morning";
}

public class Goodbye
{
    public string Say() => "Goodbye";
}

public class TerminalMiddleware
{
    Greeting _greet;

    public TerminalMiddleware(RequestDelegate next, Greeting greet)
    {
        _greet = greet;
    }

    public async Task Invoke(HttpContext context, Goodbye goodbye)
    {
        await context.Response.WriteAsync($"{_greet.Greet()} {goodbye.Say()}");
    }
}
