using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();

var app = builder.Build();

app.UseAuthorization();

app.MapGet("/", (HttpRequest request) => Results.Text($$"""
<html>
<body>
<h1>Authentication Scheme {{CookieAuthenticationDefaults.AuthenticationScheme}}</h1>
<a href="/secret">/secret</a> requires authentication.
<br/><br/>
<form action="/login" method="post">
    <button type="submit">Authenticate using cookies</button>
</form>
<form action="/logout" method="post">
    <button type="submit">Logout</button>
</form>
</body>
</html>
""", "text/html"));

app.MapGet("/secret", (ClaimsPrincipal user) => $"Hello {user.Identity?.Name}. This is a secret!")
    .RequireAuthorization();

app.MapPost("/login", async (HttpContext context) => 
{
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, "Anne")
    };

    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var authProperties = new AuthenticationProperties(); // read more https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0
    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    return Results.Redirect("/");
});

app.MapPost("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/");
});

app.Run();