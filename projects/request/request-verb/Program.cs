var app = WebApplication.Create();
app.Run(context =>
{
    return context.Response.WriteAsync($"Request {context.Request.Method}");
});

app.Run();