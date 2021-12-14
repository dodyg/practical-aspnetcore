using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using OrchardCore.Localization;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder();

builder.Services.AddPortableObjectLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddSingleton<ILocalizationFileLocationProvider, MultiplePoFilesLocationProvider>();

var supportedCultures = new List<CultureInfo>
{
    new CultureInfo("fr-FR"),
    new CultureInfo("en"),
    new CultureInfo("fr"),
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("fr");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Clear();
    options.RequestCultureProviders.Add(new CookieRequestCultureProvider());
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.UseRequestLocalization();
app.MapRazorPages();
app.MapFallbackToPage("/Index");
app.MapBlazorHub();

app.Run();

// Leave this class empty. We use it to bind to resources.
public class Global
{

}

public class MultiplePoFilesLocationProvider : ILocalizationFileLocationProvider
{
    private readonly IFileProvider _fileProvider;
    private readonly string _resourcesContainer;

    public MultiplePoFilesLocationProvider(IHostEnvironment hostingEnvironment, IOptions<LocalizationOptions> localizationOptions)
    {
        _fileProvider = hostingEnvironment.ContentRootFileProvider;
        _resourcesContainer = localizationOptions.Value.ResourcesPath;
    }

    public IEnumerable<IFileInfo> GetLocations(string cultureName)
    {
        foreach (var file in Directory.EnumerateFiles(_resourcesContainer).Where(f => f.EndsWith(cultureName + ".po")))
        {
            yield return _fileProvider.GetFileInfo(file);
        }
    }
}