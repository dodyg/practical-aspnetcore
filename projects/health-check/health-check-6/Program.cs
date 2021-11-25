using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;

var builder = WebApplication.CreateBuilder();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<OKHttpStatusCodeHealthCheck>();
builder.Services.AddHttpClient<Error2HttpStatusCodeHealthCheck>();
builder.Services.AddSingleton<StatusOK>().AddSingleton<StatusBadRequest>();
builder.Services.AddHealthChecks()
    .AddCheck<OKHttpStatusCodeHealthCheck>("OK Status Check")
    .AddCheck<Error2HttpStatusCodeHealthCheck>("Error Status 2 Check");
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapHealthChecks("/IsUp", new HealthCheckOptions
{
    ResponseWriter = async (context, health) =>
    {
        context.Response.Headers.Add("Content-Type", "text/plain");

        if (health.Status == HealthStatus.Healthy)
            await context.Response.WriteAsync("Everything is good");
        else
        {
            foreach (var h in health.Entries)
            {
                await context.Response.WriteAsync($"Key = {h.Key} :: Description = {h.Value.Description} :: Status = {h.Value.Status} \n");
            }

            await context.Response.WriteAsync($"\n\n Overall Status: {health.Status}");
        }
    }
});

app.MapDefaultControllerRoute();

app.Run();

public class StatusOK
{
    public short Status { get; set; } = StatusCodes.Status200OK;
}

public class StatusBadRequest
{
    public short Status { get; set; } = StatusCodes.Status400BadRequest;
}

public abstract class HttpStatusCodeHealthCheck : IHealthCheck
{
    readonly HttpClient _client;

    readonly IServer _server;

    readonly short _statusCode;

    public HttpStatusCodeHealthCheck(HttpClient client, IServer server, short statusCode)
    {
        _client = client;
        _server = server;
        _statusCode = statusCode;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var serverAddress = _server.Features.Get<IServerAddressesFeature>();
            var localServer = serverAddress.Addresses.First();

            var result = await _client.GetAsync(localServer + $"/home/fakestatus/?statusCode={_statusCode}");

            if (result.StatusCode == HttpStatusCode.OK)
                return HealthCheckResult.Healthy("Everything is OK");
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                context.Registration.FailureStatus = HealthStatus.Degraded;
                return HealthCheckResult.Degraded($"Degraded: Http Status returns {result.StatusCode}");
            }
            else
            {
                return HealthCheckResult.Unhealthy($"Fails: Http Status returns {result.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy($"Exception {ex.Message} : {ex.StackTrace}");
        }
    }
}

public class OKHttpStatusCodeHealthCheck : HttpStatusCodeHealthCheck
{
    public OKHttpStatusCodeHealthCheck(HttpClient client, IServer server, StatusOK status) : base(client, server, status.Status)
    {
    }
}

public class Error2HttpStatusCodeHealthCheck : HttpStatusCodeHealthCheck
{
    public Error2HttpStatusCodeHealthCheck(HttpClient client, IServer server, StatusBadRequest status) : base(client, server, status.Status)
    {
    }
}

public class HomeController : Controller
{
    public ActionResult Index()
    {
        return new ContentResult
        {
            Content = @"
                <html><body>
                <h1>Health Check - Failed/Success check</h1>
                <a href=""/isup"">Check Status</a>
                </body></html>",
            ContentType = "text/html"
        };
    }

    public ActionResult FakeStatus(int statusCode)
    {
        return StatusCode(statusCode);
    }
}