# Start a Span and record open telemetry attributes

Start creating `ActivitySource`, start a span (`Activity`) with it and record attributes in the span.

```
Activity.TraceId:          042a24143870570039c50f3a44f8a0d9
Activity.SpanId:           18dff77ab7508b0e
Activity.TraceFlags:           Recorded
Activity.ActivitySourceName: OpenTelemetry.Instrumentation.AspNetCore
Activity.DisplayName: /
Activity.Kind:        Server
Activity.StartTime:   2022-06-23T08:10:25.6581553Z
Activity.Duration:    00:00:04.0630876
Activity.Tags:
    http.host: localhost:5000
    http.method: GET
    http.target: /
    http.url: http://localhost:5000/
    http.user_agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:101.0) Gecko/20100101 Firefox/101.0
    project: practical-aspnetcore
    location: Cairo
    http.status_code: 200
   StatusCode : UNSET
Activity.Events:
    Getting trace id [6/23/2022 8:10:27 AM +00:00]
    After showing trace id [6/23/2022 8:10:27 AM +00:00]
    After showing hello world [6/23/2022 8:10:29 AM +00:00]
Resource associated with Activity:
    service.name: open-telemetry-3
    service.instance.id: 09ce9038-822e-4733-8e20-b184f83b193c
```