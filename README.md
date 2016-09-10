The majority of the samples you see here involve mixed projects (net451) that will run only in Windows. For most of us .NET developers, this is the reality for forseeable future. We are not going to port multi-year production systems to run on Linux. We want to improve the creaky .NET MVC 2.0 that we have lying around and bring it up to speed to aspnetcore MVC.

All these projects require the following dependencies

```
  "Microsoft.AspNetCore.Hosting" : "1.0.0-*"
```

*This dependency pulls its own dependencies which you can check at project.lock.json. This allows us to not explicitly specify some dependencies ourselves.*

If a sample require additional dependencies, I will list them.

I highly recommend using [Visual Studio Code](https://code.visualstudio.com/) to play around with these samples.

To run these samples, simply go each folder and execute ```dotnet restore``` and then continue with ```dotnet watch run```.

* [Hello World with reload](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-reload)

  Setup your most basic web app and enable the change+refresh development experience. 
  
  We are using ```IApplicationBuilder Run```, an extension method for adding terminal middleware.


* [Hello World with startup basic](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-startup-basic)

  This project contains all the available services available in Starup class constructor, ConfigureServices and Configure methods.


* [Hello World with console logging](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-logging)

  Setup a basic logging in your app and show it to console.

  We add the following dependencies ```"Microsoft.Extensions.Logging": "1.0.0"``` and ```"Microsoft.Extensions.Logging.Console": "1.0.0"```

  We are using ```IApplicationBuilder Run```, an extension method for adding terminal middleware.


* [Hello World with middlewares](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-middleware)

  ASPNetCore is built on top of pipelines of functions called middleware. 
  
  We are using ```IApplicationBuilder Use```, an extension method for adding middleware and ```IApplicationBuilder Run```.


* [Hello World with IApplicationLifetime](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-IApplicationLifetime)

  Respond to application startup and shutdown.

  We are using ```IApplicationLifetime``` that trigger events during application startup and shutdown.

* [Hello World with cookies](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-cookies)

  Simply read and write cookies.

* Hello world with routing.

  We take dependency on ```"Microsoft.AspNetCore.Routing" : "1.0.0-*"``` to enable routing facilities in your aspnetcore apps.
  There are several samples to illuminate this powerful library.

  * [Router](https://github.com/dodyg/practical-aspnetcore/tree/master/routing)
  
    A single route handler that handles every path request.

  * [Router 2](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-2)
  
    Two route handler, one for home page (/) and the other takes the rest of the request using asterisk (*) in the url template.

  * [Router 3](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-3)

    We are exploring default handler - this is the entry point to create your own framework.
    
  * [Router 4](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-4)

    We are mixing optional route parameter, route parameter with default value and default handler.

  * [Router 5]
    
    This is still broken. I am trying to figure out how to do nested routing. Wish me luck!
  
  * [Router 6](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-6)

    We are building a template route segment by segment and parts by parts, oldskool. We are using ```TemplateMatcher```, ```TemplateSegment``` and ```TemplatePart```. 

    Hold your mask, we are going deep.
  
  * [Router 7](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-7)

    We are creating a routing template with two segments, one with Literal part and the other Parameter part, e.g, "/page/{*title}"

  * [Router 8](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-8)

    We are creating a routing template with one segment consisted of two parts, one Literal and one Parameter, e.g. "/page{*title}". Note the difference between this example and Router 7.

  * [Router 9](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-9)
   
    I am still trying to determine whether `TemplateMatcher` uses the `InlineConstraint` information.

    Update: No, `TemplateMatcher` does not run constrants. [#362](https://github.com/aspnet/Routing/issues/362)
 
  * [Router 10](https://github.com/dodyg/practical-aspnetcore/tree/master/routing-10)    

    We have been building a `RouteTemplate` manually using `TemplateSegment` and `TemplatePart`. In this example we are using `TemplateParser` to build the `RouteTemplate` using string.

* Middleware

  * [Middleware 1](https://github.com/dodyg/practical-aspnetcore/tree/master/middleware-1)
   
    This example shows how to pass information from one middleware to another using `HttpContext.Items`.

* [Serve static files](https://github.com/dodyg/practical-aspnetcore/tree/master/serve-static-files)

  Simply serve static files (html, css, images, etc). 
  
  This additional dependency is required to enable the functionality ```"Microsoft.AspNetCore.StaticFiles": "1.0.0"```. 
  
  There are two static files being serve in this project, index.html and hello.css. They are stored under ```wwwroot``` folder, which is the default folder location for this library. 
  
  To access them you have to refer them directly e.g. ```localhost:5000/index.html``` and ```localhost:5000/hello.css```.

* [Markdown server](https://github.com/dodyg/practical-aspnetcore/tree/master/markdown-server)

  Serve markdown file as html file. You will see how you can create useful app using a few basic facilities in aspnetcore.

  We take ```"CommonMark.Net" : "0.13.4"``` as dependency. 
  
* [Markdown server - implemented as middleware component](https://github.com/dodyg/practical-aspnetcore/tree/master/markdown-server-middleware)

  Serve markdown file as html file. It has the same exact functionality as [Markdown server](https://github.com/dodyg/practical-aspnetcore/tree/master/markdown-server) but implemented using middleware component.

  We take ```"CommonMark.Net" : "0.13.4"``` as dependency. 

  [Check out](https://docs.asp.net/en/latest/migration/http-modules.html) the documentation on how to write your own middleware.

* [Password Hasher server](https://github.com/dodyg/practical-aspnetcore/tree/master/password-hasher)

  Give it a string and it will generate a secure hash for you, e.g. ```localhost:5000?password=mypassword```.

  We add dependency ```"Microsoft.AspNetCore.Identity": "1.0.0-*"``` to enable this functionality.


## Other resources

These are other aspnetcore resources with code samples

* [aspnetcore documentation](https://github.com/aspnet/Docs/tree/master/aspnet/fundamentals)