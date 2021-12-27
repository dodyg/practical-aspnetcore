using Microsoft.AspNetCore.Rewrite;

var app = WebApplication.Create();

var options = new RewriteOptions().AddRedirect("([/_0-9a-zA-Z-]+)\\.([^/]+)$", "/?path=$1&ext=$2"); //redirect any path that ends with .html 

app.UseRewriter(options);

app.MapGet("", (context) =>
{
    context.Response.Headers.Add("content-type", "text/html");
    var path = context.Request.Query["Path"];
    var ext = context.Request.Query["Ext"];
    return context.Response.WriteAsync($@"<html><body>Always display this page when path ends with an extension (e.g. .html or .aspx) and capture the their values. 
                    For example <a href=""/hello-world.html"">/hello-world.html</a> or <a href=""/welcome/everybody/inthis/train.aspx"">/welcome/everybody/inthis/train.aspx</a>.
                    <br/><br/>
                    Query String ""path"" = {path}<br/>
                    Query String ""ext"" = {ext}<br/>
                    </body></html>");
});

app.Run();