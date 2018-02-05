using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;
using KEC.Curation.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KEC.Curation.Data.Repositories
{
    public class PublicationRepository : Repository<Publication>
    {
        public PublicationRepository(CurationDataContext context) : base(context)
        {
        }
        public string GetKICDNUmber(List<Publication> publications)
        {
            var kicdNumber = string.Empty;
            do
            {
                kicdNumber = RandomCodeGenerator.GetKICDNUmber("KICD");
            } while ((Find(p => p.KICDNumber.Equals(kicdNumber)).FirstOrDefault() != null) &&
            (publications.Where(p => p.KICDNumber.Equals(kicdNumber)).FirstOrDefault() != null));

            return kicdNumber;
        }

    }
}
