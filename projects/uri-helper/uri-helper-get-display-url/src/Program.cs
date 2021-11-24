using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            //These are the four default services available at Configure

            app.Run(context =>
            {
                context.Response.Headers.Add("Content-Type", "text/html");
                return context.Response.WriteAsync($@"<html>
<body>                
    <h1>Get Display Url</h1>
    <i>Returns the combined components of the request URL in a fully un-escaped form (except for the QueryString) suitable only for display. This format should not be used in HTTP headers or other HTTP operations.</i>
    <a href=""https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.extensions.urihelper.getdisplayurl?view=aspnetcore-3.1"">Doc</a><br/><br/>
    <p style=""color:red;"">{ context.Request.GetDisplayUrl() }</p>

    <p>Click on the links to see what the helper method shows</p>
    <ul>
        <li><a href=""/about/us"">/about/us</a></li>
        <li><a href=""/city/?id=30"">/city/?id=30</a></li>
        <li><a href=""/continent/?id=300&lat=30&lng=87"">/continent/?id=300&lat=30&lng=87</a></li>
        <li><a href=""/person?name=annie"">/person?name=annie</a></li>
        <li><a href=""/president?name=Franklin D. Roosevelt"">/president?name=Franklin D. Roosevelt</a></li>
        <li><a href=""/"">/</a></li>
    </ul>
</body>          
</html>         ");
            });
        }
    }


}