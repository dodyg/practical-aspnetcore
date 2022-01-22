var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = "markdown"        
});

var app = builder.Build();

app.Run(context =>
{
    var requestPath = context.Request.Path;

    //Get default page
    if (requestPath == "/")
    {
        var defaultMd = Path.Combine(app.Environment.WebRootPath, "index.md");
        if (!File.Exists(defaultMd))
        {
            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("File Not Found");
        }

        context.Response.ContentType = "text/html";
        return context.Response.WriteAsync(ProduceMarkdown(defaultMd));
    }

    //Replace the path and remove the beginning \ of the path
    //every request path segment represent a folder within markdown folder, e.g. 
    // /about/us is mapped to markdown\about\us.md File
    // /hello is mapped to markdown\hello.md

    var localPath = requestPath.ToString().Replace('/', '\\').TrimStart(new char[]{'\\'}) + ".md";
    var md = Path.Combine(app.Environment.WebRootPath, localPath);
    if (!File.Exists(md))
    {
        context.Response.StatusCode = 404;
        return context.Response.WriteAsync("File Not Found");
    }

    context.Response.ContentType = "text/html";
    return context.Response.WriteAsync(ProduceMarkdown(md));
});

string ProduceMarkdown(string path)
{
    var md = File.ReadAllText(path);

    var res = Markdig.Markdown.ToHtml(md);
    return res;
}

app.Run();