using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore;

namespace HelloWorldWithReload 
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            var serverAddress = app.ServerFeatures.Get<IServerAddressesFeature>();

            app.Run(context =>
            {
                var str = string.Empty;
                foreach(var a in serverAddress.Addresses)
                {
                    str += $"{a}\n";
                }
                return context.Response.WriteAsync(str);
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