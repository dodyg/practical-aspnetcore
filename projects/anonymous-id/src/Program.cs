using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ReturnTrue.AspNetCore.Identity.Anonymous;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseAnonymousId(new AnonymousIdCookieOptionsBuilder()
                .SetCustomCookieName("Anoymous_Cookie_Tracker")
                //.SetCustomCookieRequireSsl(true) //Uncomment this in the case of usign SSL, such as the default setup of .NET Core 2.1 
                .SetCustomCookieTimeout(120)
            );

            app.Run(async context =>
            {
                IAnonymousIdFeature feature = context.Features.Get<IAnonymousIdFeature>();
                string anonymousId = feature.AnonymousId;

                await context.Response.WriteAsync($"Hello world with anonymous id {anonymousId}");
            });
        }
    }


}