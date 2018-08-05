using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Globalization;

namespace StartupBasic 
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //These are three services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.UseOwin(pipeline =>
            {
                pipeline(next => async environment =>{
                    byte[] response = Encoding.UTF8.GetBytes("Hello world");
                    var responseStream = (Stream)environment["owin.ResponseBody"];
                    
                    var responseHeaders = (IDictionary<string, string[]>)environment["owin.ResponseHeaders"];
                    responseHeaders["Content-Length"] = new string[] { response.Length.ToString(CultureInfo.InvariantCulture) };
                    responseHeaders["Content-Type"] = new string[] { "text/plain" };

                    await responseStream.WriteAsync(response, 0, response.Length);
                });
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