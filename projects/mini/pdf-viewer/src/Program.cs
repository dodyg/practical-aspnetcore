using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.StaticFiles;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }
        
        public void Configure(IApplicationBuilder app)
        {
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".properties"] = "application/octet-stream";
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });
            
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }


}