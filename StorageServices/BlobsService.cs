using Azure.Storage.Blobs;
using HprAbstractions.Configuration;
using System;

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


    }
}
