using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Map("/hello", (IApplicationBuilder pp) => pp.Run(context => context.Response.WriteAsync("/hello path")));
            app.Map("/world", (IApplicationBuilder pp) => pp.Run(context => context.Response.WriteAsync("/world path")));

            app.Run(context =>
            {
                context.Response.Headers.Add("Content-Type", "text/html");
                return context.Response.WriteAsync(@"
                   <a href=""/hello"">hello</a> <a href=""/world"">world</a>
                ");
            });
        }
    }


}