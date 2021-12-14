var builder = WebApplication.CreateBuilder();

//do not use services.AddEventAggregator 
builder.Services.AddScoped<EventAggregator.Blazor.IEventAggregator, EventAggregator.Blazor.EventAggregator>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapBlazorHub();
app.MapRazorPages();
app.MapFallbackToPage("/Index");

app.Run();
