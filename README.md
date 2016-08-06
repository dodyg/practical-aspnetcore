The majority of the samples you see here involves mixed projects (net451) that will run only in Windows. For most of us .NET developers, this is the reality for forseeable future. We ain't gonna port multi years systems to run on Linux. We want to improve the creaky .NET MVC 2.0 that we have lying around and bring it up to speed to aspnetcore MVC.

All these projects require the following dependencies

```
  "Microsoft.AspNetCore.Hosting" : "1.0.0-*"
```

*This dependency pulls its own dependencies which you can check at project.lock.json. This allows us to not explicitly specify some dependencies ourselves.*

If a sample require additional dependencies, I will list them.

To run these samples, simply go each folder and execute ```dotnet restore``` and then continue with ```dotnet watch run```.

* [Hello World with reload](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-reload)

  Setup your most basic web app and enable the change+refresh development experience. 
  
  We are using ```IApplicationBuilder Run```, an extension method for adding terminal middleware.

* [Hello World with middlewares](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-middleware)

  ASPNetCore is built on top of pipelines of functions called middleware. 
  
  We are using ```IApplicationBuilder Use```, an extension method for adding middleware and ```IApplicationBuilder Run```.

* [Serve static files](https://github.com/dodyg/practical-aspnetcore/tree/master/serve-static-files)

  Simply serve static files (html, css, images, etc). 
  
  This additional dependency is required to enable the functionality ```"Microsoft.AspNetCore.StaticFiles": "1.0.0"```. 
  
  There are two static files being serve in this project, index.html and hello.css. They are stored under ```wwwroot``` folder, which is the default folder location for this library. 
  
  To access them you have to refer them directly e.g. ```localhost:5000/index.html``` and ```localhost:5000/hello.css```.

* [Serve static files - implemented as middleware component](https://github.com/dodyg/practical-aspnetcore/tree/master/serve-static-files-middleware)

  Simply serve static files (html, css, images, etc). The functionality is implemented in a middleware component. 
  
  This additional dependency is required to enable the functionality ```"Microsoft.AspNetCore.StaticFiles": "1.0.0"```. 
  
  There are two static files being serve in this project, index.html and hello.css. They are stored under ```wwwroot``` folder, which is the default folder location for this library. 
  
  To access them you have to refer them directly e.g. ```localhost:5000/index.html``` and ```localhost:5000/hello.css```.


* [Markdown server](https://github.com/dodyg/practical-aspnetcore/tree/master/markdown-server)

  Serve markdown file as html file. You will see how you can create useful app using a few basic facilities in aspnetcore.

  We take ```"CommonMark.Net" : "0.13.4"``` as dependency. 