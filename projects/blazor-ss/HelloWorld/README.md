# Hello World

This is the simplest Razor component app you can create. It will show you clearly the building block of a Razor component application.

There are two extra settings for dotnet watch to monitor `*.cshtml` and `*.razor` file changes on two projects to make your development experience better.

``` xml
    <Watch Include="**\*.cshtml" />
    <Watch Include="**\*.razor" />
```

Run the sample on `HelloWorld` using `dotnet watch`.