using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services
    .AddAuthentication()
    .AddCookie()
    .AddJwtBearer(options => {
    options.IncludeErrorDetails = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "practical aspnetcore",
        ValidAudience = "https://localhost:5001/",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is custom key for practical aspnetcore sample"))
    };});;

var app = builder.Build();
app.UseAuthorization();
app.MapGet("/", (HttpRequest request) => Results.Text($$"""
<!doctype html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Multiple Authentication schemes</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-gH2yIJqKdNHPEq0n4Mqa/HGKIhSkIHeL5AyhkYV8i59U5AR6csBvApHHNl/vI1Bx" crossorigin="anonymous">
  </head>
<body>
<h1>Multiple authentication schemes</h1>

<ul>
    <li><a href="/secret">/secret</a> requires authentication.</li>
    <li><a href="/secret-jwt-only">/secret-jwt-only</a> requires JWT authentication.</li>
    <li><a href="/secret-cookies-only">/secret-cookies-only</a> requires Cookies authentication.</li>
</ul>

<div class="row">
    <div class="col-md-4">

        <div class="card">
            <div class="card-body">
                <form action="/login" method="post">
                    <button type="submit">Authenticate using cookies</button>
                </form>
                <form action="/logout" method="post">
                    <button type="submit">Logout</button>
                </form>
            </div>
        </div>

    </div>
    <div class="col-md-4">
        <div class="card">
            <div class="card-body">
                JWT:<br/>
                <div id="jwt_content"></div>
                <br/><br/>
                Response from <a href="/secret">/secret</a>
                <div id="message"></div>
                <br/><br/>

                <button id="jwt">Get Secret</button>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="card">
            <div class="card-body">
                JWT:<br/>
                <div id="jwt_content_2"></div>
                <br/><br/>
                Response from <a href="/secret-jwt-only">/secret-jwt-only</a>
                <div id="message_2"></div>
                <br/><br/>

                <button id="jwt_2">Get Secret</button>
            </div>
        </div>
    </div>
</div>

<script>
 let btn = document.getElementById('jwt');
 btn.addEventListener('click', async function() {
    const url = window.location.protocol + '//' + window.location.host  + "/jwt";
    const response = await fetch(url,  {
        headers: { 'Accept': 'application/json' }
    });

    const json = await response.json();
    document.getElementById('jwt_content').textContent = json.token;

    const url2 = window.location.protocol + '//' + window.location.host  + "/secret";
    const response2 = await fetch(url2,  {
        headers: { 'Accept': 'text/plain', 'Authorization': 'Bearer ' + json.token, }
    });

    const text =  await response2.text();

    document.getElementById('message').textContent = text;
 });
</script>


<script>
 let btn2 = document.getElementById('jwt_2');
 btn2.addEventListener('click', async function() {
    const url = window.location.protocol + '//' + window.location.host  + "/jwt";
    const response = await fetch(url,  {
        headers: { 'Accept': 'application/json' }
    });

    const json = await response.json();
    document.getElementById('jwt_content_2').textContent = json.token;

    const url2 = window.location.protocol + '//' + window.location.host  + "/secret-jwt-only";
    const response2 = await fetch(url2,  {
        headers: { 'Accept': 'text/plain', 'Authorization': 'Bearer ' + json.token, }
    });

    const text =  await response2.text();

    document.getElementById('message_2').textContent = text;
 });
</script>

</body>
</html>
""", "text/html"));

app.MapGet("/secret", (ClaimsPrincipal user) => $"Hello {user.Identity?.Name}. This is a secret!")
    .RequireAuthorization(options =>
    {
        options.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        options.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
        options.RequireAuthenticatedUser();
    });

app.MapGet("/secret-jwt-only", (ClaimsPrincipal user) => $"Hello {user.Identity?.Name}. This is a secret accessible via jwt only")
    .RequireAuthorization(options =>
    {
        options.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        options.RequireAuthenticatedUser();
    });

app.MapGet("/secret-cookies-only", (ClaimsPrincipal user) => $"Hello {user.Identity?.Name}. This is a secret accessible via cookie based authentication only only")
    .RequireAuthorization(options =>
    {
        options.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
        options.RequireAuthenticatedUser();
    });

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


app.MapGet("/jwt", () => Results.Json(new { token = GenerateJSONWebToken()}));

string GenerateJSONWebToken()    
{    
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is custom key for practical aspnetcore sample"));    
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    

    var token = new JwtSecurityToken(issuer:"practical aspnetcore",    
        audience:"https://localhost:5001/",    
        claims: new List<Claim> 
        { 
            new Claim(ClaimTypes.Name, "Anne"), 
        },
        notBefore: null,    
        expires: DateTime.Now.AddMinutes(120),    
        signingCredentials: credentials);    

    return new JwtSecurityTokenHandler().WriteToken(token);    
}    

app.Run();