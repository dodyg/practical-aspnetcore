using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Runtime.InteropServices;
using System.Reflection;

namespace VersionInfo
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                context.Response.Headers["Content-Type"] = "text/html";

                await context.Response.WriteAsync("<html><body>");
                await context.Response.WriteAsync("<h1>.NET Core Info</h1>");
                await context.Response.WriteAsync($"Environment.Version: {Environment.Version}");
                await context.Response.WriteAsync("<br/>");
                await context.Response.WriteAsync($"RuntimeInformation.FrameworkDescription: {RuntimeInformation.FrameworkDescription}");
                await context.Response.WriteAsync("<br/>");
                var coreCLR = ((AssemblyInformationalVersionAttribute[])typeof(object).Assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false))[0].InformationalVersion;
                await context.Response.WriteAsync($"CoreCLR Build: {coreCLR.Split('+')[0]}");
                await context.Response.WriteAsync("<br/>");
                await context.Response.WriteAsync($"CoreCLR Hash: {coreCLR.Split('+')[1]}");
                await context.Response.WriteAsync("<br/>");

                var coreFX = ((AssemblyInformationalVersionAttribute[])typeof(Uri).Assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false))[0].InformationalVersion;
                await context.Response.WriteAsync($"CoreFX Build: {coreFX.Split('+')[0]}");
                await context.Response.WriteAsync("<br/>");
                await context.Response.WriteAsync($"CoreFX Hash: {coreFX.Split('+')[1]}");
                await context.Response.WriteAsync("<br/>");

                await context.Response.WriteAsync("<h2>Environment info</h2>");
                await context.Response.WriteAsync($"Environment.OSVersion: {Environment.OSVersion}");
                await context.Response.WriteAsync("<br/>");
                await context.Response.WriteAsync($"RuntimeInformation.OSDescription: {RuntimeInformation.OSDescription}");
                await context.Response.WriteAsync("<br/>");
                await context.Response.WriteAsync($"RuntimeInformation.OSArchitecture: {RuntimeInformation.OSArchitecture}");
                await context.Response.WriteAsync("<br/>");
                await context.Response.WriteAsync($"Environment.ProcessorCount: {Environment.ProcessorCount}");
                await context.Response.WriteAsync("</body></html>");
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().
                    UseEnvironment("Development");
                });
    }
}

/*

Sample Output

.NET Core Info
Environment.Version: 3.0.0
RuntimeInformation.FrameworkDescription: .NET Core 3.0.0-preview7-27912-14
CoreCLR Build: 3.0.0-preview7.19327.2
CoreCLR Hash: ac4ab6c990d5ebee49dc03397a2e199241021f26
CoreFX Build: 3.0.0-preview7.19362.9
CoreFX Hash: 1719a3fe2a5c81b67a4909787da4a02fb0d0d419
Environment info
Environment.OSVersion: Microsoft Windows NT 6.2.9200.0
RuntimeInformation.OSDescription: Microsoft Windows 10.0.18362
RuntimeInformation.OSArchitecture: X64
Environment.ProcessorCount: 4

 */
