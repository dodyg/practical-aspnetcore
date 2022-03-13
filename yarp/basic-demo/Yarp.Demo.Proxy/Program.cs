using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var yarpConfigSection = builder.Configuration.GetSection("ReverseProxy");
builder.Services.AddReverseProxy()
                .LoadFromConfig(yarpConfigSection);
var app = builder.Build();
app.MapReverseProxy();
app.Run();