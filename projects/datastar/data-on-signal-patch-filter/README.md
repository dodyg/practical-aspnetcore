# data-on-signal-patch-filter

This sample shows how to use `data-on-signal-patch-filter` together with `data-on-signal-patch` to run an expression whether one or more **specific** signal patched. `patch` variable is available and contains the details of the patched signals.


``` html  
      <h1 data-init__delay.100ms="@get('/sse')" data-on-signal-patch="alert(JSON.stringify(patch))" data-on-signal-patch-filter="{include: /^greeting$/}">data-on-signal-patch-filter</h1>
```