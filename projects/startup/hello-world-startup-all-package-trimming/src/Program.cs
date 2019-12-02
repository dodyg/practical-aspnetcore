using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //These are three services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //These are the four default services available at Configure
            app.Run(context =>
            {
                context.Response.Headers.Add("Content-Type", "text/html");
                return context.Response.WriteAsync($@"
                <html>
                <body>
                For the build command, use <blockquote>dotnet build /p:TrimUnusedDependencies=true</blockquote> For further options,
                open <a href=""https://github.com/dotnet/standard/blob/master/Microsoft.Packaging.Tools.Trimming/docs/trimming.md"">this</a>
                for the command line for trimming. 
                </body>
                </html>
                ");
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