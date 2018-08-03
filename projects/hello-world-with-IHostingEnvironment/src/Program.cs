using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

namespace StartupBasic 
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are two services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure

            var applicationName = $"<tr><td>ApplicationName</td><td>{env.ApplicationName}</td></tr>";
            var contentRootPath = $"<tr><td>ContentRootPath</td><td>{env.ContentRootPath}</td></tr>";
            var contentRootFileProvider = $"<tr><td>ContentRootFileProvider</td><td>{env.ContentRootFileProvider}</td></tr>";
            var environmentName = $"<tr><td>EnvironmentName</td><td>{env.EnvironmentName}</td></tr>";
            var webRootPath = $"<tr><td>WebRootPath</td><td>{env.WebRootPath}</td></tr>";
            var webRootFileProvider = $"<tr><td>WebRootFileProvider</td><td>{env.WebRootFileProvider}</td></tr>";

            var content = $"<html><body><table><tbody><h1>Complete list of IHostingEnvironment properties</h1>{applicationName}{contentRootPath}{contentRootFileProvider}{environmentName}{webRootPath}{webRootFileProvider}</tbody></table></body></html>";
            
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
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}