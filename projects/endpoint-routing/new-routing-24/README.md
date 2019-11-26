# Host Matching on your middleware version 2

This example demonstrates on how to configure your endpoint to respond to a request from a specific host. In this example, GET `/` returns a different result depending whether you access it from `localhost:8111` and `localhost:8112`.

This produces the same exact effect as the [previous example](/projects/3-0/new-routing-23) except that here we use `IEndpointConventionBuilder.WithMetadata` and `HostAttribute` instead of `IEndpointConventionBuilder.RequireHost`.