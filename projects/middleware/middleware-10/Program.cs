using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(TerminalMiddleware));
        }
    }

    public class TerminalMiddleware
    {
        DateTime _date = DateTime.Now;

        public TerminalMiddleware(RequestDelegate next)
        {
        }

        public async Task Invoke(HttpContext context, ILogger<TerminalMiddleware> log)
        {
            log.LogDebug($"Request: {context.Request.Path}");
            context.Response.Headers.Add("Content-Type", "text/plain");
            await context.Response.WriteAsync($"Middleware is singleton. Keep refreshing the page. You will see that the date does not change {_date}.");
        }
    }


}
