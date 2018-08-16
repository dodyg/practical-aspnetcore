using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace StartupBasic 
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            //Comment this code out and you will see the whole program will stop working 
            app.UseResponseBuffering();

            //These are the four default services available at Configure
            app.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(new { Name = "Annie", Status = "Amore"}));
                
                //Without buffering this would have generated exception
                context.Response.Headers.Clear();
                context.Response.ContentType = "text/plain";
                
                context.Response.Body.SetLength(0);
                await context.Response.WriteAsync("hello world");
            });
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