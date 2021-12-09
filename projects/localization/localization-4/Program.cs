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

app.Run(async context =>
{
    var languageSwitch = context.Request.Query["lang"];

    try
    {
        if (languageSwitch == "fr")
        {
            var val = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("fr-FR"));
            context.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, val);
            context.Response.Redirect("/");
            return;
        }
        else if (languageSwitch == "en")
        {
            var val = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("en-US"));
            context.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, val);
            context.Response.Redirect("/");
            return;
        }
    }
    catch (Exception ex)
    {
        await context.Response.WriteAsync($"error {ex.Message}");
    }

    await context.Response.WriteAsync($"<h1>CookieRequestCultureProvider</h1><p>We are using a cookie named \"{CookieRequestCultureProvider.DefaultCookieName}\" to switch the request culture.</p>");

    var requestCulture = context.Features.Get<IRequestCultureFeature>().RequestCulture;

    await context.Response.WriteAsync($@"
                Click <a href=""/?lang=fr"">here</a> for French cookie and <a href=""/?lang=en"">here</a> for English cookie.<br/><br/>
                
                Request Culture: {requestCulture.Culture} <br/> 
                Localized strings: {local["Hello"]} {local["Goodbye"]} {local["Yes"]} {local["No"]}
                ");
});

app.Run();