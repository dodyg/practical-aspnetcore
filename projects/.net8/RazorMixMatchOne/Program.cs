var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();
builder.Services.AddMvc();

var app = builder.Build();

app.MapControllers();
app.MapRazorComponents<RazorMixMatchOne.App>();

app.Run();
