# Use ShortCircuit

Use `MapShortCircuit` or `.ShortCircuit()` to efficiently respond to a request without going through a middleware pipeline run. It skips processes such as authentication, CORS, etc.