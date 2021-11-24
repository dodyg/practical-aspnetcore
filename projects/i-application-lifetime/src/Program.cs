using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore 
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Hosting.IApplicationLifetime lifetime)
        {
        
            lifetime.ApplicationStarted.Register(() => System.Console.WriteLine("===== Server is starting"));
            lifetime.ApplicationStopping.Register(() => System.Console.WriteLine("===== Server is stopping"));
            lifetime.ApplicationStopped.Register(() => System.Console.WriteLine("===== Server has stopped"));
            
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello world");
            });
        }
    }
    

}