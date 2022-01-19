using Microsoft.AspNetCore.Hosting.Server.Features;

var app = WebApplication.Create();
var serverAddress = (app as IApplicationBuilder).ServerFeatures.Get<IServerAddressesFeature>();
app.Run(context =>
{
    var str = string.Empty;
    foreach(var a in serverAddress.Addresses)
    {
        str += $"{a}\n";
    }
    return context.Response.WriteAsync(str);
});

app.Run();