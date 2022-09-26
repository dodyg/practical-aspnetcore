using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder();
builder.Services.Configure<ArrayOptions>(builder.Configuration);
var app = builder.Build();

app.MapGet("/", (IOptions<ArrayOptions> options) => options.Value.Integers);

app.Run();

class ArrayOptions
{
    public int[] Integers { get; set; }
}