# Enable timeout

Use `AddRequestTimeouts` to enable timeout functionality in your endpoints. Check `HttpContext.RequestAborted.IsCancellationRequested` to see if the request has timed out. 