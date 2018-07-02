using KEC.ECommerce.Data.Database;
using KEC.ECommerce.Data.Helpers;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.Repositories.Core;
using System;
using System.Linq;

namespace KEC.ECommerce.Data.Repositories
{
    public class LicencesRepository : Repository<Licence>
    {
        private readonly ECommerceDataContext _ecommerceContext;

        public LicencesRepository(ECommerceDataContext context) : base(context)
        {
            _ecommerceContext = context as ECommerceDataContext;
        }
        public string GetNextLicence(string prefix = "KECLC#")
        {
            var code = string.Empty;
            do
            {
                code = RandomCodeGenerator.GetLicenceNumber(prefix);
            } while ((Find(p => p.Code.Equals(code)).FirstOrDefault() != null));
            return code;
        }
        public IQueryable<Licence> QueryableLicences (string identificationCode, string searchTerm)
        {
            var licences = default(IQueryable<Licence>);
            var currentDate = DateTime.Now;
            if (searchTerm == null)
            {
                licences = _ecommerceContext.Licences.Where(p => p.IdentificationCode.Equals(identificationCode) && p.ExpiryDate >= currentDate);
            }
            else
            {
                licences = _ecommerceContext.Licences.Where(p => p.IdentificationCode.Equals(identificationCode) &&
                                                                    (p.Code.Contains(searchTerm)
                                                                    || p.Publication.Level.Name.Contains(searchTerm))
                                                                    || p.Publication.Title.Contains(searchTerm) ||
                                                                    p.Publication.Subject.Name.Contains(searchTerm) ||
                                                                    p.Publication.Publisher.Company.Contains(searchTerm) ||
                                                                    p.Publication.Author.FirstName.Contains(searchTerm) ||
                                                                    p.Publication.Author.LastName.Contains(searchTerm));
            }

            return licences;
        }
    }
}
