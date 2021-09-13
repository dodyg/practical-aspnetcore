using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

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

	public class HomeController : Controller 
	{
		IWebHostEnvironment _env;

		public HomeController(IWebHostEnvironment env)
		{
			_env = env;
		}
		
		/// <summary>
		/// If you navigate to /Home/JsonPersonResult
		/// You'll get a JSON response.
		///{"id":"Some guid","name":"Jane","lastName":"Doe"}
		/// </summary>
		public JsonResult JsonPersonResult() 
		{
			var person = new Person
			{
				Id = Guid.NewGuid(),
				Name = "Jane",
				LastName = "Doe"
			};
			return Json(person);
		}

        public ActionResult Index()
        {
			var jsonData = JsonPersonResult();

            return new ContentResult
            {
                Content = $@"<html>
							<body>
							<a href=""/Home/JsonPersonResult"">Click here</a> to view the endpoint with JsonResult
							</body>
						</html>",
                ContentType = "text/html"
            };
        }
	}

	public class Person 
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
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
