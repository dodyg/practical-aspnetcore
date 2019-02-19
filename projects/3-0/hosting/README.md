# Generic Hosting

Your typical Program.cs will look like the following:

 ```
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>().
                UseEnvironment("Development");
            });
}
```    

Your hosting starts with `Host.CreateDefaultBuilder`. This is the source code to that [method](https://github.com/aspnet/Extensions/blob/master/src/Hosting/Hosting/src/Host.cs). 

It sets up for you already:

- Set ContentRoot to the current directory
- For the Host 
    - Add environmental variables that start with "DOTNET_".
- For the App
    - Add `appsetting.json`
    - Add `appsettings.{env.EnvironmentName}.json` for environment specific configuration
    - Add user secrets when you are in development mode
    - Add environmental variables for the App.
    - Add command line arguments
    - Configure Logging
      - Console
      - Debug logger
      - Event Source logger
- Configure `DefaultServiceProvider` `option.ValidateScopes`  (note: note sure what it means yet) in line with if the environment is `Development`.

Therefore once you set to this piece of code in this example, there are a lot of things that you don't need to configure.

```
Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        // do your things here
    });
```

All the things that you used to do in the previous versions, now you can do them in the context of `webBuilder`.