using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore 
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var host = context.Request.Host;
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