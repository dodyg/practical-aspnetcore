using Microsoft.AspNetCore.Mvc.ViewFeatures;

var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorPages();
builder.Services.Remove<IHtmlGenerator, DefaultHtmlGenerator>();
builder.Services.AddTransient<IHtmlGenerator, LowerCaseIdHtmlGenerator>();

var app = builder.Build();
app.MapRazorPages();
app.Run();

public static class IServiceCollectionExtensions
{
    public static void Remove<TServiceType, TImplementationType>(this IServiceCollection services)
    {
        var serviceDescriptor = services.First(s => s.ServiceType == typeof(TServiceType) && s.ImplementationType == typeof(TImplementationType));
        services.Remove(serviceDescriptor);
    }
}
