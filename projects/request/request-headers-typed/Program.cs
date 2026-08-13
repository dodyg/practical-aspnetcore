var app = WebApplication.Create();

app.Run(context =>
{
    var typedHeaders = context.Request.GetTypedHeaders();

    var accept = typedHeaders.Accept is { Count: > 0 } ? typedHeaders.Accept[0].ToString() : "(none)";
    var acceptLanguage = typedHeaders.AcceptLanguage?.FirstOrDefault()?.Value ?? "(none)";

    return context.Response.WriteAsync($@"
There are more common HTTP headers properties available in HttpContext.Request.GetTypedHeaders()              
Accept: {accept}
Accept Language : {acceptLanguage}                
    ");
});

app.Run();