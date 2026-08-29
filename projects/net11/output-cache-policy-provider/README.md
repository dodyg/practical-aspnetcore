# Custom `IOutputCachePolicyProvider`

Preview 1 adds the `IOutputCachePolicyProvider` interface for dynamic output
cache policy selection — load policies from configuration or a database, or
apply tenant-specific rules.

```csharp
public interface IOutputCachePolicyProvider
{
    IReadOnlyList<IOutputCachePolicy> GetBasePolicies();
    ValueTask<IOutputCachePolicy?> GetPolicyAsync(string policyName);
}
```

This sample registers a provider whose base policy varies the cache key on the
`X-Tenant` header:

```csharp
builder.Services.AddSingleton<IOutputCachePolicyProvider, TenantPolicyProvider>();

public sealed class TenantPolicyProvider : IOutputCachePolicyProvider
{
    public IReadOnlyList<IOutputCachePolicy> GetBasePolicies() =>
        [new TenantVaryPolicy()];

    public ValueTask<IOutputCachePolicy?> GetPolicyAsync(string policyName) =>
        ValueTask.FromResult<IOutputCachePolicy?>(null);
}
```

The provider returns a custom `IOutputCachePolicy` that adds the tenant header
name to the vary-by rules:

Verify that each tenant gets its own cached copy (compare the generated
timestamps):

```bash
curl -H "X-Tenant: a" http://localhost:5000/
curl -H "X-Tenant: b" http://localhost:5000/
```

See the [Preview 1 release notes](https://github.com/dotnet/core/blob/main/release-notes/11.0/preview/preview1/aspnetcore.md#ioutputcachepolicyprovider).
