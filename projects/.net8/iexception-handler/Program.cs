using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder();
builder.Services.AddExceptionHandler<DefaultExceptionHandler>();

var app = builder.Build();
app.UseExceptionHandler(opt => { });

app.MapGet("/", () => 
{
    throw new Exception("Something went wrong");
});

app.Run();

public class DefaultExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        await httpContext.Response.WriteAsync($"""
        <html>
            <body>
                Opps sorry. Exception message is {exception.Message} at {httpContext.Request.Method} {httpContext.Request.Path}.
            </body>
        </html>
        """);
        return true;
    }
}