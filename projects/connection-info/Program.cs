var app = WebApplication.Create();

app.Run(context =>
{
    var connection = context.Connection;
    var str = string.Empty;
    str += $"Local IP:Port => {connection.LocalIpAddress}:{connection.LocalPort}\n";
    str += $"Remote IP:Port => {connection.RemoteIpAddress}:{connection.RemotePort}\n";
    str += $"Client Certificate => {connection.ClientCertificate?.FriendlyName}\n";

    return context.Response.WriteAsync($"{str}");
});

app.Run();