using Xunit;
using Microsoft.AspNetCore.Http.HttpResults;

var app = WebApplication.Create();

app.MapGet("/", Hello.GetGreeting);
app.Run();

public record Person(string Name);

public static class Hello
{
    public static IResult GetGreeting(string name) => TypedResults.Json(new Person(name));
}

public static class Tests
{
    [Fact]
    public static void MyTest()
    {
        var result = Hello.GetGreeting("anne");
        Assert.IsType<JsonHttpResult<Person>>(result);
        
        var result2 = (JsonHttpResult<Person>) result;
        Assert.Equal(result2.Value.Name, "anne");
    }
}