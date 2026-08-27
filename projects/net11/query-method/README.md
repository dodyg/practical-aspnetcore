# HTTP QUERY method with a structured body

This sample demonstrates routing an [HTTP QUERY](https://datatracker.ietf.org/doc/draft-ietf-httpbis-safe-method-w-body/)
endpoint, a safe and idempotent HTTP method that carries a request body. It is useful for large or structured search queries that do not fit in a URL.

Because QUERY is treated as a *safe* method it works with `CacheOutput()`, is skipped by CSRF protection, and is counted in hosting metrics.

Browsers cannot send a QUERY body from a plain HTML form. It only works from an JavaScript call. 