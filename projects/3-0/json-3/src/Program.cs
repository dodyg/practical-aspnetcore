using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;

namespace JsonSample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(route =>
            {
                route.MapGet("/", async context =>
                {
                    var payload = new
                    {
                        Name = "Annie",
                        Age = 33,
                        IsMarried = false,
                        CurrentTime = DateTimeOffset.UtcNow,
                        Characters = new Dictionary<string, bool>
                        {
                            {"Funny" , true},
                            {"Feisty" , true},
                            {"Brilliant" , true},
                            {"FOMA", false}
                        }
                    };

                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        IgnoreNullValues = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
                    };

                    context.Response.Headers.Add(HeaderNames.ContentType, "application/json");
                    await JsonSerializer.SerializeAsync(context.Response.Body, payload, options);
                });
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
                    UseEnvironment(Environments.Development);
                });
    }
}