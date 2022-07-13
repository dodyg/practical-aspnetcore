var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.MapGet("/", () => Results.Content("""
<html>
<head>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-0evHe/X+R7YkIZDRvuzKMRqM+OrBnVFBL6DOitfPri4tjfHxaWutUpFmBp4vmVor" crossorigin="anonymous">
</head>
<body>
    <div class="container">
        <ul>
            <li><a href="/about">/about</a></li>
            <li><a href="/about/us">/about/us</a></li>
            <li><a href="/about/all">/about/all</a></li>
        </ul>
    </div>
</body>
</html>
""", "text/html"));


app.MapGroup("/about").MapAboutApi();

app.Run();

public static class AboutApi
{
    public static RouteGroupBuilder MapAboutApi(this RouteGroupBuilder group)
    {
        group.MapGet("/", () => Results.Ok("about"));
        group.MapGet("/us", () => Results.Ok("Us"));
        group.MapGet("/all", () => Results.Ok("All"));

        return group;
    }
}

