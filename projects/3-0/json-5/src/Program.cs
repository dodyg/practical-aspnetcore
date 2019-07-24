using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace JsonSample
{
    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public bool IsMarried { get; set; }

        public DateTimeOffset CurrentTime { get; set; }

        public bool? IsWorking { get; set; }

        public Dictionary<string, bool> Characters { get; set; }

        public Dictionary<string, object> Extensions { get; set; }
    }

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
                    var payload = new Person
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
                        },
                        IsWorking = true,
                        Extensions = new Dictionary<string, object>
                        {
                            { "SuperPowers", new { Flight = false, Humor = true, Invisibility = true }}, // ad hoc object
                            { "FavouriteWords", new string[] { "Hello", "Oh Dear", "Bye"} }, // an array of primitives
                            { "Stats", new object[] { new { Flight = 0 }, new { Humor = 99 }, new { Invisibility = 30, Charged = true }}}, // an array of mixed objects
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
                    await JsonSerializer.SerializeAsync(context.Response.Body, payload, typeof(Person), options);
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