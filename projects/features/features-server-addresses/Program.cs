using Microsoft.AspNetCore.Hosting.Server.Features;

var app = WebApplication.Create();
var serverAddress = (app as IApplicationBuilder).ServerFeatures.Get<IServerAddressesFeature>();

app.Run(context =>
{
    var str = string.Empty;

    if (serverAddress != null)
        foreach (var address in serverAddress.Addresses)
        {
            str += $"{address}\n";
        }

    return context.Response.WriteAsync(str);
});

app.Run();
