var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.MapGet("/", (HttpContext context) => Results.Content($$"""
<html>
    <head>
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-0evHe/X+R7YkIZDRvuzKMRqM+OrBnVFBL6DOitfPri4tjfHxaWutUpFmBp4vmVor" crossorigin="anonymous">
    </head>
    <body>
        <div class="container">
            {{ context.Items["message"]}}
        </div>
    </body>
</html>
""", "text/html"))
    .AddEndpointFilter((context, next) =>
    {
       context.HttpContext.Items["message"] = "filter 1";
       var result = next(context); 
       context.HttpContext.Response.Headers["x-application"] += "filter 1 ";
       return result;
    })
    .AddEndpointFilter((context, next) =>
    {
       context.HttpContext.Items["message"] += " filter 2";
       var result = next(context); 
       context.HttpContext.Response.Headers["x-application"] += "filter 2 ";
       return result;
    })
    .AddEndpointFilter((context, next) =>
    {
       context.HttpContext.Items["message"] += " filter 3";
       var result = next(context);
       context.HttpContext.Response.Headers["x-application"] += "filter 3 ";
       return result; 
    });

app.Run();