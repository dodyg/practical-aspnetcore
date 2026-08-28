# Using hx-on to handle HTMX events

This example shows how to use `hx-on` to handle HTMX events before AJAX request submitted and after AJAX request finished.

```html
    <ul hx-on::before:request="alert('before')" hx-on::after:request="alert('after')">
        <li hx-get="/htmx">GET</li>
        <li hx-post="/htmx">POST</li>
        <li hx-put="/htmx">PUT</li>
        <li hx-patch="/htmx">PATCH</li>
        <li hx-delete="/htmx">DELETE</li>
    </ul>
```

`hx-on` handles [`htmx:before:request`](https://four.htmx.org/reference/events/htmx-before-request) and [`htmx:after:request`](https://four.htmx.org/reference/events/htmx-after-request)
