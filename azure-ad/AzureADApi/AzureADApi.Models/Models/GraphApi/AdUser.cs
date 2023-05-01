using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureADApi.Models.Models.GraphApi
{
    public class AdUser
    {

        public AdUser(string displayName, string givenName, string jobTitle, string mail, string mobilePhone, string officeLocation, string prefferedLanguage, string surname, string upn)
        {
            DisplayName = displayName;
            GivenName = givenName;
            JobTitle = jobTitle;
            Mail = mail;
            MobilePhone = mobilePhone;
            OfficeLocation = officeLocation;
            PrefferedLanguage = prefferedLanguage;
            Surname = surname;
            Upn = upn;
        }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        
        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty("mail")]
        public string Mail { get; set; }

        [JsonProperty("mobilePhone")]
        public string MobilePhone { get; set; }

        [JsonProperty("officeLocation")]
        public string OfficeLocation { get; set; }

        [JsonProperty("preferredLanguage")]
        public string PrefferedLanguage { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("userPrincipalName")]
        public string Upn { get; set; }



    }
}
