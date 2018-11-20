using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting.Server;

namespace EndpointRoutingSample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddHttpClient<OKHttpStatusCodeHealthCheck>();
            services.AddHttpClient<ErrorHttpStatusCodeHealthCheck>();

            services.AddSingleton<StatusOK>()
                    .AddSingleton<StatusInternalServerError>();

            services.AddHealthChecks()
                .AddCheck<OKHttpStatusCodeHealthCheck>("OK Status Check")
                .AddCheck<ErrorHttpStatusCodeHealthCheck>("Error Status Check");

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.UseHealthChecks("/IsUp", new HealthCheckOptions
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
                            await context.Response.WriteAsync($"{h.Key} :: {h.Value.Description} \n");
                        }

                        await context.Response.WriteAsync($"\n\n Overall Status: {health.Status}");
                    }

                }
            });
            app.UseMvcWithDefaultRoute();
        }
    }

    public class StatusOK
    {
        public short Status { get; set; } = StatusCodes.Status200OK;
    }

    public class StatusInternalServerError
    {
        public short Status { get; set; } = StatusCodes.Status500InternalServerError;
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
                    return HealthCheckResult.Passed("Everything is OK");
                else
                    return HealthCheckResult.Failed($"Fails: Http Status returns {result.StatusCode}");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Failed($"Exception {ex.Message} : {ex.StackTrace}");
            }
        }
    }

    public class OKHttpStatusCodeHealthCheck : HttpStatusCodeHealthCheck
    {
        public OKHttpStatusCodeHealthCheck(HttpClient client, IServer server, StatusOK status) : base(client, server, status.Status)
        {
        }
    }

    public class ErrorHttpStatusCodeHealthCheck : HttpStatusCodeHealthCheck
    {
        public ErrorHttpStatusCodeHealthCheck(HttpClient client, IServer server, StatusInternalServerError status) : base(client, server, status.Status)
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

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}