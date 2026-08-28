# Use hx-headers to pass values via HTTP headers

This example shows how to use `hx-headers` to pass values via HTTP headers ([doc](https://htmx.org/attributes/hx-headers/))

```html
<ul hx-headers:inherited='{ "X-NAME" : "Anna"}'>
    <li hx-get="/htmx">GET</li>
    <li hx-post="/htmx">POST</li>
    <li hx-put="/htmx">PUT</li>
    <li hx-patch="/htmx">PATCH</li>
    <li hx-delete="/htmx">DELETE</li>
</ul>
```
