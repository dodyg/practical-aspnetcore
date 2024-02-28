
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers();
builder.Services.AddTransient<Greetings>();

var app = builder.Build();
app.MapControllers();
app.Run();


public class Greetings 
{
    public string SayHello() => "Hello World";
}

public class HomeController: ControllerBase
{
    [HttpGet("/")]
    public ActionResult Index(Greetings greeting)
    {
        var page = $@"<html>
        <body>
            {greeting.SayHello()}
        </body>
        </html>";

        return Content(page, "text/html");
    }
}