var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorComponents();

var app = builder.Build();

app.MapRazorComponents<RazorComponentOne.Pages.App>();
app.Run();


