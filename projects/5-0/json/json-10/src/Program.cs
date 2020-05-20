using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PracticalAspNetCore
{
    public class Person
    {
        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public int? Age { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public bool? IsMarried { get; set; }

        public DateTimeOffset CurrentTime { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenNull)]// Do not serialize this property when null
        public bool? IsWorking { get; set; }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(route =>
            {
                route.MapGet("/", async context =>
                {
                    var payload = new List<Person> { 
                        new Person
                        {
                            Name = "Annie",
                            Age = 33,
                            IsMarried = null,
                            CurrentTime = DateTimeOffset.UtcNow,
                            IsWorking = true
                        },
                        new Person
                        {
                            Name = "Dody",
                            Age = 42,
                            IsMarried = false,
                            CurrentTime = DateTimeOffset.UtcNow,
                            IsWorking = null
                        },
                    };

                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        IgnoreNullValues = false, //Pay attention here with interaction with the IsMarried and IsWorking properties
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
                    };

                    context.Response.Headers.Add(HeaderNames.ContentType, "application/json");
                    await JsonSerializer.SerializeAsync(context.Response.Body, payload, typeof(List<Person>), options);
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