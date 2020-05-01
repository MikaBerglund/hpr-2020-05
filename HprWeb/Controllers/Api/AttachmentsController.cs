using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HprAbstractions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageServices;

namespace HprWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {

        public AttachmentsController(BlobsService blobs, StorageConfigSection storageConfig)
        {
            this.Blobs = blobs ?? throw new ArgumentNullException(nameof(blobs));
            this.StorageConfig = storageConfig ?? throw new ArgumentNullException(nameof(storageConfig));
        }

        private BlobsService Blobs;
        private StorageConfigSection StorageConfig;


        // GET: api/Attachments/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            var blob = await this.Blobs.GetBlobAsync(this.StorageConfig.AttachmentsContainerName, id);
            return this.File(blob.Content, blob.ContentType, id);
        }


    }
}
