using MapServices;
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
    public class MapFunctions
    {
        public MapFunctions(SearchService search)
        {
            this.Search = search ?? throw new ArgumentNullException(nameof(search));
        }

        private SearchService Search;


        [FunctionName(Names.PositionToAddress)]
        public async Task<HttpResponseMessage> Foo([HttpTrigger(AuthorizationLevel.Function, "GET", Route = "Positions/{position}/Address")]HttpRequestMessage request, string position)
        {
            var pos = this.Search.ParsePosition(position);
            if (null == pos)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var result = await this.Search.SearchByPositionAsync(pos.Latitude, pos.Longitude);
            if(null != result)
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(result), Encoding.UTF8, "application/json")
                };
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
