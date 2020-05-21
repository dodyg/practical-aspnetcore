using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                // Duplicate the code below and write more messages. Save and refresh your browser to see the result.
                await context.Response.WriteAsync("Hello world. Make sure you run this app using 'dotnet watch run --port xxx'.");
            });
        }
    }

    public class Program
    {
        public static async Task Main(string[] args){
            var portOption = new Option("--port", "Set the port of the server");
            portOption.Argument = new Argument<short>("port", getDefaultValue: () => 5000);

            var rootCommand = new RootCommand("Practical ASP.NET Core Server");
            rootCommand.AddOption(portOption);
            rootCommand.Handler = CommandHandler.Create<short>((port) =>
            {
                 CreateHostBuilder(port, args).Build().Run();
            });

            await rootCommand.InvokeAsync(args);           
        }

        public static IHostBuilder CreateHostBuilder(short port, string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                    .ConfigureKestrel(k =>
                    {
                        k.ListenAnyIP(port);
                    })
                );
    }
}