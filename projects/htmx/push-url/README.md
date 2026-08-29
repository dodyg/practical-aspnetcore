# hx-push-url attribute

This example shows how to use `hx-push-url` to push a URL into the browser location history ([HTMX 4 documentation](https://four.htmx.org/reference/attributes/hx-push-url/)).

In HTMX 4, attribute inheritance is explicit, so `:inherited` is required when `hx-push-url` is declared on a parent element.

```html
    <ul hx-push-url:inherited="true">
        <li hx-get="/htmx/get">GET</li>
        <li hx-post="/htmx/post">POST</li>
        <li hx-put="/htmx/put">PUT</li>
        <li hx-patch="/htmx/patch">PATCH</li>
        <li hx-delete="/htmx/delete">DELETE</li>
    </ul>
```

The sample uses the HTMX 4 CDN build. Clicking an item sends its request URL to the browser history and updates the address bar. When using history navigation, the pushed URL must also be able to return a complete page.
