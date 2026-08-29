# Blazor circuit pause

This sample demonstrates both circuit-pause features:

- **Server-initiated pause (Preview 4)** — `Circuit.RequestCircuitPauseAsync()`
  lets server-side code ask the connected client to begin the graceful
  circuit-pause flow (e.g. drain circuits during deployments or load-balancer
  rebalancing). The supported way to obtain `Circuit` instances is to capture
  them from `CircuitHandler.OnConnectionUpAsync`.

- **Auto-pause (Preview 7, opt-in)** — the
  `Microsoft.AspNetCore.Components.Server.AutoPause` package pauses the circuit
  after a configurable inactivity delay while the tab is hidden:

```csharp
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .WithBrowserOptions(options =>
    {
        options.AddAutoPause(pause =>
        {
            pause.Enabled = true;
            pause.HiddenDelay = TimeSpan.FromSeconds(30); // default is 2 minutes
        });
    });
```

Try it: open the page (interactive Server circuit), then hit
`/pause-all` in another tab — or hide the tab for 30 seconds. Both pause the
circuit; interacting again resumes it with state preserved.

See the [Preview 4](https://github.com/dotnet/core/blob/main/release-notes/11.0/preview/preview4/aspnetcore.md#server-initiated-blazor-server-circuit-pause)
and [Preview 7](https://github.com/dotnet/core/blob/main/release-notes/11.0/preview/preview7/aspnetcore.md#auto-pause-blazor-circuits-on-inactivity)
release notes.
