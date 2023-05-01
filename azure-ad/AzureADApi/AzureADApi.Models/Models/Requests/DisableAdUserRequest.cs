using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureADApi.Models.Models.Requests
{
    public class DisableAdUserRequest
    {
        public DisableAdUserRequest(string upn)
        {
            Upn = upn;
        }

        public string Upn { get; set; }

    }
}
