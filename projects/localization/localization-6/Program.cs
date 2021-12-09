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

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("greet-friend", async context =>
    {
        var fac = context.RequestServices.GetService<IStringLocalizerFactory>();
        var local = fac.Create("Greet Friend", string.Empty);

        var requestCulture = context.Features.Get<IRequestCultureFeature>().RequestCulture;

        await context.Response.WriteAsync($"Request Culture `{requestCulture.UICulture}` = {local["Hello"]}");
    });

    endpoints.MapGet("greet-lover", async context =>
    {
        var fac = context.RequestServices.GetService<IStringLocalizerFactory>();
        var local = fac.Create("Greet Lover", string.Empty);

        var requestCulture = context.Features.Get<IRequestCultureFeature>().RequestCulture;

        await context.Response.WriteAsync($"Request Culture `{requestCulture.UICulture}` = {local["Hello"]}");
    });

});

app.Run(async context =>
{
    var fac = context.RequestServices.GetService<IStringLocalizerFactory>();
    var local = fac.Create(string.Empty, string.Empty);

    var requestCulture = context.Features.Get<IRequestCultureFeature>().RequestCulture;

    context.Response.Headers.Add("Content-Type", "text/html");
    await context.Response.WriteAsync("<html><body>");
    await context.Response.WriteAsync($"Request Culture `{requestCulture.UICulture}` = {local["Hello"]}<br/><br/>");
    await context.Response.WriteAsync($@"<a href=""/greet-friend"">Greet Friend</a><br/>");
    await context.Response.WriteAsync($@"<a href=""/greet-lover"">Greet Lover</a><br/>");
    await context.Response.WriteAsync("</body></html>");
});

app.Run();