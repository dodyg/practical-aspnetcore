using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var bodySize = context.Features.Get<IHttpMaxRequestBodySizeFeature>();
                bodySize.MaxRequestBodySize = 555;
                var str = "<html><body>";
                str += $"Max Request Body Size {bodySize.MaxRequestBodySize}(Is Read Only: {bodySize.IsReadOnly}) You can <strong>also</strong> set this value at KestrelServerOptions.Limits.MaxRequestBodySize";
                str += "</body></html>";
                context.Response.Headers.Add("Content-Type", "text/html");
                return context.Response.WriteAsync($"{str}");
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
                    webBuilder.UseStartup<Startup>().ConfigureKestrel(k =>
                    {
                      k.Limits.MaxRequestBodySize = 5000;
                    })
                );
    }
}