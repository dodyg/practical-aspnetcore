using ChatR.Services;
using ChatR;

var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(); //https://github.com/dotnet/aspnetcore/blob/master/src/Components/Server/src/DependencyInjection/ComponentServiceCollectionExtensions.cs
builder.Services.AddScoped<NotificationService>();

var app = builder.Build();

app.UseStaticFiles();

app.MapBlazorHub(); //https://github.com/dotnet/aspnetcore/blob/master/src/Components/Server/src/Builder/ComponentEndpointRouteBuilderExtensions.cs
app.MapHub<NotificationHub>("/notificationhub");
app.MapRazorPages();
app.MapFallbackToPage("/Index");

app.Run();