var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseMiddleware<GreeterMiddleware>();
app.MapControllers();

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
            httpContext.Items.Add("GreetingFromMiddleWare", message.Content);

        await _next.Invoke(httpContext);
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class MessageAttribute : System.Attribute
{
    public string Content { get; set; }

    public MessageAttribute(string content) => Content = content;

}
