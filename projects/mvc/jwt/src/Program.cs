using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;

namespace Jwt
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public SigningCredentials SigningCredentials { get; set; }
    }

    public class Startup
    {
        IConfiguration Configuration { get; set; }
        public Startup(IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = "SimpleServer";
                options.Audience = "http://localhost";
                options.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("1234567890")), SecurityAlgorithms.HmacSha256);

            });

            services.AddMvcCore().
                SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.UseMvc(routes =>
                routes.MapRoute(
                    name: "default_route",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" })
                );
        }
    }

    public class HomeController : Controller
    {
        readonly IOptions<JwtIssuerOptions> _options;
        public HomeController(IOptions<JwtIssuerOptions> options)
        {
            _options = options;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return new ContentResult
            {
                Content =
    $@"<html>
    <body>
    Audience: {_options.Value.Audience}
    <form action=""Jwt"" method=""post"">
        <button type=""submit"">Get Token</button>
    </form>
    </body>
</html>",
                ContentType = "text/html"
            };
        }

        [HttpPost]
        public ActionResult Jwt()
        {
            var content = "content";

            return new ContentResult
            {
                Content = content,
                ContentType = "text/html"
            };
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