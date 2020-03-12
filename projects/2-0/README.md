# ASP.NET Core 2.0

Note that all the code here will run on ASP.NET Core 2.0 however these samples uses [2.1.700](https://dotnet.microsoft.com/download/dotnet-core/2.1).

## What's new in ASP.NET Core 2.0 (11)

  This section will show new things in [ASP.NET Core 2.0](https://github.com/aspnet/Home/releases/tag/2.0.0). This is a good explanation on [what's new on ASP.NET Core 2.0](https://blogs.msdn.microsoft.com/webdev/2017/08/25/asp-net-core-2-0-features-1/).

  * [IConfiguration is now core](iconfiguration)

    ASP.NET Core 1.1

    ```
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are the three default services available at Configure
            app.Run(context =>
            {
                return context.Response.WriteAsync('hello world');
            });
        }
    ```

    ASP.NET Core 2.0

    ```
      public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
      {
          //These are the four default services available at Configure
          app.Run(context =>
          {
              return context.Response.WriteAsync(configuration["greeting"]);
          });
      }
    ```

  * [Session Feature with Redis using JSON Serialization](features-session-redis-2)

    This shows how to use session with ```Redis``` store using Json instead of ```BinaryFormatter```.

    Make sure you have ```Redis``` running on your ```localhost``` at default port. The connection string is specified at ```appsetings.json```.
