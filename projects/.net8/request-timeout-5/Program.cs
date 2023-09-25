using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers();
builder.Services.AddRequestTimeouts(options =>
{
    options.AddPolicy("quick",new Microsoft.AspNetCore.Http.Timeouts.RequestTimeoutPolicy
    {
        Timeout = TimeSpan.FromMilliseconds(1),
        TimeoutStatusCode = 200,
        WriteTimeoutResponse = async (HttpContext context) => 
        {
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("timeout is triggered");
        }
    });
});

var app = builder.Build();
app.UseRequestTimeouts();
app.MapControllers();

app.Run();

public class HomeController : ControllerBase
{
    [HttpGet("/")]
    [RequestTimeout("quick")]
    public async Task<IActionResult> Index()
    {
        await Task.Delay(100);
        HttpContext.RequestAborted.ThrowIfCancellationRequested();
        return Ok("Hello World!");
    }
}
