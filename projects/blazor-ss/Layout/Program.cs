var builder = WebApplication.CreateBuilder();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorPages();
app.MapFallbackToPage("/Index");
app.MapBlazorHub();

app.Run();


