using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace NewRouting
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(route =>
            {
                route.MapMethods("", new[] { "GET" }, async context =>
                {
                    var content =
@"<html>
<body>
    <h1>Hello World</h1>
    <form method=""post"" action=""/about"">
        <input type=""submit"" value=""POST"" />
    </form>
</body>
</html>";
                    await context.Response.WriteAsync(content);

                });

                route.MapMethods("about", new[] { "POST" }, async context =>
                {
                    var content =
@"<html>
<body>
    <h1>This page only supports POST method</h1>
    <p>
        If you try to retrieve this page directly, you will see nothing.
    </p>
</body>
</html>";
                    await context.Response.WriteAsync(content);

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
                {
                    webBuilder.UseStartup<Startup>().
                    UseEnvironment(Environments.Development);
                });
    }
}