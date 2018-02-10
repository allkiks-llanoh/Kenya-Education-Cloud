using System;

namespace KEC.Curation.Data.Models
{
    public class CuratorCreation
    {
        public int Id { get; set; }
        public string PhoneNumber{ get; set; }
        public string EmailAddress { get; set; }
        public string SirName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GUID { get; set; }
        public int TypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Subject Subject { get; set; }
       
    }
}
