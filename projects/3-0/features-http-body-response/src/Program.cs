using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http.Features;
using System.Text;

namespace HttpBodyResponse
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                context.Response.Headers.Add("Content-Type", "text/html; charset=utf-8");
                var feature = context.Features.Get<IHttpResponseBodyFeature>();
                await feature.StartAsync();
                await feature.Stream.WriteAsync(Encoding.UTF8.GetBytes("<html><body style=\"font-size:240px;text-align:center;\">Hello 🌍</body></html>"));
                await feature.CompleteAsync();
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
                    webBuilder.UseStartup<Startup>().
                    UseEnvironment("Development");
                });
    }
}