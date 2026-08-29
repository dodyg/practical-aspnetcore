using System.Collections.Concurrent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.Circuits;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Track active circuits so an operator (or hosted service) can pause them.
builder.Services.AddSingleton<CircuitTracker>();
builder.Services.AddScoped<CircuitHandler>(sp => sp.GetRequiredService<CircuitTracker>());

var app = builder.Build();

// Server-initiated circuit pause (Preview 4): pause every active circuit, e.g.
// before a deployment or during load-balancer rebalancing.
app.MapGet("/pause-all", async () =>
{
    var tracker = app.Services.GetRequiredService<CircuitTracker>();
    var paused = 0;

    foreach (var circuit in CircuitTracker.ActiveCircuits)
    {
        // Returns true once the client acknowledges the pause.
        if (await circuit.RequestCircuitPauseAsync())
        {
            paused++;
        }
    }

    return Results.Text($"Paused {paused} of {CircuitTracker.ActiveCircuits.Count} circuit(s). "
        + "The interactive component above will resume when you interact again.");
});

app.MapRazorComponents<CircuitPause.App>()
    .AddInteractiveServerRenderMode()
    .WithBrowserOptions(options =>
    {
        // Opt-in auto-pause (Preview 7): pause the circuit after the tab has
        // been hidden for a while, releasing server resources until the user
        // comes back.
        options.AddAutoPause(pause =>
        {
            pause.Enabled = true;
            pause.HiddenDelay = TimeSpan.FromSeconds(30); // default is 2 minutes
        });
    });

app.Run();

/// <summary>
/// Captures circuits from CircuitHandler.OnConnectionUpAsync — the supported
/// way to obtain Circuit instances, since there is no public registry.
/// </summary>
public sealed class CircuitTracker : CircuitHandler
{
    private static readonly ConcurrentDictionary<string, Circuit> Circuits = new();

    public static IReadOnlyCollection<Circuit> ActiveCircuits => Circuits.Values.ToArray();

    public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        Circuits[circuit.Id] = circuit;
        return Task.CompletedTask;
    }

    public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        Circuits.TryRemove(circuit.Id, out _);
        return Task.CompletedTask;
    }
}
