using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ComponentTwentyThree;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.Services.AddCascadingValue(sp => new MessageCascade("Hello World!"));

var app = builder.Build();
await app.RunAsync();

public record MessageCascade(string Message);