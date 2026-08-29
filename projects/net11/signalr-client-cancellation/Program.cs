using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.WebHost.UseUrls("http://localhost:5000");

var app = builder.Build();
app.MapHub<WorkHub>("/work");

// Start the server, then run a .NET client against it from the same process.
await app.StartAsync();
Console.WriteLine("Server listening on http://localhost:5000/work");

await using var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5000/work")
    .Build();

await connection.StartAsync();
Console.WriteLine("Client connected.");

// Canceling this token cancels the server-side invocation (new in .NET 11).
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));

try
{
    await connection.InvokeAsync("LongRunningWork", cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Client received the cancellation.");
}
finally
{
    await connection.DisposeAsync();
    await app.StopAsync();
}

public class WorkHub : Hub
{
    public async Task LongRunningWork(CancellationToken cancellationToken)
    {
        Console.WriteLine($"LongRunningWork started on connection {Context.ConnectionId}.");

        try
        {
            // The client's cancellation triggers this token.
            await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Server-side CancellationToken triggered by the client cancel.");
            throw;
        }
    }
}
