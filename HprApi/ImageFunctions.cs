using CognitiveServices;
using HprAbstractions.Imaging;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HprApi
{
    public class ImageFunctions
    {
        public ImageFunctions(ComputerVisionService visionService)
        {
            this.VisionService = visionService;
        }

        private ComputerVisionService VisionService;

        [FunctionName(Names.AnalyzeImage)]
        public async Task<HttpResponseMessage> AnalyzeImage([HttpTrigger(AuthorizationLevel.Function, "POST", Route = "Images/Analyze")]HttpRequestMessage request)
        {
            var json = await request.Content.ReadAsStringAsync();
            var imageRef = JsonConvert.DeserializeObject<ImageReference>(json);

            var data = Convert.FromBase64String(imageRef.ImageData);
            var meta = await this.VisionService.AnalyzeImageAsync(data);

            var resultJson = JsonConvert.SerializeObject(meta);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(resultJson, Encoding.UTF8, "application/json")
            };
        }
    }
}
