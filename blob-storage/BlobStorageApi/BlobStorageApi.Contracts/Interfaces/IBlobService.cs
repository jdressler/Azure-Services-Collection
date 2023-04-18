using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorageApi.Contracts.Interfaces
{
    public interface IBlobService
    {

        Task<List<BlobItem>> GetBlobsByContainer(BlobContainerClient containerClient);
        BlobClient GetBlobByContainer(BlobContainerClient containerClient, string blobName);
        Task<bool> UploadBlobToContainer(IFormFile file, BlobContainerClient containerClient);
        Task<bool> DeleteBlobFromContainer(BlobClient blob);
    }
}
