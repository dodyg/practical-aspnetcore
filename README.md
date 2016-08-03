aspnetcore is awesome but it can be a maddeninng experience. Yes it is modular but trying to build a brand new application and figure out what to include in package.json can be an exercise of futility.

The majority of the samples you see here involves mixed projects (net451) that will run only in Windows. For most of us .NET developers, this is the reality for forseeable future. We ain't gonna port multi years systems to run on Linux. We want to improve the creaky .NET MVC 3.0 that we had lying around and bring it to aspnetcore MVC.

* [Hello World with reload](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-reload)

  Setup your most basic web app and enable the change+refresh development experience. 
  
  We are using ```IApplicationBuilder.Run```.

* [Hello World with middlewares](https://github.com/dodyg/practical-aspnetcore/tree/master/hello-world-with-middleware)

  ASPNetCore is built on top of pipelines of functions called middleware (yeah, it looks exactly like Node/Express). 
  
  We are using ```IApplicationBuilder.Use``` and ```IApplicationBuilder.Run```.