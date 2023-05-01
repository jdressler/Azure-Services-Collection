using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureADApi.Models.Models.Requests
{
    public class UpdateAdUserDisplayNameRequest
    {
        public UpdateAdUserDisplayNameRequest(string id, string displayName)
        {
            Id = id;
            DisplayName = displayName;
        }

        public string Id { get; set; }
        public string DisplayName { get; set; }

    }
}
