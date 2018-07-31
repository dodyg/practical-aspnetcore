using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore;

namespace Configuration.Options 
{
    public class ApplicationOptions
    {
        public string Name { get; set; }

        public int MaximumLimit { get; set;}
    }

    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApplicationOptions>( o =>{
                o.Name = "Options Sample";
                o.MaximumLimit = 10;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            app.Run(context =>
            {
                var options = context.RequestServices.GetService<IOptions<ApplicationOptions>>();

                return context.Response.WriteAsync($"Settings Name : {options.Value.Name}  - Maximum limit : {options.Value.MaximumLimit}");
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