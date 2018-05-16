using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Curation.UI.Models
{
    public class ChiefCurators
    {
        public string Guid { get; set; }
        public int Subjectid{ get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


    }
}