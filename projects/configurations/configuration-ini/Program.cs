var builder = WebApplication.CreateBuilder();
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddIniFile("settings.ini");

var app = builder.Build();
app.Run(async context =>
{
    foreach (var c in app.Configuration.AsEnumerable())
    {
        await context.Response.WriteAsync($"{c.Key} = {c.Value}\n");
    }
});

app.Run();