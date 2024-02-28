# Infer injected services from action parameter

In previous version of ASP.NET Core you have to decorate any dependency to your action method with `[FromServices]` attribute e.g. `public ActionResult Index([FromServices] Greetings greeting)`.

Now there is no need for the attribute anymore. This is enough ``public ActionResult Index(Greetings greeting)`.

You can disable this behavior by setting 
```csharp
Services.Configure<ApiBehaviorOptions>(options =>
{
     options.DisableImplicitFromServicesParameters = true;
})
```