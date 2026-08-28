# Ask for confirmation using hx-confirm

This example shows how to use `hx-confirm` to ask for user confirmation before making a request ([doc](https://htmx.org/docs/#confirming))

```html
<ul hx-confirm:inherited="are you sure?">
    <li hx-get="/htmx">GET</li>
    <li hx-post="/htmx">POST</li>
    <li hx-put="/htmx">PUT</li>
    <li hx-patch="/htmx">PATCH</li>
    <li hx-delete="/htmx">DELETE</li>
</ul>
```

You can see here that `hx-confirm`, like many HTMX attributes, support inheritance. The HTML above has the same functional equivalent of the HTML below

```html
<ul>
    <li hx-get="/htmx" hx-confirm="are you sure?">GET</li>
    <li hx-post="/htmx" hx-confirm="are you sure?">POST</li>
    <li hx-put="/htmx" hx-confirm="are you sure?">PUT</li>
    <li hx-patch="/htmx" hx-confirm="are you sure?">PATCH</li>
    <li hx-delete="/htmx" hx-confirm="are you sure?">DELETE</li>
</ul>
```
