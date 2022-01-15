using Microsoft.AspNetCore.Localization;

var app = WebApplication.Create();
app.UseRequestLocalization();

//These are the three default services available at Configure
app.Run(async context =>
{
    var cultureFeature = context.Features.Get<IRequestCultureFeature>();
    await context.Response.WriteAsync($"Request culture : {cultureFeature.RequestCulture.Culture.EnglishName} ");
});

app.Run();