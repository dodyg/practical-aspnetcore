using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = new PathString("/login");
        });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync(@"<html><body>
                    This is home page<br/>
                    <a href=""protected"">Click here</a> trying to access protected area
                    </body></html>");
})
.AllowAnonymous();

app.MapGet("/login", async context =>
{
    await context.Response.WriteAsync("You must login");
});

app.MapGet("/protected", async context =>
{
    await context.Response.WriteAsync("Valuable area");
}).RequireAuthorization();

app.Run();