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
    new CultureInfo("fr-FR")
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
    var requestCulture = context.Features.Get<IRequestCultureFeature>();
    await context.Response.WriteAsync($"{requestCulture.RequestCulture.Culture} - {local["Hello"]} {local["Goodbye"]} {local["Yes"]} {local["No"]}");
});

app.Run();