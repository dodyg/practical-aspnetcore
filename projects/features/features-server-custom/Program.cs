var app = WebApplication.Create();

app.Use(async (context, next ) =>{
    context.Features.Set<ICustomFeature>(new CustomFeature());
    await next.Invoke();
});

app.Run(context =>
{
    var custom = context.Features.Get<ICustomFeature>();
    if (custom == null)
        return context.Response.WriteAsync($"Custom is null");
    else
        return context.Response.WriteAsync($"{custom.Greetings}");
});

app.Run();

interface ICustomFeature 
{
    string Greetings {get;}
}

public class CustomFeature : ICustomFeature
{
    public string  Greetings => "This is my custom feature set from previous middleware";
}
