using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Utf8Json;
using ut8json = Utf8Json.AspNetCoreMvcFormatter;
using Utf8Json.Resolvers;

namespace CustomJson
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
                SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddMvcOptions(option =>
                {
                    //Read more about resolver here https://github.com/neuecc/Utf8Json#resolver
                    option.OutputFormatters.Clear();
                    option.OutputFormatters.Add(new ut8json.JsonOutputFormatter(StandardResolver.ExcludeNullCamelCase));

                    option.InputFormatters.Clear();
                    option.InputFormatters.Add(new ut8json.JsonInputFormatter(StandardResolver.ExcludeNullCamelCase));
                });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, IConfiguration configuration)
        {
            app.UseMvc();
        }
    }

    [ApiController]
    public class HomeController : Controller
    {
        public enum Status
        {
            Cool,
            Hot
        }

        public class Message
        {
            public int Count { get; set; }
            public string Content { get; set; }
            public DateTime Date { get; set; }
            public bool IsActive { get; set; }
            public Dictionary<string, string> Keywords { get; set; }
            public List<string> Participants { get; set; }

            public string[] Countries { get; set; }

            public Status Status { get; set; }
        }

        [HttpGet("")]
        public ActionResult<Message> Index()
        {
            return new Message
            {
                Count = 10,
                Content = "Hello World",
                Date = DateTime.Now,
                IsActive = false,
                Keywords = new Dictionary<string, string>{
                    {"initial", "greet"},
                    {"end", "world"}
                },
                Participants = new List<string> {
                    "Dody",
                    "Anne"
                },
                Countries = new[] { "Egypt", "USA" },
                Status = Status.Cool
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