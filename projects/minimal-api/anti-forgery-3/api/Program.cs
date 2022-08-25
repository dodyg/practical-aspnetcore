using Microsoft.AspNetCore.Antiforgery;

var builder = WebApplication.CreateBuilder();
builder.Services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
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
            </body>
        </html>
    ";

    return Results.Content(html, "text/html");
});

app.MapGet("/test-cors", () => Results.Ok(new { Message = "cors works" }));

app.MapGet("/antiforgery", (IAntiforgery antiforgery, HttpContext context) =>
{
    var tokens = antiforgery.GetAndStoreTokens(context);
    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken!, new CookieOptions { HttpOnly = false });
});

app.MapPost("/validate", async (HttpContext context, IAntiforgery antiforgery) =>
{
    try
    {
        await antiforgery.ValidateRequestAsync(context);
        return Results.Ok(new { Name = "hi Anne"});
    }
    catch (Exception ex)
    {
        return Results.Problem(ex?.Message ?? string.Empty);
    }
});

app.Run();

public class AntiforgeryMiddleware : IMiddleware
{
    private readonly IAntiforgery _antiforgery;

    public AntiforgeryMiddleware(IAntiforgery antiforgery)
    {
        _antiforgery = antiforgery;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var isGetRequest = string.Equals("GET", context.Request.Method, StringComparison.OrdinalIgnoreCase);
        if (!isGetRequest)
            await _antiforgery.ValidateRequestAsync(context);

        await next(context);
    }
}