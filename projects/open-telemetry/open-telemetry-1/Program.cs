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
    var traceId = Activity.Current?.TraceId;
    await context.Response.WriteAsync($"Trace Id {traceId}");
});

await app.RunAsync();