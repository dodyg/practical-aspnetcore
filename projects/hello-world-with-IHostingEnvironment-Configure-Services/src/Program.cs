using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Options;

namespace StartupBasic
{
    public class SimpleOption
    {
        public string ContentPath { get; set; }
    }
    public class Startup
    {
        IHostingEnvironment _env;
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SimpleOption>(option =>
            {
                option.ContentPath = _env.ContentRootPath;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            app.Run(async context =>
            {
                context.Response.ContentType = "text/html";
                var simpleOption = context.RequestServices.GetService<IOptions<SimpleOption>>();

                await context.Response.WriteAsync($"{simpleOption.Value.ContentPath}");
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