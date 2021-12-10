var builder = WebApplication.CreateBuilder();
builder.Services.AddTransient<TerminalMiddleware>();

var app = builder.Build();
app.UseMiddleware(typeof(TerminalMiddleware));
app.Run();

public class TerminalMiddleware : IMiddleware
{
    ILogger<TerminalMiddleware> _log;

    DateTime _date = DateTime.Now;

    public TerminalMiddleware(ILogger<TerminalMiddleware> log)
    {
        _log = log;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _log.LogDebug($"Request: {context.Request.Path}");
        context.Response.Headers.Add("Content-Type", "text/plain");
        await context.Response.WriteAsync($"This Middleware is transient. Keep refreshing your page. The date will keep changing: {_date}.");
    }
}
