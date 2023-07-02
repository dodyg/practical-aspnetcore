var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorComponents();

var app = builder.Build();

app.MapRazorComponents<RazorFormHandling.Pages.App>();
app.Run();


