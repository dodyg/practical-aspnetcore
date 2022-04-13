using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddMvc().AddNewtonsoftJson(options =>
        {
            options.UseMemberCasing();
        });

var app = builder.Build();
app.MapControllers();
app.Run();

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    public IActionResult Index()
    {
        return Ok(new { Greeting = "Hello World" });
    }
}