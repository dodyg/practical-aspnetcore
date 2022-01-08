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

        var message = endPoint.Metadata.GetMetadata<MessageAttribute>();

        if (message != null)
        {
            if (httpContext.Request.Method == HttpMethods.Get)
                httpContext.Items.Add("GreetingFromMiddleWare", message.ContentGet);
            else if (httpContext.Request.Method == HttpMethods.Post)
                httpContext.Items.Add("GreetingFromMiddleWare", message.ContentPost);
        }

        await _next.Invoke(httpContext);
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class MessageAttribute : System.Attribute
{
    public string ContentGet { get; set; }

    public string ContentPost { get; set; }
}
