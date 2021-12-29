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

    context.Response.ContentType = "text/plain";
    await context.Response.WriteAsync($"Generated Url: {url}");
});

app.MapDefaultControllerRoute();

app.Run();


public class HelloController
{
    public ActionResult World()
    {
        return null;
    }
}

