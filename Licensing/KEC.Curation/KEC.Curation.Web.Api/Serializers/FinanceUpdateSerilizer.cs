using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System.ComponentModel.DataAnnotations;

namespace KEC.Curation.Web.Api.Serializers
{
    public class FinanceUpdateSerilizer
    {
       
       
        public int? Id { get; set; }
        [Required(ErrorMessage = "Notes/Comments cannot be blank")]
        public string Notes { get; set; }

    }
}
