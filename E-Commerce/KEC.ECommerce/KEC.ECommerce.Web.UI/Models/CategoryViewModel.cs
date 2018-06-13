using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using System.Collections.Generic;
using System.Linq;

namespace KEC.ECommerce.Web.UI.Models
{
    public class CategoryViewModel
    {
        private readonly Category _category;
        private readonly bool _includeProducts;
        private readonly IUnitOfWork _uow;

        public CategoryViewModel(IUnitOfWork uow,Category category, bool includeProducts = false)
        {
            _category = category;
            _includeProducts = includeProducts;
            _uow = uow;
        }
        public string Name
        {
            get
            {
                return _category.Name;
            }
        }
        public int Id
        {
            get
            {
                return _category.Id;
            }
        }
        public List<ProductViewModel> TopProducts
        {
            get
            {
                var productsList = default(List<ProductViewModel>);
                if (_includeProducts)
                {
                    var products = _uow.PublicationsRepository.TopProductsPerCategory(_category.Id);
                    productsList = products.Any() ? 
                        products.Select(p => new ProductViewModel(_uow, p)).ToList() : new List<ProductViewModel>();
                }
                return productsList;
            }
        }
    }
}
