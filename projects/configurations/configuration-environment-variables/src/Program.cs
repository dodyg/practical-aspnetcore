using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore 
{
    public class Startup
    {
        IConfigurationRoot _config;

        public Startup()
        {
            //This is the most basic configuration you can have
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            _config = builder.Build();

        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync(_config.GetDebugView());
            });
        }
    }
    

}