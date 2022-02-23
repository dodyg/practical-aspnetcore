using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapDefaultControllerRoute();
app.Run();

public class GreetingParams
{
    public int UserId { get; set; }

    public string Name { get; set; }

    public bool IsAmazing { get; set; }

    public short? Age { get; set; }

    public override string ToString() => $"User Id: {UserId}, Name: {Name}, Is Amazing: {IsAmazing}, Age: {Age}";
}

public class HomeController : Controller
{
    public ActionResult Index([FromQuery] GreetingParams greet)
    {
        return new ContentResult
        {
            Content = $@"<html><body>
                <h1>Class binding with [FromQuery]</h1>
                <p>You can see the difference in behavior between the nullable type and non nullable types here. Age is short? and User Id is int.<p>
                {greet}
                <ul>
                    <li><a href=""/"">/</a></li>
                    <li><a href=""/?name=annie"">/?name=annie</a></li>
                    <li><a href=""/?isamazing=true"">/?isamazing=true</a></li>
                    <li><a href=""/?userid=1"">/?userid=1</a></li>
                    <li><a href=""/?age=33"">/?age=33</a></li>
                    <li><a href=""/?userid=1&name=annie&isamazing=true"">/?userid=1&name=annie&isamazing=true&age=33</a></li>
                </ul>
                </body></html>",
            ContentType = "text/html"
        };
    }
}
