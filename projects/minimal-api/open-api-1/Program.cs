using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
{
    Description = "A sample for demonstrating using .WithOpenApi() with Minimal API",
    Title = "WithOpenApi()",
    Version = "v1",
    Contact = new OpenApiContact()
    {
        Name = "Practical ASP.NET Core",
        Url = new Uri("https://github.com/dodyg/practical-aspnetcore")
    }
}));

var app = builder.Build();
app.UseSwagger();

app.MapGet("/", () => Results.Content("""
<html>
<head>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-0evHe/X+R7YkIZDRvuzKMRqM+OrBnVFBL6DOitfPri4tjfHxaWutUpFmBp4vmVor" crossorigin="anonymous">
</head>
<body>
    <div class="container">
        <ul>
            <li><a href="/swagger/index.html">Swagger Doc</a></li>
        </ul>
    </div>
</body>
</html>
""", "text/html"));

app.MapGet("/greeting", Hello.GetGreeting).WithOpenApi(op =>
{
    op.OperationId = "GetGreetings";
    op.Summary = "Return greeting given name";
    return op;
});

app.UseSwaggerUI();
app.Run();

public record Person(string Name);
public static class Hello
{
    public static IResult GetGreeting(string name) => Results.Ok(new Person(name));
}
