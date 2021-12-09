using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder();
builder.Services.AddLocalization(options => options.ResourcesPath = "resources");

var app = builder.Build();

var stringLocalizerFactory = app.Services.GetService<IStringLocalizerFactory>();
var local = stringLocalizerFactory.Create("Common", typeof(Program).Assembly.FullName);

//This section is important otherwise aspnet won't be able to pick up the resource
var supportedCultures = new List<CultureInfo>
{
    new CultureInfo("fr-FR"),
    new CultureInfo("en-US")
};

var options = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("fr-FR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

app.UseRequestLocalization(options);

//These are the three default services available at Configure
app.Run(async context =>
{
    await context.Response.WriteAsync("<h1>QueryStringRequestCultureProvider</h1><p>We are using query string to switch the request culture.</p>");

    var requestCulture = context.Features.Get<IRequestCultureFeature>().RequestCulture;
    if (requestCulture.Culture.TwoLetterISOLanguageName != "fr")
        await context.Response.WriteAsync($"<a href=\"/?culture=fr-FR&ui-culture=fr-FR\">Switch to French</a><br/>");
    else if (requestCulture.Culture.TwoLetterISOLanguageName != "en")
        await context.Response.WriteAsync($"<a href=\"/?culture=en-US&ui-culture=en-US\">Switch to English</a><br/>");

    await context.Response.WriteAsync($@"
                Request Culture: {requestCulture.Culture} <br/> 
                Localized strings: {local["Hello"]} {local["Goodbye"]} {local["Yes"]} {local["No"]}
                ");
});

app.Run();