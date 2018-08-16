using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp.Web.DependencyInjection;

namespace ImagerSharp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddImageSharp();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseImageSharp();
            app.UseStaticFiles();

            app.Run(async (context) =>
            {
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
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
