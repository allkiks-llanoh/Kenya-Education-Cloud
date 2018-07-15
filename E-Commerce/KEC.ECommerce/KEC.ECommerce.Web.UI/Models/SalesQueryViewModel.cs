using KEC.ECommerce.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace KEC.ECommerce.Web.UI.Models
{
    public class SalesQueryViewModel
    {
        [Required]
        public string PublisherGuid { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

    }
}
