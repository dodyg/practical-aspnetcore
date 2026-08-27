var builder = WebApplication.CreateBuilder(args);

// Let the request delegate produce a 400 on binding failure so the endpoint
// filter pipeline (and our filter below) gets a chance to run. The default
// behavior in Development is to throw, which would surface an exception page
// instead of letting the filter handle it.
builder.Services.Configure<RouteHandlerOptions>(o => o.ThrowOnBadRequest = false);

var app = builder.Build();

// id binds from the route; "abc" fails to parse as an int.
app.MapGet("/items/{id}", (int id) => Results.Ok(new { id }))
   .AddEndpointFilter(async (context, next) =>
   {
       var result = await next(context);

       // Binding failed: the delegate never ran, but the filter pipeline did.
       if (context.HttpContext.Response.StatusCode == 400)
       {
           return Results.Problem(
               "Invalid route parameter — custom message from the filter.",
               statusCode: 400);
       }

       return result;
   });

app.MapGet("/", () => Results.Text("""
    <html>
    <head>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@picocss/pico@2/css/pico.min.css">
    </head>
    <body class="container">
        <h1>Endpoint filters observe binding failures</h1>
        <p>
            When an endpoint has filters, the filter pipeline runs
            even if parameter binding fails. The filter can read
            <code>HttpContext.Response.StatusCode == 400</code> and substitute
            its own response.
        </p>
        <p>If you enter a number it will work, if not, you will see the error result</p>
        <p>
            <input id="id" value="abc" />
            <button onclick="test()">Send</button>
        </p>
        <pre id="output" style="min-height:300px;white-space:pre-wrap;"></pre>
        <script>
            async function test() {
                const id = document.getElementById('id').value;
                const res = await fetch(`/items/${id}`);
                document.getElementById('output').textContent =
                    `Status: ${res.status}\nBody: ${await res.text()}`;
            }
        </script>
    </body></html>
    """, "text/html")).ExcludeFromDescription();

app.Run();
