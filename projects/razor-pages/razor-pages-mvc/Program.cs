using Microsoft.EntityFrameworkCore;
using PracticalAspNetCore.Data;

var builder = WebApplication.CreateBuilder();
builder.Services.AddDbContext<GuestbookContext>(o => o.UseInMemoryDatabase("GuestbookDatabase"));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();
app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();