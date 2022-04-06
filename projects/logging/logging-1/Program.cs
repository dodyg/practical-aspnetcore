var builder = WebApplication.CreateBuilder();

// Adjust the minimum level here and see the impact 
// on the displayed logs.
// The rule is it will show >= minimum level
// The levels are:
// - Trace = 0
// - Debug = 1
// - Information = 2
// - Warning = 3
// - Error = 4
// - Critical = 5
// - None = 6

builder.Logging.SetMinimumLevel(LogLevel.Warning);
builder.Logging.AddConsole();

var app = builder.Build();

app.Run(context =>
{
    var log = app.Logger;
    log.LogTrace("Trace message");
    log.LogDebug("Debug message");
    log.LogInformation("Information message");
    log.LogWarning("Warning message");
    log.LogError("Error message");
    log.LogCritical("Critical message");
    return context.Response.WriteAsync("Hello world. Take a look at your terminal to see the logging messages.");
});
app.Run();
