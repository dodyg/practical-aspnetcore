var app = WebApplication.Create();
app.Configuration["message"] = "hello world";
app.Run(context =>
{
    return context.Response.WriteAsync($"{app.Configuration["message"]}");
});

app.Run();
