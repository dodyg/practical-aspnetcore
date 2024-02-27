var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.MapGet("/", (HttpContext context) => Results.Content($$"""
<html>
    <head>
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-0evHe/X+R7YkIZDRvuzKMRqM+OrBnVFBL6DOitfPri4tjfHxaWutUpFmBp4vmVor" crossorigin="anonymous">
    </head>
    <body>
        <div class="container">
            <ul>
                <li><a href="/about/dody">/about/dody</a></li>
                <li><a href="/about/anne">/about/anne</a></li>
            </ul>
        </div>
    </body>
</html>
""", "text/html"));


app.MapGet("/about/{name}", (string name) =>
{
    if (name.Equals("anne"))
        return Results.Ok(name);
    else
        return Results.BadRequest();
}).AddEndpointFilter(async (context, next) =>
{
    var result = await next(context); 

    return result switch
    {
        IStatusCodeHttpResult a when a.StatusCode == StatusCodes.Status400BadRequest => Results.Ok("Bad Request"),
        _ => result
    };
});

app.Run();