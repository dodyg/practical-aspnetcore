using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.ConfigureOptions(typeof(RazorClassLibrary1.UiConfigureOptions));
            services.ConfigureOptions(typeof(RazorClassLibrary2.UiConfigureOptions));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            app.Run(async (context) =>
            {
                context.Response.Headers.Add("Content-Type", "text/html");

                await context.Response.WriteAsync(@"<html>
                <body>
                <h1>Razor Class Library sample with static files (image, css, js)</h1>
                Visit page from <a href=""/module1"">RazorClassLibrary1</a> and <a href=""/module2"">RazorClassLibrary2</a>.
                </body>
                </html>");
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
