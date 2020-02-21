using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.Remove<IHtmlGenerator, DefaultHtmlGenerator>();
            services.AddTransient<IHtmlGenerator, LowerCaseIdHtmlGenerator>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
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
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}