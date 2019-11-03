using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Host
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOrchardCore().AddMvc().WithTenants();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOrchardCore();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().
                    UseEnvironment("Development");
                });
    }
}
