# Async validation for minimal APIs

This sample demonstrates the new `Microsoft.Extensions.Validation` stack in
.NET 11: `AddValidation()`, `[ValidatableType]`, `AsyncValidationAttribute`
and `IAsyncValidatableObject`. Endpoints validate their bodies before running
and return `400` with `ProblemDetails` on failure; async rules run without
blocking a thread and concurrent where possible.


Try it:

Open <http://localhost:5000> in a browser for an interactive form that POSTs
to `/register` via JavaScript (no `curl` needed).

Or with curl:

```bash
# Invalid payload -> 400 problem+json with an `errors` entry
curl -X POST http://localhost:5000/register -H "Content-Type: application/json" \
  -d '{"email":"taken@example.com","password":"short"}'

# Valid payload -> 200
curl -X POST http://localhost:5000/register -H "Content-Type: application/json" \
  -d '{"email":"new@example.com","password":"a-strong-password","username":"bob"}'
```

