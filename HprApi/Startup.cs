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
using CognitiveServices;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using System.Net.Http;

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
            services.AddSingleton(rootConfig.Hpr.ComputerVision);
            services.AddSingleton(rootConfig.Hpr.Map);
            services.AddSingleton(rootConfig.Hpr.Storage);


            services.AddSingleton(new ComputerVisionClient(new ApiKeyServiceClientCredentials(rootConfig.Hpr.ComputerVision.Key))
            {
                Endpoint = rootConfig.Hpr.ComputerVision.Endpoint
            });

            services.AddSingleton<MailService>();
            services.AddSingleton<ComputerVisionService>();
            services.AddSingleton<MapServices.SearchService>();

            services.AddSingleton(HttpClientFactory.Create());
        }
    }
}
