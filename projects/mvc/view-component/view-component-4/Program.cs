using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapDefaultControllerRoute();
app.Run();

public class RepeatMessage
{
    public string Content { get; set; }

    public int Repeat { get; set; }
}

public class HomeController : Controller
{
    public ActionResult Index() => View();
}

