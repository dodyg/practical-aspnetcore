# In Memory Caching (a.k.a local cache) (5)

  These samples depends on `Microsoft.Extensions.Caching.Memory` library. 

  * [Caching - Absolute/Sliding expiration](/projects/caching/caching-1)

    This is the most basic caching you can use either by setting absolute or sliding expiration for your cache. Absolute expiration will remove your cache at a certain point in the future. Sliding expiration will remove your cache after period of inactivity.

  * [Caching 2 - File dependency](/projects/caching/caching-2)
    
    Add file dependency to your caching so when the file changes, your cache expires. Make sure to set `cache-file.txt` to copy over to bin.

  * [Caching 3 - Cache removal event](/projects/caching/caching-3)

    Register callback when a cached value is removed.

  * [Caching 4 - CancellationChangeToken dependency](/projects/caching/caching-4)

    Bind several cache entries to a single dependency that you can reset manually.

  * [Redis Caching Package](/projects/caching/redis-cache)

    This sample uses the `Microsoft.Extensions.Caching.StackExchangeRedis` to store caching value in Redis.

dotnet6