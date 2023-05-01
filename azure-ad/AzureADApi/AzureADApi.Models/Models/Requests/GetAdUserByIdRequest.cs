using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureADApi.Models.Models.Requests
{
    public class GetAdUserByIdRequest
    {
        public GetAdUserByIdRequest(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
