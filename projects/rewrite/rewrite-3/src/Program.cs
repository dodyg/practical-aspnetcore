using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            var options = new RewriteOptions()
               .AddRewrite("([/_0-9a-z-]+)+(.*)$", "/?path=$1&ext=$2", skipRemainingRules: false);  

            app.UseRewriter(options);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", (context) =>
                {
                    context.Response.Headers.Add("content-type", "text/html");
                    var path = context.Request.Query["Path"];
                    var ext = context.Request.Query["Ext"];
                    return context.Response.WriteAsync($@"
                    <h1>REWRITE</h1>
                    Always display this page when path ends with an extension (e.g. .html or .aspx) and capture the their values. 
                    For example <a href=""/hello-world.html"">/hello-world.html</a> or <a href=""/welcome/everybody/inthis/train.aspx"">/welcome/everybody/inthis/train.aspx</a>.
                    <br/><br/>
                    Query String ""path"" = {path}<br/>
                    Query String ""ext"" = {ext}<br/>
                    ");
                });
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}