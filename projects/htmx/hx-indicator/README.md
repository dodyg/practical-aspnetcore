# hx-indicator

This example demonstrats how to show spinning indicators waiting for AJAX requests to complete([doc](https://htmx.org/attributes/hx-indicator/))

```html
<ul hx-indicator:inherited="#spinner">
    <li hx-get="/htmx">GET</li>
    <li hx-post="/htmx">POST</li>
    <li hx-put="/htmx">PUT</li>
    <li hx-patch="/htmx">PATCH</li>
    <li hx-delete="/htmx">DELETE</li>
</ul>
<img  id="spinner" class="htmx-indicator" src="/90-ring.svg"/>
```
