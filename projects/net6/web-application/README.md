# WebApplication - Welcome Page

This sample uses the brand new `WebApplication` hosting class. This simplifies configuring an ASP.NET Core application by a mile. `app.UseWelcomePage();` shows standard ASP.NET Core Welcome Page.


You can read the implementation of ```WebApplication``` [here](https://github.com/dotnet/aspnetcore/blob/main/src/DefaultBuilder/src/WebApplication.cs) and its sibling ```WebApplicationBuilder``` [here](https://github.com/dotnet/aspnetcore/blob/main/src/DefaultBuilder/src/WebApplicationBuilder.cs)

Note:

```<PackageReference Include="Microsoft.Net.Compilers.Toolset" Version="4.0.0-2.21269.40" />``` is temporary so we can use the latest unreleased version of C# compiler that support lambda improvements.

