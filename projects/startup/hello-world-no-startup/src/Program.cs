using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

namespace StartupBasic 
{
   public class Greeter
   {
       public string Say() => "Look Ma, no Startup class";
   }    

   public class Program
   {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddSingleton(new Greeter()))
                .Configure(app =>
                {
                    app.Run(context =>
                    {
                        var greet = context.RequestServices.GetService<Greeter>();
                        return context.Response.WriteAsync($"{greet.Say()}");
                    });
                })
                .UseEnvironment("Development");
    }
}