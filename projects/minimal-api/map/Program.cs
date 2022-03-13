IResult Index() => Results.Json(new Greeting("Hello World"));
IResult About() => Results.Json(new { about = "me" });

var app = WebApplication.Create();
app.Map("/", Index);
app.Map("/about", About);

app.Run();

public record Greeting(string Message);

