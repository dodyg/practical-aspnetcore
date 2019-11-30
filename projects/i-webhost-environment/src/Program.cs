using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //These are the three default services available at Configure

            var applicationName = $"<tr><td>ApplicationName</td><td>{env.ApplicationName}</td></tr>";
            var contentRootPath = $"<tr><td>ContentRootPath</td><td>{env.ContentRootPath}</td></tr>";
            var contentRootFileProvider = $"<tr><td>ContentRootFileProvider</td><td>{env.ContentRootFileProvider}</td></tr>";
            var environmentName = $"<tr><td>EnvironmentName</td><td>{env.EnvironmentName}</td></tr>";
            var webRootPath = $"<tr><td>WebRootPath</td><td>{env.WebRootPath}</td></tr>";
            var webRootFileProvider = $"<tr><td>WebRootFileProvider</td><td>{env.WebRootFileProvider}</td></tr>";

            var content = $"<html><body><table><tbody><h1>Complete list of IWebHostEnvironment properties</h1>{applicationName}{contentRootPath}{contentRootFileProvider}{environmentName}{webRootPath}{webRootFileProvider}</tbody></table></body></html>";

            app.Run(context =>
            {
                context.Response.ContentType = "text/html";
                return context.Response.WriteAsync(content);
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