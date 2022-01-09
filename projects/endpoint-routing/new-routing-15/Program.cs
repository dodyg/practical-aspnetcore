var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapGet("/", async context =>
{
    var endpoint = context.GetEndpoint();
    var greeting = endpoint.Metadata.GetMetadata<HelloWorld>();

    await context.Response.WriteAsync($"Greeting from metadata : {greeting.Greeting}");
}).WithMetadata(new HelloWorld { Greeting = "Hello World" });

app.Run();

public class HelloWorld
{
    public string Greeting { get; set; }
}

