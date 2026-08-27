# Cache Blazor SSR output

 `CacheView` caches the rendered output of a server-side rendered
subtree. 

```html
<CacheView ExpiresAfter="TimeSpan.FromMinutes(10)"
           VaryByRoute="productId"
           VaryByQuery="page,pageSize"
           VaryByCulture="true
    <ExpensiveProductSummary ProductId="productId" />
</CacheView>
```
