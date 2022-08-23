using Microsoft.AspNetCore.Antiforgery;

var builder = WebApplication.CreateBuilder();
builder.Services.AddAntiforgery();
WebApplication app = builder.Build();

app.MapGet("/", (HttpContext context, IAntiforgery antiforgery) =>
{
    var token = antiforgery.GetAndStoreTokens(context);
    var html = $@"
        <html>
            <body>
                <h1>Implement AntiForgery Manually on Form</h1>
                <form action=""/validate"" method=""post"">
                    <input name=""{token.FormFieldName}"" type=""hidden"" value=""{token.RequestToken}"" />
                    <input type=""text"" name=""name"" placeholder=""enter name"" /> <br/><br/>
                    <input type=""submit"" />
                </form> 
            </body>
        </html>
    ";

    return Results.Content(html, "text/html");
});

app.MapPost("/validate", async (HttpContext context, IAntiforgery antiforgery) =>
{
    try
    {
        await antiforgery.ValidateRequestAsync(context);
        return Results.Ok(context.Request.Form["name"]);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex?.Message ?? string.Empty);
    }
});


app.Run();