var builder = WebApplication.CreateBuilder();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/problems" && context.RequestServices.GetService<IProblemDetailsService>() is { } problemDetailsService)
    {
        context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
        var problemDetails = new ProblemDetailsContext { HttpContext = context };
        problemDetails.ProblemDetails.Extensions.Add("custom-property-2", Guid.NewGuid().ToString());
        await problemDetailsService.WriteAsync(problemDetails);
        return;
    }
    await next(context);
});

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

app.Run();
