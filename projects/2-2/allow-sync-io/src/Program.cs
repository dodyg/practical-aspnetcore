using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.IO;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using System;

namespace EndpointRoutingSample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.Use(async (ctx, next) =>
            {
                if (ctx.Request.Path == "/home/sync")
                {
                    try
                    {
                        using (var streamReader = new StreamReader(ctx.Request.Body, Encoding.UTF8))
                        {
                            string body = streamReader.ReadToEnd(); //Sync IO!
                        }

                        ctx.Request.HttpContext.Items["Message"] = "Hello world";
                    }
                    catch (Exception ex)
                    {
                        ctx.Request.HttpContext.Items["Message"] = "Exception message " + ex.Message;
                    }
                }

                await next();
            });

            app.Use(async (ctx, next) =>
            {
                if (ctx.Request.Path == "/home/async")
                {
                    try
                    {
                        using (var streamReader = new StreamReader(ctx.Request.Body, Encoding.UTF8))
                        {
                            string body = await streamReader.ReadToEndAsync();
                        }

                        ctx.Request.HttpContext.Items["Message"] = "Hello world";
                    }
                    catch (Exception ex)
                    {
                        ctx.Request.HttpContext.Items["Message"] = "Exception message " + ex.Message;
                    }
                }

                await next();
            });

            app.UseMvcWithDefaultRoute();
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
                <p>
                    In this sample we do not allow Synchronous IO in the web system when running on Kestrel.
                </p>
                <p>
                    Sync IO is bad because it can create thread starvation.Disallowing it makes sures that you don't have such code in your request pipeline.
               </p>

               <p>
               <a href=""/Home/Sync"">Click on this link</a> and it will generate an exception because the middleware before this process will try to use synchronous IO. 
               </p>

               <p>
               <a href=""/Home/Async"">Click on this link</a> and it will work properly because the middleware uses async IO.
               </p>
               
               </body></html>",
                ContentType = "text/html"
            };
        }

        public ActionResult Sync()
        {
            var message = HttpContext.Items["Message"];
            return new ContentResult
            {
                Content = $@"
                <html><body>
                <p>If you set AllowSynchronousIO == true, you will see a greeting message. If you set AllowSynchronousIO == false, you will see an exception message</p>
                    Message from middleware : {message}.
               </body></html> ",
                ContentType = "text/html"
            };
        }

        public ActionResult Async()
        {
            var message = HttpContext.Items["Message"];
            return new ContentResult
            {
                Content = $@"
                <html><body>
                    Message from middleware : {message}.
               </body></html> ",
                ContentType = "text/html"
            };
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
                .ConfigureKestrel(k =>
                {
                    k.AllowSynchronousIO = false;
                })
                .UseEnvironment("Development");
    }
}