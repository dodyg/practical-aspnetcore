# data-on-signal-patch

This sample shows how to use `data-on-signal-patch` to run an expression whether one or more signals are patched. `patch` variable is available and contains the details of the patched signals.


``` html  
    <h1 data-init__delay.100ms="@get('/sse')" data-on-signal-patch="alert(JSON.stringify(patch))">data-on-signal-patch</h1>
```