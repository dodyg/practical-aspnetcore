var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();

//Don't forget that the area name specified must match the name of the area at [Area()] attribute used at the area controllers. 
app.MapAreaControllerRoute("AdminArea", "Admin", "Admin/{controller=Home}/{action=Index}/{id?}");
app.MapAreaControllerRoute("CustomerArea", "Customer", "Customer/{controller=Home}/{action=Index}/{id?}");
app.MapDefaultControllerRoute();

app.Run();