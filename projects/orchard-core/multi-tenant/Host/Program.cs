var builder = WebApplication.CreateBuilder();
builder.Services.AddOrchardCore().AddMvc().WithTenants();

var app = builder.Build();
app.UseOrchardCore();
app.Run();
