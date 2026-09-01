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
<div id="spinner" class="htmx-indicator" role="status" aria-live="polite">
    <img src="/90-ring.svg" width="90" height="90" alt=""/>
    <span>Loading...</span>
</div>
```

The sample disables htmx 4's generated indicator stylesheet and supplies its own CSS. See the [`hx-pending`](../hx-pending) and [`hx-browser-indicator`](../hx-browser-indicator) samples for newer indicator options.
