using Microsoft.AspNetCore.Http.Features;

var app = WebApplication.Create();

app.Use(async (context, next) =>
{
    var request = context.Features.Get<IHttpRequestFeature>();
    request.Headers.Add("greetings", "hello world");
    await next.Invoke();
});

app.Run(context =>
{
    return context.Response.WriteAsync($"hello {context.Request.Headers["greetings"]}");
});

app.Run();