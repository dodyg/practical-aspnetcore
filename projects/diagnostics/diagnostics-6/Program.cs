using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DiagnosticAdapter;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMiddlewareAnalysis();
            services.AddSingleton<SimpleDiagnosticListener>();
        }

        public void Configure(IApplicationBuilder app, DiagnosticListener diagnosticListener)
        {
            var listener = app.ApplicationServices.GetService<SimpleDiagnosticListener>();
            diagnosticListener.SubscribeWithAdapter(listener);

            app.Map("/hello", conf =>
            {
                conf.Run(async context =>
                {
                    context.Response.Headers.Add("content-type", "text/html");

                    await context.Response.WriteAsync("Hello world");
                });
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/hello2")
                {
                    await context.Response.WriteAsync("Hello world 2");
                }
                else if (context.Request.Path == "/exception")
                {
                    throw new Exception("Custom Exception");
                }
                else
                {
                    await next.Invoke();
                }
            });


            app.Run(async context =>
            {
                context.Response.Headers.Add("content-type", "text/html");

                await context.Response.WriteAsync("<h1>Middleware Analysis</h1>");
                await context.Response.WriteAsync("Check your console for the output");

                await context.Response.WriteAsync(@"
                    <ul>
                        <li><a href=""/hello"">Hello</a></li>
                        <li><a href=""/hello2"">Hello 2</a></li>
                        <li><a href=""/exception"">Exception Page</a></li>
                    </ul>
                    ");
            });
        }

        public class Program
        {
            public static void Main(string[] args) =>
                CreateHostBuilder(args).Build().Run();

            public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                        // Filter out the noise 
                        logging.AddFilter((provider, category, logLevel) =>
                        {
                            return !category.Contains("Microsoft.AspNetCore");
                        });
                    })
                    .ConfigureWebHostDefaults(webBuilder =>
                        webBuilder.UseStartup<Startup>()
                    );
        }

        public class SimpleDiagnosticListener
        {
            //There are three events that you can listen to
            //- Microsoft.AspNetCore.MiddlewareAnalysis.MiddlewareStarting
            //- Microsoft.AspNetCore.MiddlewareAnalysis.MiddlewareException
            //- Microsoft.AspNetCore.MiddlewareAnalysis.MiddlewareFinished
            //If you mispelled them, it won't work.
            //Read more about them here https://github.com/aspnet/AspNetCore/blob/master/src/Middleware/MiddlewareAnalysis/src/AnalysisMiddleware.cs

            readonly ILogger _log;

            public SimpleDiagnosticListener(ILogger<SimpleDiagnosticListener> log)
            {
                _log = log;
            }

            //The parameters in all these three methods are ALL the information provided by the MiddlewareAnalysis for each specificic event
            [DiagnosticName("Microsoft.AspNetCore.MiddlewareAnalysis.MiddlewareStarting")]
            public void OnStarting(HttpContext httpContext, string name, Guid instanceId, long timestamp)
            {
                _log.LogInformation($"MiddlewareStarting: {name}; {httpContext.Request.Path};");
            }

            [DiagnosticName("Microsoft.AspNetCore.MiddlewareAnalysis.MiddlewareException")]
            public void OnException(HttpContext httpContext, Exception exception, string name, Guid instanceId, long timestamp, long duration)
            {
                var durationInMS = (1000.0 * (double)duration / Stopwatch.Frequency);
                _log.LogInformation($"MiddlewareException: {name}; {exception.Message}; {httpContext.Request.Path};{durationInMS} ms");
            }

            [DiagnosticName("Microsoft.AspNetCore.MiddlewareAnalysis.MiddlewareFinished")]
            public void OnFinished(HttpContext httpContext, string name, Guid instanceId, long timestamp, long duration)
            {
                var durationInMS = (1000.0 * (double)duration / Stopwatch.Frequency);
                _log.LogInformation($"MiddlewareFinished: {name}; {httpContext.Response.StatusCode};{durationInMS} ms");
            }
        }
    }
}
