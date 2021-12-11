var builder = WebApplication.CreateBuilder();
builder.Configuration.AddEnvironmentVariables();
var app = builder.Build();

app.Run(async context =>
{
    //Obviously you need to be careful with GetDebugView()
    await context.Response.WriteAsync((app.Configuration as IConfigurationRoot).GetDebugView());
});

app.Run();