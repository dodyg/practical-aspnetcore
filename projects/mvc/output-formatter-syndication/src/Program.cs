using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SyndicationFeed;
using System;
using Microsoft.SyndicationFeed.Rss;

namespace OutputFormatter
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            //These are three services available at constructor
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore(options =>
            {
                options.OutputFormatters.Add(new RssOutputFormatter());
            }).
                SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=HomePage}/{action=Index}");
            });
        }
    }

    public class HomePageController : Controller
    {
        public ActionResult Index()
        {
            var item = new SyndicationItem()
            {
                Title = "Rss Writer Available",
                Description = "The new Rss Writer is now available as a NuGet Package!",
                Id = "https://www.nuget.org/packages/Microsoft.SyndicationFeed.ReaderWriter",
                Published = DateTimeOffset.UtcNow
            };

            item.AddCategory(new SyndicationCategory("Technology"));
            item.AddContributor(new SyndicationPerson("test", "test@mail.com"));

            var item2 = new SyndicationItem()
            {
                Title = "We need RSS 'frame'",
                Description = "We need a structure that hold the RSS/feed information",
                Id = "xx",
                Published = DateTimeOffset.UtcNow
            };

            return Ok(new[] { item, item2 });
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