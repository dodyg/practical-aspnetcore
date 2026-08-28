# Listen to htmx:after:init event

This example shows how to listen to `htmx:after:init` ([doc](https://htmx.org/events/#htmx:after:init)).

> This event is triggered after an element has been initialized by HTMX. It fires during element processing, before any request is made.

```js
    document.addEventListener("htmx:after:init", (evt) => {
        let li = evt.target;
        alert(li.id);
    });
```
