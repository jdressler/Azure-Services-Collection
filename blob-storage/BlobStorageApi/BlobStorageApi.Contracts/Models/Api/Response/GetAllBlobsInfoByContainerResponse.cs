using Azure.Storage.Blobs.Models;
using BlobStorageApi.Contracts.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorageApi.Contracts.Models.Api.Response
{
    public class GetAllBlobsInfoByContainerResponse : BaseApiResponse
    {

        public List<BlobItem>? Blobs { get; set; }

        public GetAllBlobsInfoByContainerResponse(bool isError, string? errorMessage, List<BlobItem>? blobs): base(isError, errorMessage) 
        {
            Blobs = blobs;
        }


    }
}
