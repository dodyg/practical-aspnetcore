var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapControllerRoute("areas", "{area}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run(async (context) =>
{
    context.Response.Headers.Add("Content-Type", "text/html");

    await context.Response.WriteAsync(@"<html>
        <body>
        <h1>Razor Class Library sample with MVC structure (controllers and views)</h1>
        <ul><li><a href=""/MyFeature"">Home</a></li><li><a href=""/MyFeature/About"">About (with attribute routing)</a></li></ul>
        </body>
        </html>");
});

app.Run();