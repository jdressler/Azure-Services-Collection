using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobStorageApi.Contracts.Interfaces;
using log4net.Core;
using Microsoft.Extensions.Logging;

namespace BlobStorageApi.Services.Services
{
    public class ContainerService : IContainerService
    {
        private readonly ILogger<ContainerService> _logger;
        private readonly BlobServiceClient _blobServiceClient;

        public ContainerService(ILogger<ContainerService> logger, BlobServiceClient blobServiceClient)
        {
            _logger = logger;
            _blobServiceClient = blobServiceClient;
        }

        public async Task<bool> CreateContainer(string containerName)
        {
            Response<BlobContainerClient> response;
           
            _logger.LogInformation("Attempting to create container with name: " + containerName);

            response = await _blobServiceClient.CreateBlobContainerAsync(containerName);
            
            if(response.GetRawResponse().Status == 201)
            {
                _logger.LogInformation("Container with name: " + containerName + " created successfully");
                return true;
            } else
            {
                _logger.LogError("Error creating containers with name: " + containerName + ". " + response.GetRawResponse().Status);
                return false;

            }
            
        }

        public List<BlobContainerItem> GetAllContainers()
        {

            _logger.LogInformation("Attempting to retrieve all containers");

            var containers = _blobServiceClient.GetBlobContainers().ToList();
            
            _logger.LogInformation("Retrieved " + containers.Count + " containers");
            
            return containers;
        }

        public async Task<bool> DeleteContainer(BlobContainerClient container)
        {
           
            _logger.LogInformation("Attempting to delete container with name: " + container.Name);

            var response = await container.DeleteAsync();

            if (!response.IsError)
                _logger.LogInformation("Container with name: " + container.Name + " deleted successfully");
            else
                _logger.LogError("Error deleting container with name: " + container.Name);

            return !response.IsError;
            
        }

        public BlobContainerClient? GetContainer(string containerName)
        {

            _logger.LogInformation("Attempting to get container with name: " + containerName);

            var container = _blobServiceClient.GetBlobContainerClient(containerName);

            _logger.LogInformation("Returning container with name: " + containerName + " - Container " + (container == null ? "not found" : "found"));

            return container;


        }

        public async Task<bool> CheckIfContainerExists(string containerName)
        {
            _logger.LogInformation("Checking if container with name " + containerName + " already exists");
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return await blobContainerClient.ExistsAsync();
        }

        public BlobContainerClient GetContainerClient(string containerName)
        {
            return _blobServiceClient.GetBlobContainerClient(containerName);
        }
    }
}
