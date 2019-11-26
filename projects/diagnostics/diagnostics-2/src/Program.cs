using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore;

namespace StartupBasic 
{
   public class Program
   {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .Configure(app => 
                {
                    app.UseDeveloperExceptionPage(); //Don't use this in production
                    app.Run(context => throw new ApplicationException("Hello World Exception"));
                })
                .UseEnvironment("Development");
    }
}