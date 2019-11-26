using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
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
                    app.UseStatusCodePagesWithRedirects("/error?status={0}");

                    app.Map("/error", errorApp =>
                    {
                        errorApp.Run(async context => {
                            await context.Response.WriteAsync($"This is a redirected error message status {context.Request.Query["status"]}");            
                        });
                    });      

                    app.Run(context => 
                    {
                        context.Response.StatusCode = 500;//change this as necessary
                        return Task.CompletedTask;
                    });
                })
                .UseEnvironment("Development");
    }
}