using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobStorageApi.Contracts.Models.Response
{
    public class BaseApiResponse
    {
        public BaseApiResponse(bool isError, string? errorMessage)
        {
            IsError = isError;
            ErrorMessage = errorMessage;
        }

        public bool IsError { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
