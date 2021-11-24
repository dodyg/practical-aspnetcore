using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            //These are the three default services available at Configure
            app.Run(async context =>
            {
                context.Response.Headers.Add("content-type", "text/html");

                StringValues queryString = context.Request.Query["message"];

                await context.Response.WriteAsync("<html><body>");
                await context.Response.WriteAsync("<h1>Query String with a single value</h1>");
                await context.Response.WriteAsync(@"<a href=""?message=hello world"">click this link to add query string</a><br/><br/>");
                await context.Response.WriteAsync($"'Message' query string: {queryString}");
                await context.Response.WriteAsync("</body></html>");
            });
        }
    }


}