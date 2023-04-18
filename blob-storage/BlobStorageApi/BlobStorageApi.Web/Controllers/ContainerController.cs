using Azure;
using BlobStorageApi.Contracts.Interfaces;
using BlobStorageApi.Contracts.Models;
using BlobStorageApi.Contracts.Models.Api.Response;
using BlobStorageApi.Contracts.Models.Request;
using BlobStorageApi.Contracts.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace BlobStorageApi.Web.Controllers
{
    [Route("api/container")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly ILogger<ContainerController> _logger;
        private readonly IContainerService _containerService;

        public ContainerController(ILogger<ContainerController> logger, IContainerService containerService)
        {
            _logger = logger;
            _containerService = containerService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateContainer(CreateContainerRequest request)
        {
            try
            {
                _logger.LogInformation("Handling request to create container with name: " + request.ContainerName);

                if (await _containerService.CheckIfContainerExists(request.ContainerName))
                {
                    return StatusCode(409, new CreateContainerResponse(true, "Container already exists"));
                }

                var response = await _containerService.CreateContainer(request.ContainerName);

                _logger.LogInformation("Returning response for request to create container with name: " + request.ContainerName);

                if (response)
                {
                    return StatusCode(201, new CreateContainerResponse(false, null));
                }
                else
                {
                    return StatusCode(500, new CreateContainerResponse(true, "Unable to create new container"));
                }
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError("Error creating container with name: " + request.ContainerName, ex.ErrorCode);

                if (ex.ErrorCode == "InvalidResourceName")
                    return StatusCode(400, new CreateContainerResponse(true, "Invalid characters in container name"));
                else
                    return StatusCode(500, new CreateContainerResponse(true, "Unable to create container. Reason: " + ex.ErrorCode));

            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating container with name: " + request.ContainerName, ex.Message);
                return StatusCode(500, new CreateContainerResponse(true, "Unable to create container"));
            }

        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteContainer(DeleteContainerRequest request)
        {
            try
            {
                _logger.LogInformation("Handling request to delete container with name: " + request.ContainerName);

                var container = _containerService.GetContainer(request.ContainerName);

                if (container == null || !await container.ExistsAsync())
                {
                    return StatusCode(400, new DeleteContainerResponse(true, "Can't find container with name: " + request.ContainerName));
                }

                var response = await _containerService.DeleteContainer(container);

                _logger.LogInformation("Returning response for request to delete container with name: " + request.ContainerName);

                if (response)
                {
                    return StatusCode(200, new DeleteContainerResponse(false, null));
                }
                else
                {
                    return StatusCode(500, new DeleteContainerResponse(true, "Unable to delete container"));
                }
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError("Unable to delete container with name " + request.ContainerName, ex);

                if (ex.ErrorCode == "InvalidResourceName")
                    return StatusCode(400, new DeleteContainerResponse(true, "Invalid characters in container name"));
                else
                    return StatusCode(500, new DeleteContainerResponse(true, "Unable to delete container. Reason: " + ex.ErrorCode));

            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating container with name: " + request.ContainerName, ex.Message);
                return StatusCode(500, new DeleteContainerResponse(true, "Error deleting container"));
            }

        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllContainers()
        {
            try
            {
                _logger.LogInformation("Handling request to get all containers");
                var containers = _containerService.GetAllContainers();

                var containerModels = new List<ContainerModel>();

                foreach (var container in containers)
                {
                    containerModels.Add(new ContainerModel(container.Name, container.Properties.LastModified.ToString("yyyy-MM-dd:HH:mm:ssZ")));
                }

                return StatusCode(200, new GetAllContainersResponse(false, null, containerModels));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving list of containers", ex);
                return StatusCode(500, new GetAllContainersResponse(true, "Unable to retrieve containers", null));
            }


        }
    }
}
