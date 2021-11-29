var builder = WebApplication.CreateBuilder();

builder.Services.AddTransient<TellTime>();
builder.Services.AddTransient<Lazy<TellTime>>(x => new Lazy<TellTime>(x.GetRequiredService<TellTime>()));

var app = builder.Build();

app.Run(context =>
{
    var tell = context.RequestServices.GetService<Lazy<TellTime>>();
    return context.Response.WriteAsync($"{tell.Value.Time}");
});

app.Run();

public class TellTime
{
    public DateTime Time => DateTime.Now;
}