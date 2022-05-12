var app = WebApplication.Create();

app.MapGet("/", Hello.GetGreeting);
app.Run();

public record Person(string Name);

public static class Hello
{
    public static IResult GetGreeting(string name) => TypedResults.Json(new Person(name));
}
