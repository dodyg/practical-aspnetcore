using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace Features.Session
{
    [Serializable]
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName {get; set;}
    }

    public class Startup
    {
        IConfiguration Configuration {get;set;}
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;        
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["redisConnectionString"];
            });
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            app.UseSession();

            app.Use(async (context, next)=>
            {
                var person = new Person
                {
                    FirstName = "Anne",
                    LastName = "M"
                };

                var session = context.Features.Get<ISessionFeature>();
                try
                {
                    session.Session.SetString("Message", "Buon giorno cuore");
                    session.Session.SetInt32("Year", DateTime.Now.Year);
                    session.Session.SetString("Amore", JsonConvert.SerializeObject(person));
                }
                catch(Exception ex)
                {
                    await context.Response.WriteAsync($"{ex.Message}");
                }
                await next.Invoke();
            });
            
            app.Run(async context =>
            {
                var session = context.Features.Get<ISessionFeature>();
                
                try
                {
                    string msg = session.Session.GetString("Message");
                    int? year = session.Session.GetInt32("Year");
                    var amore = JsonConvert.DeserializeObject<Person>(session.Session.GetString("Amore"));

                    await context.Response.WriteAsync($"{amore.FirstName}, {msg} {year}");
                }
                catch(Exception ex)
                {
                    await context.Response.WriteAsync($"{ex.Message}");
                }
            });
        }
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseEnvironment("Development");
    }
}