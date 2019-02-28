# Hello World

This is the simplest Razor component app you can create. It will show you clearly the building block of a Razor component application.

There are two extra settings for dotnet watch to monitor `*.cshtml` file changes on two projects to make your development experience better.

``` xml
<Watch Include="..\HelloWorld.App\**\*.cshtml" />
<Watch Include="**\*.cshtml" />
```

Run the sample on `HelloWorld.Server` using `dotnet watch`.