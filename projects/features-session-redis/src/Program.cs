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
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration["redisConnectionString"];
            });
            services.AddSession();
        }

        byte[] Serialize(object val)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, val);
                return stream.ToArray();
            } 
        }

        T Deserialize<T>(byte[] val) where T: class
        {
            IFormatter formatter = new BinaryFormatter();

            using (var stream = new MemoryStream(val))
            {
                return formatter.Deserialize(stream) as T;
            }
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
                    session.Session.Set("Amore", Serialize(person));
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
                    var amore = Deserialize<Person>(session.Session.Get("Amore"));

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