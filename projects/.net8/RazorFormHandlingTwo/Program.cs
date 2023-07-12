var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorComponents();

var app = builder.Build();

app.MapRazorComponents<RazorFormHandlingTwo.Pages.App>();
app.Run();


