using LokiLoggingProvider.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddLoki(configure =>
{
    configure.Client = PushClient.Grpc;
    configure.StaticLabels.JobName = "LokiWebApplication";
});

var app = builder.Build();

app.MapGet("/test", (ILoggerFactory loggerFactory) =>
{
    var logger = loggerFactory.CreateLogger("Start");
    logger.LogTrace("Trace message");
    logger.LogDebug("Debug message");
    logger.LogInformation("Information message");
    logger.LogWarning("Warning message");
    logger.LogError("Error message");
    logger.LogCritical("Critical message");
    return "OK";
});

app.Run();
