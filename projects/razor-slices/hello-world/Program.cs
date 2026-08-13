var app = WebApplication.Create();

app.MapGet("/", () => HelloWorld.Slices.Index.Create("Hello world"));

app.Run();


