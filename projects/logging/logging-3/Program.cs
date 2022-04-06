using System.Text.Json;

var builder = WebApplication.CreateBuilder();
// Trace, Debug, Information, Warning, Error, Critical, None
builder.Logging.AddFilter("Microsoft", LogLevel.Warning); //Only show Warning log and above from anything that contains Microsoft.
builder.Logging.AddFilter("AppLogger", LogLevel.Trace);//Pretty much show everything from AppLogger
builder.Logging.AddJsonConsole(options =>
{
    options.JsonWriterOptions = new JsonWriterOptions { Indented = true }; 
});

var app = builder.Build();

app.Run(context =>
{
    app.Logger.LogInformation("This is a information message");
    app.Logger.LogDebug("This is debug message");
    return context.Response.WriteAsync(app.Configuration["greeting"]);
});

app.Run();