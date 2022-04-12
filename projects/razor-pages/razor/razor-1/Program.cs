var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorPages();

var app = builder.Build();
app.MapRazorPages();

app.Run();
