using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore;

namespace Features.ServerRequest 
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next ) =>{
                var request = context.Features.Get<IHttpRequestFeature>();
                request.Headers.Add("greetings", "hello world");
                await next.Invoke();
            });

            app.Run(context =>
            {
                return context.Response.WriteAsync($"hello {context.Request.Headers["greetings"]}");
            });
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