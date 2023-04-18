using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorageApi.Contracts.Interfaces
{
    public interface IContainerService
    {
        Task<bool> CreateContainer(string containerName);
        Task<bool> CheckIfContainerExists(string containerName);
        BlobContainerClient? GetContainer(string containerName);
        List<BlobContainerItem> GetAllContainers();
        Task<bool> DeleteContainer(BlobContainerClient container);
        BlobContainerClient GetContainerClient(string containerName);
    }
}
