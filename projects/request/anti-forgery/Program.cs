using Microsoft.AspNetCore.Antiforgery;

var builder = WebApplication.CreateBuilder();
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = "AntiForgery";
    options.Cookie.Domain = "localhost";
    options.Cookie.Path = "/";
    options.FormFieldName = "Antiforgery";
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

var app = builder.Build();

//These are the four default services available at Configure
app.Run(async context =>
{
    var antiForgery = context.RequestServices.GetService<IAntiforgery>();
    if (HttpMethods.IsPost(context.Request.Method))
    {
        await antiForgery.ValidateRequestAsync(context);
        await context.Response.WriteAsync("Response validated with anti forgery");
        return;
    }

    var token = antiForgery.GetAndStoreTokens(context);

    context.Response.Headers.Add("Content-Type", "text/html");
    await context.Response.WriteAsync($@"
    <html>
    <body>
        View source to see the generated anti forgery token
        <form method=""post"">
            <input type=""hidden"" name=""{token.FormFieldName}"" value=""{token.RequestToken}"" />
            <input type=""submit"" value=""Push""/>
        </form>
    </body>
    </html>   
    ");
});

app.Run();