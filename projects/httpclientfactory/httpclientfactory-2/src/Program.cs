using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("rss", c =>
            {
                // Configure your http client here
                c.DefaultRequestHeaders.Add("Accept", "application/rss+xml");
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var httpClient = context.RequestServices.GetService<IHttpClientFactory>();
                var client = httpClient.CreateClient("rss"); // use the preconfigured http client
                var result = await client.GetStringAsync("http://scripting.com/rss.xml");

                context.Response.Headers.Add("Content-Type", "application/rss+xml");
                await context.Response.WriteAsync(result);
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