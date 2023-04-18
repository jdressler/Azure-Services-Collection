using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorageApi.Contracts.Models.Response
{
    public class DeleteContainerResponse : BaseApiResponse
    {
        public DeleteContainerResponse(bool isError, string? errorMessage) : base(isError, errorMessage) { }

    }
}
