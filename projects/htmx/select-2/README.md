# hx-select

This example shows how to use `hx-select` with multiple selectors to pick up multiple elements from server response ([doc](https://htmx.org/attributes/hx-select/))

```html
 <ul hx-select:inherited="#result,#result2,.results">
    <li hx-get="/htmx">GET</li>
    <li hx-post="/htmx">POST</li>
    <li hx-put="/htmx">PUT</li>
    <li hx-patch="/htmx">PATCH</li>
    <li hx-delete="/htmx">DELETE</li>
  </ul>
```
