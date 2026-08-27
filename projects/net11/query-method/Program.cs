var builder = WebApplication.CreateBuilder(args);

// QUERY is a *safe* method, so the endpoint plays well with output caching.
builder.Services.AddOutputCache();

var app = builder.Build();
app.UseOutputCache();

const string IndexHtml = """
<!DOCTYPE html>
<html>
<body>
    <h1>HTTP QUERY endpoint</h1>
    <p>
        Browsers cannot send a QUERY request from a plain <code>&lt;form&gt;</code> (HTML forms only
        support GET/POST), so this page issues the request with JavaScript:
    </p>
    <input id="query" value="api" />
    <button onclick="search()">Search</button>
    <pre id="result"></pre>
    <script>
        async function search() {
            const query = document.getElementById('query').value;
            const response = await fetch('/search', {
                method: 'QUERY', // a safe, idempotent method with a request body
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ query, maxResults: 5 })
            });
            document.getElementById('result').textContent =
                JSON.stringify(await response.json(), null, 2);
        }
    </script>
</body>
</html>
""";

app.MapMethods("/search", ["QUERY"], (SearchRequest request) => Results.Ok(SearchService.Run(request))).CacheOutput();

app.MapGet("/", () => TypedResults.Content(IndexHtml, "text/html")).ExcludeFromDescription(); 

app.Run();

public record SearchRequest(string Query, int MaxResults, string? Category);

public record SearchHit(int Rank, string Title, string Category);

public record SearchResponse(string Query, List<SearchHit> Results);

public static class SearchService
{
    private static readonly (string Title, string Category)[] Catalog =
    [
        ("Minimal APIs", "apis"),
        ("MVC", "apis"),
        ("Razor Pages", "web-ui"),
        ("Blazor", "web-ui"),
        ("SignalR", "realtime"),
        ("gRPC", "apis"),
        ("Output Caching", "performance"),
    ];

    public static SearchResponse Run(SearchRequest request)
    {
        var results = Catalog
            .Where(item => item.Title.Contains(request.Query, StringComparison.OrdinalIgnoreCase))
            .Take(request.MaxResults)
            .Select((item, index) => new SearchHit(index + 1, item.Title, item.Category))
            .ToList();

        return new SearchResponse(request.Query, results);
    }
}
