WebApplication app = WebApplication.Create();

app.MapGet("/", () => Results.Text(@"
<html>

<body>
    <h1>What's new in C# 10</h1>
    <ul>
        <li><a href=""/person"">File namespace and global using</a></li>
        <li><a href=""/anonymous"">Anonymous With</a></li>
        <li><a href=""/natural-lambda"">Natural Lambda</a></li>
        <li><a href=""/first-or-default"">FirstOrDefault</a></li>
        <li><a href=""/constant"">const string interpolation</a></li>
    </ul>
</body>
</html>
", "text/html"));


app.MapGet("/person", () => Results.Json(new Person("Dody", "Gunawinata")));
app.MapGet("/anonymous", () => 
{
    var person = new { Name = "Dody", Nationality = "Indonesia " };

    var person2 = person with { Nationality = "Italy"};

    return Results.Json(person2);
});

app.MapGet("/natural-lambda", () => 
{
    var result = () => Results.Json(new Person("Dody", "Gunawinata"));
    Func<IResult> result2 = result;
    return result2();
});

app.MapGet("/first-or-default", () => 
{
    var list = new List<int>()
    {
        1,2,3,4,5
    };

    var find = list.Where(x => x > 10).FirstOrDefault(-10);

    return find;
});

const string MyName = "Dody Gunawinata";
const string Profile = $"{MyName}";

app.MapGet("/constant", () => Profile);

await app.RunAsync();