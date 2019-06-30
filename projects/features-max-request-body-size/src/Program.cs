using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore;

namespace Features.Connection
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var bodySize = context.Features.Get<IHttpMaxRequestBodySizeFeature>();
                bodySize.MaxRequestBodySize = 555;
                var str = string.Empty;
                str = $"Max Request Body Size {bodySize.MaxRequestBodySize}(Is Read Only: {bodySize.IsReadOnly}) You can <strong>also</strong> set this value at KestrelServerOptions.Limits.MaxRequestBodySize";
                return context.Response.WriteAsync($"{str}");
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
                .UseKestrel(k =>
                {
                    k.Limits.MaxRequestBodySize = 5000;
                })
                .UseEnvironment("Development");
    }
}