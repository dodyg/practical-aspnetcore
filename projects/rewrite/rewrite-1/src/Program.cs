using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            var options = new RewriteOptions()
                .AddRedirect("/$", "/"); //redirect when path ends with /

            app.UseRewriter(options);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", (context) =>
                {
                    context.Response.Headers.Add("content-type", "text/html");
                    return context.Response.WriteAsync($"Always display this page when path ends with / e.g. <a href=\"/hello-world/\">/hello-world/</a> or <a href=\"/welcome/everybody/inthis/train/\">/welcome/everybody/inthis/train/</a>.");
                });
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