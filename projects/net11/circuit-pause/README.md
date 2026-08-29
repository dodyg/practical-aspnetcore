# Blazor circuit pause

This sample demonstrates both circuit-pause features:

- **Server-initiated pause** — `Circuit.RequestCircuitPauseAsync()`
  lets server-side code ask the connected client to begin the graceful
  circuit-pause flow (e.g. drain circuits during deployments or load-balancer
  rebalancing). The supported way to obtain `Circuit` instances is to capture
  them from `CircuitHandler.OnConnectionUpAsync`.

- **Automatic pause (opt-in)** — the
  `Microsoft.AspNetCore.Components.Server.AutoPause` package pauses the circuit
  after a configurable inactivity delay while the tab is hidden:


