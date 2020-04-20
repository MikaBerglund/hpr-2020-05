using HprApi;
using HprAbstractions.Configuration;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGridServices;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: WebJobsStartup(typeof(Startup))]

namespace HprApi
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var services = builder.Services;
            var serviceProvider = services.BuildServiceProvider();

            var config = serviceProvider.GetService<IConfiguration>();
            var rootConfig = config.Get<RootConfigSection>();
            services.AddSingleton(rootConfig);
            services.AddSingleton(rootConfig.Hpr.SendGrid);

            services.AddSingleton<MailService>();
        }
    }
}
