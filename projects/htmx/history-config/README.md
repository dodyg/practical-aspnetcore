# htmx history configuration

htmx 4 does not keep a localStorage history cache. Back/forward re-fetches the page and swaps into `body` or the `[hx-history-elt]` element. Use `htmx.config.history = "reload"` for full reloads or `false` to disable history handling.
