using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder();
builder.Services.AddMvc(config =>
{
    config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
});

var app = builder.Build();
app.MapDefaultControllerRoute();
app.Run();

public class Greeting
{
    public bool Hello { get; set; }
    public bool World { get; set; }
}

public class HomeController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        var h = new Greeting
        {
            Hello = true,
            World = true
        };

        Response.ContentType = "text/xml";
        return Ok(h);
    }
}
