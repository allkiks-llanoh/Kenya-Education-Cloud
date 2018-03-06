using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEC.Curation.UI.Models
{
    public class ChiefCurators
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public virtual subjects Subjects { get; set; }
    }
}