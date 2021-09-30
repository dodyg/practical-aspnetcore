using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var app = WebApplication.CreateBuilder(new WebApplicationOptions { EnvironmentName = Environments.Development }).Build();

app.UseStatusCodePages();

app.MapGet("/", () =>
{
    return Results.Text(@"
    <html>
        <head>
            <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css"" integrity=""sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU"" crossorigin=""anonymous"">
        </head>
        <body>
            <h1>Sample for {int}, {min()}, {max()}, and {range(min, max))} route constraints</h1>
            <ul>
                <li>/{id:int} <a href=""/10"">/10</a></li>
                <li>/{id:int} <a href=""/10f"">/10f</a> (404)</li>
                <li>/{id:int} <a href=""/10_000"">/10_000</a> (404)</li>
                <li>/{id:int} <a href=""/10.4"">/10.4</a> (404)</li>
                <li>/min/{minId:min(1)} <a href=""/min/100"">/min/100</a></li>
                <li>/min/{minId:min(1)} <a href=""/min/0"">/min/0</a> (404)</li>
                <li>/max/{maxId:max(10)} <a href=""/max/10"">/max/10</a></li>
                <li>/max/{maxId:max(10)} <a href=""/max/11"">/max/11</a> (404)</li>
                <li>/range/{rangeId:range(1, 10)} <a href=""/range/1"">/range/1</a></li>
                <li>/range/{rangeId:range(1, 10)} <a href=""/range/10"">/range/10</a></li>
                <li>/range/{rangeId:range(1, 10)} <a href=""/range/0"">/range/0</a> (404)</li>
                <li>/range/{rangeId:range(1, 10)} <a href=""/range/11"">/range/11</a> (404)</li>
            </ul>
        </body>
    </html>
    ", "text/html");
});

app.MapGet("/{id:int}", (int id) => id.ToString());
app.MapGet("/min/{minId:min(1)}", (int minId) => minId.ToString());
app.MapGet("/max/{maxId:max(10)}", (int maxId) => maxId.ToString());
app.MapGet("/range/{rangeId:range(1, 10)}", (int rangeId) => rangeId.ToString());

app.Run();