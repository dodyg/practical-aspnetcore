var app = WebApplication.Create();

string Plaintext(HttpRequest request)
{
    var name = request.Query["name"];
    if (!string.IsNullOrWhiteSpace(name))
        return "hello " + name;
    else
        return "hello";
}

app.MapGet("/hello", Plaintext);

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync(
$@"
    <html>
        <body>
            <ul>
                <li><a href=""/hello?name=anne"">Greet with name</a></li>
                <li><a href=""/hello"">Just greet</a></li>
            </ul>
        </body>
    </html>
");
});

app.Run();
