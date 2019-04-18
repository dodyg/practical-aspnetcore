# Razor Pages (3)

This section contains all micro samples for **ASP.NET Core Razor Pages 2.2**.

Pre-requisite: Make sure you download .NET Core SDK 2.2 (`2.2.100`) otherwise below examples won't work.

* [Hello World](/projects/razor-pages/hello-world)

  This is the simplest example for Razor Page. Razor Page by default routes `.cshtml` files with `@page` attribute under `/Pages`. So `/Pages/Index.cshtml` becomes `/` and `/Pages/AboutUs.cshtml` becomes `/AboutUs`. 

* [Routing](/projects/razor-pages/routing)

  Use `@page` directive on your Razor Page file to customize the url of your Razor Page. Each Razor Page can only contain 1 `@page` definition.


* [Routing-2](/projects/razor-pages/routing-2)

  Capture routing data from `@page` url template using `RouteData.Values[]`.
