using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting.Server;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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

        IReadOnlyCollection<string> _addresses;

        public AddressesMiddleware(RequestDelegate next, IServer server)
        {
            _server = server;
        }

        public async Task Invoke(HttpContext context)
        {
            var str = string.Empty;

            // We cache the result because the addresses are not going to change. 
            // Remember middlewares are singletons, so we can just do this simple check. 
            if (_addresses == null)
            {
                var address = _server.Features.Get<IServerAddressesFeature>();
                _addresses = address.Addresses.ToList();
            }

            foreach (var a in _addresses)
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