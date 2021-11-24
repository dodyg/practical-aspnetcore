using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            var arguments = new Dictionary<string, string>()
            {
                {"greetings", "hello-world"},
                {"origin", "cairo"}
            };

            var path = QueryHelpers.AddQueryString("/greet", arguments);
            var path2 = QueryHelpers.AddQueryString(path, "name", "annie");

            app.Run(async context =>
            {
                await context.Response.WriteAsync($"{path}\n{path2}");
            });
        }
    }


}