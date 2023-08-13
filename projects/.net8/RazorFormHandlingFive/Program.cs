var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorComponents();

var app = builder.Build();

app.MapRazorComponents<RazorFormHandlingOne.Pages.App>();
app.Run();


