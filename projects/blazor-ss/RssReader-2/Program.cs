using RssReader.Services;

var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<RssNews>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorPages();
app.MapFallbackToPage("/Index");
app.MapBlazorHub();

app.Run();