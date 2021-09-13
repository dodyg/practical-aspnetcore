# Logging Static File Servings

There may be a scenario where you need to log information about static file being served. This example shows you how.

```
public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
{
    app.UseStaticFiles(new StaticFileOptions {
        OnPrepareResponse = ctx => {
            logger.LogInformation($"Serving static file: {ctx.File.Name}");
        }
    });
}    
```

Contribution by [Lohit](https://github.com/lohithgn).