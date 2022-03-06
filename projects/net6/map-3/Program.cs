IResult TryContext(MyData data) => Results.Json(new { greetings = $"Hello {data.Name}" });

var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<MyData>();

var app = builder.Build();
app.Map("/", TryContext);
app.Run();

public class MyData 
{
    public string Name => "Anne";
}
