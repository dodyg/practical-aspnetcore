# Listen to htmx:response:error event to obtain AJAX error message

This example shows how to listen to `htmx:response:error` to obtain AJAX error message from the server([doc](https://htmx.org/events/#htmx:response:error))

```js
    document.addEventListener("htmx:response:error", (evt) => {
        console.log("event", evt);
        alert(evt.detail.ctx.response.status + ":" + evt.detail.ctx.response.statusText);
    });
```
