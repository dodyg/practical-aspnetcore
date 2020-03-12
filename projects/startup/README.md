# Startup class

* [Startup basic](/projects/startup/startup-basic)

  This project contains all the available services available in Startup class constructor, `ConfigureServices` and `Configure` methods.
  
* [Environmental settings](/projects/startup/env-development)

  Set your application environment to `Development` or `Production` or other mode directly from code. 


* [Custom startup class name](/projects/startup/startup-custom-name)

  You don't have to call your startup class `Startup`. Any valid C# class will do.

* [Responding to multiple urls](/projects/startup/startup-basic-multiple-urls)

  Configure so that your web app responds to multiple urls.

* [Multiple startups](/projects/startup/startup-basic-multiple)

  This project highlights the fact that you can create multiple Startup classes and choose them at start depending on your needs. 

* [Multiple startups using environment](/projects/startup/startup-basic-multiple-environment)

  This project highlights the fact that you can create multiple startup classes and choose them at start depending on your needs depending on your environment (You do have to name the startup class with Startup). 

* [Multiple Configure methods based on environment](/projects/startup/startup-multiple-configure-environment)

  This project demonstrates the ability to pick `Configure` method in a single Startup class based on environment.

* [Multiple ConfigureServices methods based on environment](/projects/startup/startup-multiple-configure-environment-services)

  This project demonstrates the ability to pick `ConfigureServices` method in a single Startup class based on environment.

* [Not using startup class](/projects/startup/no-startup)

  Why? just because we can.

* [Using IStartupFilter](/projects/startup/startup-istartupfilter)

  Use `IStartupFilter` to configure your middleware. This is an advanced topic. [This article](https://andrewlock.net/exploring-istartupfilter-in-asp-net-core/) tries at explaining `IStartupFilter`. I will add more samples so `IStartupFilter` can be clearer.

* [Show errors during startup](/projects/startup/startup-capture-errors)

  Show a detailed report on exceptions that happen during the startup phase of your web app. It is very useful during development.


* [Supress Status Messages](/projects/startup/suppress-status-messages)
 
  You can hide status messages when you start up your web application. It's a small useful thing.
