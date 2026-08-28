# hx-select

This example shows how to use `hx-select` to pick up specific element from server response ([doc](https://htmx.org/attributes/hx-select/))

```html
    <ul hx-select:inherited="#result">
        <li hx-get="/htmx">GET</li>
        <li hx-post="/htmx">POST</li>
        <li hx-put="/htmx">PUT</li>
        <li hx-patch="/htmx">PATCH</li>
        <li hx-delete="/htmx">DELETE</li>
    </ul>
```
