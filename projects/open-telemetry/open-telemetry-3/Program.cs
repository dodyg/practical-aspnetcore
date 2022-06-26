using System.Diagnostics;

using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using OpenTelemetry.Exporter;

var builder = WebApplication.CreateBuilder();
builder.Services.AddOpenTelemetryTracing(b =>
{
    b.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName))
     .AddAspNetCoreInstrumentation()
     .AddConsoleExporter( options => options.Targets = ConsoleExporterOutputTargets.Console);
});

WebApplication app = builder.Build();
app.Run(async context =>
{
    if (context.Request.Path == "/")
    {
        await Task.Delay(2000);
        Activity.Current?.SetTag("project", "practical-aspnetcore");
        Activity.Current?.SetTag("location", "Cairo");
        var traceId = Activity.Current?.TraceId;
        await context.Response.WriteAsync($"Trace Id {traceId}");
        await Task.Delay(2000);
        await context.Response.WriteAsync($"\nHello World");
    }
});

await app.RunAsync();