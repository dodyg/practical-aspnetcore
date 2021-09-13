# WebApplication - UseFileServer()

This sample uses the brand new `WebApplication` hosting class. ```app.UseFileServer();``` enables Kestrel to server static files and automatically look for the following files

* default.htm
* default.html
* index.htm
* index.html

at `wwwroot`.


You can read the implementation of ```WebApplication``` [here](https://github.com/dotnet/aspnetcore/blob/main/src/DefaultBuilder/src/WebApplication.cs) and its sibling ```WebApplicationBuilder``` [here](https://github.com/dotnet/aspnetcore/blob/main/src/DefaultBuilder/src/WebApplicationBuilder.cs)

Note:

```<PackageReference Include="Microsoft.Net.Compilers.Toolset" Version="4.0.0-2.21269.40" />``` is temporary so we can use the latest unreleased version of C# compiler that support lambda improvements.

