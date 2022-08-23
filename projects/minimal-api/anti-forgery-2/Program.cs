using Microsoft.AspNetCore.Antiforgery;

var builder = WebApplication.CreateBuilder();
builder.Services.AddAntiforgery();
WebApplication app = builder.Build();

app.MapGet("/", (HttpContext context, IAntiforgery antiforgery) =>
{
    var token = antiforgery.GetAndStoreTokens(context);
    var html = $@"<!DOCTYPE html>
        <html>
            <body>
                <h1>Implement Antiforgery on AJAX call </h1>
                <button id=""fetchName"">Fetch</button>
                <script>
                    async function getName(){{
                        const response = await fetch(""/validate"", {{
                           method : ""POST"",
                           headers : {{ 
                            {token.HeaderName} : ""{token.RequestToken}"",
                            ""Content-Type"": ""application/json""
                            }}
                        }});

                        const result = await response.json();
                        alert(result.name);
                    }}

                    const btn = document.getElementById(""fetchName"");
                    btn.addEventListener(""click"", async () => {{
                       await getName();
                    }});
                </script>
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
        return Results.Ok(new { Name = "hi Anne"});
    }
    catch (Exception ex)
    {
        return Results.Problem(ex?.Message ?? string.Empty);
    }
});


app.Run();