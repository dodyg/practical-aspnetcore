using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    var linkGenerator = context.RequestServices.GetService<LinkGenerator>();

    var url = linkGenerator.GetUriByAction(context,
            controller: "Hello",
            action: "World"
        );

    var url2 = linkGenerator.GetUriByAction(context,
            controller: "Hello",
            action: "Goodbye"
        );

    var url3 = linkGenerator.GetUriByAction(context,
            controller: "Hello",
            action: "CallMe"
        );

    var url4 = linkGenerator.GetUriByAction(context,
            controller: "Greet",
            action: "Index"
        );

    var url5 = linkGenerator.GetUriByAction(context,
            controller: "Wave",
            action: "Away"
        );

    var url6 = linkGenerator.GetUriByAction(context,
            controller: "XXXX",
            action: "YYYY"
        );

    context.Response.ContentType = "text/plain";
    await context.Response.WriteAsync($@"Generated Url:
{url}
{url2}
{url3}
{url4}
{url5}
{url6}(It won't produce any link if it cannot figure out controller and action information)");
});

app.MapDefaultControllerRoute();
app.Run();

[Route("[controller]")]
public class HelloController
{
    [HttpGet("")]
    public ActionResult World() => null;

    [HttpGet("Goodbye")]
    public ActionResult Goodbye() => null;

    [HttpGet("[action]")]
    public ActionResult CallMe() => null;
}

[Route("Greet")]
public class GreetController
{
    public ActionResult Index() => null;
}

public class WaveController
{
    [Route("Wave-Away")]
    public ActionResult Away() => null;
}

