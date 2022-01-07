var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();
app.MapRazorPages();
app.MapGet("/contact/us", async context =>
{
    await context.Response.WriteAsync("Contact Us");
});

app.MapFallbackToPage("/Index");

app.Run();