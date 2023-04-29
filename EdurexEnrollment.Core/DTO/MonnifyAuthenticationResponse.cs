using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdurexEnrollment.Core.DTO
{
    public class MonnifyAuthenticationResponse
    {
        [JsonProperty("requestSuccessful")]
        public bool RequestSuccessful { get; set; }

        [JsonProperty("responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonProperty("responseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty("responseBody")]
        public AuthenticationResponseDetails ResponseDetails { get; set; }
    }

    public class AuthenticationResponseDetails
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("expiresIn")]
        public string ExpiresIn { get; set; }

    }

}
