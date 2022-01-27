var builder = WebApplication.CreateBuilder();
builder.Services.AddOrchardCore().AddMvc();

var app = builder.Build();
app.UseOrchardCore();
app.Run();

