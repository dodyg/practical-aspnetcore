using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

var app = WebApplication.Create();
MapEndpoints(app);
Pages.AllPages(app);

app.Run();

void MapEndpoints(IEndpointRouteBuilder endpoints)
{
    var header = "<html><body>";
    var footer = "</body></html>";

    endpoints.Map("/", () => 
        Results.Text(header +  @"<a href=""/about-us"">About Us</a><br/><a href=""/contact-us"">Contact Us</a>" + footer, "text/html") 
    );
}

public static class Pages
{
    public static void AllPages(IEndpointRouteBuilder endpoints)
    {
        endpoints.Map("/about-us", () => "About Us" );
        endpoints.Map("/contact-us", () => "Contact Us" );
    }
}
