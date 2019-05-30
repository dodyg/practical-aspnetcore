using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace RazorPageCustomHtmlGenerator
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //These are three services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().
                SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Remove<IHtmlGenerator, DefaultHtmlGenerator>();
            services.AddTransient<IHtmlGenerator, LowerCaseIdHtmlGenerator>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.UseMvc();
        }
    }

    public static class IServiceCollectionExtensions
    {
        public static void Remove<TServiceType, TImplementationType>(this IServiceCollection services)
        {
            var serviceDescriptor = services.First(s => s.ServiceType == typeof(TServiceType) && s.ImplementationType == typeof(TImplementationType));
            services.Remove(serviceDescriptor);
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