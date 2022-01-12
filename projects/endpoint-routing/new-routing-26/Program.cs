using Microsoft.AspNetCore.Mvc.Routing;

var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<NumberTransformer>();
builder.Services.AddRazorPages();

var app = builder.Build();
app.MapRazorPages();
app.MapDynamicPageRoute<NumberTransformer>("{number}");

app.Run();

public class NumberTransformer : DynamicRouteValueTransformer
{
    public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
    {
        if (!values.ContainsKey("number"))
            return new ValueTask<RouteValueDictionary>(values);

        var page = values["number"] switch
        {
            "1" => "/one",
            "2" => "/two",
            "3" => "/three",
            _ => "/undefined"
        };

        Console.WriteLine("Route Page");
        foreach (var k in values)
        {
            Console.WriteLine("Key" + k);
        }

        values["page"] = page;

        return new ValueTask<RouteValueDictionary>(values);
    }
}
