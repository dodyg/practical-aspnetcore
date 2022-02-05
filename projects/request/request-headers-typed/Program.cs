var app = WebApplication.Create();

app.Run(context =>
{
    var typedHeaders = context.Request.GetTypedHeaders();

    return context.Response.WriteAsync($@"
There are more common HTTP headers properties available in HttpContext.Request.GetTypedHeaders()              
Accept: {typedHeaders.Accept[0]}
Accept Language : {typedHeaders.AcceptLanguage.FirstOrDefault()?.Value}                
    ");
});

app.Run();