using Microsoft.AspNetCore.OutputCaching;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOutputCache();

// Replace the default policy provider with a dynamic one. This is useful for
// loading policies from configuration/DB or applying tenant-specific rules.
builder.Services.AddSingleton<IOutputCachePolicyProvider, TenantPolicyProvider>();

var app = builder.Build();

app.UseOutputCache();

app.MapGet("/", () => Results.Content($"""
    <html><body>
        <h1>Custom IOutputCachePolicyProvider</h1>
        <p>The base policy varies the cache key on the <code>X-Tenant</code> header.</p>
        <p>Generated at: {DateTime.UtcNow:O}</p>
        <p>
            Try: <code>curl -H "X-Tenant: a" http://localhost:5000/</code> twice,
            then with <code>X-Tenant: b</code> — each tenant gets its own cached copy.
        </p>
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
