using BlobStorageApi.Contracts.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorageApi.Contracts.Models.Api.Response
{
    public class DeleteBlobFromContainerResponse : BaseApiResponse
    {
        public DeleteBlobFromContainerResponse(bool isError, string? errorMessage): base(isError, errorMessage) { }
    }
}
