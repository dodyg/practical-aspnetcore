# Datastar (21)

The following samples show how to use [Datastar](https://data-star.dev/) hypermedia framework using .NET 10 and [pico](https://picocss.com) CSS framework.

* [Hello World](hello-world)
  
  Use `data-init` attribute with `@get` action to receive SSE event from a Minimal API endpoint that returns `datastar-patch-elements` SSE event type.

* [Backend SSE patch-signals](backend-patch-signals)

  This sample demonstrates how the backend can patch signals through SSE and how the UI reacts with it. The sample adds 3 seconds delays on the SSE response so the changes on the UI is visible.   

* [Backend SSE patch-signals 2](backend-patch-signals-2)

  Similar to previous sample except this time we add an extra signal and did not initialize any of the them.

* [Backend SSE patch-signals 3](backend-patch-signals-3)

  This sample shows how to use `filterSignals` option to only send specific signals to the backend.

* [Backend SSE patch-signals 3](backend-patch-signals-4)

  This sample shows the backend add one extra signal to be used for later action at the UI.

* [data-attr](data-attr)

  This example shows how to use `data-attr` to set HTML attribute to an expression. This sample demonstrates the usage of direct value, object and also signal expression.

* [data-bind](data-bind)

  This sample shows how to use `data-bind` to bind input (text, textarea, select, radio, checkbox, range) values to signals.

* [data-class](data-class)

  This example shows how to add or remove a class to or from an element based on an expression using `data-class`.

* [data-compute](data-compute)

  This example shows to create a read only signal that is computed based on expression.

* [data-effect](data-effect)

  This example shows to use `data-effect` to update other signals or perform operations.

* [data-on-click](data-on-click)

  This example shows to use `data-on:click` to update other signals or perform operations.

* [data-on-custom-event](data-on-custom-event)

  This example shows to use `data-on-{custom-event}` to handle custom even on three different occasion, local event, bubbling event, and event handle attached to `window`.

* [data-on-interval](data-on-interval)

  This example shows to use `data-on-interval` to run expression at regular time. It defaults to one second.

* [data-show](data-show)

  This example shows to use `data-show` to run expression to hide or show an element. 

* [data-style](data-style)

  This example shows how to set inline CSS style using `data-style`.

* [data-indicator](data-indicator)
  
  This sample shows how to use `data-indicator` to create a singal set to `true` while an SSE request is in flight.

* [data-on-signal-patch](data-on-signal-patch)
  
  This sample shows how to use `data-on-signal-patch` to run an expression whether one or more signals are patched. `patch` variable is available and contains the details of the patched signals.

* [data-on-signal-patch-filter](data-on-signal-patch-filter)

  This sample shows how to use `data-on-signal-patch-filter` together with `data-on-signal-patch` to run an expression whether one or more **specific** signal patched. `patch` variable is available and contains the details of the patched signals.

* [data-patch-elements-outer](data-patch-elements-outer)

  This example shows to use `data-init` with `@get` action to patch an element using `datastar-patch-elements` SSE event type (outer mode).

* [data-ignore](data-ignore)

  This example shows to use `data-ignore` to tell Datastar to skip an element and its descendants. 

* [data-ref](data-ref)

  This example shows to use `data-ref` to obtain a reference to the [HTML element](https://developer.mozilla.org/en-US/docs/Web/API/HTMLElement) itself.