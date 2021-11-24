using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SyndicationFeed;
using System;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.OutputFormatters.Add(new RssOutputFormatter());
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }

    public class HomeController : Controller
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


}