var builder = WebApplication.CreateBuilder();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "localhostOnly",
                      policy  =>
                      {
                          policy.WithOrigins(builder.Configuration["origin"])
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                      });
});

WebApplication app = builder.Build();
app.Urls.Add("https://localhost:5001");
app.UseCors("localhostOnly");

app.MapGet("/", (HttpContext context) =>
{
    var html = $@"<!DOCTYPE html>
        <html>
            <body>
                System is UP            
                <a href=""/message-cookie"">Get a message cookie</a>  
            </body>
        </html>
    ";

    return Results.Content(html, "text/html");
});

app.MapGet("/test-cors", () => Results.Ok(new { Message = "cors works" }));

app.MapGet("/message-cookie", (HttpContext context) =>
{
    context.Response.Cookies.Append("message", "hello world", new CookieOptions { HttpOnly = false });
});


app.Run();
