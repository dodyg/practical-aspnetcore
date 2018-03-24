# MVC (1)

This section contains all micro samples for **ASP.NET Core MVC 2.1**.

Pre-requisite: Make sure you download .NET Core SDK 2.1 Preview 1 2.1.0-preview1-final otherwise below examples won't work.

* [Hello World Minimalistic](/projects/mvc/hello-world)
  This is the most basic hello world you can do using ASP.NET MVC. It uses `services.AddMvcCore()` with behavior compatibility set to `2.1` and with manually configured route.

  You shouldn't use this configuration for your typical ASP.NET MVC Core application (it does not support Razor, etc). This is just an example on how to do it with the least amount of functionality and configuration. You can find a discussion about `services.AddMvcCore()` [here](https://offering.solutions/blog/articles/2017/02/07/difference-between-addmvc-addmvcore/).
