using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
namespace PracticalAspNetCore
{
    public class Startup
    {
        public record Greeting(string Message);

        IResult About() => Results.Json(new { about = "me" });

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
                
            app.UseEndpoints(endpoints =>
            {
                IResult Index() => Results.Json(new Greeting("Hello World"));

                endpoints.Map("/", Index);
                endpoints.Map("/about", About);
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}