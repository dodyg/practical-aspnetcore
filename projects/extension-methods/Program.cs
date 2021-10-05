using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;

namespace PracticalAspNetCore
{
    public static class DateTimeExtensions
    {
        public static DateTime NextDay(this DateTime dateTime)
        {
            return dateTime.AddDays(1);
        }

        public static DateTime PreviousDay(this DateTime dateTime) => dateTime.AddDays(-1);
    }

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                // Duplicate the code below and write more messages. Save and refresh your browser to see the result.
                var today = new DateTime(2001, 5, 25);

                await context.Response.WriteAsync($"Say Today is {today.ToShortDateString()}. So the tomorrow is {today.NextDay().ToShortDateString()} and yesterday was {today.PreviousDay().ToShortDateString()}");
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}
