# Listen to htmx:response:error event to obtain AJAX error message

This example shows how to listen to `htmx:response:error` to obtain AJAX error message from the server([doc](https://htmx.org/events/#htmx:response:error))

This event is for HTTP error responses. Network, timeout, swap, and target failures are consolidated into `htmx:error`; see the [`htmx-error`](../htmx-error) sample.

```js
    document.addEventListener("htmx:response:error", (evt) => {
        console.log("event", evt);
        let response = evt.detail.ctx.response;
        let message = evt.detail.ctx.text?.trim() || "Request failed";
        alert(`HTTP ${response.status}: ${message}`);
    });
```
