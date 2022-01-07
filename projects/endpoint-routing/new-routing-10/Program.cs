var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();
app.UseMiddleware<GreeterMiddleware>();
app.MapRazorPages();
app.Run();

public class GreeterMiddleware
{
    RequestDelegate _next;

    readonly LinkGenerator _linkGenerator;

    public GreeterMiddleware(RequestDelegate next, LinkGenerator linkGenerator)
    {
        _next = next;
        _linkGenerator = linkGenerator;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        Endpoint endPoint = httpContext.GetEndpoint();

        if (endPoint.DisplayName == "/about")
        {
            httpContext.Items.Add("GreetingFromMiddleWare", "Hello world from GreetingMiddleware");
        }

        await _next.Invoke(httpContext);
    }
}
