using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using StartupExample;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: WebJobsStartup(typeof(StartUp))]
namespace StartupExample
{
    class StartUp : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
        }
    }
}
