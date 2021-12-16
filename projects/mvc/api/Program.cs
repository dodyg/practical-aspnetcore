using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();

[Route("api")]
public class ToDoItemsController : Controller
{
    [HttpGet("[Action]")]
    public IActionResult AnEndpoint()
    {
        return new ObjectResult("This string is returned with this controller endpoint");
    }
}

