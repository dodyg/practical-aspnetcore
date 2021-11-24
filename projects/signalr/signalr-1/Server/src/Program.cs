using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Send", message);
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("all",
                    policy => policy.WithOrigins("http://localhost:5002")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowCredentials());
            });

            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("all");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chatHub");
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("This site host signalR server that allows remote clients to access thanks to CORS all policy.");
            });
        }
    }


}
