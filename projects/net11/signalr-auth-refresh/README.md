# SignalR authentication refresh

Preview 6 lets SignalR connections refresh their authentication **without
dropping the connection** when an access token expires. The server exposes a
`/refresh` endpoint alongside `/negotiate` and reports the token lifetime in
the negotiate response; the .NET client re-authenticates before expiry.

```csharp
app.MapHub<ChatHub>("/chat", options =>
{
    options.EnableAuthenticationRefresh = true;
    options.OnAuthenticationRefresh = context => ValueTask.FromResult(true);
});

public class ChatHub : Hub
{
    public override Task OnAuthenticationRefreshedAsync()
    {
        // The connection's User has been updated with the refreshed token.
        return Task.CompletedTask;
    }
}
```

On the .NET client (automatic refresh is on by default):

```csharp
var connection = new HubConnectionBuilder()
    .WithUrl("https://example.com/chat")
    .WithAuthenticationRefresh(options =>
    {
        options.RefreshBeforeExpiration = TimeSpan.FromMinutes(1);
        options.OnAuthenticationRefreshed = context => Task.CompletedTask;
        options.OnAuthenticationRefreshFailed = context => Task.CompletedTask;
    })
    .Build();
```

Note: in this preview the feature is implemented for the .NET client only —
JavaScript/TypeScript client and Azure SignalR Service support are in progress.

See the [Preview 6 release notes](https://github.com/dotnet/core/blob/main/release-notes/11.0/preview/preview6/aspnetcore.md#signalr-authentication-refresh).
