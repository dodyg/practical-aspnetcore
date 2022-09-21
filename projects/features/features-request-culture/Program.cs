using Microsoft.AspNetCore.Localization;

var app = WebApplication.Create();
app.UseRequestLocalization();

app.Run(async context =>
{
    var cultureFeature = context.Features.Get<IRequestCultureFeature>();
    await context.Response.WriteAsync($"Request culture: {cultureFeature?.RequestCulture.Culture.EnglishName}");
});

app.Run();
