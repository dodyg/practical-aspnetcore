using Microsoft.AspNetCore.OutputCaching;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOutputCache();

// Replace the default policy provider with a dynamic one. This is useful for
// loading policies from configuration/DB or applying tenant-specific rules.
builder.Services.AddSingleton<IOutputCachePolicyProvider, TenantPolicyProvider>();

var app = builder.Build();

app.UseOutputCache();

app.MapGet("/", () => Results.Content($$"""
    <html>
    <head>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
    </head>
    <body class="container">
        <h1>Custom IOutputCachePolicyProvider</h1>
        <p>The base policy varies the cache key on the <code>X-Tenant</code> header.</p>
        <p>Generated at: <time id="generated-at">{{DateTime.UtcNow:O}}</time></p>
        <p>
            Try: <code>curl -H "X-Tenant: a" http://localhost:5000/</code> twice,
            then with <code>X-Tenant: b</code>. Each tenant gets its own cached copy.
        </p>
        <h2>Try it in the browser</h2>
        <p>Click the button to make the same requests with JavaScript.</p>
        <button id="run-test" type="button">Run tenant cache test</button>
        <pre id="test-results" style="min-height:100px;white-space:pre-wrap !important;">Test results will appear here.</pre>
        <script>
            const results = document.querySelector('#test-results');

            async function requestForTenant(tenant) {
                const response = await fetch('/', {
                    headers: { 'X-Tenant': tenant },
                    cache: 'no-store'
                });

                if (!response.ok) {
                    throw new Error(response.status + ' ' + response.statusText);
                }

                const html = await response.text();
                const document = new DOMParser().parseFromString(html, 'text/html');
                return document.querySelector('#generated-at')?.textContent ?? 'not found';
            }

            document.querySelector('#run-test').addEventListener('click', async () => {
                results.textContent = 'Running...';

                try {
                    const tenantATimestamps = [
                        await requestForTenant('a'),
                        await requestForTenant('a')
                    ];

                    const tenantBTimestamps = [
                        await requestForTenant('b'),
                        await requestForTenant('b')
                    ];

                    results.textContent = [
                        'Tenant a: ' + tenantATimestamps.join(' | '),
                        'Tenant b: ' + tenantBTimestamps.join(' | '),
                        '',
                        tenantATimestamps[0] === tenantATimestamps[1] &&
                        tenantBTimestamps[0] === tenantBTimestamps[1]
                            ? 'Each tenant received a cached response.'
                            : 'The timestamps did not match; try again.'
                    ].join('\n');
                } catch (error) {
                    results.textContent = 'Request failed: ' + error.message;
                }
            });
        </script>
    </body></html>
    """, "text/html")).CacheOutput();

app.Run();

/// <summary>
/// Returns a base policy that varies the cache entry on the X-Tenant header.
/// </summary>
public sealed class TenantPolicyProvider : IOutputCachePolicyProvider
{
    public IReadOnlyList<IOutputCachePolicy> GetBasePolicies() =>
        [new TenantVaryPolicy()];

    // No named policies in this sample — the convention picks the base policy.
    public ValueTask<IOutputCachePolicy?> GetPolicyAsync(string policyName) =>
        ValueTask.FromResult<IOutputCachePolicy?>(null);
}

/// <summary>
/// A custom policy: vary the cache key on the X-Tenant request header.
/// </summary>
public sealed class TenantVaryPolicy : IOutputCachePolicy
{
    public ValueTask CacheRequestAsync(OutputCacheContext context, CancellationToken cancellationToken)
    {
        context.CacheVaryByRules.HeaderNames = "X-Tenant";
        context.AllowCacheLookup = true;
        context.AllowCacheStorage = true;
        return ValueTask.CompletedTask;
    }

    public ValueTask ServeFromCacheAsync(OutputCacheContext context, CancellationToken cancellationToken) =>
        ValueTask.CompletedTask;

    public ValueTask ServeResponseAsync(OutputCacheContext context, CancellationToken cancellationToken) =>
        ValueTask.CompletedTask;
}
