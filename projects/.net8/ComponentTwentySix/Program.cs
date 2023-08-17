using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components;
using ComponentTwentySix;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.Services.AddCascadingValue(sp => 
{
    var msg = new MessageCascade("Hello World! " + DateTime.Now);
    var source = new CascadingValueSource<MessageCascade>("hello", msg, isFixed: false);
    return source;
});

var app = builder.Build();
await app.RunAsync();

public record MessageCascade(string Message);