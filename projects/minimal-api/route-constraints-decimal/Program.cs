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
            <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css"" decimalegrity=""sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU"" crossorigin=""anonymous"">
        </head>
        <body>
            <h1>Sample for {decimal}, {float}, and {double} route constraints</h1>
            <ul>
                <li>/{id:decimal} <a href=""/decimal/10"">/decimal/10</a></li>
                <li>/{id:decimal} <a href=""/decimal/-20"">/decimal/-20</a></li>
                <li>/{id:decimal} <a href=""/decimal/10f"">/decimal/10f</a> (404)</li>
                <li>/{id:decimal} <a href=""/decimal/10_000"">/decimal/10_000</a> (404)</li>
                <li>/{id:decimal} <a href=""/decimal/10.4"">/decimal/10.4</a> (404)</li>
            </ul>
            <ul>
                <li>/{id:float} <a href=""/float/10"">/float/10</a></li>
                <li>/{id:float} <a href=""/float/-20"">/float/-20</a></li>
                <li>/{id:float} <a href=""/float/10f"">/float/10f</a> (404)</li>
                <li>/{id:float} <a href=""/float/10_000"">/float/10_000</a> (404)</li>
                <li>/{id:float} <a href=""/float/10.4"">/float/10.4</a> (404)</li>
            </ul>
            <ul>
                <li>/{id:double} <a href=""/double/10"">/double/10</a></li>
                <li>/{id:double} <a href=""/double/-20"">/double/-20</a></li>
                <li>/{id:double} <a href=""/double/10f"">/double/10f</a> (404)</li>
                <li>/{id:double} <a href=""/double/10_000"">/double/10_000</a> (404)</li>
                <li>/{id:double} <a href=""/double/10.4"">/double/10.4</a> (404)</li>
            </ul>
        </body>
    </html>
    ", "text/html");
});

app.MapGet("decimal/{id:decimal}", (decimal id) => id.ToString());
app.MapGet("float/{yid:float}", (float id) => id.ToString());
app.MapGet("double/{id:double}", (double id) => id.ToString());

app.Run();