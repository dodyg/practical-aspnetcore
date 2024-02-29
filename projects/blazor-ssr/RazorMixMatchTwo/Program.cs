var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();
builder.Services.AddRazorPages();
builder.Services.AddAntiforgery();

var app = builder.Build();
app.UseAntiforgery();

app.MapRazorPages();
app.MapRazorComponents<RazorMixMatchTwo.App>();

app.Run();
