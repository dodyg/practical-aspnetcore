using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var app = WebApplication.Create();

IResult Plaintext(HttpRequest request)
{
    var name = request.Query["name"];
    if (!string.IsNullOrWhiteSpace(name))
        return Results.Text("hello " + name, "text/html");
    else
        return Results.Text("hello", "text/html");
}

app.MapGet("/hello", Plaintext);

app.MapGet("/", () =>
{
    return Results.Text(
$@"<html>
        <body>
            <ul>
                <li><a href=""/hello?name=anne"">Greet with name</a></li>
                <li><a href=""/hello"">Just greet</a></li>
            </ul>
        </body>
    </html>
", "text/html");
});

app.Run();
