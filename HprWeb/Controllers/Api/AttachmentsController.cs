using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HprAbstractions.Configuration;
using LogicAppsServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageServices;

namespace HprWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {

        public AttachmentsController(BlobsService blobs, StorageConfigSection storageConfig, LogicAppsClient logicApps)
        {
            this.Blobs = blobs ?? throw new ArgumentNullException(nameof(blobs));
            this.StorageConfig = storageConfig ?? throw new ArgumentNullException(nameof(storageConfig));
            this.LogicApps = logicApps ?? throw new ArgumentNullException(nameof(logicApps));
        }

        private BlobsService Blobs;
        private StorageConfigSection StorageConfig;
        private LogicAppsClient LogicApps;


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var blob = await this.Blobs.GetBlobAsync(this.StorageConfig.AttachmentsContainerName, id);
            return this.File(blob.Content, blob.ContentType, id);
        }

        [HttpPost("Observations")]
        public async Task<IActionResult> UploadObservation()
        {
            decimal.TryParse(this.HttpContext.Request.Query["lat"].FirstOrDefault(), out decimal lat);
            decimal.TryParse(this.HttpContext.Request.Query["lon"].FirstOrDefault(), out decimal lon);

            byte[] buffer = new byte[0];
            var file = HttpContext.Request?.Form?.Files?.FirstOrDefault();
            using (var strm = new MemoryStream())
            {
                await file.CopyToAsync(strm);

                strm.Position = 0;
                buffer = new byte[strm.Length];
                await strm.ReadAsync(buffer, 0, buffer.Length);
            }

            var msg = new HprAbstractions.Messaging.ReceiveImageRequest {
                Lat = lat,
                Lon = lon,
                Image = Convert.ToBase64String(buffer)
            };

            await this.LogicApps.ReceiveImageAsync(msg);

            return this.Redirect("/uploadobservation");
        }
    }
}
