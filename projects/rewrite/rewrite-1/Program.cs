using Microsoft.AspNetCore.Rewrite;

var app = WebApplication.Create();

var options = new RewriteOptions()
    .AddRedirect("/$", "/"); //redirect when path ends with /

app.UseRewriter(options);

app.MapGet("/", (context) =>
{
    context.Response.Headers.Add("content-type", "text/html");
    return context.Response.WriteAsync($@"<html><body>
    Always display this page when path ends with / e.g. <a href=""/hello-world/"">/hello-world/</a> or 
    <a href=""/welcome/everybody/inthis/train/"">/welcome/everybody/inthis/train/</a>.</body></html>");
});

app.Run();