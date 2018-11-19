using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting.Server;
using System.Threading.Tasks;

namespace HelloWorldWithReload
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<AddressesMiddleware>();
        }
    }

    public class AddressesMiddleware
    {
        IServer _server;

        public AddressesMiddleware(RequestDelegate next, IServer server)
        {
            _server = server;
        }

        public async Task Invoke(HttpContext context)
        {
            var str = string.Empty;
            var address = _server.Features.Get<IServerAddressesFeature>();

            foreach (var a in address.Addresses)
            {
                str += $"{a}\n";
            }

            await context.Response.WriteAsync(str);
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