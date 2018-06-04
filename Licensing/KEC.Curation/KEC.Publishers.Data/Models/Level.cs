using System;
using System.Collections.Generic;
using System.Text;

namespace KEC.Publishers.Data.Models
{
    public class Level
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
    }
}
