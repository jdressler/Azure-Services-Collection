using BlobStorageApi.Contracts.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorageApi.Contracts.Models.Api.Response
{
    public class GetAllContainersResponse : BaseApiResponse
    {
        public List<ContainerModel>? Containers { get; set; }
        public GetAllContainersResponse(bool isError, string? errorMessage, List<ContainerModel>? containers) : base(isError, errorMessage) 
        {
            Containers = containers;  
        }
    }
}
