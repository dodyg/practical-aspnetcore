using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ComponentTwentyFour;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.Services.AddCascadingValue("hello", sp => new MessageCascade("Hello World!"));
builder.Services.AddCascadingValue("goodbye", sp => new MessageCascade("Goodbye World!"));

var app = builder.Build();
await app.RunAsync();

public record MessageCascade(string Message);