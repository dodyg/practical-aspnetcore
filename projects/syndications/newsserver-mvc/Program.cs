using NewsServer.Models;

var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton(sp =>
{
    return builder.Configuration.GetSection(NewsServerOptions.NewsServer).Get<NewsServerOptions>();
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{**slug}");
app.Run();