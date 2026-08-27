# Native OpenTelemetry tracing

ASP.NET Core now populates OpenTelemetry semantic convention attributes on the `Microsoft.AspNetCore` activity source natively. It is aligned with the [OTel HTTP server span spec](https://opentelemetry.io/docs/specs/semconv/http/http-spans/#http-server-span).

Previously these attributes were only available through the `OpenTelemetry.Instrumentation.AspNetCore` package.

Hit `/` and `/slow` a few times and watch the console: the request activity now carries `http.request.method`, `url.path`, `http.response.status_code`, `server.address`, and friends without any instrumentation package.
