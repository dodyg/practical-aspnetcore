# New Routing - use RequestDelegate directly

This example shows how to use `RequestDelegate` directly in `app.UseRouting` using `Map`. `Map` does not filter based on HTTP Verb, hence you will have to check the request Verb yourself. It's useful to handle request such as `PATCH` that does not have helper methods already.