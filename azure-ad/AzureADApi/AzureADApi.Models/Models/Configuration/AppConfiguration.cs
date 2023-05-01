using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureADApi.Models.Models.Configuration
{
    public class AppConfiguration
    {
        public AppConfiguration(string mailDomain)
        {
            MailDomain = mailDomain;
        }

        public string MailDomain { get; set; }

    }
}
