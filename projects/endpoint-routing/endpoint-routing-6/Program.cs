using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    var linkGenerator = context.RequestServices.GetService<LinkGenerator>();

    var url = linkGenerator.GetPathByAction(
           controller: "Hello",
           action: "World",
           values: new
           {
               name = "Annie"
           }
       );

    var url2 = linkGenerator.GetPathByAction(
           controller: "Hello",
           action: "Goodbye",
           values: new
           {
               age = 55
           }
       );

    var url3 = linkGenerator.GetPathByAction(
           controller: "Hello",
           action: "CallMe"
       );

    var url4 = linkGenerator.GetPathByAction(
           controller: "Greet",
           action: "Index",
           values: new
           {
               isNice = true
           }
           );

    var url5 = linkGenerator.GetPathByAction(
           controller: "Wave",
           action: "Away",
           values: new
           {
               danger = "see",
               ahead = "soon"
           }
       );

    context.Response.ContentType = "text/plain";
    await context.Response.WriteAsync($@"Generated Url:
{url}
{url2}
{url3}
{url4}
{url5}
");
});

app.MapDefaultControllerRoute();
app.Run();

[Route("[controller]")]
public class HelloController
{
    [HttpGet("{name}")]
    public ActionResult World(string name) => null;

    [HttpGet("Goodbye/{age:int}")]
    public ActionResult Goodbye(int age) => null;

    [HttpGet("[action]/{byYourName?}")]
    public ActionResult CallMe(string byYourName) => null;
}

[Route("Greet/{isNice:bool}")]
public class GreetController
{
    public ActionResult Index() => null;
}

public class WaveController
{
    [Route("Wave-Away/{danger:required}/{ahead:required}")]
    public ActionResult Away(string danger, string ahead) => null;
}
