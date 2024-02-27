using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ComponentTwentySeven;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.Services.AddKeyedSingleton<IMessage>("msg1", (_, _) => new Msg("Hello world"));
builder.Services.AddKeyedSingleton<IMessage>("msg2", (_, _) => new Msg("Goodbyeworld"));

var app = builder.Build();
await app.RunAsync();


public interface IMessage 
{
    string Message { get; } 
}


public class Msg(string message) : IMessage
{
    public string Message => message;
}

