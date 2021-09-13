# Map to JsonResult

This is a new functionality in ASP.NET Core to enable creating API without using Controller. You can track the progress of this functionality [here](https://github.com/dotnet/aspnetcore/issues/27347).

In this example we create two endpoints, `/` and `/about`.

`IEndpointConventionBuilder.Map` responds to all HTTP Verbs. 

Note:

```<PackageReference Include="Microsoft.Net.Compilers.Toolset" Version="4.0.0-2.21269.40" />``` is temporary so we can use the latest unreleased version of C# compiler that support lambda improvements.

