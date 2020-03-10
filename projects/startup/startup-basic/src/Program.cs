using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Logging;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            //These are two services available at constructor
            //All these null checks are just for demonstrative purpose. Don't do it on production code.
            //I add them here just to show them that the services are availble for use.
            if (env is null)
                throw new ArgumentNullException("env is null");

            if (configuration is null)
                throw new ArgumentNullException("configuration is null");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
            //All these null checks are just for demonstrative purpose. Don't do it on production code.
            //I add them here just to show them that the services are availble for use.
            if (services is null)
                throw new Exception("services is null");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            // These are the four services available by default
            //All these null checks are just for demonstrative purpose. Don't do it on production code.
            //I add them here just to show them that the services are availble for use.
            if (app is null)
                throw new ArgumentNullException("app is null");

            if (env is null)
                throw new ArgumentNullException("env is null");

            if (configuration is null)
                throw new ArgumentNullException("configuration is null");

            if (loggerFactory is null)
                throw new ArgumentNullException("loggerFactory is null");
        

            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello world");
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
                    webBuilder.UseStartup<Startup>()
                );
    }
}