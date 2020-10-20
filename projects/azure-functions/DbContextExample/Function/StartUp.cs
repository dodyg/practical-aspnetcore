using Function;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

[assembly: WebJobsStartup(typeof(StartUp))]
namespace Function
{
    public class StartUp : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var config = new ConfigurationBuilder()
                         .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                         .AddEnvironmentVariables()
                         .Build();

            builder.Services.AddDbContext<ApplicationDbContext>(options1 =>
            {
                options1.UseSqlServer(
                  config["ConnectionStrings:DefaultConnection"],
                  builder =>
                  {
                      builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                      builder.CommandTimeout(10);
                  }
                );
            });
        }
    }
}
