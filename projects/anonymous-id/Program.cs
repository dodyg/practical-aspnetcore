using ReturnTrue.AspNetCore.Identity.Anonymous;

var app = WebApplication.Create();

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

app.Run();