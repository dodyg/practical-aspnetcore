# Uri Helper (5)
  
  This section shows various methods available at `Microsoft.AspNetCore.Http.Extensions.UriHelper`.

  * [Get Display Url](/projects/uri-helper/uri-helper-get-display-url) 

    `Request.GetDisplayUrl()` shows complete url with host, path and query string of the current request. It's to be used for display purposes only.

  * [Get Encoded Url](/projects/uri-helper/uri-helper-get-encoded-url)

    `Request.GetEncodedUrl()` returns the combined components of the request URL in a fully escaped form suitable for use in HTTP headers and other HTTP operations.

  * [Get Encoded Path and Query](/projects/uri-helper/uri-helper-get-encoded-path-and-query)

    `UriHelper.GetEncodedPathAndQuery` returns the relative URL of a request.

  * [From Absolute](/projects/uri-helper/uri-helper-from-absolute)

    `UriHelper.FromAbsolute` separates the given absolute URI string into components.

  * [Build Absolute](/projects/uri-helper/uri-helper-build-absolute)

    `UriHelper.BuildAbsolute` combines the given URI components into a string that is properly encoded for use in HTTP headers. This sample
    shows 9 ways on how to use it.

dotnet6