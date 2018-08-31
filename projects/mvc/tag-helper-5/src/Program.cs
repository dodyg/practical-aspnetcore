using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace TagHelperSeries
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

    public enum AlertType
    {
        Success,
        Warning,
        Info,
        Danger
    }

    [HtmlTargetElement("danger")]
    public class DangerTagHelper : TagHelper
    {
        public override void Init(TagHelperContext context)
        {
            context.Items["AlertType"] = AlertType.Danger;
        }
    }

    [HtmlTargetElement("warning")]
    public class WarningTagHelper : TagHelper
    {
        public override void Init(TagHelperContext context)
        {
            context.Items["AlertType"] = AlertType.Warning;
        }
    }

    [HtmlTargetElement("alert")]
    public class AlertHelper : TagHelper
    {
        AlertType _type { get; set; } = AlertType.Info;

        public override void Init(TagHelperContext context)
        {
            if (context.Items.ContainsKey("AlertType"))
            {
                _type = (AlertType)context.Items["AlertType"];
            }
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", $"alert {GetAlertType()}");
        }

        string GetAlertType()
        {
            switch (_type)
            {
                case AlertType.Success: return "alert-success";
                case AlertType.Warning: return "alert-warning";
                case AlertType.Info: return "alert-info";
                case AlertType.Danger: return "alert-danger";
                default: throw new System.ArgumentOutOfRangeException();
            }
        }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
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