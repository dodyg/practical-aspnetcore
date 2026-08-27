# Endpoint filters observe parameter-binding failures

Now the filter pipeline so it runs even when parameter binding fails. A filter can inspect `HttpContext.Response.StatusCode == 400` and write its own response.

Notes:

- `ThrowOnBadRequest` must be `false` in Development, otherwise the request delegate throws instead of producing a 400 and the filter never sees it. It already defaults to `false` outside Development.
- The filter pipeline runs for **parameter** binding failures (route/query). A JSON **body** that fails to deserialize is still short-circuited before the filters run.
