using OrchardCore.Modules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using Microsoft.AspNetCore.Http;

namespace Modules.HelloWorld 
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("hello world from module 1");
            });
        }
    }
}