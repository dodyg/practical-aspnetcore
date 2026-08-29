using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization();
builder.Services.AddValidation();

builder.Services.AddSingleton<IStringLocalizerFactory, InMemoryStringLocalizerFactory>();
builder.Services.AddSingleton<IStringLocalizer>(InMemoryStringLocalizer.Instance);

var app = builder.Build();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = [new CultureInfo("en"), new CultureInfo("es")],
    SupportedUICultures = [new CultureInfo("en"), new CultureInfo("es")],
});

// Render the localized form.
app.MapGet("/", (IStringLocalizer l) =>
    Results.Content(RenderPage(l, name: "", email: "", errors: null, success: false), "text/html; charset=utf-8"));

// Switch culture, persist it in a cookie, and redirect back to the form.
app.MapPost("/culture", (HttpContext http, [FromForm] string culture, string? returnUrl) =>
{
    culture = culture == "es" ? "es" : "en";

    http.Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

    return Results.Redirect(returnUrl ?? "/");
});

// Validate the submitted customer and re-render the page with localized errors or a success message.
app.MapPost("/customers", async (HttpContext http, [FromForm][SkipValidation] CustomerModel customer, IStringLocalizer l) =>
{
    var errors = Validate(customer, http.RequestServices);
    var hasErrors = errors is { Count: > 0 };

    return Results.Content(
        RenderPage(l, customer.Name, customer.Email, hasErrors ? errors : null, !hasErrors), "text/html");
});

app.Run();

static IReadOnlyDictionary<string, IReadOnlyList<ValidationError>> Validate(object instance, IServiceProvider services)
{
    var options = services.GetRequiredService<IOptions<ValidationOptions>>().Value;
    var context = new ValidateContext
    {
        ServiceProvider = services,
        ValidationOptions = options,
        CurrentDepth = 0,
        CurrentValidationPath = null,
    };

    if (options.TryGetValidatableTypeInfo(instance.GetType(), out var info))
    {
        info.Validate(instance, context);
    }

    return context.ValidationErrors;
}

static string RenderPage(
    IStringLocalizer l,
    string name,
    string email,
    IReadOnlyDictionary<string, IReadOnlyList<ValidationError>>? errors,
    bool success)
{
    var lang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "es" ? "es" : "en";

    string FieldError(string key) => errors is not null &&
        errors.TryGetValue(key, out var list) && list is { Count: > 0 } && !string.IsNullOrEmpty(list[0].ErrorMessage)
            ? $"<span class=\"error\">{WebUtility.HtmlEncode(list[0].ErrorMessage)}</span>"
            : string.Empty;

    string SelectOption(string value, string label) =>
        $"<option value=\"{value}\"{(value == lang ? " selected" : "")}>{label}</option>";

    var errorSummary = errors is { Count: > 0 }
        ? $"<div class=\"errors\"><strong>{WebUtility.HtmlEncode(l["ErrorsTitle"])}</strong></div>"
        : string.Empty;

    var successHtml = success
        ? $"<p class=\"success\">{WebUtility.HtmlEncode(l["Success"])}</p>"
        : string.Empty;

    return $$"""
        <!DOCTYPE html>
        <html lang="{{lang}}">
        <head>
        <meta charset="utf-8" />
        <title>{{WebUtility.HtmlEncode(l["PageTitle"])}}</title>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
        </head>
        <body>
        <header class="container">
          <form method="post" action="/culture">
            <label for="culture">{{WebUtility.HtmlEncode(l["Language"])}}</label>
            <select id="culture" name="culture" onchange="this.form.submit()">
              {{SelectOption("en", "English")}}
              {{SelectOption("es", "Español")}}
            </select>
          </form>
        </header>
        <main class="container">
          <div class="card">
            <h1>{{WebUtility.HtmlEncode(l["PageTitle"])}}</h1>
            {{successHtml}}
            {{errorSummary}}
            <form method="post" action="/customers">
              <div class="field">
                <label for="Name">{{WebUtility.HtmlEncode(l["CustomerName"])}}</label>
                <input id="Name" name="Name" value="{{WebUtility.HtmlEncode(name)}}" />
                {{FieldError("Name")}}
              </div>
              <div class="field">
                <label for="Email">{{WebUtility.HtmlEncode(l["CustomerEmail"])}}</label>
                <input id="Email" name="Email" value="{{WebUtility.HtmlEncode(email)}}" />
                {{FieldError("Email")}}
              </div>
              <button type="submit">{{WebUtility.HtmlEncode(l["Submit"])}}</button>
            </form>
          </div>
        </main>
        </body>
        </html>
        """;
}

[ValidatableType]
public class CustomerModel
{
    // "CustomerName" / "NameRequired" are localization keys, not literal strings.
    [Display(Name = "CustomerName")]
    [Required(ErrorMessage = "NameRequired")]
    public string? Name { get; set; }

    [Display(Name = "CustomerEmail")]
    [EmailAddress(ErrorMessage = "EmailInvalid")]
    public string? Email { get; set; }
}

public sealed class InMemoryStringLocalizerFactory : IStringLocalizerFactory
{
    public IStringLocalizer Create(Type resourceSource) => InMemoryStringLocalizer.Instance;

    public IStringLocalizer Create(string baseName, string location) =>
        InMemoryStringLocalizer.Instance;
}

public sealed class InMemoryStringLocalizer : IStringLocalizer
{
    public static readonly InMemoryStringLocalizer Instance = new();

    private static readonly IReadOnlyDictionary<string, string> En = new Dictionary<string, string>
    {
        ["CustomerName"] = "Name",
        ["CustomerEmail"] = "Email",
        ["NameRequired"] = "The name is required.",
        ["EmailInvalid"] = "The email address is not valid.",
        ["PageTitle"] = "Customer registration",
        ["Language"] = "Language",
        ["Submit"] = "Submit",
        ["ErrorsTitle"] = "Please fix the following:",
        ["Success"] = "Customer saved.",
    };

    private static readonly IReadOnlyDictionary<string, string> Es = new Dictionary<string, string>
    {
        ["CustomerName"] = "Nombre",
        ["CustomerEmail"] = "Correo",
        ["NameRequired"] = "El nombre es obligatorio.",
        ["EmailInvalid"] = "El correo no es válido.",
        ["PageTitle"] = "Registro de clientes",
        ["Language"] = "Idioma",
        ["Submit"] = "Enviar",
        ["ErrorsTitle"] = "Corrija lo siguiente:",
        ["Success"] = "Cliente guardado.",
    };

    public LocalizedString this[string name]
    {
        get
        {
            var table = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "es" ? Es : En;
            return table.TryGetValue(name, out var value)
                ? new LocalizedString(name, value, resourceNotFound: false)
                : new LocalizedString(name, name, resourceNotFound: true);
        }
    }

    public LocalizedString this[string name, params object[] arguments] => this[name];

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) =>
        En.Select(kv => new LocalizedString(kv.Key, kv.Value));

    public IStringLocalizer WithCulture(CultureInfo culture) => this;
}
