using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using System;

namespace MiddleWareLogRequestResponse
{
    // Code for this middleware is from here https://www.reddit.com/r/dotnet/comments/94jt95/core_21_httpcontextresponsebody_is_empty_when/
    public class LogMiddleware
    {
        RequestDelegate _next;

        IHostingEnvironment _env;

        public LogMiddleware(RequestDelegate next, IHostingEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                // To avoid logging twice via Chrome
                // https://github.com/aspnet/HttpAbstractions/issues/743
                if (context.Request.Path == "/favicon.ico")
                    return;

                string responseBodyAsString = await FormatResponse(context.Response);

                WriteToFile(responseBodyAsString);
                if (context.Response.StatusCode != 204)
                    await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"Response: {text}";
        }

        private void AddText(FileStream fs, string value)
        {
            byte[] info = new System.Text.UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private void WriteToFile(string responseBodyAsText)
        {
            string dt = DateTime.Now.ToString("hh_mm");
            string path = Path.Combine(_env.ContentRootPath, $"Logs/{dt}");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var ms = DateTime.Now.Millisecond; 
            string filePath = Path.Combine(_env.ContentRootPath, $"Logs/{dt}/{ms}.txt");
            using (FileStream fs = File.Create(filePath))
            {
                AddText(fs, responseBodyAsText);
            }
        }
    }

    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //These are two services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //These are the four default services available at Configure
            app.UseMiddleware<LogMiddleware>();

            app.Run(async context =>
            {
                context.Response.Headers.Add("content-type", "text/html");
                await context.Response.WriteAsync(@"
<html>
    <body>
        Hello World
    </body>
</html>                
                ");
            });
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