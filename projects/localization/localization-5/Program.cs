using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder();
builder.Services.AddMemoryCache();
builder.Services.AddPortableObjectLocalization();

var app = builder.Build();

//We are limiting the supported culture here so this sample works in any browser from different culture setting.
//To make it pick up French or other language, simply change it-IT with something else or add more supported cultures
//I have added PO file for French and English
var supportedCultures = new List<CultureInfo>
{
    new CultureInfo("it-IT")
};

var option = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("it-IT"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

app.UseRequestLocalization(option);

app.Run(context =>
{
    var fac = context.RequestServices.GetService<IStringLocalizerFactory>();
    var local = fac.Create(string.Empty, string.Empty);

    var requestCulture = context.Features.Get<IRequestCultureFeature>().RequestCulture;

    return context.Response.WriteAsync($"Request Culture `{requestCulture.UICulture}` = {local["Hello"]}");
});

app.Run();