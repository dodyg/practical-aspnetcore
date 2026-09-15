# htmx 4 request headers

htmx 4 identifies the source with `HX-Source` in `tagName#id` form, removes `HX-Trigger-Name`, adds `HX-Request-Type` (`full` or `partial`), and sends `Accept: text/html` for core requests. This sample echoes those headers.
