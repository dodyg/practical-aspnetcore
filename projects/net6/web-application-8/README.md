# WebApplication - list all the configuration information available as JSON

This sample uses the brand new `WebApplication` hosting class. 

In this example we list all the information available in the `app.Configuration` property and return it as JSON.

## WARNING

Exposing your sensitive configuration information over the wire like this is a terrible idea. Do not do this in your application. This sample is just meant to demonstrate a technique. 

You can read the implementation of ```WebApplication``` [here](https://github.com/dotnet/aspnetcore/blob/main/src/DefaultBuilder/src/WebApplication.cs) and its sibling ```WebApplicationBuilder``` [here](https://github.com/dotnet/aspnetcore/blob/main/src/DefaultBuilder/src/WebApplicationBuilder.cs)

