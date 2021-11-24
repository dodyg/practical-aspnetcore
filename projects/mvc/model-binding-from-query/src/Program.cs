using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
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

    public class GreetingParams
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public bool IsAmazing { get; set; }

        public short? Age { get; set; }

        public override string ToString() => $"User Id: {UserId}, Name: {Name}, Is Amazing: {IsAmazing}, Age: {Age}";
    }

    public class HomeController : Controller
    {
        public ActionResult Index([FromQuery] GreetingParams greet)
        {
            return new ContentResult
            {
                Content = $@"<html><body>
                <h1>Class binding with [FromQuery]</h1>
                <p>You can see the difference in behavior between the nullable type and non nullable types here. Age is short? and User Id is int.<p>
                {greet}
                <ul>
                    <li><a href=""/"">/</a></li>
                    <li><a href=""/?name=annie"">/?name=annie</a></li>
                    <li><a href=""/?isamazing=true"">/?isamazing=true</a></li>
                    <li><a href=""/?userid=1"">/?userid=1</a></li>
                    <li><a href=""/?age=33"">/?age=33</a></li>
                    <li><a href=""/?userid=1&name=annie&isamazing=true"">/?userid=1&name=annie&isamazing=true&age=33</a></li>
                </ul>
                </body></html>",
                ContentType = "text/html"
            };
        }
    }


}