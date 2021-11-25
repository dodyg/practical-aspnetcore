using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder();

builder.Services.AddHealthChecks().AddCheck<AlwaysBadHealthCheck>("Bad");
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapHealthChecks("/IsUp");
app.MapDefaultControllerRoute();

app.Run();

public class AlwaysBadHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
        throw new NotImplementedException();
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
                <h1>Health Check - Failed check</h1>
                This <a href=""/IsUp"">/IsUp</a> always fails.
                </body></html> ",
            ContentType = "text/html"
        };
    }
}