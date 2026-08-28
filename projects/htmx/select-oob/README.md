# hx-select-oob

This example shows how to use `hx-select-oob` to pick up a specific element from server response and swap it with element of the same selection criteria ([doc](https://htmx.org/attributes/hx-select-oob/))

```html
  <ul hx-select:inherited="#result,#result2,.results">
      <li hx-get="/htmx" hx-select-oob="#result-get">GET</li>
      <li hx-post="/htmx" hx-select-oob="#result-post">POST</li>
      <li hx-put="/htmx" hx-select-oob="#result-put">PUT</li>
      <li hx-patch="/htmx" hx-select-oob="#result-patch">PATCH</li>
      <li hx-delete="/htmx" hx-select-oob="#result-delete">DELETE</li>
  </ul>
```
