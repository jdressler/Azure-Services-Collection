using Azure;
using BlobStorageApi.Contracts.Interfaces;
using BlobStorageApi.Contracts.Models.Api.Request;
using BlobStorageApi.Contracts.Models.Api.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlobStorageApi.Web.Controllers
{
    [Route("api/blob")]
    [ApiController]
    public class BlobController : ControllerBase
    {

        private readonly ILogger<BlobController> _logger;
        private readonly IBlobService _blobService;
        private readonly IContainerService _containerService;

        public BlobController(ILogger<BlobController> logger, IBlobService blobService, IContainerService containerService)
        {
            _logger = logger;
            _blobService = blobService;
            _containerService = containerService;
        }


        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllBlobsInfoByContainer(string containerName)
        {
            try
            {
                _logger.LogInformation("Handling request to retrieve all blob info for container " + containerName);

                var containerClient = _containerService.GetContainerClient(containerName);

                var blobs = await _blobService.GetBlobsByContainer(containerClient);

                _logger.LogInformation("Returning response for request to get all blobs in container named: " + containerName);

                return StatusCode(200, new GetAllBlobsInfoByContainerResponse(false, null, blobs));

            } 
            catch(RequestFailedException ex)
            {
                _logger.LogError("Unable to retrieve blobs for container " + containerName, ex);

                if (ex.ErrorCode == "InvalidResourceName")
                    return StatusCode(400, "Invalid characters in container name");
                else
                    return StatusCode(500, "Unable to get blobs from container. Reason: " + ex.ErrorCode);
            }
            catch (Exception ex) 
            {
                _logger.LogError("Unable to retrieve blobs for container " + containerName, ex);
                return StatusCode(500, "Unable to retrieve blobs for container " + containerName);
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadBlobToContainer(IFormFile file, string containerName)
        {
            try
            {
                _logger.LogInformation("Handling request to upload blob to container: " + containerName);

                var containerClient = _containerService.GetContainerClient(containerName);

                var response = await _blobService.UploadBlobToContainer(file, containerClient);

                _logger.LogInformation("Returning response for request to upload file with name: " + file.FileName + " to container: " + containerName);

                if (response)
                {
                    return StatusCode(200, new UploadBlobToContainerResponse(false, null));
                }else
                {
                    return StatusCode(500, new UploadBlobToContainerResponse(true, "Unable to upload file with name: " + file.FileName + " to container: " + containerName));
                }

            }
            catch(RequestFailedException ex)
            {
                _logger.LogError("Unable to upload file with name: " + file.FileName + " to container: " + containerName + ". Reason: " + ex.ErrorCode, ex);
                if (ex.ErrorCode == "InvalidResourceName")
                    return StatusCode(409, new UploadBlobToContainerResponse(true, "Invalid characters in file or container name"));
                else
                    return StatusCode(500, "Unable to upload file with name: " + file.FileName + " to container: " + containerName);        
            }
            catch(Exception ex)
            {
                _logger.LogError("Unable to upload file with name: " + file.FileName + " to container: " + containerName, ex);
                return StatusCode(500, new UploadBlobToContainerResponse(true, "Unable to upload file with name: " + file.FileName + " to container: " + containerName));
            }
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteBlobFromContainer(DeleteBlobFromContainerRequest request)
        {
            try
            {
                _logger.LogInformation("Handling request to delete blob named: " + request.BlobName + " from container: " + request.ContainerName);

                var containerClient = _containerService.GetContainerClient(request.ContainerName);

                if (!await containerClient.ExistsAsync())
                {
                    _logger.LogInformation("Container named: " + request.ContainerName + " not found");
                    return StatusCode(404, new DeleteBlobFromContainerResponse(true, "Container not found"));
                }

                var blobClient = _blobService.GetBlobByContainer(containerClient, request.BlobName);

                if (!await blobClient.ExistsAsync())
                {
                    return StatusCode(404, new DeleteBlobFromContainerResponse(true, "Blob not found"));
                }

                var response = await _blobService.DeleteBlobFromContainer(blobClient);

                if (response)
                {
                    return StatusCode(200, new DeleteBlobFromContainerResponse(false, null));
                } else
                {
                    return StatusCode(500, new DeleteBlobFromContainerResponse(true, "Error deleting blob from container"));
                }

            } catch(RequestFailedException ex)
            {
                _logger.LogError("Unable to delete blob with name: " + request.BlobName + " from container: " + request.ContainerName + ". Reason: " + ex.ErrorCode, ex);
                if (ex.ErrorCode == "InvalidResourceName")
                    return StatusCode(409, new UploadBlobToContainerResponse(true, "Invalid characters in blob or container name"));
                else
                    return StatusCode(500, "Unable to delete blob with name: " + request.BlobName + " from container: " + request.ContainerName);
            } catch (Exception ex)
            {
                _logger.LogError("Unable to delete blob with name: " + request.BlobName + " from container: " + request.ContainerName, ex);
                return StatusCode(500, new UploadBlobToContainerResponse(true, "Unable to upload file with name: " + request.BlobName + " to container: " + request.ContainerName));
            }
           
        }

    }
}
