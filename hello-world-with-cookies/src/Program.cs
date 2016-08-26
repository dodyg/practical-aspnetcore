using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HelloWorldWithCookies 
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var cookie = context.Request.Cookies["MyCookie"];

                if (string.IsNullOrWhiteSpace(cookie))
                {
                    context.Response.Cookies.Append
                    (
                        "MyCookie",
                        "Hello World",
                        new CookieOptions
                        {
                            Path = "/",
                            HttpOnly = false,
                            Secure = false
                        }
                    );
                }

                return context.Response.WriteAsync($"Hello World Cookie: {cookie}. Refresh page to see cookie value.");
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