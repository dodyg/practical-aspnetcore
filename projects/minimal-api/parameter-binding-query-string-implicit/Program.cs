using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

WebApplication app = WebApplication.Create();

app.MapGet("/", () => Results.Text(@"
<html>
<body>
    <a href=""/greet?number=1&name=anne"">Click here</a>
</body>
</html>
", "text/html"));

app.MapGet("/greet", (int number, string name) => Results.Text(@$"
<html>
<body>
    Hello {name}. You are number {number}.
</body>
</html>
", "text/html"));

await app.RunAsync();