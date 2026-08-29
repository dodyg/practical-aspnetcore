# Cancel hub invocations from the client

Preview 6 lets a SignalR client cancel a regular (non-streaming) hub method
invocation. Passing a `CancellationToken` to `InvokeAsync` and canceling it
sends a cancellation message; the hub method's `CancellationToken` parameter is
triggered on the server.

```csharp
// Hub
public class WorkHub : Hub
{
    public async Task LongRunningWork(CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
    }
}
```

```csharp
// Client
using var cts = new CancellationTokenSource();
var work = connection.InvokeAsync("LongRunningWork", cts.Token);
// ...
cts.Cancel();
```

This sample runs the server and a .NET client in the same `Program.cs`: the
client connects to the app it started, invokes `LongRunningWork`, cancels after
2 seconds, and both sides report the cancellation.

See the [Preview 6 release notes](https://github.com/dotnet/core/blob/main/release-notes/11.0/preview/preview6/aspnetcore.md#cancel-hub-invocations-from-the-client).
