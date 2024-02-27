using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers();
builder.Services.AddRequestTimeouts();

var app = builder.Build();
app.UseRequestTimeouts();
app.MapControllers();

app.Run();

public class HomeController : ControllerBase
{
    [HttpGet("/")]
    [RequestTimeout(milliseconds: 1)]
    public async Task<IActionResult> Index()
    {
        await Task.Delay(100);
        HttpContext.RequestAborted.ThrowIfCancellationRequested();
        return Ok("Hello World!");
    }
}
