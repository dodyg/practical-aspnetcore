using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapControllers();

app.Run();

[ApiController]
public class HomeController : Controller
{
    [HttpGet("")]
    public ActionResult<string> Index()
    {
        try
        {
            throw new ApplicationException("Catch this one");
        }
        catch (Exception ex)
        {
            return new ObjectResult(
                new ProblemDetails
                {
                    Title = "Use Microsoft.AspNetCore.Mvc.ProblemDetails to describe error in your web APIs",
                    Detail = "It is implemeting this RFC https://tools.ietf.org/html/rfc7807 and can be easily extended.",
                    Status = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError
                });
        }
    }
}
