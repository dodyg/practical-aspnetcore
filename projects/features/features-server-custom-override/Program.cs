var app = WebApplication.Create();

app.Use(async (context, next) =>
   {
       context.Features.Set<ICustomFeature>(new CustomFeature("First greeting"));
       await next.Invoke();
   });

app.Use(async (context, next) =>
{
    var custom = context.Features.Get<ICustomFeature>();
    await context.Response.WriteAsync($"{custom?.Greetings}\n");
    context.Features.Set<ICustomFeature>(new CustomFeature("Second greeting"));
    await next.Invoke();
});

app.Run(context =>
{
    var custom = context.Features.Get<ICustomFeature>();
    return context.Response.WriteAsync($"{custom?.Greetings}");
});

app.Run();

interface ICustomFeature
{
    string Greetings { get; }
}

public class CustomFeature : ICustomFeature
{

    string _greetings;

    public CustomFeature(string greetings)
    {
        _greetings = greetings;
    }

    public string Greetings => _greetings;
}