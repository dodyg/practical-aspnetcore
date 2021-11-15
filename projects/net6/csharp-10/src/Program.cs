WebApplication app = WebApplication.Create();
app.UseDeveloperExceptionPage();
Demo demoSettings = new();

app.Configuration.GetRequiredSection("demo").Bind(demoSettings);

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
        <li><a href=""/assign-and-init"">assign and init</a></li>
        <li><a href=""/record-struct"">record struct</a></li>
        <li><a href=""/name/?title=sir"">ArgumentNullException.ThrowIfNull</a></li>
        <li><a href=""/name/"">ArgumentNullException.ThrowIfNull 2</a></li>
        <li><a href=""/process-path"">Environment.ProcessPath</a></li>
        <li><a href=""/periodic-timer"">Periodic Timer</a></li>
        <li><a href=""/random"">random generator</a></li>
        <li><a href=""/settings"">settings</a></li>
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
    var result = IResult () => Results.Json(new Person("Dody", "Gunawinata"));
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

app.MapGet("/assign-and-init", () =>
{
    Person GetPerson() => new Person("Dody", "Gunawinata");

    string firstName;

    (firstName, var lastName) = GetPerson();

    return firstName + " " + lastName;
});

app.MapGet("/record-struct", () =>
{
    var id = new Id(11);

    var id2 = new Id 
    {
        Value = 10
    };

    var id3 = id with { Value = 10};

    var isTheSame = id2 == id3;

    return id + " | " + id2 + " == " + id3 + " is " + isTheSame;
});

app.MapGet("/name", (string? title) =>
{
    ArgumentNullException.ThrowIfNull(title);
    return title;
});

app.MapGet("/process-path", () => Environment.ProcessPath);

async IAsyncEnumerable<DateTime> Timer ()
{
    using PeriodicTimer timer = new (TimeSpan.FromSeconds(2));

    while(await timer.WaitForNextTickAsync())
        yield return DateTime.UtcNow;
}

app.MapGet("/periodic-timer", Timer);
app.MapGet("/random", () => BitConverter.ToInt32(System.Security.Cryptography.RandomNumberGenerator.GetBytes(3000)));
app.MapGet("/settings", () => demoSettings);

const string MyName = "Dody Gunawinata";
const string Profile = $"{MyName}";

app.MapGet("/constant", () => Profile);

await app.RunAsync();