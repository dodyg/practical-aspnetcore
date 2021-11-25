using System.Net;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<HttpStatusCodeHealthCheck>();
builder.Services.AddHealthChecks().AddCheck<HttpStatusCodeHealthCheck>("HttpStatusCheck");
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapHealthChecks("/IsUp", new HealthCheckOptions
{
    ResponseWriter = async (context, health) =>
    {
        if (health.Status == HealthStatus.Healthy)
            await context.Response.WriteAsync("Everything is good");
        else
        {
            foreach (var h in health.Entries)
            {
                await context.Response.WriteAsync($"{h.Key} {h.Value.Description}");
            }
        }
    }
});


app.MapDefaultControllerRoute();

app.Run();

public class HttpStatusCodeHealthCheck : IHealthCheck
{
    readonly HttpClient _client;

    readonly IServer _server;

    public HttpStatusCodeHealthCheck(HttpClient client, IServer server)
    {
        _client = client;
        _server = server;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var serverAddress = _server.Features.Get<IServerAddressesFeature>();
            var localServer = serverAddress.Addresses.First();

            var result = await _client.GetAsync(localServer + "/home/fakestatus/?statusCode=500");

            if (result.StatusCode == HttpStatusCode.OK)
                return HealthCheckResult.Healthy("Everything is OK");
            else
                return HealthCheckResult.Degraded($"Fails: Http Status returns {result.StatusCode}");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy($"Exception {ex.Message} : {ex.StackTrace}");
        }
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
                This <a href=""/IsUp"">/IsUp</a> always fails at the moment. If you want to see it works, change the following code

                <pre>
                    var result = await _client.GetAsync(localServer + ""/home/fakestatus/?statusCode=500"");
                </pre> 

                to

                <pre>
                    var result = await _client.GetAsync(localServer + ""/home/fakestatus/?statusCode=200"");
                </pre> 
                </body></html>",
            ContentType = "text/html"
        };
    }

    public ActionResult FakeStatus(int statusCode)
    {
        return StatusCode(statusCode);
    }
}
