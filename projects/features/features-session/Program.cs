using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();
app.UseSession();

app.Use(async (context, next) =>
{
    var session = context.Features.Get<ISessionFeature>();
    try
    {
        session.Session.SetString("Message", "Hello world");
        session.Session.SetInt32("Year", DateTime.Now.Year);
    }
    catch (Exception ex)
    {
        await context.Response.WriteAsync($"{ex.Message}");
    }
    await next.Invoke();
});

app.Run(async context =>
{
    var session = context.Features.Get<ISessionFeature>();

    try
    {
        string msg = session.Session.GetString("Message");
        int? year = session.Session.GetInt32("Year");
        await context.Response.WriteAsync($"{msg} {year}");
    }
    catch (Exception ex)
    {
        await context.Response.WriteAsync($"{ex.Message}");
    }
});

app.Run();