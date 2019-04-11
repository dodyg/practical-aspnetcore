using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Antiforgery;

namespace StartupBasic 
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = "AntiForgery";
                options.Cookie.Domain = "localhost";
                options.Cookie.Path = "/";
                options.FormFieldName = "Antiforgery";
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration, IAntiforgery antiForgery)
        {
            //These are the four default services available at Configure
            app.Run(async context =>
            {
                if (HttpMethods.IsPost(context.Request.Method))
                {
                    await antiForgery.ValidateRequestAsync(context);        
                    await context.Response.WriteAsync("Response validated with anti forgery");
                    return;
                }
                
                var token = antiForgery.GetAndStoreTokens(context);
               
                context.Response.Headers.Add("ContentType", "text/html");
                await context.Response.WriteAsync($@"
                <html>
                <body>
                    View source to see the generated anti forgery token
                    <form method=""post"">
                        <input type=""hidden"" name=""{token.FormFieldName}"" value=""{token.RequestToken}"" />
                        <input type=""submit"" value=""Push""/>
                    </form>
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
            CreateBuildWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateBuildWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}