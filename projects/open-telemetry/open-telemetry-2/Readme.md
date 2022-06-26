# Record events into a current OTel span

Use existing ASP.NET Core OTel span to record events.

```
Activity.TraceId:          5e1f4ddfeecde7b52a3d7823fd2256a3
Activity.SpanId:           442681a750f843b8
Activity.TraceFlags:           Recorded
Activity.ActivitySourceName: OpenTelemetry.Instrumentation.AspNetCore
Activity.DisplayName: /
Activity.Kind:        Server
Activity.StartTime:   2022-06-21T08:01:16.0231599Z
Activity.Duration:    00:00:04.0263921
Activity.Tags:
    http.host: localhost:5000
    http.method: GET
    http.target: /
    http.url: http://localhost:5000/
    http.user_agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:101.0) Gecko/20100101 Firefox/101.0
    http.status_code: 200
   StatusCode : UNSET
Activity.Events:
    Getting trace id [6/21/2022 8:01:18 AM +00:00]
    After showing trace id [6/21/2022 8:01:18 AM +00:00]
    After showing hello world [6/21/2022 8:01:20 AM +00:00]
Resource associated with Activity:
    service.name: open-telemetry-2
    service.instance.id: 49c0f853-4ba3-4920-b2d5-6c1147c40402
```