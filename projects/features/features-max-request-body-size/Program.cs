using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder();
builder.WebHost.ConfigureKestrel(k =>
{
    k.Limits.MaxRequestBodySize = 5000;
});

var app = builder.Build();

app.Run(context =>
{
    var bodySize = context.Features.Get<IHttpMaxRequestBodySizeFeature>();
    bodySize.MaxRequestBodySize = 555;
    var str = "<html><body>";
    str += $"Max Request Body Size {bodySize.MaxRequestBodySize}(Is Read Only: {bodySize.IsReadOnly}) You can <strong>also</strong> set this value at KestrelServerOptions.Limits.MaxRequestBodySize";
    str += "</body></html>";
    context.Response.Headers.Add("Content-Type", "text/html");
    return context.Response.WriteAsync($"{str}");
});

app.Run();
