using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzureADApi.Models.Models.GraphApi
{
    public class AuthenticationResponse
    {

        public AuthenticationResponse(string tokenType, int expiresIn, int extExpiresIn, string accessToken)
        {
            TokenType = tokenType;
            ExpiresIn = expiresIn;
            ExtExpiresIn = extExpiresIn;
            AccessToken = accessToken;
        }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("ext_expires_in")]
        public int ExtExpiresIn { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
