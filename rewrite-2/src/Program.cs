using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;

namespace StartupBasic 
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {            
             var options = new RewriteOptions()
                .AddRedirect("([/_0-9a-z-]+)+(.*)$", "/?path=$1&ext=$2"); //redirect any path that ends with .html 

            app.UseRewriter(options);

            var routes = new RouteBuilder(app);
            routes.MapGet("", (context) => {
                context.Response.Headers.Add("content-type", "text/html");
                var path = context.Request.Query["Path"];
                var ext = context.Request.Query["Ext"];
                return context.Response.WriteAsync($@"Always display this page when path ends with an extension (e.g. .html or .aspx) and capture the their values. 
                For example <a href=""/hello-world.html"">/hello-world.html</a> or <a href=""/welcome/everybody/inthis/train.aspx"">/welcome/everybody/inthis/train.aspx</a>.
                <br/><br/>
                Query String ""path"" = {path}<br/>
                Query String ""ext"" = {ext}<br/>
                ");
            });

            app.UseRouter(routes.Build());
        }
    }
    
   public class Program
    {
        public static void Main(string[] args)
        {
              var host = new WebHostBuilder()
                .UseKestrel()
                .UseEnvironment("Development") //Change to 'Production' if you want to make it in production mode
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}