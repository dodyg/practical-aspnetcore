using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder();
builder.Services.AddExceptionHandler<TimeOutHandler>();
builder.Services.AddExceptionHandler<DefaultExceptionHandler>();

var app = builder.Build();
app.UseExceptionHandler(opt => { });

app.MapGet("/", () => 
{
    return Results.Content("""
    <html>
    <body>
        <ul>
            <li><a href="other-exception">Other exception</a></li>
            <li><a href="time-out">Time out</a></li>
        </ul>
    </body
    </html>
    """, "text/html");
});

app.MapGet("/other-exception", () => 
{
    throw new Exception("Something went wrong");
});

app.MapGet("/time-out", () => 
{
    throw new TimeoutException("Out of time");
});

app.Run();

public class TimeOutHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is TimeoutException)
        {
            await httpContext.Response.WriteAsync($"""
            <html>
                <body>
                     From {nameof(TimeOutHandler)}. The exception message is {exception.Message} at {httpContext.Request.Method} {httpContext.Request.Path}.
                </body>
            </html>
            """);
            return true;
        }

        return false;
    }
}

public class DefaultExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        await httpContext.Response.WriteAsync($"""
        <html>
            <body>
                From {nameof(DefaultExceptionHandler)}. The exception message is {exception.Message} at {httpContext.Request.Method} {httpContext.Request.Path}.
            </body>
        </html>
        """);
        return true;
    }
}