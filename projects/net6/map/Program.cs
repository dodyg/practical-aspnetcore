using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

IResult Index() => Results.Json(new Greeting("Hello World"));
IResult About() => Results.Json(new { about = "me" });

var app = WebApplication.Create();
app.Map("/", Index);
app.Map("/about", About);

app.Run();

public record Greeting(string Message);

