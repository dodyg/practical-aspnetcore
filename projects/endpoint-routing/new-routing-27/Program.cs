var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorPages(x =>
{
    x.Conventions.AddAreaPageRoute("Admin", "/Index", "/Manage");
    x.Conventions.AddAreaPageRoute("Customer", "/Index", "/CustomerService");
});

var app = builder.Build();
app.MapRazorPages();

app.Run();