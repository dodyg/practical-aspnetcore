using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Security.Claims;

WebApplication app = WebApplication.Create();

app.MapGet("/", (HttpContext context) => Results.Text(@$"
<html>
<body>
    <h1>Special types binding - HttpContext</h1>
    <p>
    This is the value from HttpContext.TraceIdentifier { context.TraceIdentifier }
    </p>
    <ul>
        <li><a href=""/http-request"">Bind HttpRequest</a></li>
        <li><a href=""/http-response"">Bind HttpResponse</a></li>
        <li><a href=""/cancellation-token"">Bind CancellationToken</a></li>
        <li><a href=""/claims-principal"">Bind ClaimsPrincipal</a></li>
    </ul>
</body>
</html>
", "text/html"));

app.MapGet("/claims-principal", (ClaimsPrincipal claims) => Results.Text(@$"
<html>
<body>
    <h1>Special types binding - ClaimsPrincipal</h1>
    <p>
        This is the value of ClaimsPrincipal.Identity.IsAuthenticated { claims.Identity.IsAuthenticated }
    </p?
</body>
</html>
", "text/html"));

app.MapGet("/cancellation-token", (CancellationToken token) => Results.Text(@$"
<html>
<body>
    <h1>Special types binding - CancellationToken</h1>
    <p>
        This is the value of CancellationToken.IsCancellationRequested { token.IsCancellationRequested }
    </p?
</body>
</html>
", "text/html"));

app.MapGet("/http-response", (HttpResponse response) => Results.Text(@$"
<html>
<body>
    <h1>Special types binding - HttpRequest</h1>
    <p>
        This is the value of HttpResponse.StatusCode { response.StatusCode }
    </p?
</body>
</html>
", "text/html"));


app.MapGet("/http-request", (HttpRequest request) => Results.Text(@$"
<html>
<body>
    <h1>Special types binding - HttpRequest</h1>
    <p>
        This is the value of HttpRequest.Path { request.Path }
    </p?
</body>
</html>
", "text/html"));

await app.RunAsync();