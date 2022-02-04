var app = WebApplication.Create();
app.Run(async context =>
{
    var protocol = context.Request.IsHttps ? "https://": "http://";
    var host = protocol + context.Request.Host;
    context.Response.Headers["Content-Type"] = "text/html";

    await context.Response.WriteAsync($@"
        <html>
        <body>
            <h1>HttpContext.Request.Path</h1>
            <a href=""{host}/hello"" target=""_blank"">{host}/hello</a> <br/>
            <a href=""{host}//double-slash"" target=""_blank"">{host}//double-slash</a> <br/>
            <a href=""{host}/double-slash//version-2"" target=""_blank"">{host}/double-slash//version-2</a> <br/>
            <a href=""{host}/about-us/"" target=""_blank"">{host}/about-us/</a> <br/>
            <a href=""{host}/catalog/?id=10"" target=""_blank"">{host}/catalog/?id=10</a> <br/>
            <a href=""{host}/admin/index?secure=true"" target=""_blank"">{host}/admin/index?secure=true</a> <br/>
            <p>
                Value of HttpContext.Request.Path :
                { context.Request.Path }
            </p>
        </body>
        </html>
        ");
});

app.Run();