using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore;

namespace MarkdownServer
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Run(context =>
            {
                env.ContentRootPath = Directory.GetCurrentDirectory();
                env.WebRootPath = Path.Combine(env.ContentRootPath, "markdown");

                var requestPath = context.Request.Path;

                //Get default page
                if (requestPath == "/")
                {
                    var defaultMd = Path.Combine(env.WebRootPath, "index.md");
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
                var md = Path.Combine(env.WebRootPath, localPath);
                if (!File.Exists(md))
                {
                    context.Response.StatusCode = 404;
                    return context.Response.WriteAsync("File Not Found");
                }

                context.Response.ContentType = "text/html";
                return context.Response.WriteAsync(ProduceMarkdown(md));
            });
        }

        string ProduceMarkdown(string path)
        {
            var md = File.ReadAllText(path);

            var res = Markdig.Markdown.ToHtml(md);
            return res;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}