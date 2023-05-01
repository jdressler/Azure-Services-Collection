using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureADApi.Models.Models.Responses
{
    public class DisableAdUserResponse : BaseApiResponse
    {
        public DisableAdUserResponse(bool isError, string? errorMessage) : base(isError, errorMessage) { }
    }
}
