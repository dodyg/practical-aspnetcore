using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddMvc();

var app = builder.Build();

app.MapGet("hello", async context => await context.Response.WriteAsync("hello"));
app.MapPost("new-hello", async context => await context.Response.WriteAsync("hello"));
app.MapDelete("hello", async context => await context.Response.WriteAsync("hello"));
app.MapPut("hello", async context => await context.Response.WriteAsync("hello"));
app.MapControllers();
app.MapRazorPages();

app.Map("", async context =>
{
    var route = app as IEndpointRouteBuilder;

    foreach (EndpointDataSource x in route.DataSources)
    {
        await context.Response.WriteAsync($"{x}\n");
        foreach (RouteEndpoint e in x.Endpoints)
        {
            await context.Response.WriteAsync($"Display Name: {e.DisplayName}\n");
            await context.Response.WriteAsync($"Route Pattern: {e.RoutePattern.RawText}\n");
            await context.Response.WriteAsync($"Metadata Count: {e.Metadata.Count}\n");
            foreach (var mm in e.Metadata)
            {
                await context.Response.WriteAsync($"Metadata: {mm}\n");
            }
            await context.Response.WriteAsync("\n");
        }
        await context.Response.WriteAsync("\n\n");
    }
});

app.Run();

[Route("MVC")]
public class HomeController : Controller
{
    [HttpGet("Greeting")]
    public IActionResult Index() => Content("Oi");

    [HttpPost("Greeting")]
    public IActionResult Greeting() => Content("Oi");
}
