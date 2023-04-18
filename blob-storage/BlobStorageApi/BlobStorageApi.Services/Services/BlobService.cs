using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobStorageApi.Contracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BlobStorageApi.Services.Services
{
    public class BlobService : IBlobService
    {

        private readonly ILogger<BlobService> _logger;
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(ILogger<BlobService> logger, BlobServiceClient blobServiceClient)
        {
            _logger = logger;
            _blobServiceClient = blobServiceClient;
        }

        public async Task<List<BlobItem>> GetBlobsByContainer(BlobContainerClient containerClient)
        {
            List<BlobItem> blobs = new List<BlobItem>();
            await foreach (var item in containerClient.GetBlobsAsync())
            {
                blobs.Add(item);
            }

            return blobs;

        }

        public BlobClient GetBlobByContainer(BlobContainerClient containerClient, string blobName)
        {
            return containerClient.GetBlobClient(blobName);
        }

        public async Task<bool> UploadBlobToContainer(IFormFile file, BlobContainerClient containerClient)
        {
            var response = await containerClient.UploadBlobAsync(file.FileName, file.OpenReadStream());

            if (!response.GetRawResponse().IsError)
                _logger.LogInformation("File with name: " + file.FileName + " uploaded successfully to container with name: " + containerClient.Name);
            else
                _logger.LogError("Error uploading file with name: " + file.FileName + " uploaded successfully to container with name: " + containerClient.Name);

            return !response.GetRawResponse().IsError;
        }

        public async Task<bool> DeleteBlobFromContainer(BlobClient blob)
        {
            var response = await blob.DeleteAsync();
            if (!response.IsError)
            {
                _logger.LogInformation("Blob with name: " + blob.Name + " deleted from container: " + blob.BlobContainerName);
            }else
            {
                _logger.LogError("Error deleting blob with name: " + blob.Name + " from container: " + blob.BlobContainerName);
            }

            return !response.IsError;
        }


    }
}
