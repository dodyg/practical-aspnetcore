# hx-indicator

This example demonstrats how to show spinning indicators waiting for AJAX requests to complete([doc](https://htmx.org/attributes/hx-indicator/))

```html
<ul>
    <li hx-get="/htmx" hx-indicator="#spinner">GET</li>
    <li hx-post="/htmx" hx-indicator="#spinner">POST</li>
    <li hx-put="/htmx" hx-indicator="#spinner">PUT</li>
    <li hx-patch="/htmx" hx-indicator="#spinner">PATCH</li>
    <li hx-delete="/htmx" hx-indicator="#spinner">DELETE</li>
</ul>
<div id="spinner" class="htmx-indicator" role="status" aria-live="polite">
    <img src="/90-ring.svg" width="90" height="90" alt=""/>
    <span>Loading...</span>
</div>
```

The sample disables htmx 4's generated indicator stylesheet and supplies its own CSS. See the [`hx-pending`](../hx-pending) and [`hx-browser-indicator`](../hx-browser-indicator) samples for newer indicator options.
