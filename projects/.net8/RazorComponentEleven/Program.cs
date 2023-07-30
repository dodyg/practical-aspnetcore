var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorComponents();

var app = builder.Build();
app.MapRazorComponents<RazorComponentEleven.App>();
app.Run();


