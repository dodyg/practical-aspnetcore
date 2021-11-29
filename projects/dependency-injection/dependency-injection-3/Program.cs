var builder = WebApplication.CreateBuilder();

//Register all objects configured by classes that implements IBootstrap.
//This is useful when you have large amount of classes in your project that needs
//registration. You can register them near where they are (usually in the same folder) instead of
//registering them somewhere in a giant registration function
var type = typeof(IBootstrap);
var types = System.AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(x => x.GetTypes())
    .Where(p => type.IsAssignableFrom(p) && p.IsClass);

foreach (var p in types)
{
    var config = (IBootstrap)System.Activator.CreateInstance(p);
    config.Register(builder.Services);
}

var app = builder.Build();

app.Run(context =>
{
    var person = context.RequestServices.GetService<Person>();
    var greeting = context.RequestServices.GetService<Greeting>();

    return context.Response.WriteAsync($"{greeting.Message} {person.Name}");
});

app.Run();

public interface IBootstrap
{
    void Register(IServiceCollection services);
}

public class Registration1 : IBootstrap
{
    public void Register(IServiceCollection services)
    {
        services.AddTransient(x => new Person { Name = "Mahmoud" });
        //continue registering all your classes here
    }
}

//This is a contrite sample but it demonstrates that you can have these two registration classes in farflung folders near the classes
//they are registrating.
public class Registration2 : IBootstrap
{
    public void Register(IServiceCollection services)
    {
        services.AddTransient(x => new Greeting { Message = "Good Morning" });
        //continue registering all your classes here
    }
}

public class Person
{
    public string Name { get; set; }
}

public class Greeting
{
    public string Message { get; set; }
}
