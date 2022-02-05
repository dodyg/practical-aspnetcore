var app = WebApplication.Create();

app.Run(async context =>
{
    var dicts = new Dictionary<string, string>()
    {
        ["id"] = "10",
        ["name"] = "dody gunawinata",
        ["date"] = "2020/05/30",
        ["date2"] = "2020-05-30",
        ["guid"] = System.Guid.NewGuid().ToString(),
        ["artist"] = "Simon & Garfunkel",
        ["formula"] = "10 = 10 * 1"
    };

    var queryString = QueryString.Create(dicts);
    
    context.Response.Headers.Add("Content-Type", "text/html");
    await context.Response.WriteAsync($@"<html>
    <head>
        <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/bulma/0.7.5/css/bulma.css"" />
    </head>
    <body class=""content"">
    <div class=""container"">
    <h1>Using QueryString.Create to get URL encoded query string</h1>
    <strong>Input</strong>
    ");
    await context.Response.WriteAsync("<ul>");
    foreach(var k in dicts)
    {
        await context.Response.WriteAsync($"<li>{k.Key} = {k.Value}</li>");
    }
    await context.Response.WriteAsync("</ul>");
    await context.Response.WriteAsync("<strong>Output</strong><br/>");
    await context.Response.WriteAsync(queryString.Value);
    await context.Response.WriteAsync("</ul>");
    await context.Response.WriteAsync(@"</div></body></html>");
});

app.Run();