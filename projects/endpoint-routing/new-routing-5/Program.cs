var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.Map("/", async context =>
{
    if (context.Request.Method == "GET")
        await context.Response.WriteAsync("Hello world");
});

app.Run();
