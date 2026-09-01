# Using hx-vals on all HTTP verbs

This example shows how to use `hx-vals` to pass parameters to all supported HTTP verbs ([doc](https://htmx.org/attributes/hx-vals/))

```html
    <ul>
        <li hx-get="/htmx" hx-vals='{"Name": "Anna"}'>GET</li>
        <li hx-post="/htmx" hx-vals='{"Name": "Anna"}'>POST</li>
        <li hx-put="/htmx" hx-vals='{"Name": "Anna"}'>PUT</li>
        <li hx-patch="/htmx" hx-vals='{"Name": "Anna"}'>PATCH</li>
        <li hx-delete="/htmx" hx-vals='{"Name": "Anna"}'>DELETE</li>
    </ul>
```

On `GET` and `DELETE`, the parameters are accessible via `Request.Query`. For the rest, you can access the parameters via `Request.Form`.

Htmx 4 uses the `js:` prefix for computed values. The second panel sends `hx-vals="js:{ count: eventDetail() }"`; this replaces the removed `hx-vars` attribute.
