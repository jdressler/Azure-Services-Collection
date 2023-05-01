using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureADApi.Models.Models.Requests
{
    public class CreateAdUserRequest
    {

        public CreateAdUserRequest(bool accountEnabled, string displayName, string mailNickname, string upn)
        {
            AccountEnabled = accountEnabled;
            DisplayName = displayName;
            MailNickname = mailNickname;
            Upn = upn;
        }

        public bool AccountEnabled { get; set; }
        public string DisplayName { get; set; }
        public string MailNickname { get; set; }

        public string Upn { get; set; }



    }
}
