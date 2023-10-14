var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();
builder.Services.AddMvc();
builder.Services.AddAntiforgery();

var app = builder.Build();
app.UseAntiforgery();

app.MapControllers();
app.MapRazorComponents<RazorMixMatchOne.App>();

app.Run();
