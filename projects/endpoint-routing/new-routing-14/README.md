# New Routing - Obtaining Endpoint feature via IEndpointFeature

Use `HttpContext.Features.Get<IEndpointFeature>();` to obtain `Endpoint` information for a given Middleware. You can accomplish the same thing using `HttpContext.GetEndpoint`.