var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorPages();

WebApplication app = builder.Build();
app.Urls.Add("https://localhost:5002");

app.MapRazorPages();
app.Run();