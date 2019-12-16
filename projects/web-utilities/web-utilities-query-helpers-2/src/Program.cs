using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.WebUtilities;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are two services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.Run(async context =>
            {
                var queryString = QueryHelpers.ParseQuery(context.Request.QueryString.ToString());

                var output = "";

                foreach (var qs in queryString)
                {
                    output += qs.Key + " = " + qs.Value + "<br/>";
                }
                await context.Response.WriteAsync($@"<html>
                <body>
                <h1>Parsing Raw Query String</h1>
                <ul>
                    <li><a href=""?name=anne"">?name=anne</a></li>
                    <li><a href=""?name=anne&name=mishkind"">?name=anne&name=mishkind</a></li>
                    <li><a href=""?age=25&smart=true"">?age=25&smart=true</a></li>
                    <li><a href=""?country=zambia&country=senegal&country="">?country=zambia&country=senegal&country=</a></li>
                    <li><a href=""?"">?</a></li>
                    <br /><br /> 
                    <strong>Query String</strong><br/> 
                    {output}
                </ul>
                </body>
                </html>");
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