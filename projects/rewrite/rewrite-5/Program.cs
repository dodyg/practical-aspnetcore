using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;

var app = WebApplication.Create();
app.UseRouting();

var options = new RewriteOptions()
   .Add(new ExtensionRedirection(".png", "/images/png"))
   .Add(new ExtensionRedirection(".jpg", "/images/jpeg"));

app.UseRewriter(options);
app.UseStaticFiles();

app.MapGet("", async context =>
{
    context.Response.Headers.Add("content-type", "text/html");

    await context.Response.WriteAsync($"<h1>Extension Based Redirection</h1><img src=\"ryan-wong-25025.jpg\" /> <br/> <img src=\"Acorn_PNG744.png\" />");
});

app.Run();

public class ExtensionRedirection : IRule
{
    readonly string _extension;
    readonly PathString _newPath;

    public ExtensionRedirection(string extension, string newPath)
    {
        _extension = extension;
        _newPath = new PathString(newPath);
    }

    public void ApplyRule(RewriteContext context)
    {
        var request = context.HttpContext.Request;

        // Because we're redirecting back to the same app, stop processing if the request has already been redirected
        // This is to prevent crazy loop. Try it, comment below code and you are going to crash.
        if (request.Path.StartsWithSegments(new PathString(_newPath)))
        {
            return;
        }

        if (request.Path.Value.EndsWith(_extension, StringComparison.OrdinalIgnoreCase))
        {
            var response = context.HttpContext.Response;
            response.StatusCode = StatusCodes.Status301MovedPermanently;
            context.Result = RuleResult.EndResponse;
            response.Headers[HeaderNames.Location] = _newPath + request.Path + request.QueryString;
        }
    }
}
