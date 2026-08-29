using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<ChatHub>("/chat", options =>
{
    // Exposes a /refresh endpoint next to /negotiate so the .NET client can
    // re-authenticate before its bearer token expires without reconnecting.
    options.EnableAuthenticationRefresh = true;

    // Optional: accept or reject a refresh attempt per connection. Returning
    // false responds with 403 and keeps the current user in place.
    options.OnAuthenticationRefresh = context =>
    {
        var newUser = context.NewUser;
        app.Logger.LogInformation("Refresh for connection {ConnectionId}: {User} (expires {Expiration})",
            context.ConnectionId,
            newUser?.Identity?.Name ?? "anonymous",
            context.NewExpiration);

        return ValueTask.FromResult(true);
    };
});

app.MapGet("/", () => Results.Text("""
    <html><body>
        <h1>SignalR authentication refresh</h1>
        <p>The server exposes <code>/negotiate</code> and <code>/refresh</code> for the hub at <code>/chat</code>.</p>
        <pre>curl -s http://localhost:5000/chat/negotiate?negotiateVersion=1</pre>
        <p>Watch the server log: when a .NET client calls
        <code>WithAuthenticationRefresh(...)</code>, the refresh callback above is invoked.</p>
    </body></html>
    """, "text/html")).ExcludeFromDescription();

app.Run();

public class ChatHub : Hub
{
    // Called after the connection's User has been updated with the refreshed
    // token. Update per-connection state that depended on the old identity.
    public override Task OnAuthenticationRefreshedAsync()
    {
        var user = Context.User;
        // The connection's User has been updated with the refreshed token.
        return Task.CompletedTask;
    }
}
