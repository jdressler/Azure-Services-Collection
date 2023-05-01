using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureADApi.Models.Models.Responses
{
    public class GetAdUserByIdResponse : BaseApiResponse
    {
        public GetAdUserByIdResponse(bool isError, string? errorMessage, User? user): base(isError, errorMessage) {
            User = user;
        }

        public User? User { get; set; }
    }
}
