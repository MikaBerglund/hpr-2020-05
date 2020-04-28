using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveServices
{
    public class ComputerVisionService
    {
        public ComputerVisionService(ComputerVisionClient client)
        {
            this.Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        private ComputerVisionClient Client;


        public async Task<ImageMetadata> AnalyzeImageAsync(byte[] data)
        {
            ImageMetadata meta = null;

            using(var strm = new MemoryStream(data))
            {
                var result = await this.Client.DescribeImageInStreamAsync(strm);
                meta = new ImageMetadata
                {
                    Format = result.Metadata.Format,
                    Height = result.Metadata.Height,
                    Width = result.Metadata.Width,
                    Caption = result.Captions.OrderByDescending(x => x.Confidence).Select(x => x.Text).FirstOrDefault(),
                    Tags = result.Tags
                };
            }

            return meta;
        }
    }
}
