using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

namespace EndpointRoutingSample
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            //We need this switch because we are connecting to an unsecure server. If the server runs on SSL, there's no need for this switch.
            app.Run(async context =>
            {
                bool supportTrailers = context.Response.SupportsTrailers();

                Stopwatch watch = null;

                if (supportTrailers)
                {
                    context.Response.DeclareTrailer("Server-Timing");
                    watch = new Stopwatch();
                }

                watch?.Start();

                context.Response.Headers.Add("Content-Type", "text/plain");
                await context.Response.WriteAsync("Open your browser developer tools to see the 'Server-Timing' HTTP header in this request " + supportTrailers);

                watch?.Stop();
                if (supportTrailers)
                    context.Response.AppendTrailer("Server-Timing", $"app;dur={watch.ElapsedMilliseconds}.0");
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .ConfigureKestrel(k =>
                    {
                        k.Listen(IPAddress.Any, 5000, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                        listenOptions.UseHttps();
                    });
                    }).
                    UseEnvironment(Environments.Development);
                });
    }
}