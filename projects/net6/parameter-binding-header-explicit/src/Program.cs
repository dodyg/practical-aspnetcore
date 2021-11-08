using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

WebApplication app = WebApplication.Create();

app.MapGet("/", ([FromHeader(Name = "Accept-Language")] string lang, [FromHeaderAttribute(Name = "User-Agent")] string userAgent) => Results.Text(@$"
<html>
<body>
    Header `Accept-Language` = { lang }<br/>
    Header `User-Agent` = { userAgent }
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

app.Run();