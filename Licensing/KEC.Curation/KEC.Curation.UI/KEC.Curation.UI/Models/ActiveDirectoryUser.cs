using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Curation.UI.Models
{
    public class ActiveDirectoryUser
    {
       
        public string Id { get; set; }
        public string UserPrincipalName { get; set; }
        public string DisplayName { get; set; }
        public string GivenName { get; set; }
        public string Mail { get; set; }
        public string MobilePhone { get; set; }
        public string PreferredLanguage { get; set; }
        public string Surname { get; set; }
    }
}