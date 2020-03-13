using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp.Web.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddImageSharp();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseImageSharp();
            app.UseStaticFiles();

            app.Run(async context =>
            {
                context.Response.Headers.Add("Content-Type", "text/html");
                await context.Response.WriteAsync($@"
<html>
<body>
    <h1>Imager</h1>
    <blockquote>siwa.jpg?width=200</blockquote>
    <img src=""siwa.jpg?width=200"" />
    <br/>
    <blockquote>siwa.jpg?height=200</blockquote>
    <img src=""siwa.jpg?height=300"" />
    <br/>
    <blockquote>siwa.jpg</blockquote>
    <br/>
    <img src=""siwa.jpg"" />
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
