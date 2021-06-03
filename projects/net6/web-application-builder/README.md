# WebApplicationBuilder - MVC

In most cases using ```WebApplication``` isn't enough because you need to configure additional services to be used in your system. This is where ```WebApplicationBuilder``` comes. It allows you to configure services and other properties.

This example shows how to enable MVC.

You can read the implementation of ```WebApplication``` [here](https://github.com/dotnet/aspnetcore/blob/main/src/DefaultBuilder/src/WebApplication.cs) and its sibling ```WebApplicationBuilder``` [here](https://github.com/dotnet/aspnetcore/blob/main/src/DefaultBuilder/src/WebApplicationBuilder.cs)

Note:

```<PackageReference Include="Microsoft.Net.Compilers.Toolset" Version="4.0.0-2.21269.40" />``` is temporary so we can use the latest unreleased version of C# compiler that support lambda improvements.

