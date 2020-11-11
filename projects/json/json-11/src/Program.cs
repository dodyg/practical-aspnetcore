using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

namespace PracticalAspNetCore
{
    public class Person
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(route =>
            {
                route.MapGet("/", async context =>
                {
                    var payload = new Person
                    {
                        Name = "Annie",
                        DateOfBirth = new DateTime(1984, 11, 20) // 1000 days
                    };

                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        IgnoreNullValues = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                        Converters = { new DateTimeConverter() }
                    };

                    var filePath = Path.Combine(env.ContentRootPath, "person.json");

                    //Serialize to file
                    {
                        using FileStream write = File.Create(filePath);
                        await JsonSerializer.SerializeAsync(write, payload, typeof(Person), options);
                    }
                    
                    using FileStream fs = File.OpenRead(filePath);
                    Person p = await JsonSerializer.DeserializeAsync(fs, typeof(Person), options) as Person;
                    context.Response.Headers.Add(HeaderNames.ContentType, "text/html");
                    await context.Response.WriteAsync($@"<html><body>
                    Name : { p.Name} <br/>
                    Date of Birth : { p.DateOfBirth } <br/>
                    </body></html>");
                });
            });
        }
    }

    public class DateTimeConverter : JsonConverter<DateTime>
    {
        const long InitialJavaScriptDateTicks = 621355968000000000;
        Regex JsDate = new Regex(@"Date\((?<Date>[0-9]+)\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            var match = JsDate.Match(value);
            var val = match.Groups["Date"].Value;
            return new DateTime((long.Parse(val) * 10000) + InitialJavaScriptDateTicks, DateTimeKind.Utc);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            DateTime utcDateTime = value.ToUniversalTime();
            long javaScriptTicks = (utcDateTime.Ticks - InitialJavaScriptDateTicks) / 10000;

            writer.WriteStringValue("/Date(" + javaScriptTicks.ToString() + ")/");
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