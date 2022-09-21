using Microsoft.AspNetCore.Http.Features;

var app = WebApplication.Create();
app.Run(context =>
{
    var connection = context.Features.Get<IHttpConnectionFeature>();
    var str = string.Empty;
    str += $"Local IP:Port: {connection?.LocalIpAddress}:{connection?.LocalPort}\n";
    str += $"Remote IP:Port: {connection?.RemoteIpAddress}:{connection?.RemotePort}\n";
    str += $"Connection Id: {connection?.ConnectionId}\n";

    return context.Response.WriteAsync($"{str}");

});

app.Run();
