using Azure.Storage.Blobs;
using HprAbstractions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Azure.Storage.Blobs.Models;

namespace StorageServices
{
    public class BlobsService
    {
        public BlobsService(StorageConfigSection config)
        {
            this.Config = config ?? throw new ArgumentNullException(nameof(config));
            this.Client = new BlobServiceClient(this.Config.ConnectionString);
        }

        private StorageConfigSection Config;
        private BlobServiceClient Client;


        public async Task<IEnumerable<string>> EnumBlobsAsync(string containerName)
        {
            var containerClient = await this.GetContainerClientAsync(containerName);

            var blobs = new List<string>();
            await foreach(var blob in containerClient.GetBlobsAsync())
            {
                blobs.Add(blob.Name);
            }

            return blobs;
        }

        public async Task<BlobDownloadInfo> GetBlobAsync(string containerName, string blobName)
        {
            var blobClient = await this.GetBlobClientAsync(containerName, blobName);
            var response = await blobClient.DownloadAsync();
            return response.Value;
        }

        private async Task<BlobClient> GetBlobClientAsync(string containerName, string blobName)
        {
            var containerClient = await this.GetContainerClientAsync(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            return blobClient;
        }

        private async Task<BlobContainerClient> GetContainerClientAsync(string containerName)
        {
            var client = this.Client.GetBlobContainerClient(containerName);
            await client.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);

            return client;
        }
    }
}
