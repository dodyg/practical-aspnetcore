var builder = WebApplication.CreateBuilder();
builder.Services.AddProblemDetails();
var app = builder.Build();

app.UseExceptionHandler();

app.MapGet("/", (HttpContext context) => Results.Content($$"""
<html>
<head>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-0evHe/X+R7YkIZDRvuzKMRqM+OrBnVFBL6DOitfPri4tjfHxaWutUpFmBp4vmVor" crossorigin="anonymous">
</head>
<body>
    <div class="container">
        <a href="/problems">Show problems</a>
    </div>
</body>
</html>
""", "text/html"));

app.MapGet("/problems", async (HttpContext context) =>
{
    throw new ApplicationException("We got problems");
});

app.Run();
