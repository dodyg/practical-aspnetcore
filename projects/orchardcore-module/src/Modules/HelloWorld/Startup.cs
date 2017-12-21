using OrchardCore.Modules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using Microsoft.AspNetCore.Http;

namespace Modules.HelloWorld 
{
    public class Greet 
    {
        public string Hello => "Hello world";
    }
    
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<Greet>();
        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            app.UseRouter(r => 
            {
                r.MapGet("/",  async context => await context.Response.WriteAsync("Shit"));
            });
        }
    }
}