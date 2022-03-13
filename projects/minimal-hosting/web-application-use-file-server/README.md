# WebApplication - UseFileServer()

This sample uses the brand new `WebApplication` hosting class. ```app.UseFileServer();``` enables Kestrel to server static files and automatically look for the following files

* default.htm
* default.html
* index.htm
* index.html

at `wwwroot`.


You can read the implementation of ```WebApplication``` [here](https://github.com/dotnet/aspnetcore/blob/main/src/DefaultBuilder/src/WebApplication.cs) and its sibling ```WebApplicationBuilder``` [here](https://github.com/dotnet/aspnetcore/blob/main/src/DefaultBuilder/src/WebApplicationBuilder.cs)

