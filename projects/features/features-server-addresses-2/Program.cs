using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;

var app = WebApplication.Create();
app.UseMiddleware<AddressesMiddleware>();
app.Run();

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
