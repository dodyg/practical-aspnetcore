# `[ShortCircuit]` attribute

This sample demonstrates the `[ShortCircuit]` attribute, the attribute form of the existing `ShortCircuit()` endpoint
convention. A marked endpoint runs immediately after routing, skipping the rest
of the middleware pipeline (auth, CORS, logging, ...). Perfect for health checks
or `robots.txt`.

An optional status code is supported: `[ShortCircuit(404)]`.

Verify that the marker middleware is skipped:

```bash
curl -i http://localhost:5000/health   # no X-Middleware-Ran header
curl -i http://localhost:5000/         # has X-Middleware-Ran: true
```