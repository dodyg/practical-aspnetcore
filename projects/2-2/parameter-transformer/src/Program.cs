using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Text.RegularExpressions;

namespace EndpointRoutingSample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(options =>
                {
                    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.UseMvc();
        }
    }

    [Route("")]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public ActionResult Index()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                <h1>Parameter Transformer</h1>
                <p>
                    Now all your PascalCase action becomes kebab-case, e.g. AboutUs becomes about-us. Remember that the transformation happens only on the token replacement [controller] and [action].
                </p> 
                <ul>
                    <li><a href=""/"">/</a></li>
                    <li><a href=""/home/about-us"">/home/about-us</a </li>
                    <li><a href=""/home/order-items-now"">/home/order-items-now</a></li>
                    <li><a href=""/WeAreAmazing"">/WeAreAmazing</a></li>
                    <li><a href=""/get-this-offers-now"">/get-this-offers-now</a></li>
                </ul>
                </body></html> ",
                ContentType = "text/html"
            };
        }

        public ActionResult AboutUs()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                    <h2>About Us</h2>
                </ul>
                </body></html> ",
                ContentType = "text/html"
            };
        }

        public ActionResult OrderItemsNow()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                    <h2>Order Items Now</h2>
                </ul>
                </body></html> ",
                ContentType = "text/html"
            };
        }

        [Route("WeAreAmazing")]
        public ActionResult Random()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                    <h2>We are Amazing</h2>
                </ul>
                </body></html> ",
                ContentType = "text/html"
            };
        }
    }

    [Route("[controller]")]
    public class GetThisOffersNowController : Controller
    {
        public ActionResult TheNameOfThisActionDoesNotMatterBecauseThisIsTheOnlyOneInThisController()
        {
            return new ContentResult
            {
                Content = @"
                <html><body>
                    <h2>Get This Offers Now</h2>
                    This is an example where the transformation applied to the [controller] token.
                </ul>
                </body></html> ",
                ContentType = "text/html"
            };
        }
    }

    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            if (value == null)
                return null;

            return Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
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