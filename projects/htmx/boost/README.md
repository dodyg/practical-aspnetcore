# HTMX hx-boost

This example shows how to use `hx-boost` attribute [doc](https://htmx.org/attributes/hx-boost/). `hx-boost` make links and form tags to issue AJAX request and target the response to `body` tag.

```html
    <ul hx-boost:inherited="true">
        <li><a href="/htmx/one">One</a></li>
        <li><a href="/htmx/two">two</a></li>
    </ul>
```

