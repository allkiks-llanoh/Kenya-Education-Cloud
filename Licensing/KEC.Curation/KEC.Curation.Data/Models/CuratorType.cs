using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using KEC.Curation.Data.Repositories;

namespace KEC.Curation.Data.Models
{
    public class CuratorType
    {
 
        public int Id { get; set; }
        public string TypeName { get; set; }
        
    }
}
