using HprAbstractions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LogicAppsServices
{
    public class LogicAppsClient
    {
        public LogicAppsClient(LogicAppsConfigSection config, HttpClient httpClient)
        {
            this.Config = config ?? throw new ArgumentNullException(nameof(config));
            this.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        private LogicAppsConfigSection Config;
        private HttpClient HttpClient;


        public async Task<HttpResponseMessage> ReceiveImageAsync(HprAbstractions.Messaging.ReceiveImageRequest request)
        {
            var httpReq = new HttpRequestMessage(HttpMethod.Post, this.Config.ReceiveImageUrl);
            var json = JsonConvert.SerializeObject(request);
            httpReq.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await this.HttpClient.SendAsync(httpReq);
            return response;
        }
    }
}
