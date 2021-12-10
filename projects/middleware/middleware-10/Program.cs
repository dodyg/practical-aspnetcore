var app = WebApplication.Create();
app.UseMiddleware(typeof(TerminalMiddleware));
app.Run();

public class TerminalMiddleware
{
    DateTime _date = DateTime.Now;

    public TerminalMiddleware(RequestDelegate next)
    {
    }

    public async Task Invoke(HttpContext context, ILogger<TerminalMiddleware> log)
    {
        log.LogDebug($"Request: {context.Request.Path}");
        context.Response.Headers.Add("Content-Type", "text/plain");
        await context.Response.WriteAsync($"Middleware is singleton. Keep refreshing the page. You will see that the date does not change {_date}.");
    }
}
