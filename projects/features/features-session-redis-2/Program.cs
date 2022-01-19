using System.Text.Json;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["redisConnectionString"];
});

builder.Services.AddSession();

var app = builder.Build();

app.UseSession();

app.Use(async (context, next) =>
{
    var person = new Person
    {
        FirstName = "Anne",
        LastName = "M"
    };

    var session = context.Features.Get<ISessionFeature>();
    try
    {
        session.Session.SetString("Message", "Buon giorno cuore");
        session.Session.SetInt32("Year", DateTime.Now.Year);
        session.Session.SetString("Amore", JsonSerializer.Serialize(person));
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
        var amore = JsonSerializer.Deserialize<Person>(session.Session.GetString("Amore"));

        await context.Response.WriteAsync($"{amore.FirstName}, {msg} {year}");
    }
    catch (Exception ex)
    {
        await context.Response.WriteAsync($"{ex.Message}");
    }
});

app.Run();

public class Person
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
}
