using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace StartupBasic 
{
   public class Program
   {
        public static void Main(string[] args)
        {
              var host = new WebHostBuilder()
                .UseKestrel()
                .Configure(app =>
                {
                    app.UseWelcomePage();
                })
                .Build();

            host.Run();
        }
    }
}