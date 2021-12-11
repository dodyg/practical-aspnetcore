var builder = WebApplication.CreateBuilder();
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddXmlFile("settings.xml");

var app = builder.Build();
app.Run(async (context) =>
{
    foreach (var c in app.Configuration.AsEnumerable())
    {
        await context.Response.WriteAsync($"{c.Key} = {app.Configuration[c.Key]}\n");
    }
});

app.Run();