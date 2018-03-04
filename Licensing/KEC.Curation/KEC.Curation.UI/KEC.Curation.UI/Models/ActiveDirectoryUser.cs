using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Curation.UI.Models
{
    public class ActiveDirectoryUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("userPrincipalName")]
        public string UserPrincipalName { get; set; }
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        [JsonProperty("givenName")]
        public string GivenName { get; set; }
        [JsonProperty("mail")]
        public string Mail { get; set; }
        [JsonProperty("mobilePhone")]
        public string MobilePhone { get; set; }
        [JsonProperty("officeLocation")]
        public string OfficeLocation { get; set; }
        [JsonProperty("preferredLanguage")]
        public string PreferredLanguage { get; set; }
        [JsonProperty("surname")]
        public string Surname { get; set; }
    }
}