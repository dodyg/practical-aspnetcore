using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

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
              var host = new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices(services => services.AddSingleton(new Greeter()))
                .Configure(app =>
                {
                    app.Run(context =>
                    {
                        var greet = context.RequestServices.GetService<Greeter>();
                        return context.Response.WriteAsync($"{greet.Say()}");
                    });
                })
                .Build();

            host.Run();
        }
    }
}