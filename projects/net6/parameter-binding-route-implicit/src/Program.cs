using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

WebApplication app = WebApplication.Create();

app.MapGet("/", () => Results.Text(@"
<html>
<body>
    <a href=""/1/anne"">Click here</a>
</body>
</html>
", "text/html"));

app.MapGet("/{number:int}/{name:alpha}", (int number, string name) => Results.Text(@$"
<html>
<body>
    Hello {name}. You are number {number}.
</body>
</html>
", "text/html"));

await app.RunAsync();