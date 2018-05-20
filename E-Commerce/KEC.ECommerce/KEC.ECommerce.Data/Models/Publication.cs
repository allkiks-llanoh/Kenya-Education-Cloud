using System;

namespace KEC.ECommerce.Data.Models
{
    public class Publication
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string ThumbnailUrl { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        #endregion


        #region Foreign Keys
        public int PublisherId { get; set; }
        public int SubjectId { get; set; }
        public int AuthorId { get; set; }
        public int LevelId { get; set; }
        public int CategoryId { get; set; }
        #endregion

        #region Virtual Properties
        public virtual Author Author { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual Category Category { get; set; }
        #endregion
    }
}
