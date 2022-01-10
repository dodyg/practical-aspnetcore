var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.MapControllerRoute("Admin", "Admin", new
{
    Controller = "Admin",
    Action = "Index"
});

app.MapControllerRoute("About", "About", new
{
    Controller = "Home",
    Action = "About"
});

app.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");

app.Run();