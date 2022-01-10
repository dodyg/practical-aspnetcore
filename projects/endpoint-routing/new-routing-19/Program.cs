using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorPages();

var app = builder.Build();
app.MapControllers();
app.MapRazorPages();

app.Run();

[Route("/API/Message")]
public class APIController : ControllerBase
{
    [HttpGet("")]
    public ActionResult GetMessage()
    {
        return Ok(new { Message = "services.AddRazorPages() add supports for Razor Pages and MVC API. There is no MVC Views support." });
    }
}
