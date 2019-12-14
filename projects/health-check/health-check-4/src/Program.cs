using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Linq;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient<HttpStatusCodeHealthCheck>();
            services.AddHealthChecks().AddCheck<HttpStatusCodeHealthCheck>("HttpStatusCheck");

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(route =>
            {
                route.MapHealthChecks("/IsUp", new HealthCheckOptions
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

                route.MapDefaultControllerRoute();

            });
        }
    }

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

    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}