using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureADApi.Models.Models.GraphApi
{
    public class ListUsersResponse
    {
        public ListUsersResponse(List<AdUser> users)
        {
            Users = users;
        }

        [JsonProperty("value")]
        public List<AdUser> Users { get; set; }

    }
}
