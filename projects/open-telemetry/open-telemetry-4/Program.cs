using System.Diagnostics;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder();
builder.Services.AddOpenTelemetryTracing(b =>
{
    b.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName))
     .AddHttpClientInstrumentation();
});

builder.Services.AddHttpClient();
builder.Logging.SetMinimumLevel(LogLevel.Information);

WebApplication app = builder.Build();
app.MapGet("/", async (HttpRequest request, IHttpClientFactory clientFactory) =>
{
        Activity.Current?.AddBaggage ("project", "practical-aspnetcore");
        Activity.Current?.AddBaggage ("location", "Cairo");

        var baggage = string.Join(",", Activity.Current.Baggage.ToDictionary(b => b.Key, b => b.Value).Select(x => x.Key + "=" + x.Value));  

        var client = clientFactory.CreateClient();
        //client.DefaultRequestHeaders.Add("baggage", baggage);

        var url = request.Scheme + "://" + request.Host + "/baggage";
        app.Logger.LogInformation("REQUEST URL " + url);
        var response = await client.GetAsync(url);

        return Results.Text("Check the log", "text/plain");
});

app.MapGet("/baggage", (HttpRequest request) =>
{
    var baggage = request.Headers["baggage"];
    app.Logger.LogInformation($"BAGGAGE VALUES {baggage}");
    return Results.Ok();
});

app.Run();