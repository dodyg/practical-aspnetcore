# htmx:error

htmx 4 consolidates network, timeout, swap, and target failures into `htmx:error`; inspect `event.detail.error`. HTTP 4xx/5xx responses still have the separate `htmx:response:error` event.
