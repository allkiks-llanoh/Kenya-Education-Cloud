using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.ECommerce.Web.UI.Models
{
    public class LicenceViewModel
    {
        private readonly Licence _licence;
        private readonly IUnitOfWork _uow;

        public LicenceViewModel(IUnitOfWork uow, Licence licence)
        {
            _licence = licence;

            _uow = uow;
        }
        public string Publication
        {
            get
            {
                var product = _uow.PublicationsRepository.GetPublicationDetails(_licence.PublicationId);
                return $"{product.Level.Name} - {product?.Title} by {product?.Author?.FirstName} {product?.Author?.LastName}({product?.Publisher?.Company})";
            }
        }
        public int Id
        {
            get
            {
                return _licence.Id;
            }
        }
        public string Code
        {
            get
            {
                return _licence.Code;
            }
        }
        public DateTime ExpiryDate
        {
            get
            {
                return _licence.ExpiryDate;
            }
        }
       
    }
}
