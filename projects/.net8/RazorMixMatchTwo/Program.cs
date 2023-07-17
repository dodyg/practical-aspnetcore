var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();
builder.Services.AddRazorPages();

var app = builder.Build();

app.MapRazorPages();
app.MapRazorComponents<RazorMixMatchTwo.App>();

app.Run();
