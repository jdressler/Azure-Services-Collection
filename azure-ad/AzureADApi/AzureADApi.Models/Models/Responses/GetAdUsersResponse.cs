using Microsoft.Graph;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureADApi.Models.Models.Responses
{
    public class GetAdUsersResponse : BaseApiResponse
    {

        public GetAdUsersResponse(bool isError, string? errorMessage, List<User>? users) : base(isError, errorMessage)
        {
            Users = users;
        }

        public List<User>? Users { get; set; }
    }
}
