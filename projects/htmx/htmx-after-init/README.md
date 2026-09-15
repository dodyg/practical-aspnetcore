# htmx:after:init

This sample listens for htmx 4's `htmx:after:init` event after htmx initializes an element ([docs](https://four.htmx.org/reference/events/htmx-after-init)).

> This event is triggered after an element has been initialized by HTMX. It fires during element processing, before any request is made.

```js
    document.addEventListener("htmx:after:init", (evt) => {
        let li = evt.target;
        alert(li.id);
    });
```
