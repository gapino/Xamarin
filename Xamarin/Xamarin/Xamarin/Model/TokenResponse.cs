using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Model
{
   public class TokenResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string Accesstoken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string Tokentype { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int Expiresin { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }


        [JsonProperty(PropertyName = "invalid_name__issued")]
        public string Invalidname_issued { get; set; }

        [JsonProperty(PropertyName = "invalid_name__expires")]
        public string Invalidnameexpires { get; set; }
    }
}
