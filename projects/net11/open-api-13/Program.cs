using System.Net.ServerSentEvents;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// OpenAPI 3.2 is the default generated version in .NET 11
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.MapGet("/", () => Results.Text("""
    <html><body>
        <h1>OpenAPI 3.2 + Server-Sent Events itemSchema</h1>
        <ul>
            <li><a href="/openapi/v1.json">OpenAPI document</a></li>
            <li><a href="/todos/stream">GET /todos/stream</a> a text/event-stream endpoint</li>
        </ul>
    </body></html>
    """, "text/html")).ExcludeFromDescription();

app.MapGet("/todos/stream", (CancellationToken ct) => TypedResults.ServerSentEvents(GetTodosAsync(ct)))
    .WithName("StreamTodos");

app.Run();

static async IAsyncEnumerable<SseItem<Todo>> GetTodosAsync([EnumeratorCancellation] CancellationToken ct = default)
{
    foreach (var todo in Todos.All)
    {
        yield return new SseItem<Todo>(todo) { EventId = todo.Id.ToString() };
        await Task.Delay(1000, ct);
    }
}

public record Todo(int Id, string Title, bool Done);

public static class Todos
{
    public static readonly List<Todo> All = [
        new(1, "Learn ASP.NET Core 11", false), 
        new(2, "Write a micro sample", true)
        ];
}
