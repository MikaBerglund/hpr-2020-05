using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HprAbstractions.Messaging
{
    public class ReceiveImageRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("lat")]
        public decimal? Lat { get; set; }

        [JsonProperty("lon")]
        public decimal? Lon { get; set; }



        public async Task SetImageAsync(Stream source)
        {
            if((source?.CanRead).Value)
            {
                try
                {
                    byte[] buffer = new byte[source.Length];
                    await source.ReadAsync(buffer, 0, buffer.Length);
                    this.Image = Convert.ToBase64String(buffer);
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
            else
            {
                this.Image = null;
            }
        }

    }
}
