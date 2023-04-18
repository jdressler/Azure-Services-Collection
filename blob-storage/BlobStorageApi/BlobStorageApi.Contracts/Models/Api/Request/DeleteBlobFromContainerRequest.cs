using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorageApi.Contracts.Models.Api.Request
{
    public class DeleteBlobFromContainerRequest
    {
        public string ContainerName { get; set; }
        public string BlobName { get; set; }

        public DeleteBlobFromContainerRequest(string containerName, string blobName)
        {
            ContainerName = containerName;
            BlobName = blobName;
        }

   

    }
}
