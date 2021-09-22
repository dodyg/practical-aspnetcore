# NUnit 1

This sample consists of two projects:
- a simple ASP.NET Core Hello world application 
- a NUnit test project that uses WebApplicationFactory

The test project uses the `WebApplicationFactory` to start an in-proc instance of the web application. The unit tests will query the web application via an instance of `HttpClient` that skips the wire and queries directly the web application.
