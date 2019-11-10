using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

namespace JsonSample
{
    public class Person
    {
        public string Name { get; set; }

        public TimeSpan TimeWaiting { get; set; }
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
                        TimeWaiting = new TimeSpan(1000, 0, 0, 0) // 1000 days
                    };

                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        IgnoreNullValues = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                        Converters = { new TimeSpanConverter() }
                    };

                    context.Response.Headers.Add(HeaderNames.ContentType, "application/json");
                    await JsonSerializer.SerializeAsync(context.Response.Body, payload, typeof(Person), options);
                });
            });
        }
    }

    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeSpan.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
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