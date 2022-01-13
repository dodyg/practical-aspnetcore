var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorPages();

var app = builder.Build();

app.MapRazorPages();
app.MapFallbackToAreaPage("/Other", "Admin");
app.MapFallbackToAreaPage("{segment}/{segment2}", "/OtherLevel", "Admin");
app.MapFallbackToAreaPage("{number:int}", "/OtherNumber", "Admin");

app.Run();