using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

namespace HelloWorldWithCookies 
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var deleteCookie = context.Request.Query["delete"];

                if(!string.IsNullOrWhiteSpace(deleteCookie))
                {
                    context.Response.Cookies.Delete("MyCookie");
                    await context.Response.WriteAsync($"Delete cookie");
                    return;
                }

                var cookie = context.Request.Cookies["MyCookie"];

                if (string.IsNullOrWhiteSpace(cookie))
                {
                    context.Response.Cookies.Append
                    (
                        "MyCookie",
                        "Hello World",
                        new CookieOptions{
                            Path = "/",
                            Expires = DateTimeOffset.Now.AddDays(1)
                        }
                    );

                    await context.Response.WriteAsync($"Writing a new cookie <br/>");
                }

                await context.Response.WriteAsync($"Hello World Cookie: {cookie}. Refresh page to see cookie value.");
            });
        }
    }
    
   public class Program
    {
        public static void Main(string[] args)
        {
              var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}