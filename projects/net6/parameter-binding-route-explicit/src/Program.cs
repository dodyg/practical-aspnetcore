using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

WebApplication app = WebApplication.Create();

app.MapGet("/", () => Results.Text(@"
<html>
<body>
    <a href=""/1/anne"">Click here</a>
</body>
</html>
", "text/html"));

app.MapGet("/{number:int}/{nm:alpha}", ([FromRoute]int number, [FromRoute(Name = "nm")]string name) => Results.Text(@$"
<html>
<body>
    Hello {name}. You are number {number}.
</body>
</html>
", "text/html"));

await app.RunAsync();