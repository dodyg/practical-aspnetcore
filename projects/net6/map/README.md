# Map to JsonResult

This is a new functionality in ASP.NET Core to enable creating API without using Controller. You can track the progress of this functionality [here](https://github.com/dotnet/aspnetcore/issues/27347).

In this example we create two endpoints, `/` and `/about`.

`IEndpointConventionBuilder.Map` responds to all HTTP Verbs. 

This sample is using the latest daily build Roslyn compiler. Make sure you execute the following command in your terminal before trying to run the sample. 

```dotnet add package Microsoft.Net.Compilers.Toolset --prerelease   --source https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet-tools/nuget/v3/index.json```

